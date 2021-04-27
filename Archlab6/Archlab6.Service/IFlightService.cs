using System.Collections.Generic;
using System.ServiceModel;

namespace Archlab6.FlightService
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IFlightService
    {
        [OperationContract]
        Flight[] GetFlights();

        [OperationContract]
        bool BookFlight(int flightID);
    }
}
