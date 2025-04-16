using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kalamarket.DataLayer.Entities.Entitieproduct
{
    public class Category
    {
        [Key]
        public int Categoryid { get; set; }

        [Display(Name = "عنوان دسته بندی به فارسی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد .")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باید")]
        public string CategoryFaTitle { get; set; }


        [Display(Name = "عنوان دسته بندی به خارجی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد .")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باید")]
        public string CategoryEnTitle { get; set; }

        public int? SubCategory { get; set; }

        public bool IsDelete { get; set; }

        #region Relation
        public List<product> products { get; set; }
        public List<Propertyname_Category> propertynames { get; set; }

        [ForeignKey("SubCategory")]
        public Category Categori { get; set; }


        #endregion
    }
}
