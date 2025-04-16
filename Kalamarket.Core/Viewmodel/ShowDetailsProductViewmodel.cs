using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Viewmodel
{
    public class ShowDetailsProductViewmodel
    {
        public int Productid { get; set; }
        public string Productfatitle { get; set; }
        public string ProductEntitle { get; set; }
        public int Productsell { get; set; }
        public int productstar { get; set; }
        public string Categoryname { get; set; }
        public string productTag { get; set; }
        public string brandname { get; set; }
        public string Colorname { get; set; }
        public string ColorCode { get; set; }
        public string guranteename { get; set; }
        public int MainPrice { get; set; }
        public int? sepcialprice { get; set; }
        public DateTime? EndDiscount { get; set; }
        public string Productimg { get; set; }
        public int Productpriceid { get; set; }
    }
}
