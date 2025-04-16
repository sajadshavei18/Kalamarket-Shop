using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalamarket.Core.Service
{
    public class BrandService : IBrandService
    {
        private KalamarketContext _Context;
        public BrandService(KalamarketContext Context)
        {
            _Context = Context;
        }

        public List<brand> ShowAllBrand()
        {
            return _Context.brands.ToList();
        }

    }
}
