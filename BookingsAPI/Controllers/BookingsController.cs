using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingsAPI.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;

        public BookingsController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IEnumerable<Booking> GetAllSeats()
        {
            return bookingRepository.GetAllSeats();
        }

        [HttpGet]
        public IEnumerable<Booking> GetAvailableSeats()
        {
            return bookingRepository.GetAvailableSeats();
        }

        [HttpGet]
        public IEnumerable<Booking> GetBookedSeats()
        {
            return bookingRepository.GetBookedSeats();
        }

        [HttpGet("{id}")]
        public Booking GetBooking(string id)
        {
            return bookingRepository.GetBookingByID(id);
        }

        [HttpPost]
        public bool CreateBooking([FromBody] Booking value)
        {
            return bookingRepository.CreateBooking(value);
        }

        [HttpPost]
        public bool CreateBooking([FromBody] List<Booking> value, out string message)
        {
            return bookingRepository.CreateBooking(value, out message);
        }

        [HttpPut]
        public bool UpdateBooking([FromBody] Booking value)
        {
            return bookingRepository.UpdateBooking(value);
        }

        [HttpDelete]
        public bool Delete([FromBody] Booking value)
        {
            return bookingRepository.CancelBooking(value);
        }
    }
}
