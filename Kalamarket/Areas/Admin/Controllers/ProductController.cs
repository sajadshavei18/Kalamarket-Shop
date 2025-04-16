﻿using Kalamarket.Core.ExtentionMethod;
using Kalamarket.Core.Service.Interface;
using Kalamarket.Core.Viewmodel;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalamarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private Iproductservice _productservice;
        private ICategoryService _Categoryservice;
        private IBrandService _brandservice;
        private IguranteeService _Guranteeservice;
        private ICartService _CartService;
        public ProductController(Iproductservice productservice, ICategoryService Categoryservice,
            IBrandService brandservice, IguranteeService Guranteeservice,
            ICartService CartService)
        {
            _productservice = productservice;
            _Categoryservice = Categoryservice;
            _brandservice = brandservice;
            _Guranteeservice = Guranteeservice;
            _CartService = CartService;
        }

        #region PropertyColor
        public IActionResult ShowAllColor()
        {
            return View(_productservice.showallColor());
        }

        [HttpGet]
        public IActionResult AddColor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddColor(productColor productColor)
        {
            if (!ModelState.IsValid)
                return View(productColor);
            if (_productservice.ExistColor(productColor.colorname, productColor.ColorCode, 0))
            {
                ModelState.AddModelError("ErrorColor", "رنگ انتخابی تکراری است .");
                return View(productColor);
            }
            int colorid = _productservice.AddColor(productColor);
            TempData["Result"] = colorid > 0 ? "true" : "false";
            return RedirectToAction(nameof(ShowAllColor));
        }


        [HttpGet]
        public IActionResult UpdateColor(int id)
        {
            productColor productColor = _productservice.findcolorbuyeid(id);
            if (productColor == null)
            {
                return NotFound();
            }
            return View(productColor);
        }

        [HttpPost]
        public IActionResult UpdateColor(productColor productColor)
        {
            if (!ModelState.IsValid)
            {
                return View(productColor);
            }
            if (_productservice.ExistColor(productColor.colorname, productColor.ColorCode, productColor.colorid))
            {
                ModelState.AddModelError("ErrorColor", "رنگ انتخابی تکراری است .");
                return View(productColor);
            }
            bool res = _productservice.updateColor(productColor);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(ShowAllColor));

        }
        #endregion

        #region Propertyname

        public IActionResult ShowAllPropertyname()
        {

            return View(_productservice.ShowAllProperty());
        }

        [HttpGet]
        public IActionResult AddPropertyname()
        {
            ViewBag.Category = _Categoryservice.Showsubcategory();
            return View();
        }
        [HttpPost]
        public IActionResult AddPropertyname(PropertyName propertyName, List<int> Categoryid)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = _Categoryservice.Showsubcategory();
                return View(propertyName);
            }
            if (_productservice.ExistPropertyname(propertyName.PropertyTitle, 0))
            {
                ModelState.AddModelError("PropertyTitle", "خصوصیات تکراری است .");
                return View(propertyName);
            }
            int nameid = _productservice.AddPropertyname(propertyName);
            if (nameid <= 0)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(ShowAllPropertyname));
            }
            List<Propertyname_Category> Addpc = new List<Propertyname_Category>();

            foreach (var item in Categoryid)
            {
                Addpc.Add(new Propertyname_Category
                {
                    Categoryid = item,
                    PropertyNameId = nameid,

                });
            }

            bool res = _productservice.AddPropertyForCategory(Addpc);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(ShowAllPropertyname));
        }

        [HttpGet]
        public IActionResult UpdateProperty(int id)
        {
            ViewBag.Category = _Categoryservice.Showsubcategory();
            ViewBag.Property = _productservice.ShowpropertynameForUpdate(id);
            return View(_productservice.findpropbuyeid(id));
        }

        [HttpPost]
        public IActionResult UpdateProperty(PropertyName propertyName, List<int> Categoryid)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = _Categoryservice.Showsubcategory();
                ViewBag.Property = _productservice.ShowpropertynameForUpdate(propertyName.PropertyNameId);
                return View();
            }
            if (_productservice.ExistPropertyname(propertyName.PropertyTitle, propertyName.PropertyNameId))
            {
                ModelState.AddModelError("PropertyTitle", "خصوصیات تکراری است .");
                return View(propertyName);
            }
            bool updateprop = _productservice.UpdatePropertyname(propertyName);
            if (!updateprop)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(ShowAllPropertyname));
            }
            bool deleteprop = _productservice.DeleteProperyForCategory(propertyName.PropertyNameId);
            if (!deleteprop)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(ShowAllPropertyname));
            }
            List<Propertyname_Category> categories = new List<Propertyname_Category>();
            foreach (var item in Categoryid)
            {
                categories.Add(new Propertyname_Category
                {
                    Categoryid = item,
                    PropertyNameId = propertyName.PropertyNameId,

                });
            }
            bool addpropertyforcategory = _productservice.AddPropertyForCategory(categories);
            TempData["Result"] = addpropertyforcategory ? "true" : "false";
            return RedirectToAction(nameof(ShowAllPropertyname));
        }
        #endregion

        #region Product

        public IActionResult ShowAllProduct()
        {
            return View(_productservice.ShowallProduct());
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Category = _Categoryservice.Showsubcategory();
            ViewBag.brand = _brandservice.ShowAllBrand();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(product product, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = _Categoryservice.Showsubcategory();
                ViewBag.brand = _brandservice.ShowAllBrand();
                return View(product);
            }
            if (file == null)
            {
                ModelState.AddModelError("SliderImg", "لطفا یک تصویر برای اسلایدر انتخاب نمایید .");
                return View(product);

            }

            string imgname = uplodimg.CreateImage(file);
            if (imgname == "false")
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(ShowAllProduct));
            }
            product.ProductCreate = DateTime.Now;
            product.Productimage = imgname;
            int productid = _productservice.AddProduct(product);
            TempData["Result"] = productid > 0 ? "true" : "false";
            return RedirectToAction(nameof(ShowAllProduct));

        }


        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            product update = _productservice.findproductbuyeid(id);
            if (update == null)
            {
                return NotFound();
            }
            ViewBag.Category = _Categoryservice.Showsubcategory();
            ViewBag.brand = _brandservice.ShowAllBrand();

            return View(update);
        }

        [HttpPost]
        public IActionResult UpdateProduct(product product, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = _Categoryservice.Showsubcategory();
                ViewBag.brand = _brandservice.ShowAllBrand();
                return View(product);
            }
            if (file != null)
            {
                string imgname = uplodimg.CreateImage(file);
                if (imgname == "false")
                {
                    TempData["Result"] = "false";
                    return RedirectToAction(nameof(Index));
                }
                bool DeleteImage = uplodimg.DeleteImg("ImageSite", product.Productimage);
                if (!DeleteImage)
                {
                    TempData["Result"] = "false";
                    return RedirectToAction(nameof(Index));
                }
                product.Productimage = imgname;

            }
            product.ProductUpdate = DateTime.Now;
            bool update = _productservice.UpdateProduct(product);
            TempData["Result"] = update ? "true" : "false";
            return RedirectToAction(nameof(ShowAllProduct));

        }

        [HttpGet]
        public IActionResult ShowPropertynameForProduct(int id)
        {
            int categoryid = _productservice.FindCategoryForProduct(id);
            ViewBag.propertyname = _productservice.showallpropertyforcategory(categoryid);
            ViewBag.propertyvalue = _productservice.showpropertyvalueforproduct(id);
            TempData["productid"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddPropertyForProduct(List<int> nameid, List<string> value)
        {
            int productid = int.Parse(TempData["productid"].ToString());
            if (nameid.Count != value.Count)
            {
                int categoryid = _productservice.FindCategoryForProduct(productid);
                ViewBag.propertyname = _productservice.showallpropertyforcategory(categoryid);
                ViewBag.propertyvalue = _productservice.showpropertyvalueforproduct(productid);
                TempData["productid"] = productid;
                return View("ShowPropertynameForProduct");
            }
            List<PropertyValue> propertyValues = new List<PropertyValue>();

            for (int i = 0; i < nameid.Count; i++)
            {
                propertyValues.Add(new PropertyValue
                {
                    productid = productid,
                    propertyvalue = value[i],
                    propertynameid = nameid[i],

                });
            }
            List<PropertyValue> PropertyValue = propertyValues.Where(p => p.propertyvalue != null).ToList();
            bool res = _productservice.AddOrUpdatePropertynameForProduct(PropertyValue, productid);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(ShowAllProduct));
        }

        public IActionResult ShowAllPrice(int id)
        {
            ViewBag.id = id;
            return View(_productservice.ShowAllPriceForProduct(id));
        }

        public IActionResult AddPrice(int id)
        {
            ViewBag.Gurantee = _Guranteeservice.ShowAllGurantee();
            ViewBag.Color = _productservice.showallColor();
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddPrice(AddOrUpdateProductpriceviewmodel productPrice)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Gurantee = _Guranteeservice.ShowAllGurantee();
                ViewBag.Color = _productservice.showallColor();
                ViewBag.id = productPrice.productid;
                return View(productPrice);
            }
            ProductPrice product = new ProductPrice
            {
                count = productPrice.count,
                Createdate = DateTime.Now,
                EndDateDisCount = productPrice.EndDateDisCount.ShamsiToMiladi(),
                mainprice = productPrice.mainprice,
                MaxorderCount = productPrice.MaxorderCount,
                productcolorid = productPrice.productcolorid,
                productguranteeid = productPrice.productguranteeid,
                productid = productPrice.productid,
                sepcialprice = productPrice.sepcialprice,
                Productpriceid = productPrice.Productpriceid,
            };
            int priceid = _productservice.AddPriceForProduct(product);
            TempData["Result"] = priceid > 0 ? "true" : "false";
            return RedirectToAction(nameof(ShowAllProduct));

        }
        #endregion

        #region review

        public IActionResult ShowReview(int id)
        {
            ViewBag.review = _productservice.findreviewbuyeid(id);
            TempData["productid"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddReview(List<string> posative, List<string> negative, Review review)
        {
            int productid = int.Parse(TempData["productid"].ToString());
            if (!ModelState.IsValid)
            {
                ViewBag.review = _productservice.findreviewbuyeid(productid);
                TempData["productid"] = productid;
                return View(review);
            }
            bool DeleteReview = _productservice.Deletereview(productid);
            if (!DeleteReview)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(ShowAllProduct));
            }
            Review AddReview = new Review()
            {
                ArticleDescription = review.ArticleDescription,
                AticleTitle = review.AticleTitle,
                reviewDescription = review.reviewDescription,
                Reviewnegative = string.Join("^", negative),
                ReviewPositive = string.Join("^", posative),
            };

            if (AddReview.reviewDescription != null ||
                AddReview.AticleTitle != null ||
                AddReview.ArticleDescription != null ||
                (!String.IsNullOrEmpty(AddReview.ReviewPositive) || !String.IsNullOrEmpty(AddReview.Reviewnegative)))
            {

                AddReview.productid = productid;
                bool addreview = _productservice.AddOrupdatereview(AddReview);
                TempData["Result"] = addreview ? "true" : "false";
                return RedirectToAction(nameof(ShowAllProduct));
            }
            TempData["Result"] = DeleteReview ? "true" : "false";
            return RedirectToAction(nameof(ShowAllProduct));

        }

        #endregion

        #region Posted

        [Route("showposted")]
        public IActionResult showposted()
        {
            return View(_CartService.Showposteds());
        }

        [Route("DetailPosted/{id}")]
        public IActionResult DetailPosted(int id)
        {
            return View(_CartService.Detailposteds(id));
        }
        [Route("Accept/{id}")]
        public IActionResult Accept(int id)
        {
            _CartService.Accept(id);
            return RedirectToAction(nameof(showposted));
        }

        #endregion


    }
}
