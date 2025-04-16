using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Service.Interface
{
    public interface IDiscountService
    {
        int checkDiscount(int Cartid, string discountcode);
    }
}
