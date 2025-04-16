using Kalamarket.DataLayer.Entities.Entitieproduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Service.Interface
{
   public interface IguranteeService
    {
        List<ProductGurantee> ShowAllGurantee();
        ProductGurantee FindGuranteebuyeid(int guranteeid);
        bool updategurantee(ProductGurantee productGurantee);
        bool DeleteGurantee(ProductGurantee productGurantee);
        bool ExistGurantee(string guranteename, int guranteeid);
        int AddGurante(ProductGurantee productGurantee);
    }
}
