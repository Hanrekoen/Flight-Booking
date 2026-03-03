using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Fact]
        public void Books_flights()
        {
            var entities = new Entities();
            
            var bookingService = new BookingService();

            bookingService.Book(new BookDto(
                flightId: Guid.NewGuid(), passengerEmail: "a@b.com", numberOfSeats: 2));

            bookingService.FindBookings().Should().ContainEquivalentOf(
                new BookingRm(passengerEmail: "a@b.com", numberOfSeats: 2)
                );
        }
    }

    public class BookingService
    {
        public void Book(BookDto bookDto)
        {

        }
        public IEnumerable<BookingRm> FindBookings()
        {
            return new[]
            {
                new BookingRm(passengerEmail: "a@b.com", numberOfSeats: 2)
            };
        }
    }
    public class BookDto //Data transfer object
    {
        public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
        {
            
        }
    }

    public class BookingRm //Read model
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }
        public BookingRm(string passengerEmail, int numberOfSeats)
        {
            this.PassengerEmail = passengerEmail;
            this.NumberOfSeats = numberOfSeats;
        }
    }

}