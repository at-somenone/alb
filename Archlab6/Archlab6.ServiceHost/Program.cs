using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Archlab6.FlightService;

namespace Archlab6.FlightServiceHost
{
    class Program
    {
        private static void Main() {
            var baseAddress = new Uri("http://localhost:8000/Archlab6.FlightService");
            var host = new ServiceHost(typeof(FlightService.FlightService), baseAddress);

            // Step 3: Add a service endpoint.
            host.AddServiceEndpoint(typeof(IFlightService), new WSHttpBinding(), "FlightService");

            // Step 4: Enable metadata exchange.
            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.HttpsGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            // Step 5: Start the service.
            host.Open();
            Console.WriteLine("The service is ready.");

            // Close the ServiceHost to stop the service.
            Console.WriteLine("Press <Enter> to terminate the service.");
            Console.WriteLine();
            Console.ReadLine();
            host.Close();
            
        }
    }
}
