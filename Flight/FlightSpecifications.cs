using Domain;
using FluentAssertions;

namespace FlightTest
{
    public class FlightSpecifications
    {
        [Fact]
        public void Booking_Reduces_Number_of_Seats()
        {
            var flight = new Flight(seatCapacity: 3);

            flight.Book("Hanre@gmail.com", 1);

            flight.RemainingNumberOfSeats.Should().Be(2);
        }

        public void Avoids_Overbooking()
        {
            //Given
            var flight = new Flight(seatCapacity: 3);

            //When
            var error = flight.Book("Micks@gmail.com",4);

            //Then
            error.Should().BeOfType<OverBookingErrors>();
        }
    }
}