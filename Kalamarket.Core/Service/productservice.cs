using Kalamarket.Core.Service.Interface;
using Kalamarket.Core.Viewmodel;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using Kalamarket.DataLayer.Entities.Entitieproduct.FaQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalamarket.Core.Service
{
    public class productservice : Iproductservice
    {
        private KalamarketContext _Context;
        public productservice(KalamarketContext Context)
        {
            _Context = Context;
        }



        #region PropertyColor
        public int AddColor(productColor productColor)
        {
            try
            {
                _Context.ProductColors.Add(productColor);
                _Context.SaveChanges();
                return productColor.colorid;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool ExistColor(string namecolor, string codecolors, int colorid)
        {
            return _Context.ProductColors.Any(c => c.colorname == namecolor && c.ColorCode == codecolors && c.colorid != colorid);
        }

        public productColor findcolorbuyeid(int colorid)
        {
            return _Context.ProductColors.Find(colorid);
        }

        public List<productColor> showallColor()
        {
            return _Context.ProductColors.ToList();
        }

        public bool updateColor(productColor productColor)
        {
            try
            {
                _Context.ProductColors.Update(productColor);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Propertyname

        public List<PropertyName> ShowAllProperty()
        {
            return _Context.propertyNames.ToList();
        }

        public int AddPropertyname(PropertyName propertyName)
        {

            try
            {
                _Context.propertyNames.Add(propertyName);
                _Context.SaveChanges();
                return propertyName.PropertyNameId;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool ExistPropertyname(string name, int id)
        {
            return _Context.propertyNames.Any(p => p.PropertyTitle == name && p.PropertyNameId != id);
        }

        public bool AddPropertyForCategory(List<Propertyname_Category> categories)
        {
            try
            {
                _Context.propertyname_Categories.AddRange(categories);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<UpdatePropertynameViewmodel> ShowpropertynameForUpdate(int propertynameid)
        {
            List<UpdatePropertynameViewmodel> updates = (from pc in _Context.propertyname_Categories
                                                         join p in _Context.propertyNames on pc.PropertyNameId equals p.PropertyNameId
                                                         where (pc.PropertyNameId == propertynameid)
                                                         select new UpdatePropertynameViewmodel
                                                         {
                                                             pcId = pc.pcId,
                                                             Categoryid = pc.Categoryid,
                                                             PropertyNameId = p.PropertyNameId,
                                                             PropertyTitle = p.PropertyTitle,

                                                         }).ToList();
            return updates;
        }

        public bool UpdatePropertyname(PropertyName propertyName)
        {
            try
            {
                _Context.propertyNames.Update(propertyName);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProperyForCategory(int propid)
        {
            try
            {
                List<Propertyname_Category> categories = _Context.propertyname_Categories.Where(c => c.PropertyNameId == propid).ToList();
                _Context.propertyname_Categories.RemoveRange(categories);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public PropertyName findpropbuyeid(int id)
        {
            return _Context.propertyNames.Find(id);
        }
        #endregion

        #region Product

        public List<product> ShowallProduct()
        {
            return _Context.products.Where(p => !p.IsDelete).ToList();
        }

        public int AddProduct(product product)
        {
            try
            {
                _Context.products.Add(product);
                _Context.SaveChanges();
                return product.productid;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public product findproductbuyeid(int productid)
        {
            return _Context.products.Find(productid);
        }

        public bool UpdateProduct(product product)
        {
            try
            {
                _Context.products.Update(product);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int FindCategoryForProduct(int product)
        {
            return _Context.products.Where(p => p.productid == product).Select(p => p.Categoryid).SingleOrDefault();
        }

        public List<PropertyName> showallpropertyforcategory(int categoryid)
        {
            List<PropertyName> propertyNames = (from pc in _Context.propertyname_Categories
                                                join pn in _Context.propertyNames on pc.PropertyNameId equals pn.PropertyNameId
                                                where (pc.Categoryid == categoryid)
                                                select new PropertyName
                                                {
                                                    PropertyNameId = pn.PropertyNameId,
                                                    PropertyTitle = pn.PropertyTitle,
                                                }).ToList();
            return propertyNames;
        }

        public bool AddOrUpdatePropertynameForProduct(List<PropertyValue> propertyValues, int productid)
        {
            try
            {
                if (DeletePropertyvalueForProduct(productid))
                {
                    _Context.propertyValues.AddRange(propertyValues);
                    _Context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePropertyvalueForProduct(int productid)
        {
            try
            {
                List<PropertyValue> propertyValues = _Context.propertyValues.Where(p => p.productid == productid).ToList();
                _Context.propertyValues.RemoveRange(propertyValues);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<PropertyValue> showpropertyvalueforproduct(int productid)
        {
            return _Context.propertyValues.Where(p => p.productid == productid).ToList();
        }

        public List<showPriceForProductViewmodel> ShowAllPriceForProduct(int Productid)
        {
            List<showPriceForProductViewmodel> price = (from pr in _Context.ProductPrices
                                                        join p in _Context.products on pr.productid equals p.productid
                                                        join g in _Context.productGurantees on pr.productguranteeid equals g.GuranteeId
                                                        join c in _Context.ProductColors on pr.productcolorid equals c.colorid
                                                        where (pr.productid == Productid)

                                                        select new showPriceForProductViewmodel
                                                        {
                                                            Colorname = c.colorname,
                                                            count = pr.count,
                                                            Createdate = pr.Createdate,
                                                            EndDateDisCount = pr.EndDateDisCount,
                                                            guranteename = g.guranteename,
                                                            mainprice = pr.mainprice,
                                                            MaxorderCount = pr.mainprice,
                                                            productid = p.productid,
                                                            Productpriceid = pr.Productpriceid,
                                                            sepcialprice = pr.sepcialprice,

                                                        }).ToList();
            return price;
        }

        public int AddPriceForProduct(ProductPrice productPrice)
        {
            try
            {
                _Context.ProductPrices.Add(productPrice);
                _Context.SaveChanges();
                return productPrice.Productpriceid;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public List<sepcialProductViewmoddel> ShowAllSepcialproduct()
        {
            List<sepcialProductViewmoddel> sepcials = (from pr in _Context.ProductPrices
                                                       join p in _Context.products on pr.productid equals p.productid

                                                       where (pr.sepcialprice < pr.mainprice && pr.EndDateDisCount >= DateTime.Now.Date)

                                                       select new sepcialProductViewmoddel
                                                       {
                                                           Endsepcialprice = pr.EndDateDisCount,
                                                           mainprice = pr.mainprice,
                                                           productfatitle = p.productFaTitle,
                                                           productid = p.productid,
                                                           productimg = p.Productimage,
                                                           sepcialprice = pr.sepcialprice,
                                                           ValuesList = (from pv in _Context.propertyValues
                                                                         join pn in _Context.propertyNames on pv.propertynameid equals pn.PropertyNameId
                                                                         where (pv.productid == pr.productid)
                                                                         select new ValueViewmodel
                                                                         {
                                                                             propname = pn.PropertyTitle,
                                                                             value = pv.propertyvalue,

                                                                         }).Take(3).ToList()


                                                       }).ToList();

            return sepcials;
        }

        public List<SliderForCategoryViewmodel> showproductForCategory(int categoryid)
        {
            List<SliderForCategoryViewmodel> sliders = (from pr in _Context.ProductPrices
                                                        join p in _Context.products on pr.productid equals p.productid

                                                        where (p.Categoryid == categoryid)

                                                        select new SliderForCategoryViewmodel
                                                        {

                                                            mainprice = pr.mainprice,
                                                            productfatitle = p.productFaTitle,
                                                            Productid = p.productid,
                                                            productimg = p.Productimage,
                                                            sepcialprice = pr.EndDateDisCount >= DateTime.Now.Date && pr.sepcialprice < pr.mainprice ? pr.sepcialprice : pr.mainprice

                                                        }).ToList();

            return sliders;
        }

        public List<ShowDetailsProductViewmodel> ShowDetailsProduct(int productid)
        {
            List<ShowDetailsProductViewmodel> detail = (from pr in _Context.ProductPrices
                                                        join p in _Context.products on pr.productid equals p.productid
                                                        join g in _Context.productGurantees on pr.productguranteeid equals g.GuranteeId
                                                        join pc in _Context.ProductColors on pr.productcolorid equals pc.colorid
                                                        join c in _Context.categories on p.Categoryid equals c.Categoryid
                                                        join b in _Context.brands on p.brandid equals b.brandid

                                                        where (pr.productid == productid)

                                                        select new ShowDetailsProductViewmodel
                                                        {
                                                            brandname = b.brandname,
                                                            Categoryname = c.CategoryFaTitle,
                                                            ColorCode = pc.ColorCode,
                                                            Colorname = pc.colorname,
                                                            EndDiscount = pr.EndDateDisCount,
                                                            guranteename = g.guranteename,
                                                            MainPrice = pr.mainprice,
                                                            ProductEntitle = p.productEnTitle,
                                                            Productfatitle = p.productFaTitle,
                                                            Productid = p.productid,
                                                            Productimg = p.Productimage,
                                                            Productsell = p.ProductSell,
                                                            productstar = p.producStart,
                                                            productTag = p.productTag,
                                                            sepcialprice = pr.sepcialprice < pr.mainprice && pr.EndDateDisCount >= DateTime.Now.Date ? pr.sepcialprice : pr.mainprice,
                                                            Productpriceid = pr.Productpriceid,
                                                        }).ToList();
            return detail;
        }

        public List<ValueViewmodel> showValueForProduct(int productid)
        {
            List<ValueViewmodel> values = (from pv in _Context.propertyValues
                                           join pn in _Context.propertyNames on pv.propertynameid equals pn.PropertyNameId
                                           select new ValueViewmodel
                                           {
                                               propname = pn.PropertyTitle,
                                               value = pv.propertyvalue,

                                           }).Take(4).ToList();
            return values;
        }

        public List<ValueViewmodel> ShowAllPropertyForProduct(int productid)
        {
            List<ValueViewmodel> values = (from pv in _Context.propertyValues
                                           join pn in _Context.propertyNames on pv.propertynameid equals pn.PropertyNameId

                                           where (pv.productid == productid)
                                           select new ValueViewmodel
                                           {
                                               propname = pn.PropertyTitle,
                                               value = pv.propertyvalue,

                                           }).ToList();
            return values;
        }


        public List<ShowCommentForProductViewmodel> ShowAllCommentForProduct(int productid)
        {
            List<ShowCommentForProductViewmodel> showComments = (from c in _Context.comments
                                                                 join u in _Context.users on c.userid equals u.userid

                                                                 where (c.productid == productid && c.IsActive)

                                                                 select new ShowCommentForProductViewmodel
                                                                 {

                                                                     CommentDescription = c.commnetDescription,
                                                                     commentDislike = c.commentDislike,
                                                                     Commentlike = c.commentlike,
                                                                     commentTitle = c.commentTitle,
                                                                     CreateDate = c.CreateDate,
                                                                     Recomment = c.recommend,
                                                                     username = u.username + " " + u.userfamily,

                                                                 }).ToList();
            return showComments;
        }

        public List<ProductGallery> showgalleryforproduct(int productid)
        {
            return _Context.ProductGalleries.Where(p => p.productid == productid).ToList();
        }

        public List<comapreviewmodel> Showcompareproduct(List<int?> productid)
        {
            var compare = (from p in _Context.products.Where(p => productid.Contains(p.productid))
                           join pr in _Context.ProductPrices on p.productid equals pr.productid into pp
                           from pr in pp.DefaultIfEmpty()

                           select new comapreviewmodel
                           {
                               categoryid = p.Categoryid,
                               productfatitle = p.productFaTitle,
                               productid = p.productid,
                               productimg = p.Productimage,
                               productprice = pr.mainprice,
                               Compareviewmodel = (from pn in _Context.propertyNames
                                                   join pv in _Context.propertyValues on pn.PropertyNameId equals pv.propertynameid
                                                   where (pv.productid == p.productid)
                                                   select new Propertyproductcompare
                                                   {
                                                       productid = p.productid,
                                                       propertyname = pn.PropertyTitle,
                                                       propertynameid = pn.PropertyNameId,
                                                       propertyvalue = pv.propertyvalue,

                                                   }).ToList()


                           }).ToList();
            return compare;
        }

        public List<Propertyproductcompare> ShowPropertyCompare(int categoryid)
        {
            var compare = (from pg in _Context.propertyname_Categories
                           join pn in _Context.propertyNames on pg.PropertyNameId equals pn.PropertyNameId
                           join pv in _Context.propertyValues on pn.PropertyNameId equals pv.propertynameid

                           where (pg.Categoryid == categoryid)

                           select new Propertyproductcompare
                           {
                               productid = pv.productid,
                               propertyname = pn.PropertyTitle,
                               propertynameid = pn.PropertyNameId,
                               propertyvalue = pv.propertyvalue,


                           }).ToList();
            return compare;
        }

        public List<GetProductForCompare> GetProductForCompare(int ctagoryid, List<int?> productid)
        {
            var product = (from p in _Context.products
                           join c in _Context.categories on p.Categoryid equals c.Categoryid
                           where (p.Categoryid == ctagoryid && !productid.Contains(p.productid))

                           select new GetProductForCompare
                           {

                               Categoryid = c.Categoryid,
                               productfatitle = p.productFaTitle,
                               productid = p.productid,

                           }).ToList();
            return product;
        }

        public List<product> search(string text, List<int> categoryid,
            List<int> brandid, int minprice = 0, int maxprice = 0, int sort = 1)
        {

            IQueryable<product> products = _Context.products.Where(c => c.productFaTitle.Contains(text) || c.productEnTitle.Contains(text));

            switch (sort)
            {
                case 1:
                    products = products.OrderByDescending(c => c.ProductSell);
                    break;
                case 2:
                    products = products.OrderByDescending(c => c.ProductCreate);
                    break;
            }
            if (categoryid.Count() > 0)
            {
                products = products.Where(c => categoryid.Contains(c.Categoryid));
            }
            if (brandid.Count() > 0)
            {
                products = products.Where(c => brandid.Contains(c.brandid));
            }

            var query = (from p in products
                         join pr in _Context.ProductPrices on p.productid equals pr.productid
                         select new
                         {
                             productstar = p.producStart,
                             image = p.Productimage,
                             fatitle = p.productFaTitle,
                             mainprice = pr.mainprice,
                             sepcialprice = pr.sepcialprice,
                             productid = p.productid,
                         }).ToList();

            List<product> ListProduct = new List<product>();

            foreach (var item in products)
            {
                ListProduct.Add(new product
                {
                    producStart = item.producStart,
                    Productimage = item.Productimage,
                    productFaTitle = item.productFaTitle,
                    mainprice = query.Where(p => p.productid == item.productid).OrderBy(c => c.mainprice).Select(c => c.mainprice).FirstOrDefault(),
                    sepcialprice = query.Where(p => p.productid == item.productid).OrderBy(c => c.sepcialprice).Select(c => c.sepcialprice).FirstOrDefault(),

                });
            }

            if (minprice > 0)
            {
                ListProduct = ListProduct.Where(c => c.mainprice >= minprice && c.mainprice > 0).ToList();
            }
            if (maxprice > 0)
            {
                ListProduct = ListProduct.Where(c => c.mainprice <= maxprice && c.mainprice > 0).ToList();
            }

            return ListProduct;


        }
        public List<RandomProductViewmodel> RandomProduct()
        {

            return (from p in _Context.products
                    join pr in _Context.ProductPrices
                    on p.productid equals pr.productid
                    orderby Guid.NewGuid()
                    select new RandomProductViewmodel
                    {
                        mainprice =
                        (pr.sepcialprice < pr.mainprice
                        && pr.EndDateDisCount >= DateTime.Now.Date) ? pr.sepcialprice : pr.mainprice,

                        productfatitle = p.productFaTitle,
                        productid = p.productid,
                        productimage = p.Productimage,

                    }).Take(2).ToList();


        }

        public DetailProductViewmodel DetailProduct(int productid)
        {
            return (from p in _Context.products
                    join b in _Context.brands on p.brandid equals b.brandid
                    join c in _Context.categories on p.Categoryid equals c.Categoryid
                    where (p.productid == productid)
                    select new DetailProductViewmodel
                    {
                        categoryname = c.CategoryFaTitle,
                        productbrand = b.brandname,
                        productentitle = p.productEnTitle,
                        productfatitle = p.productFaTitle,
                        productid = p.productid,
                        productimage = p.Productimage,
                        productsell = p.ProductSell,
                        producttag = p.productTag,
                        productstar = p.producStart,

                    }).FirstOrDefault();
        }
        public List<productpricevm> GetProductprice(int productid)
        {
            return (from pr in _Context.ProductPrices
                    join g in _Context.productGurantees on pr.productguranteeid equals g.GuranteeId
                    join c in _Context.ProductColors on pr.productcolorid equals c.colorid
                    where (pr.productid == productid && pr.count > 0)
                    select new productpricevm
                    {
                        colorcode = c.ColorCode,
                        colorname = c.colorname,
                        count = pr.count,
                        enddiscount = pr.EndDateDisCount,
                        guranteename = g.guranteename,
                        mainprice = pr.mainprice,
                        maxordercount = pr.MaxorderCount,
                        productpriceid = pr.Productpriceid,
                        sepcialprice = pr.sepcialprice,


                    }).ToList();
        }

        #endregion

        #region Review

        public Review findreviewbuyeid(int productid)
        {
            return _Context.reviews.Where(r => r.productid == productid).FirstOrDefault();
        }

        public bool AddOrupdatereview(Review review)
        {
            try
            {
                _Context.reviews.Add(review);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Deletereview(int productid)
        {
            try
            {
                Review review = _Context.reviews.Where(r => r.productid == productid).FirstOrDefault();
                if (review != null)
                {
                    _Context.reviews.Remove(review);
                    _Context.SaveChanges();
                    return true;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }




        #endregion

        #region Faq

        public List<ShowfaqViewmodel> ShowAllFaq(int productid)
        {
            var qustion = _Context.questions.Where(q => q.IsActive && q.productid == productid);

            List<ShowfaqViewmodel> showfaq = (from q in qustion
                                              join uq in _Context.users on q.userid equals uq.userid

                                              join a in _Context.answers on q.questionid equals a.questionid into qa
                                              from a in qa.DefaultIfEmpty()

                                              join ua in _Context.users on a.userid equals ua.userid into u
                                              from ua in u.DefaultIfEmpty()

                                              select new ShowfaqViewmodel
                                              {
                                                  CreateDateq = q.CreateDate,
                                                  descriptionq = q.questionDescription,
                                                  questionid = q.questionid,
                                                  usernameq = uq.username + " " + uq.userfamily,
                                                  showAnswerVm = new ShowAnswerViewmodel
                                                  {
                                                      answerid = a.Answerid,
                                                      CreateDatea = a.CreateDate,
                                                      descriptiona = a.AnswerDescription,
                                                      usernamea = ua.username + " " + ua.userfamily,
                                                  }

                                              }).ToList();
            return showfaq;
        }

        public int AddQustion(question question)
        {
            try
            {
                _Context.Add(question);
                _Context.SaveChanges();
                return question.questionid;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int AddAnswer(Answer answer)
        {
            try
            {
                _Context.Add(answer);
                _Context.SaveChanges();
                return answer.Answerid;
            }
            catch (Exception)
            {
                return 0;
            }

        }


        #endregion


        #region Faviorate

        public Faviorate findfavioratebuyeuserid(int userid, int productid)
        {
            return _Context.Faviorates.Where(f => f.userid == userid && f.productid == productid).FirstOrDefault();
        }

        public bool AddFaiorate(Faviorate faviorate)
        {
            try
            {
                _Context.Faviorates.Add(faviorate);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool removefaviorate(Faviorate faviorate)
        {
            try
            {
                _Context.Faviorates.Remove(faviorate);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Reating

        public List<ReatingViewmodel> GetreatingForProduct(int productid)
        {
            List<ReatingViewmodel> reatings = (from pr in _Context.ProductReatings
                                               join p in _Context.products on pr.productid equals p.productid
                                               join pn in _Context.propertyNames on pr.propnameid equals pn.PropertyNameId
                                               where (pr.productid == productid)
                                               select new ReatingViewmodel
                                               {
                                                   productid = p.productid,
                                                   propertyname = pn.PropertyTitle,
                                                   Reatingid = pr.Reatingid,
                                                   Value = pr.Value,
                                                   userid = pr.userid,
                                                   propertynameid = pn.PropertyNameId,
                                               }).ToList();
            return reatings;

        }

        #endregion

    }
}
