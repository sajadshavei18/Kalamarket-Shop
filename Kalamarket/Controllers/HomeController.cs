using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalamarket.Controllers
{

    public class HomeController : Controller
    {
        private Iproductservice _Productservice;
        public HomeController(Iproductservice Productservice)
        {
            _Productservice = Productservice;
        }

        //[CheckPermission(1)]
        public IActionResult Index()
        {

            ViewBag.sepcialprice = _Productservice.ShowAllSepcialproduct();
            ViewBag.RandomProduct = _Productservice.RandomProduct();
            return View();
        }

        public IActionResult Error()
        {

            return View();
        }


    }
}
