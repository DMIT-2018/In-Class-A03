using eRestaurant.DAL;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eRestaurant.Entities.DTOs; // Needed for the Lambda version of the Include() method

namespace eRestaurant.BLL
{
    [DataObject]
    public class MenuController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Item> ListMenuItems()
        {
            using (var context = new RestaurantContext())
            {
                // Note: To use the Lambda or Method style of Include, you need to use System.Data.Entity
                // Get the Item data and include the Category data for each item
                // The .Include() method on the DbSet<T> class performs "eager loading" of data.
                return context.Items.Include(it => it.Category).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Category> ListCategorizedMenuItems()
        {
            using (var context = new RestaurantContext())
            {
                var data = from cat in context.MenuCategories
                           orderby cat.Description
                           select new Category()
                           {
                               Description = cat.Description,
                               MenuItems = from item in cat.MenuItems
                                           where item.Active
                                           orderby item.Description
                                           select new MenuItem()
                                           {
                                               Description = item.Description,
                                               Price = item.CurrentPrice,
                                               Calories = item.Calories,
                                               Comment = item.Comment
                                           }
                           };
                return data.ToList();
            }
        }
    }
}
