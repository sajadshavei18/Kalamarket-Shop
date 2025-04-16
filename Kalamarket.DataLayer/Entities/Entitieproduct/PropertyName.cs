﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kalamarket.DataLayer.Entities.Entitieproduct
{
    public class PropertyName
    {
        [Key]
        public int PropertyNameId { get; set; }

        [Display(Name = "عنوان خصوصیات")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد .")]
        [MinLength(5, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باید")]
        public string PropertyTitle { get; set; }


        #region Relation
        public List<Propertyname_Category> propertynames { get; set; }
        public List<PropertyValue> PropertyValues { get; set; }
        public List<ProductReating> ProductReatings { get; set; }

        #endregion
    }
}
