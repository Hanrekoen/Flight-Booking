# Flight Booking

A flight booking domain built **test-first** in C#, organised into separate domain, application and data layers with their own test projects.

---

## Structure

```
Domain/             Flight, Booking, and the error types    - business rules, no dependencies
Application/        BookingService, DTOs and read models    - orchestrates the domain
Data/               Entities                                - persistence
Domain.Tests/       domain-level specifications
Application.Tests/  FlightApplicationSpecifications
Flight/             FlightSpecifications
```

`Domain` depends on nothing, so the booking rules can be tested without a database or a web server.

---

## The rules it enforces

- A flight has a seat capacity, and booking reduces the seats remaining.
- **Overbooking is refused** — booking more seats than remain returns an `OverBookingErrors` result instead of throwing.
- Cancelling a booking frees seats back up.
- **Cancelling for a passenger who never booked** returns a `BookingNotFoundError`.

Errors are returned as result objects rather than thrown as exceptions, so callers handle failure as a normal outcome instead of relying on try/catch.

---

## Tests

Tests are named as specifications, so the test output reads as a description of the rules:

```
Books_flights_succesfully
Booking_Reduces_Number_of_Seats
Avoids_Overbooking
Remembers_bookings
Cancel_Bookings_free_up_the_seats
Doesnt_cancel_bookings_for_passengers_who_havent_booked
Returns_null_when_succesfully_cancels_a_booking
```

`[Theory]` is used where a rule should hold across several inputs, `[Fact]` where it is a single case.

---

## Running the tests

```bash
dotnet test
```

---

## Built with

C# · .NET · xUnit · layered architecture · Entity Framework

---

## Author

**Hanré Koen** — [@Hanrekoen](https://github.com/Hanrekoen)
