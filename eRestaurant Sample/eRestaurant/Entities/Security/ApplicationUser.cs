using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Entities.Security
{
    public class ApplicationUser : IdentityUser
    {
        public int? WaiterID { get; set; }
    }
}
