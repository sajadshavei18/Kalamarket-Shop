using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalamarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDashbordController : Controller
    {
        private ICartService _CartService;
        public AdminDashbordController(ICartService CartService)
        {
            _CartService = CartService;
        }
        public IActionResult Index()
        {
            ViewBag.chart = _CartService.hichart();
            return View();
        }
    }
}
