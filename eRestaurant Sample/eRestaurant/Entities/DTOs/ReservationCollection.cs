using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Entities.DTOs
{
    public class ReservationCollection
    {
        public int Time { get; set; }

        public List<ReservationSummary> Reservations { get; set; }
    }
}
