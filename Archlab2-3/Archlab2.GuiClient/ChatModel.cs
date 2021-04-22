using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using Archlab2.Common;

namespace Archlab2.GuiClient
{
    public class ChatModel
    {
        public ConnectionState ConnectionState {
            get => connectionState;
            private set {
                connectionState = value;
                OnConnectionStateChanged();
            }
        }

        public List<string> usernames;
        private Communicator communicator;
        private ConnectionState connectionState;

        public delegate void MessageReceivedHandler(object sender, MessageReceivedEventArgs eventArgs);
        public event MessageReceivedHandler MessageReceived;
        public event EventHandler UserListChanged;
        public event EventHandler ConnectionStateChanged;


        public ChatModel() {
            usernames = new();
            ConnectionState = ConnectionState.Disconnected;
        }

        public void Connect(IPEndPoint remoteEP) {
            var tcpClient = new TcpClient();
            try {
                tcpClient.Connect(remoteEP);
            }
            catch (Exception e) {
                Console.WriteLine("Error while connecting to server");
                Console.WriteLine(e);
                ConnectionState = ConnectionState.Errored;
                return;
            }

            if (communicator is not null) {
                communicator.RequestReceived -= RequestReceivedHandler;
                communicator.ConnectionLost -= ConnectionLostHandler;
            }

            communicator = new();
            communicator.RequestReceived += RequestReceivedHandler;
            communicator.ConnectionLost += ConnectionLostHandler;
            communicator.Start(tcpClient);
            ConnectionState = ConnectionState.Connected;
            RetrieveUsers();
        }

        private void ConnectionLostHandler(object sender, EventArgs e) {
            ConnectionState = ConnectionState.Disconnected;
        }

        public void RetrieveUsers() {
            communicator.Send("GETUSERS");
        }

        public void Connect(string ipString, int port) {
            var ip = IPAddress.Parse(ipString);
            var remoteEP = new IPEndPoint(ip, port);
            Connect(remoteEP);
        }

        private void RequestReceivedHandler(object sender, RequestReceivedEventArgs eventArgs) {
            Application.Current.Dispatcher.Invoke(() => {
                switch (eventArgs.Type) {
                    case "NEWMESSAGE": {
                        OnMessageReceived(new(eventArgs.Content));
                        break;
                    }
                    case "SETUSERS": {
                        var users = eventArgs.Content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        usernames = users.ToList();
                        OnUserListChanged();
                        break;
                    }
                    case "USERJOINED": {
                        Console.WriteLine("yeah");
                        var username = eventArgs.Content;
                        usernames.Add(username);
                        OnUserListChanged();
                        break;
                    }

                    case "USERLEFT": {
                        var username = eventArgs.Content;
                        usernames.Remove(username);
                        OnUserListChanged();
                        break;
                    }

                    case "YOULOGGEDIN": {
                        ConnectionState = ConnectionState.LoggedIn;
                        break;
                    }
                }
            });
        }

        public void Send(string message) {
            communicator.Send("SENDMESSAGE", message);
        }

        public void Disconnect() {
            communicator?.Stop();
        }

        public void RenameUser(string username) {
            communicator.Send("SETNAME", username);
        }

        protected void OnMessageReceived(MessageReceivedEventArgs eventArgs) {
            MessageReceived?.Invoke(this, eventArgs);
        }

        protected virtual void OnConnectionStateChanged() {
            ConnectionStateChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUserListChanged() {
            Console.WriteLine("changeeeeee");
            UserListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
