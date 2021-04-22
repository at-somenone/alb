using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Archlab2.Common;

namespace Archlab2.Server
{
    // todo: IServer interface, also unit tests? how would you unit test a server?
    // also what was the interface even supposed to be for ???
    public class Server
    {
        private List<ClientConnection> connections;
        private bool started;
        private IPAddress ip;
        private int port;

        public Server(IPAddress ip, int port) {
            this.ip = ip;
            this.port = port;
            connections = new();
        }

        public void Start() {
            if (started)
                throw new InvalidOperationException("The server is already started");

            started = true;
            var endpoint = new IPEndPoint(ip, port);
            var listener = new TcpListener(endpoint);
            listener.Start();
            Console.WriteLine("Server started! Listening for clients...");

            while (true) {
                var tcpClient = listener.AcceptTcpClient();
                var clientConnection = new ClientConnection();
                clientConnection.RequestReceived += HandleRequestReceived;
                clientConnection.ConnectionLost += HandleClientLostConnection;
                lock (connections) {
                    connections.Add(clientConnection);
                }

                clientConnection.Start(tcpClient);
            }
        }

        private void HandleClientLostConnection(object sender, EventArgs e) {
            lock (connections) {
                var client = (ClientConnection) sender;
                connections.Remove(client);
                Broadcast("USERLEFT", client.Name);
            }
        }

        private void HandleRequestReceived(object sender, RequestReceivedEventArgs eventArgs) {
            // todo: handle  b a d  messages
            var type = eventArgs.Type;
            var content = eventArgs.Content;
            var client = (ClientConnection) sender;
            lock (connections) {
                switch (type) {
                    case "SETNAME": {
                        if (string.IsNullOrWhiteSpace(content)
                         || content.Any(char.IsWhiteSpace)
                         || connections.Any(c => c.Name == content)
                         || content.Length > 32) {
                            break;
                        }

                        if (string.IsNullOrEmpty(client.Name)) {
                            Broadcast("USERJOINED", eventArgs.Content);
                            client.Send("YOULOGGEDIN");

                            Console.WriteLine($"{eventArgs.Content} has joined");
                        }
                        else {
                            Broadcast("USERRENAMED", $"{client.Name}\n{eventArgs.Content}");
                            Console.WriteLine($"{client.Name} has changed their name to {eventArgs.Content}.");
                        }

                        client.Name = content;
                        break;
                    }
                    case "SENDMESSAGE": {
                        if (string.IsNullOrEmpty(client.Name)
                         || content.Length > 512) {
                            break;
                        }

                        var name = client.Name;
                        Console.WriteLine($"{name}: {content}");
                        Broadcast("NEWMESSAGE", $"{name}: {content}");
                        break;
                    }
                    case "GETUSERS": {
                        var usernames = string.Join('\n', connections
                                                          .Where(c=>!string.IsNullOrEmpty(c.Name))
                                                          .Select(c => c.Name));

                        client.Send("SETUSERS", usernames);
                        break;
                    }
                }
            }
        }

        public void Broadcast(string command, string message) {
            lock (connections)
                connections.ForEach(c => c.Send(command, message));
        }
    }
}
