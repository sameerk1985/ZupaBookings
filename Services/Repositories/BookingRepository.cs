using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private List<Booking> Bookings = null;
        private bool disposed = false;
        private readonly int rows = 10;
        private readonly int seats = 10;

        public BookingRepository()
        {
            InitialiseBookingRepository(rows, seats);
        }

        private void InitialiseBookingRepository(int rows, int seats)
        {
            Bookings = new List<Booking>();

            for (int i = 1; i <= rows; i++)
            {
                string strAplha = ((char)(i + 64)).ToString();
                for (int j = 1; j <= seats; j++)
                {
                    Booking booking = new Booking {
                        ID = strAplha + j.ToString(),
                        Name = string.Empty,
                        Email = string.Empty,
                        Status = BookingStatus.Available
                    };

                    Bookings.Add(booking);
                }
            }
        }

        public bool CancelBooking(Booking booking)
        {
            Booking booking1 = Bookings.Where(b => b.ID.Equals(booking.ID)).FirstOrDefault();

            if (booking1.Status == BookingStatus.Booked)
            {
                booking1.ID = booking.ID;
                booking1.Name = string.Empty;
                booking1.Email = string.Empty;
                booking1.Status = BookingStatus.Available;
                return true;
            }

            return false;
        }

        public bool CreateBooking(Booking booking)
        {
            Booking booking1 = Bookings.Where(b => b.ID.Equals(booking.ID)).FirstOrDefault();

            if (booking1.Status == BookingStatus.Booked)
                return false;

            booking1.ID = booking.ID;
            booking1.Name = booking.Name;
            booking1.Email = booking.Email;
            booking1.Status = BookingStatus.Booked;

            return true;
        }

        public bool CreateBooking(List<Booking> bookings, out string message)
        {
            string seats = string.Empty;

            if (bookings.Count > 4)
            {
                message = "Only maximum of 4 bookings allowed per person";
                return false;
            }

            foreach(Booking booking in bookings)
            {
                Booking booking1 = Bookings.Where(b => b.ID.Equals(booking.ID)).FirstOrDefault();

                if (booking1.Status != BookingStatus.Booked)
                {
                    booking1.ID = booking.ID;
                    booking1.Name = booking.Name;
                    booking1.Email = booking.Email;
                    booking1.Status = BookingStatus.Booked;
                }
                else
                {
                    seats += booking.ID + ", ";
                }
            }

            if (!string.IsNullOrEmpty(seats))
            {
                message = "Seats " + seats + "are booked. Please select other seats.";
                return false;
            }

            message = "Booking successfully made";
            return true;
        }

        public List<Booking> GetAllSeats()
        {
            return Bookings;
        }

        public List<Booking> GetAvailableSeats()
        {
            return Bookings.Where(b => b.Status == BookingStatus.Available).ToList();
        }

        public List<Booking> GetBookedSeats()
        {
            return Bookings.Where(b => b.Status == BookingStatus.Booked).ToList();
        }

        public Booking GetBookingByID(string ID)
        {
            return Bookings.Where(b => b.ID.Equals(ID)).FirstOrDefault();
        }

        public bool UpdateBooking(Booking booking)
        {
            Booking booking1 = Bookings.Where(b => b.ID.Equals(booking.ID)).FirstOrDefault();
            booking1.ID = booking.ID;
            booking1.Name = booking.Name;
            booking1.Email = booking.Email;
            booking1.Status = BookingStatus.Booked;

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Bookings.Clear();
                }
            }
            this.disposed = true;
        }
    }
}
