using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.CartItem;
using TLBD.Areas.admin.Models.Account;
using TLBD.Areas.admin.Models.Cart;
using TLBD.Areas.admin.Controllers;
using Newtonsoft.Json;
using TLBD.Areas.admin.Models.Order;
using System.Web.Security;
using WebMatrix.WebData;
using System.Text;

namespace TLBD.Controllers
{
    //[Authorize]
    public class CartController : Controller
    {
        Cart_Class cCart = new Cart_Class();

        public ActionResult Index()
        {
            int _userid = WebSecurity.GetUserId(User.Identity.Name);
            List<CartItem_Model> lst = new List<CartItem_Model>();
            if (User.Identity.Name == "")
            {
                //lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(cHelper.GetCookie(Request, cHelper.CART_COOKIE));
                var cookie = cHelper.GetCookie(Request, cHelper.CART_COOKIE);
                if (cookie != "")
                {
                    byte[] bytes = Encoding.Default.GetBytes(cookie);
                    var utf8cooki = Encoding.UTF8.GetString(bytes);
                    lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(utf8cooki);
                }
                if (lst == null)
                {
                    lst = new List<CartItem_Model>();
                }
            }
            else
            {
                lst = cCart.GetCartList(_userid);
            }
            return View(lst);
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            List<CartItem_Model> cookie_lst = null;
            SanPham_Class bv = new SanPham_Class();

            if (User.Identity.Name == "")
            {
                BaiViet_Model model = bv.Get(productId);
                var item = new CartItem_Model()
                {
                    Product = new BaiViet_Model()
                    {
                        ID = productId,
                        TIEU_DE = model.TIEU_DE,
                        SP_GIAKM = model.SP_GIAKM != null ? model.SP_GIAKM : model.SP_GIA,
                        IMAGE_URL = model.IMAGE_URL,
                        SP_SOLUONG = 9999
                        //SP_SOLUONG = model.SP_SOLUONG
                    },
                    Quantity = quantity
                };

                var cookie = cHelper.GetCookie(Request, cHelper.CART_COOKIE);
                if (cookie != "")
                {
                    byte[] bytes = Encoding.Default.GetBytes(cookie);
                    var utf8cooki = Encoding.UTF8.GetString(bytes);
                    cookie_lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(utf8cooki);
                    //cookie_lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(cookie);
                    if (cookie_lst == null)
                    {
                        cookie_lst = new List<CartItem_Model>();
                    }
                    var exist = cookie_lst.Exists(x => x.Product.ID == productId);
                    if (exist)
                    {
                        cookie_lst.Where(x => x.Product.ID == productId)
                            .ToList()
                            .ForEach(x => x.Quantity += quantity);
                    }
                    else
                    {
                        cookie_lst.Add(item);
                    }
                }
                else
                {
                    cHelper.AddCookie(Response, cHelper.CART_COOKIE, "", 180);
                    cookie_lst = new List<CartItem_Model>();
                    cookie_lst.Add(item);
                }
                cHelper.SetCookieValue(Response, cHelper.CART_COOKIE, JsonConvert.SerializeObject(cookie_lst));
            }
            else
            {
                int userid = WebSecurity.GetUserId(User.Identity.Name);
                cCart.AddProductToCart(productId, quantity, userid);
            }

            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            List<CartItem_Model> lst = null;
            var jsonCart = JsonConvert.DeserializeObject<List<CartItem_Model>>(cartModel);
            if (User.Identity.Name == "")
            {
                if (Request[cHelper.CART_COOKIE] != null)
                {
                    var cookie = cHelper.GetCookie(Request, cHelper.CART_COOKIE);
                    byte[] bytes = Encoding.Default.GetBytes(cookie);
                    var utf8cooki = Encoding.UTF8.GetString(bytes);
                    lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(utf8cooki);

                    jsonCart.ToList()
                        .ForEach(x =>
                        {
                            lst.Where(y => y.Product.ID == x.Product.ID)
                                .ToList()
                                .ForEach(y => y.Quantity = x.Quantity);
                        });
                    cHelper.SetCookieValue(Response, cHelper.CART_COOKIE, JsonConvert.SerializeObject(lst));
                }
            }
            else
            {
                int userid = WebSecurity.GetUserId(User.Identity.Name);
                cCart.UpdateCart(jsonCart, userid);
            }

            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            if (User.Identity.Name == "")
            {
                cHelper.DeleteCookie(Response, cHelper.CART_COOKIE);
            }
            else
            {
                int userid = WebSecurity.GetUserId(User.Identity.Name);
                cCart.DeleteCart(userid);
            }

            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            List<CartItem_Model> cookie_lst = null;
            if (User.Identity.Name == "")
            {
                if (Request[cHelper.CART_COOKIE] != null)
                {
                    var cookie = cHelper.GetCookie(Request, cHelper.CART_COOKIE);
                    byte[] bytes = Encoding.Default.GetBytes(cookie);
                    var utf8cooki = Encoding.UTF8.GetString(bytes);
                    cookie_lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(utf8cooki);
                    cookie_lst.RemoveAll(x => x.Product.ID == id);
                    cHelper.SetCookieValue(Response, cHelper.CART_COOKIE, JsonConvert.SerializeObject(cookie_lst));
                }
            }
            else
            {
                int userid = WebSecurity.GetUserId(User.Identity.Name);
                cCart.DeleteCartProduct(id, userid);
            }

            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            int _userid = WebSecurity.GetUserId(User.Identity.Name);
            List<CartItem_Model> lst = null;

            if (User.Identity.Name == "")
            {
                var cookie = cHelper.GetCookie(Request, cHelper.CART_COOKIE);
                if (cookie != "")
                {
                    byte[] bytes = Encoding.Default.GetBytes(cookie);
                    var utf8cooki = Encoding.UTF8.GetString(bytes);
                    lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(utf8cooki);
                    if (lst == null)
                    {
                        lst = new List<CartItem_Model>();
                    }
                }
                else
                {
                    lst = new List<CartItem_Model>();
                }
            }
            else
            {
                ViewBag.Username = User.Identity.Name;
                lst = cCart.GetCartList(_userid);
            }

            return View(lst);
        }

        [HttpPost]
        public ActionResult Payment(string recieverName, string phone, string address, string email)
        {
            List<CartItem_Model> lst = null;

            if (User.Identity.Name == "")//fast order
            {
                var cookie = cHelper.GetCookie(Request, cHelper.CART_COOKIE);
                if (cookie != "")
                {
                    byte[] bytes = Encoding.Default.GetBytes(cookie);
                    var utf8cooki = Encoding.UTF8.GetString(bytes);
                    lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(utf8cooki);
                    if (lst == null)
                    {
                        lst = new List<CartItem_Model>();
                    }
                }
                else
                {
                    lst = new List<CartItem_Model>();
                }

                Order_Model model = new Order_Model()
                {
                    FullName = recieverName,
                    Phone = phone,
                    Address = address,
                    Email = email,
                    detail_lst = lst
                };
                cCart.FastOrder(model);
                cHelper.DeleteCookie(Response, cHelper.CART_COOKIE);
            }
            else
            {
                User_Model model = new User_Model()
                {
                    sTEN_USER = recieverName,
                    sSO_DIEN_THOAI = phone,
                    sDIA_CHI = address,
                    sEMAIL = email
                };
                int userid = WebSecurity.GetUserId(User.Identity.Name);
                cCart.Order(model, userid);
            }

            return Redirect("Hoan-thanh");
        }

        public ActionResult Success()
        {
            return View("Success");
        }

    }
}
