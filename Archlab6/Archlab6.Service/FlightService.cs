using System;
using System.Collections.Generic;
using System.Linq;

namespace Archlab6.FlightService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class FlightService: IFlightService
    {
        private List<Flight> flights;

        public FlightService() {
            flights = new List<Flight> {
                new Flight(
                    0,
                    "Moscow",
                    "Sochi",
                    new DateTime(1234, 5, 6, 12, 5, 0),
                    new DateTime(2134, 6, 5, 00, 52, 10),
                    10
                ),
                new Flight(
                    1,
                    "St. Petersburg",
                    "Kaliningrad",
                    new DateTime(1534, 2, 14, 12, 05, 0),
                    new DateTime(1634, 8, 2, 00, 52, 10),
                    11
                ),
                new Flight(
                    2,
                    "St. Petersburg",
                    "Anapa",
                    new DateTime(1234, 3, 23, 12, 05, 0),
                    new DateTime(1739, 12, 5, 00, 52, 10),
                    69
                ),
                new Flight(
                    3,
                    "Moscow",
                    "St. Petersburg",
                    new DateTime(2500, 3, 29, 12, 05, 0),
                    new DateTime(5200, 11, 1),
                    3
                ),
                new Flight(
                    4,
                    "Kaliningrad",
                    "Anapa",
                    new DateTime(2021, 4, 26, 12, 05, 0),
                    new DateTime(2021, 6, 26, 00, 52, 10),
                    9
                )
            };
        }

        public int Testy()
        {
            Console.WriteLine("yeah");
            return 5;
        }

        public Flight[] GetFlights() {
            Console.WriteLine("yeah");
            return flights.ToArray();
        }

        public Flight FTesty()
        {
            return new Flight(0, "h", "h", DateTime.Now, DateTime.Now, 1);
        }

        public bool BookFlight(int flightID) {
            var flight = flights.FirstOrDefault(f => f.ID == flightID);
            if (flight is null || flight.TicketsLeft <= 0)
                return false;

            flight.TicketsLeft -= 1;
            return true;
        }

        public int[] Testy2()
        {
            return new int[] { 1, 2, 3 };
        }
    }
}
