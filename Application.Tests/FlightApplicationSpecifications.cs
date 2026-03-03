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
        readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);

        readonly BookingService bookingService;

        public FlightApplicationSpecifications()
        {
            bookingService = new BookingService(entities: entities);
        }


        [Theory]
        [InlineData("m@m.com", 2)]
        [InlineData("a@a.com", 2)]
        public void Books_flights(string passengerEmail, int numberOfSeats)
        {
                        
            var flight = new Flight(3);

            entities.Flights.Add(flight);
            
            bookingService.Book(new BookDto(
                flightId: flight.Id, passengerEmail, numberOfSeats));

            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail, numberOfSeats)
                );
        }

        [Fact]
        public void Cancels_booking()
        {
            //Given
            
            var flight = new Flight(3);
            entities.Flights.Add(flight);


            bookingService.Book(new BookDto(flightId: flight.Id, 
                passengerEmail: "m@m.com",
                numberOfSeats: 2));
            //when
            bookingService.CancelBooking(
                new CancelBookingDto(flightId: Guid.NewGuid(),
                    passengerEmail: "m@m.com",
                    numberOfSeats: 2)
                );
            //then
            bookingService.GetRemainingNumberOfSeatsFor(flight.Id).Should().Be(3);
        }
    }

}
