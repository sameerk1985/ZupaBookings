using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class Booking
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public BookingStatus Status { get; set; }
    }

    public enum BookingStatus
    {
        None = 0,
        Available,
        Booked
    }
}
