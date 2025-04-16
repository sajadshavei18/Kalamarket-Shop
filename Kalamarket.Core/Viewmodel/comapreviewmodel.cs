﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Viewmodel
{
    public class comapreviewmodel
    {
        public int productid { get; set; }
        public string productfatitle { get; set; }
        public int? productprice { get; set; }
        public string productimg { get; set; }
        public int categoryid { get; set; }
        public List<Propertyproductcompare> Compareviewmodel { get; set; }
    }

    public class Propertyproductcompare
    {
        public int propertynameid { get; set; }
        public string propertyname { get; set; }
        public string propertyvalue { get; set; }
        public int productid { get; set; }
    }

    public class GetProductForCompare
    {
        public int productid { get; set; }
        public string productfatitle { get; set; }
        public int Categoryid { get; set; }
    }
}
