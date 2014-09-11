using eRestaurant.Entities; // Needed for accessing my Entity classes
using System;
using System.Collections.Generic;
using System.Data.Entity; // Needed for the DbContext base class
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.DAL
{
    public class RestaurantContext : DbContext
    {
        // Constructor that calls a base-class constructor to specify the
        // connection string we need to use
        public RestaurantContext() : base("name=EatIn") { }

        #region Table to Entity mappings
        public DbSet<Table> Tables { get; set; }
        public DbSet<SpecialEvent> SpecialEvents { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        #endregion
    }
}
