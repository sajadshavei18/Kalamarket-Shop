using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalamarket.Core.Service
{
    public class guranteeService : IguranteeService
    {
        private KalamarketContext _Context;
        public guranteeService(KalamarketContext Context)
        {
            _Context = Context;
        }

        public int AddGurante(ProductGurantee productGurantee)
        {
            try
            {
                _Context.productGurantees.Add(productGurantee);
                _Context.SaveChanges();
                return productGurantee.GuranteeId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteGurantee(ProductGurantee productGurantee)
        {
            try
            {
                productGurantee.IsDelete = true;
                _Context.productGurantees.Update(productGurantee);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExistGurantee(string guranteename, int guranteeid)
        {
            return _Context.productGurantees.Any(g => g.guranteename == guranteename && g.GuranteeId != guranteeid&&!g.IsDelete);
        }

        public ProductGurantee FindGuranteebuyeid(int guranteeid)
        {
            return _Context.productGurantees.Find(guranteeid);
        }

        public List<ProductGurantee> ShowAllGurantee()
        {
            return _Context.productGurantees.Where(g => !g.IsDelete).ToList();
        }

        public bool updategurantee(ProductGurantee productGurantee)
        {
            try
            {
                _Context.productGurantees.Update(productGurantee);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
