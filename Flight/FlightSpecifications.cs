using Domain;
using FluentAssertions;
using System.ComponentModel;

namespace FlightTest
{
    public class FlightSpecifications
    {
        [Theory]
        [InlineData(3,1,2)]
        [InlineData(6,3,3)]
        [InlineData(10,6,4)]
        [InlineData(12,8,4)]
        public void Booking_Reduces_Number_of_Seats(int seatCapacity, int numberOfseats, int remainingNumberOfSeats)
        {
            var flight = new Flight(seatCapacity: seatCapacity);

            flight.Book("Hanre@gmail.com", numberOfseats);

            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }
        [Fact]
        public void Avoids_Overbooking()
        {
            //Given
            var flight = new Flight(seatCapacity: 3);

            //When
            var error = flight.Book("Micks@gmail.com",4);

            //Then
            error.Should().BeOfType<OverBookingErrors>();
        }

        [Fact]
        public void Books_flights_succesfully()
        {
            var flight = new Flight(seatCapacity: 3);
            
            var error = flight.Book("Micks@gmail.com", 1);
            error.Should().BeNull();
        }

        [Fact]
        public void Remembers_bookings()
        {
            var flight = new Flight(seatCapacity: 150);

            flight.Book(passengerEmail: "a@b.com", NumberOfSeats: 4);

            flight.BookingList.Should().ContainEquivalentOf(new Booking("a@b.com", 4));
        }

        [Theory]
        [InlineData(3,1,1,3)]
        [InlineData(4,2,2,4)]
        [InlineData(7,5,4,6)]
        public void Cancel_Bookings_free_up_the_seats(
            int initialCapacity,
            int numberofSeatstoBook,
            int numberOfSeatsToBook,
            int remainingNumberOfSeats)
        {
            //Given
            var flight = new Flight(initialCapacity);
            flight.Book(passengerEmail: "a@b.com", NumberOfSeats: numberofSeatstoBook);

            //when
            flight.CancelBooking(passengerEmail: "a@b.com", NumberOfSeats: numberOfSeatsToBook);

            //then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Fact]
        public void Doesnt_cancel_bookings_for_passengers_who_havent_booked()
        {
            var flight = new Flight(3);

            var error = flight.CancelBooking(passengerEmail: "a@b.com", NumberOfSeats: 2);

            error.Should().BeOfType<BookingNotFoundError>();

        }

        [Fact]
        public void Returns_null_when_succesfully_cancels_a_booking()
        {
            var flight = new Flight(3);
            flight.Book(passengerEmail: "a@b.com", NumberOfSeats: 1);
            var error = flight.CancelBooking(passengerEmail: "a@b.com", NumberOfSeats: 1);
            error.Should().BeNull();
        }
    }
}