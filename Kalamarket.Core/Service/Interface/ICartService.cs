using Kalamarket.Core.Viewmodel;
using Kalamarket.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Service.Interface
{
    public interface ICartService
    {
        int AddCart(int userid, int productid,int productcount);
        List<CartViewmodel> DetailCart(int userid);
        int changebasket(int userid, int productid, int count);
        void RemoveProductForBasket(int productpriceid, int cartid);
        void UpdateCart(Cart cart);
        Cart FindCartBuyeuserid(int userid);
        Cart findcartbuyeid(int cartid);
        List<hichartViewmodel> hichart();
        List<ShowBasketViewmodel> ShowAllProductForBasket(int userid);
        List<DetailpostedViewmodel> Detailposteds(int cartid);
        List<showpostedViewmodel> Showposteds();
        void Accept(int cartid);
    }
}
