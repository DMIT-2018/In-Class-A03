using eRestaurant.DAL;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; // Needed for the Lambda version of the Include() method

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
    }
}
