﻿using Kalamarket.DataLayer.Entities.Entitieproduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Service.Interface
{
    public interface ICategoryService
    {
        List<Category> ShowAllCategory();

        int AddCategory(Category category);

        bool updatecategory(Category category);
        bool DeleteCategory(Category category);

        List<Category> showAllSubCategory(int categoryid);

        Category findcategorybuyeid(int categoryid);

        bool ExistCategory(string fatitle, string entitle, int cateid);
        List<Category> Showsubcategory();
        List<Category> GetAllCategoryForMenu();
    }
}
