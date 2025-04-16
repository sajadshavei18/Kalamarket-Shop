using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalamarket.Core.Service
{
    public class CategoryService : ICategoryService
    {
        private KalamarketContext _Context;
        public CategoryService(KalamarketContext Context)
        {
            _Context = Context;
        }

        public int AddCategory(Category category)
        {
            try
            {
                _Context.categories.Add(category);
                _Context.SaveChanges();
                return category.Categoryid;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteCategory(Category category)
        {
            try
            {
                _Context.categories.Update(category);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category findcategorybuyeid(int categoryid)
        {
            return _Context.categories.Find(categoryid);
        }

        public List<Category> ShowAllCategory()
        {
            return _Context.categories.Where(c => !c.IsDelete && c.SubCategory == null).ToList();
        }

        public List<Category> showAllSubCategory(int categoryid)
        {
            return _Context.categories.Where(c => !c.IsDelete && c.SubCategory == categoryid).ToList();
        }

        public bool updatecategory(Category category)
        {
            try
            {
                _Context.categories.Update(category);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExistCategory(string fatitle, string entitle, int cateid)
        {
            return _Context.categories.Any(c => c.CategoryFaTitle == fatitle && c.CategoryEnTitle == entitle && c.Categoryid != cateid);
        }

        public List<Category> Showsubcategory()
        {
            return _Context.categories.Where(c => c.SubCategory != null).ToList();
        }

        public List<Category> GetAllCategoryForMenu()
        {
            return _Context.categories.Where(c => !c.IsDelete).ToList();
        }
    }
}
