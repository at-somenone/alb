using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Archlab2.Common
{
    public class Communicator: IDisposable
    {
        private Thread processThread;
        private TcpClient tcpClient;
        private string nextMessage;
        private byte[] buffer = new byte[128];

        public delegate void RequestReceivedHandler(object sender, RequestReceivedEventArgs eventArgs);
        public event RequestReceivedHandler RequestReceived;
        public event EventHandler ConnectionLost;

        public void Start(TcpClient tcpClient) {
            if (this.tcpClient is not null || processThread is not null)
                throw new InvalidOperationException("Communicator is already started");

            if (!tcpClient.Connected)
                throw new ArgumentException("The TcpClient must be already connected.");

            this.tcpClient = tcpClient;
            processThread = new(Process) {
                IsBackground = true,
                Name = "CommunicatorProcess"
            };

            processThread.Start();
        }

        public void Process() {
            Console.WriteLine("Communicator: started processing");
            while (true) {
                try {
                    RetrieveRequest();
                }
                catch (Exception e) {
                    Console.WriteLine("Communicator: error! stopping..");
                    Console.WriteLine(e);
                    OnConnectionLost();
                    break;
                }
            }
        }

        public void Stop() {
            tcpClient.Close();
            tcpClient = null;
            processThread = null;
        }

        public void Send(string command, string data = "") {
            if (tcpClient is null)
                throw new InvalidOperationException($"{nameof(tcpClient)} must not be null");

            var escapedData = data.Replace("\n.\n", "\n\\.\n");
            var d = Encoding.UTF8.GetBytes($"{command}\n{escapedData}\n.\n");
            tcpClient.GetStream().Write(d);
        }

        private void RetrieveRequest() {
            var stream = tcpClient.GetStream();
            buffer.Initialize();
            do {
                var byteCount = stream.Read(buffer, 0, buffer.Length);
                if (byteCount == 0)
                    throw new IOException("The connection has been lost..");

                nextMessage += Encoding.UTF8.GetString(buffer, 0, byteCount);
                if (nextMessage.Length > 1024) {
                    Stop();
                    break;
                }
            } while (stream.DataAvailable);

            while (nextMessage.Contains("\n.\n")) {
                var both = nextMessage.Split("\n.\n", 2);
                var left = both[0];
                var right = both[1];
                var unescapedData = left.Replace("\n\\.\n", "\n.\n");
                OnRequestReceived(new(unescapedData));
                Console.WriteLine($"Communicator: retrieved message [{left}], with [{right}] left over");
                nextMessage = right;
            }
        }

        protected void OnRequestReceived(string request) {
            var splitResult = request.Split('\n', 2);
            var type = splitResult[0];
            var content = "";

            if (splitResult.Length > 1)
                content = splitResult[1];

            var eventArgs = new RequestReceivedEventArgs(type, content);

            RequestReceived?.Invoke(this, eventArgs);
        }

        protected void OnConnectionLost() {
            Console.WriteLine("connection lost yeah");
            ConnectionLost?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose() {
            Console.WriteLine("DISPOSINGGGGGGGGGG");
            tcpClient?.Dispose();
        }
    }
}
