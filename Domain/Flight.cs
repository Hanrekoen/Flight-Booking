namespace Domain
{
    public class Flight
    {
        public List<Booking> BookingList { get; set; } = new List<Booking>();
        public int RemainingNumberOfSeats { get; set; }
        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }
        public object? Book(string passengerEmail, int NumberOfSeats)
        {
            if(NumberOfSeats > this.RemainingNumberOfSeats)
                return new OverBookingErrors();

            RemainingNumberOfSeats -= NumberOfSeats;
            
            BookingList.Add(new Booking(passengerEmail, NumberOfSeats));

            return null;
        }


    }
}
