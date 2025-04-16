using Kalamarket.Core.Viewmodel;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using Kalamarket.DataLayer.Entities.Entitieproduct.FaQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Service.Interface
{
    public interface Iproductservice
    {
        #region ProductColor
        List<productColor> showallColor();
        int AddColor(productColor productColor);
        bool updateColor(productColor productColor);
        productColor findcolorbuyeid(int colorid);
        bool ExistColor(string namecolor, string codecolors, int colorid);

        #endregion

        #region Propertyname
        List<PropertyName> ShowAllProperty();
        int AddPropertyname(PropertyName propertyName);
        bool ExistPropertyname(string name, int id);
        bool AddPropertyForCategory(List<Propertyname_Category> categories);
        List<UpdatePropertynameViewmodel> ShowpropertynameForUpdate(int propertynameid);
        bool UpdatePropertyname(PropertyName propertyName);
        bool DeleteProperyForCategory(int propid);
        PropertyName findpropbuyeid(int id);
        #endregion

        #region Product
        List<product> ShowallProduct();
        int AddProduct(product product);
        product findproductbuyeid(int productid);
        bool UpdateProduct(product product);
        int FindCategoryForProduct(int product);
        List<PropertyName> showallpropertyforcategory(int categoryid);
        bool AddOrUpdatePropertynameForProduct(List<PropertyValue> propertyValues, int productid);
        bool DeletePropertyvalueForProduct(int productid);
        List<PropertyValue> showpropertyvalueforproduct(int productid);
        List<showPriceForProductViewmodel> ShowAllPriceForProduct(int Productid);
        int AddPriceForProduct(ProductPrice productPrice);
        List<sepcialProductViewmoddel> ShowAllSepcialproduct();
        List<SliderForCategoryViewmodel> showproductForCategory(int categoryid);
        List<ShowDetailsProductViewmodel> ShowDetailsProduct(int productid);
        List<ValueViewmodel> showValueForProduct(int productid);
        List<ValueViewmodel> ShowAllPropertyForProduct(int productid);
        List<ShowCommentForProductViewmodel> ShowAllCommentForProduct(int productid);
        List<ProductGallery> showgalleryforproduct(int productid);
        List<comapreviewmodel> Showcompareproduct(List<int?> productid);
        List<Propertyproductcompare> ShowPropertyCompare(int categoryid);
        List<GetProductForCompare> GetProductForCompare(int ctagoryid, List<int?> productid);
        List<product> search(string text, List<int> categoryid,
            List<int> brandid, int minprice = 0, int maxprice = 0, int sort = 1);

        List<RandomProductViewmodel> RandomProduct();

        List<productpricevm> GetProductprice(int productid);
        DetailProductViewmodel DetailProduct(int productid);

        #endregion

        #region review
        Review findreviewbuyeid(int productid);
        bool AddOrupdatereview(Review review);
        bool Deletereview(int productid);

        #endregion

        #region Faq
        List<ShowfaqViewmodel> ShowAllFaq(int productid);
        int AddAnswer(Answer answer);
        int AddQustion(question question);
        #endregion

        #region Faviorate
        bool removefaviorate(Faviorate faviorate);
        bool AddFaiorate(Faviorate faviorate);
        Faviorate findfavioratebuyeuserid(int userid, int productid);
        #endregion

        #region Reating
        List<ReatingViewmodel> GetreatingForProduct(int productid);
        #endregion
    }
}
