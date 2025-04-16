using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kalamarket.DataLayer.Entities
{
    public class MainSlider
    {
        [Key]
        public int Sliderid { get; set; }

        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است .")]
        public string SliderTitle { get; set; }

        [Display(Name = "الت اسلایدر")]
        public string sliderAlt { get; set; }

        [Display(Name = "ترتیب اسلایدر")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمایید .")]
        public int SliderSort { get; set; }

        [Display(Name = "لینک اسلایدر")]
        public string SliderLink { get; set; }

        [Display(Name = "تصویر اسلایدر اسلایدر")]
        public string SliderImg { get; set; }

        public bool IsActive { get; set; }
    }
}
