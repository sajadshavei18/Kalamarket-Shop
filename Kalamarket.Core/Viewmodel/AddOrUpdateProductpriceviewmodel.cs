﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kalamarket.Core.Viewmodel
{
  public  class AddOrUpdateProductpriceviewmodel
    {
        public int Productpriceid { get; set; }


        [Display(Name = "قیمت اصلی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد .")]
        public int mainprice { get; set; }


        [Display(Name = "قیمت ویژه")]
        public int? sepcialprice { get; set; }


        [Display(Name = "تعداد کالا")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد .")]
        public int count { get; set; }



        [Display(Name = "تعداد خرید کاربر")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد .")]
        public int MaxorderCount { get; set; }


        public int productcolorid { get; set; }
        public int productguranteeid { get; set; }
        public int productid { get; set; }

        public DateTime Createdate { get; set; }
        public string EndDateDisCount { get; set; }
    }
}
