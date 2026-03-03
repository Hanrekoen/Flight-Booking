using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Tests;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Theory]
        [InlineData("M@m.com", 2)]
        [InlineData("a@a.com", 1)]
        public void Books_flights(string passengerEmail, int numberOfSeats)
        {
            var entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);
            
            var flight = new Flight(3);
            entities.Flights.Add(flight);
            
            var bookingService = new BookingService(entities: entities);

            bookingService.Book(new BookDto(
                flightId: flight.Id, passengerEmail, numberOfSeats));

            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail, numberOfSeats)
                );
        }
    }

}


namespace Application.Tests
{

}