using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Viewmodel
{
    public class ShowDetailorder
    {
        public int cartid { get; set; }
        public int productid { get; set; }
        public string productFaTitle { get; set; }
        public int price { get; set; }
        public int Totalprice { get; set; }
    }
}
