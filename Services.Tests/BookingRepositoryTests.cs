﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Models;
using Services.Repositories;

namespace BookingsAPI.Tests
{
    [TestClass]
    public class BookingRepositoryTests
    {
        IBookingRepository bookingRepository;

        [TestInitialize]
        public void Setup()
        {
            bookingRepository = new BookingRepository();
        }

        [TestMethod]
        public void GetSeats()
        {
            List<Booking> bookings = bookingRepository.GetAllSeats();

            Assert.IsNotNull(bookings);
            Assert.AreEqual(100, bookings.Count);
        }

        [TestMethod]
        public void GetAvailableSeats()
        {
            List<Booking> bookings = bookingRepository.GetAvailableSeats();

            Assert.AreEqual(100, bookings.Count);

            Booking booking = new Booking() {
                ID = "A1",
                Name = "test",
                Email = "test@test.com"
            };

            var success = bookingRepository.CreateBooking(booking);
            Assert.IsTrue(success);

            Booking booking1 = bookingRepository.GetBookingByID("A1");
            Assert.AreEqual(BookingStatus.Booked, booking1.Status);

            bookings = bookingRepository.GetAvailableSeats();
            Assert.AreEqual(99, bookings.Count);

            bookings = bookingRepository.GetBookedSeats();
            Assert.AreEqual(1, bookings.Count);

            booking.Email = "test1@test.com";
            success = bookingRepository.UpdateBooking(booking);
            Assert.IsTrue(success);

            booking1 = bookingRepository.GetBookingByID("A1");
            Assert.AreEqual(booking.Email, booking1.Email);

            success = bookingRepository.CancelBooking(booking);
            Assert.IsTrue(success);

            bookings = bookingRepository.GetAvailableSeats();
            Assert.AreEqual(100, bookings.Count);
        }

        [TestMethod]
        public void CreateBooking()
        {
            List<Booking> seats = new List<Booking>();
            for(int i = 1; i<= 4; i++)
            {
                Booking booking = new Booking()
                {
                    ID = "A" + i.ToString(),
                    Name = "test",
                    Email = "test@test.com"
                };
                seats.Add(booking);
            }

            string message;
            var success = bookingRepository.CreateBooking(seats, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("Booking successfully made", message);

            seats.Add(new Booking() {
                ID = "A5",
                Name = "test",
                Email = "test@test.com"
            });
            success = bookingRepository.CreateBooking(seats, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Only maximum of 4 bookings allowed per person", message);

            seats = new List<Booking>();
            for (int i = 1; i <= 4; i++)
            {
                Booking booking = new Booking()
                {
                    ID = "A" + i.ToString(),
                    Name = "test",
                    Email = "test@test.com"
                };
                seats.Add(booking);
            }

            success = bookingRepository.CreateBooking(seats, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Seats A1, A2, A3, A4, are booked. Please select other seats.", message);
        }
    }
}
