using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kalamarket.DataLayer.Entities
{
    public class Cart
    {
        [Key]
        public int cartid { get; set; }

        public int userid { get; set; }

        public bool ispay { get; set; }

        public string RefId { get; set; }

        public DateTime CreateDate { get; set; }

        public int TotalPrice { get; set; }
        public bool posted { get; set; }

        #region Relation

        [ForeignKey(nameof(userid))]
        public user user { get; set; }

        public List<CartDetail> cartDetails { get; set; }

        #endregion

    }
}
