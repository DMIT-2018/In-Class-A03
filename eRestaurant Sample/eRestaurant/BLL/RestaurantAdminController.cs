using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.BLL
{
    public class RestaurantAdminController
    {
        #region Manage Waiters
        #region Command
        public int AddWaiter(Waiter item)
        {
            throw new NotImplementedException();
        }

        public void UpdateWaiter(Waiter item)
        {
        }

        public void DeleteWaiter(Waiter item)
        {
        }
        #endregion
        #region Query
        public List<Waiter> ListAllWaiters()
        {
            throw new NotImplementedException();
        }
        public Waiter GetWaiter(int waiterId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region Manage Tables
        #region Command
        #endregion
        #region Query
        #endregion
        #endregion

        #region Manage Items
        #region Command
        #endregion
        #region Query
        #endregion
        #endregion

        #region Manage Special Events
        #region Command
        #endregion
        #region Query
        #endregion
        #endregion
    }
}
