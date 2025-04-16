using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalamarket.Core.Service
{
    public class DiscountService : IDiscountService
    {
        private KalamarketContext _Context;
        private ICartService _CartService;
        public DiscountService(KalamarketContext Context, ICartService CartService)
        {
            _Context = Context;
            _CartService = CartService;
        }
        public int checkDiscount(int Cartid, string discountcode)
        {
            var Discount = _Context.discounts.FirstOrDefault(c => c.discountcode == discountcode);

            if (Discount == null)
                return 1;// کد تخفیف وارد شده نامعتبر می باشد .

            if (Discount.StartDate != null && Discount.StartDate > DateTime.Now.Date)
                return 2; // تاریخ کد تخفیف به پایان رسیده است

            if (Discount.EndDate != null && Discount.EndDate < DateTime.Now.Date)
                return 3; // تاریخ کد تخفیف به پایان رسیده است

            if (Discount.Useablecount != null && Discount.Useablecount <= 0)
                return 4; //  تعداد کد تخفیف به پایان رسیده است .

            var cart = _Context.cart.Find(Cartid);

            if (_Context.UserDiscounts.Any(c => c.userid == cart.userid && c.Discountid == Discount.discountid))
                return 5; // شما از قبل از این کد تخفیف استفاده کرده اید .

            int percent = (cart.TotalPrice * Discount.Discountpersent) / 100;

            cart.TotalPrice = cart.TotalPrice - percent;
            _CartService.UpdateCart(cart);

            if (Discount.Useablecount != null)
                Discount.Useablecount -= 1;

            _Context.Update(Discount);

            _Context.UserDiscounts.Add(new DataLayer.Entities.DisCount.UserDiscount
            {
                Discountid = Discount.discountid,
                userid = cart.userid,
            });
            _Context.SaveChanges();
            return 6; // کد تخفیف به موفقیت اعمال شد .
        }

    }
}
