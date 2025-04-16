using Kalamarket.Core.ExtentionMethod;
using Kalamarket.Core.Service.Interface;
using Kalamarket.Core.Viewmodel;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalamarket.Core.Service
{
    public class Userservice : IUserservice
    {
        private KalamarketContext _Context;
        public Userservice(KalamarketContext Context)
        {
            _Context = Context;
        }
        public int AddUser(user user)
        {
            try
            {
                _Context.users.Add(user);
                _Context.SaveChanges();
                return user.userid;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool deleteuser(user user)
        {
            try
            {
                user.IsDelete = true;
                _Context.users.Update(user);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExistEmail(string email, int id)
        {
            return _Context.users.Any(u => u.email == email && u.userid != id);
        }

        public bool updateuser(user user)
        {
            try
            {
                _Context.users.Update(user);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public user Finduser(int userid, string Code)
        {
            return _Context.users.Where(u => u.userid == userid && u.ActiveCode == Code).FirstOrDefault();
        }

        public user FindUserbuyeEmail(string Email)
        {
            return _Context.users.Where(u => u.email == Email).FirstOrDefault();
        }


        public user LoginUser(string email, string Password)
        {
            return _Context.users.Where(u => u.password == Password && u.email == email).SingleOrDefault();
        }


        public informationAccountViewmodel informationAccount(int userid)
        {
            return _Context.users.Where(x => x.userid == userid).Select(x => new informationAccountViewmodel
            {

                DateTime = x.CreateAccount,
                email = x.email,
                userid = x.userid,
                phone = x.phone,
                userfamily = x.userfamily,
                username = x.username,

            }).FirstOrDefault();
        }

        public edituserViewmodel finduserbuyeid(int userid)
        {
            return _Context.users.Where(x => x.userid == userid)
                .Select(x => new edituserViewmodel
                {
                    email = x.email,
                    phone = x.phone,
                    userfamily = x.userfamily,
                    username = x.username

                }).FirstOrDefault();
        }
        public user findEditUserbuyeid(int userid)
        {
            return _Context.users.Find(userid);

        }

        public List<showfavoirateViewmodel> showfavoirateUser(int userid)
        {
            return (from f in _Context.Faviorates
                    join u in _Context.users on f.userid equals u.userid
                    join p in _Context.products on f.productid equals p.productid
                    where (f.userid == userid)
                    select new showfavoirateViewmodel
                    {
                        productfatitle = p.productFaTitle,
                        productid = p.productid,
                        productimage = p.Productimage,
                        productstar = p.producStart,

                        productprice = _Context.ProductPrices.Where(x => x.count > 0
                          && x.productid == p.productid)
                        .OrderBy(x => x.mainprice).Select(x => x.mainprice).FirstOrDefault(),

                    }).ToList();

        }

        public List<showorderForUser> showorderForUsers(int userid)
        {
            return _Context.cart.Where(x => x.userid == userid).Select(x => new showorderForUser
            {
                cartid = x.cartid,
                createdate = x.CreateDate.MilatiToShamsi(),
                ispaye = x.ispay,
                totalprice = x.TotalPrice,

            }).ToList();

        }


        public List<mycommentViewmodel> mycomment(int userid)
        {
            return (from c in _Context.comments
                    join p in _Context.products on c.productid equals p.productid
                    where (c.userid == userid)
                    select new mycommentViewmodel
                    {
                        commenttitle = c.commentTitle,
                        isactive = c.IsActive,
                        productFaTitle = p.productFaTitle,
                        productid = p.productid,
                        productstar = p.producStart,
                        productimage = p.Productimage,
                    }).ToList();


        }

        public List<ShowDetailorder> showDetailorders(int orderid)
        {
            var b = (from cd in _Context.CartDetail
                     join c in _Context.cart on cd.Cartid equals c.cartid
                     join p in _Context.products on cd.productid equals p.productid
                     where (cd.Cartid == orderid)
                     select new ShowDetailorder
                     {
                         productid = p.productid,
                         cartid = c.cartid,
                         price = cd.price,
                         productFaTitle = p.productFaTitle,
                         Totalprice = c.TotalPrice,

                     }).ToList();
            return b;
        }

    }
}
