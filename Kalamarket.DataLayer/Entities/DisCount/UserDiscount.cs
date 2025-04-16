using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kalamarket.DataLayer.Entities.DisCount
{
    public class UserDiscount
    {
        [Key]
        public int userdiscountid { get; set; }

        public int userid { get; set; }
        public int Discountid { get; set; }



        #region Relation

        [ForeignKey(nameof(userid))]
        public user user { get; set; }

        [ForeignKey(nameof(Discountid))]
        public discount discount { get; set; }
        #endregion


    }
}
