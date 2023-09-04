using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TLBD.Areas.admin.Controllers;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.CartItem;
using TLBD.Areas.admin.Models.OrderItem;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.Order
{
    public class Order_Class
    {
         static EntitiesData ctx = new EntitiesData();
        public List<OrderItem_Model> GetOrderList(int _userid)
        {
            return ctx.ORDERs
                .Where(x => x.UserID == _userid)
                .Join(ctx.ORDER_STATUS,
                x => x.Status,
                y => y.ID,
                (x, y) => new OrderItem_Model
                {
                    OrderID = x.ID,
                    Date = x.OrderDate,
                    Status = y.Description
                })
                .OrderByDescending(x => x.Date)
                .ToList();

        }

        public  Order_Model GetOrderDetail(string id, int _userid)
        {
            return ctx.ORDERs
                .Where(x => x.ID == id)
                .Select(x => new Order_Model
                {
                    ID = x.ID,
                    Address = x.Address,
                    Email = x.Email,
                    FullName = x.Name,
                    Phone = x.Phone,
                    Status = x.Status.Value,
                    detail_lst = ctx.ORDER_DETAIL
                                    .Where(y => y.OrderID == id)
                                    .Join(ctx.BAI_VIET.Where(y => y.TRANG_THAI != 99),
                                    y => y.ProductID,
                                    z => z.ID,
                                    (y, z) => new CartItem_Model
                                    {
                                        Product = new BaiViet_Model
                                        {
                                            ID = z.ID,
                                            TIEU_DE = z.TIEU_DE,
                                            IMAGE_URL = z.IMAGE_URL,
                                            SP_GIAKM = z.SP_GIAKM
                                        },
                                        Quantity = y.Quantity
                                    }).ToList()
                }).FirstOrDefault();
        }

        public static Order_Model GetOrderDetail_(int _userid)
        {
            return ctx.ORDERs
               
                .Select(x => new Order_Model
                {
                    ID = x.ID,
                    Address = x.Address,
                    Email = x.Email,
                    FullName = x.Name,
                    Phone = x.Phone,
                    Status = x.Status.Value,
                   
                }).FirstOrDefault();
        }

        public List<ORDER_STATUS> GetOrderStatusList()
        {
            return ctx.ORDER_STATUS.ToList();
        }

        public int GetOrderCount(int id_trangthai, string sd, string ed, int _userid)
        {
            DateTime sDate = Convert.ToDateTime(DateTime.ParseExact(sd, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            DateTime eDate = Convert.ToDateTime(DateTime.ParseExact(ed, "dd-MM-yyyy", CultureInfo.InvariantCulture)).AddDays(1);

            var _r = ctx.ORDERs.Count(x => x.Status == id_trangthai
                && DateTime.Compare(x.OrderDate.Value, sDate) >= 0
                && DateTime.Compare(x.OrderDate.Value, eDate) < 0);

            return _r;
        }

        public IEnumerable<OrderItem_Model> GetOrderList(int page, int pageSize, int id_trangthai, string sd, string ed, int _userid)
        {
            DateTime sDate = Convert.ToDateTime(DateTime.ParseExact(sd, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            DateTime eDate = Convert.ToDateTime(DateTime.ParseExact(ed, "dd-MM-yyyy", CultureInfo.InvariantCulture)).AddDays(1);

            var _r = ctx.ORDERs
                .Where(x => x.Status == id_trangthai
                && DateTime.Compare(x.OrderDate.Value, sDate) >= 0
                && DateTime.Compare(x.OrderDate.Value, eDate) < 0)
                .Select(x => new OrderItem_Model
                {
                    Date = x.OrderDate,
                    OrderID = x.ID,
                    FullName = x.Name
                })
                .OrderByDescending(x => x.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _r;
        }

        public string Capnhat(OrderItem_Model model)
        {
            ctx.ORDERs
                .Where(x => x.ID == model.OrderID)
                .ToList()
                .ForEach(x =>
                {
                    //Neu trang thai chuyen la [Huy] va trang thai truoc do khac [Huy]
                    //thi thuc hien cong them vao KHO
                    ctx.ORDER_DETAIL.Where(y => model.StatusID == (int)cHelper.ORDER_STATUS.CANCEL
                        && x.Status != (int)cHelper.ORDER_STATUS.CANCEL
                        && y.OrderID == x.ID)
                    .ToList()
                    .ForEach(y =>
                    {
                        ctx.BAI_VIET.Where(z => z.ID == y.ProductID)
                            .ToList()
                            .ForEach(z =>
                            {
                                z.SP_SOLUONG += y.Quantity;
                            });
                    });

                    //Neu trang thai chuyen khac [Huy] va trang thai truoc do la [Huy]
                    //thi thuc hien tru trong KHO
                    ctx
                        .ORDER_DETAIL
                        .Where(y => x.Status == (int)cHelper.ORDER_STATUS.CANCEL
                            && model.StatusID != (int)cHelper.ORDER_STATUS.CANCEL
                            && y.OrderID == x.ID)
                    .ToList()
                    .ForEach(y =>
                    {
                        ctx.BAI_VIET.Where(z => z.ID == y.ProductID)
                            .ToList()
                            .ForEach(z =>
                            {
                                z.SP_SOLUONG -= y.Quantity;
                            });
                    });

                    x.Status = model.StatusID;
                });

            try
            {
                ctx.SaveChanges();
                return model.OrderID;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return "";
            }
        }

        public OrderItem_Model Get(string id)
        {
            return ctx.ORDERs
                           .Where(x => x.ID == id)
                           .Join(ctx.ORDER_STATUS,
                           x => x.Status,
                           y => y.ID,
                           (x, y) => new OrderItem_Model
                           {
                               Date = x.OrderDate,
                               OrderID = x.ID,
                               FullName = x.Name,
                               Address = x.Address,
                               Phone = x.Phone,
                               Total = ctx.ORDER_DETAIL.Where(z => z.OrderID == id).Sum(z => z.Quantity * z.Price),
                               StatusID = x.Status,
                               Status = y.Description
                           }).FirstOrDefault();
        }

        public int GetOrderDetailCount(string id_donhang)
        {
            return ctx.ORDERs
                .Where(x => x.ID == id_donhang)
                .Join(ctx.ORDER_DETAIL,
                x => x.ID,
                y => y.OrderID,
                (x, y) => new { x, y })
                .Count();
        }

        public IEnumerable<CartItem_Model> GetOrderDetailList(int page, int pageSize, string id_donhang)
        {
            return ctx.ORDERs
                .Where(x => x.ID == id_donhang)
                .Join(ctx.ORDER_DETAIL,
                x => x.ID,
                y => y.OrderID,
                (x, y) => new { x, y })
                .Join(ctx.BAI_VIET.Where(t => t.TRANG_THAI != 99),
                x => x.y.ProductID,
                y => y.ID,
                (x, y) => new CartItem_Model
                {
                    Product = new BaiViet_Model
                    {
                        ID = y.ID,
                        TIEU_DE = y.TIEU_DE,
                        IMAGE_URL = y.IMAGE_URL,
                        SP_GIAKM = x.y.Price
                    },
                    Quantity = x.y.Quantity
                }).ToList();
        }
    }
}