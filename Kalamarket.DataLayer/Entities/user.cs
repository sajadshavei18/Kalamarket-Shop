﻿using Kalamarket.DataLayer.Entities.DisCount;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using Kalamarket.DataLayer.Entities.Entitieproduct.FaQ;
using Kalamarket.DataLayer.Entities.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kalamarket.DataLayer.Entities
{
    public class user
    {
        [Key]
        public int userid { get; set; }
        public string userAccount { get; set; }
        public string username { get; set; }
        public string userfamily { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public DateTime CreateAccount { get; set; }
        public bool isActive { get; set; }
        public bool IsDelete { get; set; }
        public string email { get; set; }
        public string ActiveCode { get; set; }

        #region relation

        public List<question> questions { get; set; }

        public List<comment> comments { get; set; }
        public List<Faviorate> Faviorates { get; set; }
        public List<ProductReating> ProductReatings { get; set; }
        public List<Cart> Carts { get; set; }
        public List<UserDiscount> UserDiscount { get; set; }
        public List<UserRole> UserRoles { get; set; }
        #endregion

    }
}
