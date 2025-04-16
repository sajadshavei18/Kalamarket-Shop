using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kalamarket.DataLayer.Entities.Entitieproduct
{
    public class brand
    {
        [Key]
        public int brandid { get; set; }

        [Display(Name = "عنوان برند")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد .")]
        [MinLength(5, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باید")]
        public string brandname { get; set; }

        #region relation

        public List<product> products { get; set; }

        #endregion
    }
}
