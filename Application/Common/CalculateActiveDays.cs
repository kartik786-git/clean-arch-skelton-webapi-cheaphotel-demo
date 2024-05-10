using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public static class CalculateActiveDays
    {
        public static double CalculateDays(Room room)
        {
            var now = DateTime.Now;
            double leftDays = 0;

            var booking = room.Bookings.FirstOrDefault();
            if (booking?.CheckOut > now)
            {
                leftDays += Math.Round((booking.CheckOut - now).TotalDays);
            }

            return leftDays;
        }
    }
}
