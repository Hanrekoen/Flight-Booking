namespace Domain
{
    public class Flight
    {
        public int RemainingNumberOfSeats { get; set; }
        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }
        public void Book(string vpassengerEmail, int NumberOfSeats)
        {
            RemainingNumberOfSeats -= NumberOfSeats;
        }
    }
}
