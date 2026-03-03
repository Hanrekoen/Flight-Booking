using Domain;
using FluentAssertions;

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
    }
}