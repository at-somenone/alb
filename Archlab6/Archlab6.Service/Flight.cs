using System;
using System.Runtime.Serialization;

namespace Archlab6.FlightService
{
    [DataContract]
    public class Flight
    {
        [DataMember] public int ID { get; set; }
        [DataMember] public string From { get; set; }
        [DataMember] public string To { get; set; }
        [DataMember] public DateTime Departure { get; set; }
        [DataMember] public DateTime Arrival { get; set; }
        [DataMember] public int TicketsLeft { get; set; }

        public Flight(int id, string from, string to, DateTime departure, DateTime arrival, int ticketsLeft) {
            ID = id;
            From = from;
            To = to;
            Departure = departure;
            Arrival = arrival;
            TicketsLeft = ticketsLeft;
        }
    }
}
