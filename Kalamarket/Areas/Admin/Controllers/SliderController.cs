using Kalamarket.Core.ExtentionMethod;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalamarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private ISliderService _SliderService;
        public SliderController(ISliderService SliderService)
        {
            _SliderService = SliderService;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.page = page;
            ViewBag.CountSlider = _SliderService.SliderCount();
            return View(_SliderService.ShowAllSlider(page));
        }

        [HttpGet]
       // [CheckPermission(2)]
        public IActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSlider(MainSlider mainSlider, IFormFile file)
        {


            if (mainSlider.SliderSort <= 0)
            {
                ModelState.AddModelError("ErrorSort", "لطفا ترتیب اسلایدر را وارد نمایید .");
                return View(mainSlider);
            }

            if (!ModelState.IsValid)
                return View(mainSlider);


            if (file == null)
            {
                ModelState.AddModelError("SliderImg", "لطفا یک تصویر برای اسلایدر انتخاب نمایید .");
                return View(mainSlider);

            }

            string imgname = uplodimg.CreateImage(file);
            if (imgname == "false")
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }
            mainSlider.SliderImg = imgname;
            int res = _SliderService.AddSlider(mainSlider);
            TempData["Result"] = res > 0 ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult UpdateSlider(int id)
        {
            MainSlider mainSlider = _SliderService.FindSliderBuyeId(id);
            if (mainSlider == null)
            {
                TempData["NotFoundSlider"] = "true";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(mainSlider);
            }
        }

        [HttpPost]
        public IActionResult UpdateSlider(MainSlider mainSlider, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(mainSlider);
            }
            if (file != null)
            {
                string imgname = uplodimg.CreateImage(file);
                if (imgname == "false")
                {
                    TempData["Result"] = "false";
                    return RedirectToAction(nameof(Index));
                }
                bool DeleteImage = uplodimg.DeleteImg("ImageSite", mainSlider.SliderImg);
                if (!DeleteImage)
                {
                    TempData["Result"] = "false";
                    return RedirectToAction(nameof(Index));
                }
                mainSlider.SliderImg = imgname;

            }
            bool res = _SliderService.UpdateSlider(mainSlider);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DeleteSlider(int id)
        {
            MainSlider mainSlider = _SliderService.FindSliderBuyeId(id);
            if (mainSlider == null)
            {
                TempData["NotFoundSlider"] = "true";
                return RedirectToAction(nameof(Index));
            }
            return View(mainSlider);
        }

        [HttpPost]
        public IActionResult DeleteSlider(MainSlider mainSlider)
        {
            bool DeleteImage = uplodimg.DeleteImg("ImageSite", mainSlider.SliderImg);
            if (!DeleteImage)
            {
                TempData["Result"] = "false";
                return RedirectToAction(nameof(Index));
            }
            bool res = _SliderService.DeleteSlider(mainSlider);
            TempData["Result"] = res ? "true" : "false";
            return RedirectToAction(nameof(Index));
        }
    }
}
