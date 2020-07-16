using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Repositories
{
    public interface IBookingRepository : IDisposable
    {
        List<Booking> GetAllSeats();

        List<Booking> GetAvailableSeats();

        List<Booking> GetBookedSeats();

        Booking GetBookingByID(string ID);

        bool CreateBooking(Booking booking);

        bool CreateBooking(List<Booking> bookings, out string message);

        bool CancelBooking(Booking booking);

        bool UpdateBooking(Booking booking);
    }
}
