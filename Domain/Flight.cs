namespace Domain
{
    public class Flight
    {
        public int RemainingNumberOfSeats { get; set; }
        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }
        public object? Book(string vpassengerEmail, int NumberOfSeats)
        {
            if(NumberOfSeats > this.RemainingNumberOfSeats)
                return new OverBookingErrors();

            RemainingNumberOfSeats -= NumberOfSeats;
            return null;
        }


    }
}
