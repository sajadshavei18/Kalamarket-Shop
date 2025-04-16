using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Kalamarket.DataLayer.Entities;
using Kalamarket.Core.Viewmodel;
using Kalamarket.Core.ExtentionMethod;
using Microsoft.EntityFrameworkCore;

namespace Kalamarket.Core.Service
{
    public class CartService : ICartService
    {
        private KalamarketContext _Context;
        public CartService(KalamarketContext Context)
        {
            _Context = Context;
        }

        public int AddCart(int userid, int productid, int productcount)
        {
            Cart cart = _Context.cart.SingleOrDefault(c => !c.ispay && c.userid == userid);
            var product = _Context.ProductPrices.FirstOrDefault(c => c.Productpriceid == productid);

            if (cart == null)
            {
                cart = new Cart
                {
                    CreateDate = DateTime.Now.Date,
                    ispay = false,
                    RefId = "",
                    userid = userid,
                    TotalPrice = product.sepcialprice < product.mainprice && Convert.ToDateTime(product.EndDateDisCount).Date >= DateTime.Now.Date ? product.sepcialprice.Value : product.mainprice,

                    cartDetails = new List<CartDetail>
                    {
                        new CartDetail
                        {
                            count=productcount,
                            CreateDate=DateTime.Now.Date,
                            productid=product.Productpriceid,
                            price=product.sepcialprice < product.mainprice && Convert.ToDateTime(product.EndDateDisCount).Date >= DateTime.Now.Date ? product.sepcialprice.Value : product.mainprice,
                        }
                    }
                };
                _Context.Add(cart);
                _Context.SaveChanges();
                return 2;
            }
            else
            {
                CartDetail cartdetail =
                    _Context.CartDetail
                    .FirstOrDefault(c => c.Cartid == cart.cartid && c.productid == productid);

                if (cartdetail != null &&
                     (cartdetail.count + productcount) <= product.count
                    && (cartdetail.count + productcount) <= product.MaxorderCount)
                {
                    Decrease(cartdetail.Cartid, cartdetail.CartDetailid);
                    cartdetail.count += productcount;
                    _Context.CartDetail.Update(cartdetail);
                    sumprice(cartdetail.Cartid, cartdetail.CartDetailid);
                    return 2;
                }
                else if (cartdetail == null)
                {
                    cartdetail = new CartDetail
                    {
                        Cartid = cart.cartid,
                        count = productcount,
                        CreateDate = DateTime.Now.Date,
                        price = product.sepcialprice < product.mainprice && Convert.ToDateTime(product.EndDateDisCount).Date >= DateTime.Now.Date ? product.sepcialprice.Value : product.mainprice,
                        productid = product.Productpriceid,
                    };
                    _Context.Add(cartdetail);
                    _Context.SaveChanges();
                    sumprice(cartdetail.Cartid, cartdetail.CartDetailid);
                    return 2;
                }

            }
            return 3;
        }
        public void sumprice(int cartid, int detailid)
        {
            var cart = _Context.cart.Find(cartid);
            var detail = _Context.CartDetail.Where(c => c.Cartid == cartid && c.CartDetailid == detailid).FirstOrDefault();
            cart.TotalPrice += detail.count * detail.price;
            _Context.Update(cart);
            _Context.SaveChanges();
        }
        public void Decrease(int cartid, int detailid)
        {
            var cart = _Context.cart.Find(cartid);
            var detail = _Context.CartDetail.Where(c => c.Cartid == cartid && c.CartDetailid == detailid).FirstOrDefault();
            cart.TotalPrice -= detail.count * detail.price;
            _Context.Update(cart);
            _Context.SaveChanges();
        }

        public List<CartViewmodel> DetailCart(int userid)
        {
            List<CartViewmodel> carts = (from c in _Context.cart
                                         join cd in _Context.CartDetail on c.cartid equals cd.Cartid
                                         join pr in _Context.ProductPrices on cd.productid equals pr.Productpriceid
                                         join p in _Context.products on pr.productid equals p.productid
                                         join color in _Context.ProductColors on pr.productcolorid equals color.colorid
                                         join g in _Context.productGurantees on pr.productguranteeid equals g.GuranteeId
                                         where (c.userid == userid && !c.ispay)
                                         select new CartViewmodel
                                         {
                                             Colorname = color.colorname,
                                             guranteename = g.guranteename,
                                             ordercount = cd.count,
                                             productFaTitle = p.productFaTitle,
                                             Productid = p.productid,
                                             ProductPrice = cd.price,
                                             productpriceid = pr.Productpriceid,
                                             productimage = p.Productimage,
                                             TotalPrice = c.TotalPrice,
                                             Maxordercount = pr.MaxorderCount,
                                             productcount = pr.count,
                                             Cartid = cd.Cartid,
                                             ColorCode = color.ColorCode,
                                         }).ToList();
            return carts;
        }


        public int changebasket(int userid, int productid, int count)
        {
            Cart cart = _Context.cart.SingleOrDefault(c => !c.ispay && c.userid == userid);
            var product = _Context.ProductPrices.FirstOrDefault(c => c.Productpriceid == productid);

            if (cart == null)
            {
                cart = new Cart
                {
                    CreateDate = DateTime.Now.Date,
                    ispay = false,
                    RefId = "",
                    userid = userid,
                    TotalPrice = product.sepcialprice < product.mainprice && Convert.ToDateTime(product.EndDateDisCount).Date < DateTime.Now.Date ? product.sepcialprice.Value : product.mainprice,

                    cartDetails = new List<CartDetail>
                    {
                        new CartDetail
                        {
                            count=count,
                            CreateDate=DateTime.Now.Date,
                            productid=product.Productpriceid,
                            price=product.sepcialprice < product.mainprice && Convert.ToDateTime(product.EndDateDisCount).Date < DateTime.Now.Date ? product.sepcialprice.Value : product.mainprice,
                        }
                    }
                };
                _Context.Add(cart);
                _Context.SaveChanges();
                return 2;
            }
            else
            {
                CartDetail cartdetail = _Context.CartDetail.FirstOrDefault(c => c.Cartid == cart.cartid && c.productid == product.Productpriceid);

                if (cartdetail != null && cartdetail.count <= product.MaxorderCount)
                {
                    Decrease(cartdetail.Cartid, cartdetail.CartDetailid);
                    cartdetail.count = count;
                    _Context.CartDetail.Update(cartdetail);
                    sumprice(cartdetail.Cartid, cartdetail.CartDetailid);
                    return 2;
                }
                else if (cartdetail == null)
                {
                    cartdetail = new CartDetail
                    {
                        Cartid = cart.cartid,
                        count = count,
                        CreateDate = DateTime.Now.Date,
                        price = product.sepcialprice < product.mainprice && Convert.ToDateTime(product.EndDateDisCount).Date < DateTime.Now.Date ? product.sepcialprice.Value : product.mainprice,
                        productid = product.Productpriceid,
                    };
                    _Context.Add(cartdetail);
                    _Context.SaveChanges();
                    sumprice(cartdetail.Cartid, cartdetail.CartDetailid);
                    return 2;
                }

            }
            return 3;
        }

        public void RemoveProductForBasket(int productpriceid, int cartid)
        {
            var basket = _Context.CartDetail.Where(c => c.productid == productpriceid && c.Cartid == cartid).FirstOrDefault();
            Decrease(basket.Cartid, basket.CartDetailid);
            _Context.Remove(basket);
            _Context.SaveChanges();
        }
        public void UpdateCart(Cart cart)
        {
            _Context.Update(cart);
            _Context.SaveChanges();
        }

        public Cart FindCartBuyeuserid(int userid)
        {
            return _Context.cart.Where(c => !c.ispay && c.userid == userid).FirstOrDefault();
        }

        public Cart findcartbuyeid(int cartid)
        {
            return _Context.cart.Find(cartid);
        }

        public List<hichartViewmodel> hichart()
        {
            var chart = _Context.cart.Where(c => c.ispay).GroupBy(c => c.CreateDate.Date).OrderByDescending(c => c.Key).Take(7).Select(c => new hichartViewmodel
            {

                name = c.Key.MilatiToShamsi(),
                y = c.Count()

            }).ToList();
            //var chart = _Context.cart.Select(c=>new hichartViewmodel { 

            //name =c.CreateDate.ToString(),
            //y=c.cartid,

            //}).ToList();
            return chart.OrderBy(c => c.name).ToList();
        }

        public List<ShowBasketViewmodel> ShowAllProductForBasket(int userid)
        {
            return (from c in _Context.cart
                    where (c.userid == userid && !c.ispay)
                    join cd in _Context.CartDetail on c.cartid equals cd.Cartid

                    join pr in _Context.ProductPrices on cd.productid equals pr.Productpriceid
                    join p in _Context.products on pr.productid equals p.productid

                    select new ShowBasketViewmodel
                    {
                        Mainprice =
                        (pr.mainprice > pr.sepcialprice &&
                        pr.EndDateDisCount >= DateTime.Now.Date)
                        ? pr.sepcialprice : pr.mainprice,

                        productfaTitle = p.productFaTitle,
                        productid = p.productid,
                        productimage = p.Productimage,
                        totalbasket = c.TotalPrice,

                    }).ToList();

        }

        public List<showpostedViewmodel> Showposteds()
        {
            var b = _Context.cart.Where(x => !x.posted)
                .Include(c => c.user)
                .Select(x => new showpostedViewmodel
                {
                    cartid = x.cartid,
                    DateTime = x.CreateDate,
                    email = x.user.email,
                    TotalPrice = x.TotalPrice,
                }).ToList();
            return b;

        }

        public List<DetailpostedViewmodel> Detailposteds(int cartid)
        {
            var detail = (from ct in _Context.cart
                          join cd in _Context.CartDetail on ct.cartid equals cd.Cartid

                          join pr in _Context.ProductPrices on cd.productid equals pr.Productpriceid
                          join p in _Context.products on pr.productid equals p.productid
                          join c in _Context.ProductColors on pr.productcolorid equals c.colorid
                          join g in _Context.productGurantees on pr.productguranteeid equals g.GuranteeId

                          join u in _Context.users on ct.userid equals u.userid
                          join ua in _Context.useraddresses on u.userid equals ua.userid
                          join province in _Context.provinces on ua.provinceid equals province.provinceid
                          join citi in _Context.cities on ua.cityid equals citi.cityid

                          where (ct.cartid == cartid && (!ua.Isdelete && ua.userid == ct.userid))
                          select new DetailpostedViewmodel
                          {
                              Address = province.provincename + "-" + citi.cityname + "-" + ua.FullAddress,
                              colorname = c.colorname,
                              gurantename = g.guranteename,
                              productid = p.productid,
                              productimage = p.Productimage,
                              productname = p.productFaTitle,
                              productprice = cd.price,

                          }).ToList();
            return detail;

        }

        public void Accept(int cartid)
        {
            var cart = _Context.cart.Find(cartid);
            cart.posted = true;
            _Context.SaveChanges();
        }


        public bool ExistAddress(int userid)
        {
            return _Context.useraddresses.Any(x => x.userid == userid && !x.Isdelete);
        }

    }
}
