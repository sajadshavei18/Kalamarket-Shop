using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Viewmodel
{
   public class showpostedViewmodel
    {
        public int cartid { get; set; }
        public string email { get; set; }
        public DateTime DateTime { get; set; }
        public int TotalPrice { get; set; }
    }

    public class DetailpostedViewmodel
    {
        public int productid { get; set; }
        public string productimage { get; set; }
        public string productname { get; set; }
        public int productprice { get; set; }
        public string gurantename { get; set; }
        public string colorname { get; set; }
        public string Address { get; set; }
    }
}
