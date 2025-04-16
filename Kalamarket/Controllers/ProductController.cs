using Kalamarket.Core.ExtentionMethod;
using Kalamarket.Core.Service;
using Kalamarket.Core.Service.Interface;
using Kalamarket.Core.Viewmodel;
using Kalamarket.DataLayer.Entities;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using Kalamarket.DataLayer.Entities.Entitieproduct.FaQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalamarket.Controllers
{
    public class ProductController : Controller
    {
        private Iproductservice _productservice;
        private IBrandService _Brandservice;
        private ICategoryService _Categoryservice;
        private ICartService _CartService;
        private IDiscountService _Discountservice;
        public ProductController(Iproductservice productservice, IBrandService Brandservice,
            ICategoryService Categoryservice, ICartService CartService, IDiscountService Discountservice)
        {
            _productservice = productservice;
            _Brandservice = Brandservice;
            _Categoryservice = Categoryservice;
            _CartService = CartService;
            _Discountservice = Discountservice;
        }


        public IActionResult Detail(int id)
        {
            //var b = _productservice.ShowDetailsProduct(id);
            var detailproduct = _productservice.DetailProduct(id);
            var productprice = _productservice.GetProductprice(id);

            ViewBag.PropertyValue = _productservice.showValueForProduct(id);
            ViewBag.Galley = _productservice.showgalleryforproduct(id);
            if (detailproduct == null)
            {
                return NotFound();
            }
            return View(Tuple.Create(detailproduct, productprice));
        }

        public IActionResult ShowReview(int id)
        {
            return View(_productservice.findreviewbuyeid(id));
        }

        public IActionResult ShowAllProperty(int id)
        {
            return View(_productservice.ShowAllPropertyForProduct(id));
        }

        public IActionResult ShowComment(int id)
        {
            ViewBag.Reating = _productservice.GetreatingForProduct(id);
            return View(_productservice.ShowAllCommentForProduct(id));
        }


        public IActionResult ShowFaq(int id)
        {
            TempData["productid"] = id;
            return View(_productservice.ShowAllFaq(id));
        }


        [Route("compare/{id?}/{id2?}/{id3?}")]
        public IActionResult Compare(int id, int? id2, int? id3)
        {
            List<int?> Listid = new List<int?> { id, id2, id3 };

            if (id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var ListProduct = _productservice.Showcompareproduct(Listid);

            var productgroup = ListProduct.GroupBy(p => p.productid).Select(p => new comapreviewmodel
            {
                categoryid = p.FirstOrDefault().categoryid,
                productfatitle = p.FirstOrDefault().productfatitle,
                productid = p.Key,
                productimg = p.FirstOrDefault().productimg,
                productprice = p.FirstOrDefault().productprice,
                Compareviewmodel = p.FirstOrDefault().Compareviewmodel,

            }).ToList();

            ViewBag.property = _productservice.ShowPropertyCompare(productgroup[0].categoryid);
            ViewBag.Product = _productservice.GetProductForCompare(productgroup[0].categoryid, Listid);
            return View(productgroup.OrderBy(p => p.productid));

        }

        [HttpPost]
        [Route("changefaviorate")]
        public IActionResult changefaviorate(int productid)
        {
            int userid = int.Parse(User.FindFirst("userid").Value);
            Faviorate faviorate = _productservice.findfavioratebuyeuserid(userid, productid);
            Faviorate addorDelete = new Faviorate();
            if (faviorate == null)
            {
                addorDelete.productid = productid;
                addorDelete.userid = userid;

                bool result = _productservice.AddFaiorate(addorDelete);
                return Json(result);
            }
            else
            {
                bool result = _productservice.removefaviorate(faviorate);
                return Json(result); ;
            }
        }

        [HttpPost]
        [Route("CheckFaviorate")]
        public IActionResult CheckFaviorate(int productid)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userid = int.Parse(User.FindFirst("userid").Value);
                Faviorate faviorate = _productservice.findfavioratebuyeuserid(userid, productid);
                if (faviorate != null)
                {
                    return Json(true);
                }
                return Json(false);
            }
            return Json(false);

        }

        [HttpPost]
        [Route("AddOrDeleteFaviorate")]
        public IActionResult AddOrDeleteFaviorate(int productid)
        {
            int userid = int.Parse(User.FindFirst("userid").Value);
            Faviorate faviorate = _productservice.findfavioratebuyeuserid(userid, productid);
            Faviorate Addfaviorate = new Faviorate();
            if (faviorate != null)
            {
                _productservice.removefaviorate(faviorate);
                return Json(true);
            }
            else
            {
                Addfaviorate.productid = productid;
                Addfaviorate.userid = userid;
                _productservice.AddFaiorate(Addfaviorate);
                return Json(false);
            }
        }

        [HttpPost]
        public IActionResult AddQustionOrAnswer(string text, int qustionid, int productid)
        {
            int userid = int.Parse(User.FindFirst("userid").Value);

            if (qustionid > 0)
            {
                Answer answer = new Answer
                {
                    AnswerDescription = text,
                    CreateDate = DateTime.Now,
                    questionid = qustionid,
                    userid = userid,

                };
                int answerid = _productservice.AddAnswer(answer);
                return Json(answerid);
            }
            else
            {
                question question = new question
                {
                    userid = userid,
                    questionDescription = text,
                    CreateDate = DateTime.Now,
                    IsActive = false,
                    productid = productid,
                };
                int Qustionid = _productservice.AddQustion(question);
                return Json(Qustionid);
            }

        }

        [HttpGet]
        public IActionResult search(List<int> categoryid,
            List<int> brandid, string text = "", string minprice = "", string maxprice = "", int sort = 1)
        {
            if (text == null)
            {
                text = "";
            }
            var ListProduct = _productservice.search(text, categoryid, brandid, minprice.replacePrice(), maxprice.replacePrice());
            ViewBag.brand = _Brandservice.ShowAllBrand();
            ViewBag.category = _Categoryservice.Showsubcategory();

            ViewBag.text = text;
            ViewBag.sort = sort;
            ViewBag.categoryid = categoryid;
            ViewBag.brandid = brandid;
            return View(ListProduct);
        }

        [Route("ShowAllProductForBasket")]
        public IActionResult ShowAllProductForBasket()
        {
            List<ShowBasketViewmodel> showBaskets = new List<ShowBasketViewmodel>();

            if (!User.Identity.IsAuthenticated)
                return View(showBaskets);

            int userid = int.Parse(User.FindFirst("userid").Value);
            showBaskets = _CartService.ShowAllProductForBasket(userid);

            return View(showBaskets);

        }

        #region Cart
        [HttpPost]
        [Route("AddCart/{id}/{productcount}")]
        public IActionResult AddCart(int id, int productcount)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(1);
            }
            int userid = int.Parse(User.FindFirst("userid").Value);
            int Code = _CartService.AddCart(userid, id, productcount);
            return Json(Code);
        }


        [HttpGet]
        [Authorize]
        [Route("basket")]
        public IActionResult basket(int code)
        {
            int userid = int.Parse(User.FindFirst("userid").Value);
            var viewmodel = _CartService.DetailCart(userid);
            ViewBag.Code = code;
            return View(viewmodel);
        }

        [HttpPost]
        [Route("changecart")]
        public IActionResult changecart(int productpriceid, int count)
        {
            int userid = int.Parse(User.FindFirst("userid").Value);
            _CartService.changebasket(userid, productpriceid, count);
            return Json(1);
        }

        [HttpGet]
        [Route("removeProductForBasket/{productpriceid}/{cartid}")]
        public IActionResult removeProductForBasket(int productpriceid, int cartid)
        {
            _CartService.RemoveProductForBasket(productpriceid, cartid);

            return RedirectToAction(nameof(basket));
        }


        [HttpPost]
        [Route("Discount")]
        public IActionResult Discount(string DiscountCode, int Cartid)
        {
            int userid = int.Parse(User.FindFirst("userid").Value);
            int Code = _Discountservice.checkDiscount(Cartid, DiscountCode);

            return RedirectToAction(nameof(basket), new { code = Code });
        }

        #endregion


        #region Payment


        [HttpGet]
        [Route("Payment")]
        [Authorize]
        public IActionResult Payment()
        {
            int userid = int.Parse(User.FindFirst("userid").Value);
            Cart cart = _CartService.FindCartBuyeuserid(userid);

            var zarinpal = new ZarinpalSandbox.Payment(cart.TotalPrice);
            var result = zarinpal.PaymentRequest("پرداخت سبد خرید کالا مارکت", "https://localhost:44366/onlinepayment/" + cart.cartid);

            if (result.Result.Status == 100)
            {
                var a = result.Result.Link;
                var a1 = result.Result.Authority;

                //return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
                return Redirect(a);
            }

            return null;
        }

        [Route("onlinepayment/{cartid}")]
        public IActionResult onlinepayment(int cartid)
        {
            bool res = false;
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                var authority = HttpContext.Request.Query["Authority"];

                var cart = _CartService.findcartbuyeid(cartid);

                var zarinpal = new ZarinpalSandbox.Payment(cart.TotalPrice);
                var result = zarinpal.Verification(authority).Result;

                if (result.Status == 100)
                {
                    ViewBag.refid = result.RefId;
                    res = true;
                    cart.ispay = true;
                    cart.RefId = result.RefId.ToString();
                    _CartService.UpdateCart(cart);
                }
            }
            ViewBag.res = res;
            return View();
        }


        #endregion


    }
}
