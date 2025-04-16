using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kalamarket.Componets
{
    public class sliderForCategoryViewComponent : ViewComponent
    {
        private Iproductservice _Productservice;
        public sliderForCategoryViewComponent(Iproductservice Productservice)
        {
            _Productservice = Productservice;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult(View("ShowSliderForsepcial", _Productservice.showproductForCategory(id)));
        }
    }
}
