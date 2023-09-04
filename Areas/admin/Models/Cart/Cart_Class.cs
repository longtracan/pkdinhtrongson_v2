using MVC4WEB.Functions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TLBD.Areas.admin.Controllers;
using TLBD.Areas.admin.Models.Account;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.CartItem;
using TLBD.Areas.admin.Models.Order;
using TLBD.Models.EntityModel;
using WebMatrix.WebData;

namespace TLBD.Areas.admin.Models.Cart
{
    public class Cart_Class
    {
        EntitiesData ctx = new EntitiesData();
        public List<CartItem_Model> GetCartList(int _userid)
        {
            return ctx.CARTs
                .Where(x => x.UserID == _userid && x.Status == (int)cHelper.CART_STATUS.CART)
                .Join(ctx.BAI_VIET.Where(x => x.TRANG_THAI != 99),
                x => x.ProductID,
                y => y.ID,
                (x, y) => new CartItem_Model
                {
                    Product = new BaiViet_Model
                    {
                        ID = y.ID,
                        TIEU_DE = y.TIEU_DE,
                        IMAGE_URL = y.IMAGE_URL,
                        SP_GIAKM = y.SP_GIAKM,
                        //SP_SOLUONG = y.SP_SOLUONG
                        SP_SOLUONG = 9999
                    },
                    Quantity = x.Quantity
                })
                .ToList();
        }

        public void AddProductToCart(int productID, int quantity, int _userid)
        {
            var exist = ctx.CARTs.Count(x => x.UserID == _userid && x.ProductID == productID) > 0;

            if (exist)
            {
                ctx.CARTs
                    .Where(x => x.UserID == _userid && x.ProductID == productID)
                    .ToList()
                    .ForEach(x => x.Quantity += quantity);
            }
            else
            {
                ctx.CARTs.Add(new CART
                {
                    ProductID = productID,
                    Quantity = quantity,
                    Status = (int)cHelper.CART_STATUS.CART,
                    UserID = _userid
                });
            }

            ctx.SaveChanges();
        }

        public void UpdateCart(List<CartItem_Model> lst, int _userid)
        {
            foreach (var item in lst)
            {
                ctx.CARTs
                    .Where(x => x.UserID == _userid && x.ProductID == item.Product.ID && x.Status == (int)cHelper.CART_STATUS.CART)
                    .ToList()
                    .ForEach(x => x.Quantity = item.Quantity);
            }

            ctx.SaveChanges();
        }

        public void DeleteCart(int _userid)
        {
           ctx.CARTs
                .RemoveRange(ctx.CARTs.Where(x => (x.UserID == _userid && x.Status == (int)cHelper.CART_STATUS.CART)));
            ctx.SaveChanges();
        }

        public void DeleteCartProduct(int id, int _userid)
        {
            var lst = ctx
                 .CARTs
                 .RemoveRange(ctx.CARTs.Where(x => (x.UserID == _userid && x.ProductID == id && x.Status == (int)cHelper.CART_STATUS.CART)));

            ctx.SaveChanges();
        }

        public void Order(User_Model m, int _userid)
        {
            string orderid = Guid.NewGuid().ToString();
            ctx.ORDERs.Add(new ORDER
            {
                ID = orderid,
                IsPaid = false,
                OrderDate = DateTime.Now,
                Status = (int)cHelper.ORDER_STATUS.WAITING,
                UserID = _userid,
                Name = m.sTEN_USER,
                Address = m.sDIA_CHI,
                Email = m.sEMAIL,
                Phone = m.sSO_DIEN_THOAI
            });

            ctx.CARTs
                .Where(x => x.UserID == _userid && x.Status == (int)cHelper.CART_STATUS.CART)
                .Join(ctx.BAI_VIET.Where(x => x.TRANG_THAI != 99),
                x => x.ProductID,
                y => y.ID,
                (x, y) => new
                {
                    x.ProductID,
                    x.Quantity,
                    y.SP_GIAKM
                })
                .ToList()
                .ForEach(x =>
                {
                    ctx.ORDER_DETAIL.Add(new ORDER_DETAIL()
                    {
                        OrderID = orderid,
                        ProductID = x.ProductID,
                        Price = x.SP_GIAKM != null ? x.SP_GIAKM.Value : 0,
                        Quantity = x.Quantity
                    });

                    ctx.BAI_VIET.Where(y => y.ID == x.ProductID)
                        .ToList()
                        .ForEach(y => y.SP_SOLUONG -= x.Quantity);
                });

            ctx.CARTs
                .RemoveRange(ctx.CARTs.Where(x => x.UserID == _userid && x.Status == (int)cHelper.CART_STATUS.CART));

            ctx.SaveChanges();

            Toolkits.SndMAIL(orderid);
        }

        public void FastOrder(Order_Model m)
        {
            string orderid = Guid.NewGuid().ToString();
            ctx.ORDERs.Add(new ORDER
            {
                ID = orderid,
                IsPaid = false,
                OrderDate = DateTime.Now,
                Status = (int)cHelper.ORDER_STATUS.WAITING,
                UserID = -1,
                Name = m.FullName,
                Address = m.Address,
                Email = m.Email,
                Phone = m.Phone
            });

            m.detail_lst.ForEach(x =>
            {
                ctx.ORDER_DETAIL.Add(new ORDER_DETAIL()
                {
                    OrderID = orderid,
                    ProductID = x.Product.ID,
                    Price = x.Product.SP_GIAKM != null ? x.Product.SP_GIAKM.Value : (x.Product.SP_GIA != null ? x.Product.SP_GIA.Value : 0),
                    Quantity = x.Quantity
                });

                ctx.BAI_VIET.Where(y => y.ID == x.Product.ID)
                    .ToList()
                    .ForEach(y => y.SP_SOLUONG -= x.Quantity);
            });

            ctx.SaveChanges();

            Toolkits.SndMAIL(orderid);
        }



        public static int GetCartCount(string username)
        {
            EntitiesData ctx = new EntitiesData();
            if (username == "")
            {
                return 0;
            }

            int _userid = WebSecurity.GetUserId(username);
            var data = ctx.CARTs
                .Where(x => x.UserID == _userid);

            int _r = 0;

            if (data.Count() > 0)
            {
                _r = data.Sum(x => x.Quantity);
            }

            return _r;
        }

        public static void UpdateCartCookie(List<CartItem_Model> lst, int _userid)
        {
            EntitiesData ctx = new EntitiesData();
            foreach (var item in lst)
            {
                if (ctx.CARTs.Count(y => y.ProductID == item.Product.ID) == 0)
                {
                    ctx.CARTs.Add(new CART()
                    {
                        ProductID = item.Product.ID,
                        Quantity = item.Quantity,
                        Status = (int)cHelper.CART_STATUS.CART,
                        UserID = _userid
                    });
                }
                else
                {
                    ctx.CARTs
                    .Where(x => x.UserID == _userid && x.ProductID == item.Product.ID && x.Status == (int)cHelper.CART_STATUS.CART)
                    .ToList()
                    .ForEach(x => x.Quantity += item.Quantity);
                }
            }

            ctx.SaveChanges();
        }
    }
}