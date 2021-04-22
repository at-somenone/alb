using System;
using System.Net;

namespace Archlab2.Server
{
    class Program
    {
        private static void Main(string[] args) {
            if (args.Length < 2) {
                Console.WriteLine("You need to provide an IP address and a port.");
                return;
            }

            if (!IPAddress.TryParse(args[0], out IPAddress ip)) {
                Console.WriteLine("Invalid IP address.");
                return;
            }

            if (!ushort.TryParse(args[1], out ushort port)) {
                Console.WriteLine("Invalid port.");
                return;
            }

            var server = new Server(ip, port);
            server.Start();
        }
    }
}

