using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi_test.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int ResId { get; set; }

        public string UserId { get; set; }
        
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime BookingDate { get; set; }
    }
}
