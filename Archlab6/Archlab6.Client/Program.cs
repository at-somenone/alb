using System;
using Archlab6.Client.FlightService;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archlab6.Client
{
    class Program
    {
        private static Flight[] flights;
        private static FlightServiceClient client;
        private static string filterFrom = "";
        private static string filterTo = "";
        static void Main(string[] args)
        {
            client = new FlightServiceClient();
            RefreshFlightList();
            var stop = false;
            while (!stop)
            {
                Console.Clear();
                var filteredFlights = from f in flights
                                      where f.From.ToLower().Contains(filterFrom.ToLower())
                                         && f.To.ToLower().Contains(filterTo.ToLower())
                                      select f;
                foreach (var f in filteredFlights)
                {
                    Console.WriteLine($"№{f.ID} {f.From}-{f.To} at [{f.Departure}] - [{f.Arrival}]; {f.TicketsLeft} tickets");
                }

                Console.WriteLine();
                Console.WriteLine("1 - book a flight;");
                Console.WriteLine("2 - filter list;");
                Console.WriteLine("3 - refresh;");
                Console.WriteLine("4 - quit;");
                Console.WriteLine("Enter option:");

                var selection = Console.ReadLine();
                switch (selection)
                {
                    case "1":
                        BookFlight();
                        break;
                    case "2":
                        SetFilter();
                        break;
                    case "3":
                        RefreshFlightList();
                        break;
                    case "4":
                        stop = true;
                        break;
                }
            }

            client.Close();
        }

        private static void BookFlight()
        {
            Console.WriteLine("Input flight number: ");
            int fID;
            while (!int.TryParse(Console.ReadLine(), out fID))
            {
                Console.WriteLine("Invalid number, try again..");
            }

            if (!client.BookFlight(fID))
            {
                Console.WriteLine("Failed to book flight, refresh the list and try again..");
                Console.ReadKey();
            }

            RefreshFlightList();
        }

        private static void SetFilter()
        {
            Console.Write("From: ");
            filterFrom = Console.ReadLine();
            Console.Write("To: ");
            filterTo = Console.ReadLine();
        }

        private static void RefreshFlightList()
        {
            flights = client.GetFlights();
        }
    }
}
