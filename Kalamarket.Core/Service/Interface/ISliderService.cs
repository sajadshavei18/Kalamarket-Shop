using Kalamarket.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Service.Interface
{
    public interface ISliderService
    {
        List<MainSlider> ShowAllSlider(int page);
        MainSlider FindSliderBuyeId(int sliderid);
        int AddSlider(MainSlider mainSlider);
        bool UpdateSlider(MainSlider mainSlider);
        bool DeleteSlider(MainSlider mainSlider);
        List<MainSlider> ShowSliderForUser();
        int SliderCount();
    }
}
