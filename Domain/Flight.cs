
namespace Domain
{
    public class Flight
    {
        List<Booking> bookingList = new();
        public IEnumerable<Booking> BookingList => bookingList;

        public int RemainingNumberOfSeats { get; set; }

        public Guid Id { get; }
        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }
        public object? Book(string passengerEmail, int NumberOfSeats)
        {
            if(NumberOfSeats > this.RemainingNumberOfSeats)
                return new OverBookingErrors();

            RemainingNumberOfSeats -= NumberOfSeats;

            bookingList.Add(new Booking(passengerEmail, NumberOfSeats));

            return null;
        }

        public object? CancelBooking(string passengerEmail, int NumberOfSeats)
        {
            if(!bookingList.Any(b => b.Email == passengerEmail))
                return new BookingNotFoundError();

            RemainingNumberOfSeats += NumberOfSeats;
            return null;
        }
    }
}
