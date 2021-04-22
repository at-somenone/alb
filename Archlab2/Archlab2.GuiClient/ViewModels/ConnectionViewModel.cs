using System;
using System.Linq;
using System.Windows.Input;

namespace Archlab2.GuiClient
{
    public enum ConnectionState
    {
        Disconnected,
        Connected,
        LoggedIn,
        Errored
    }

    public class ConnectionViewModel: BaseViewModel
    {
        private ChatModel model;


        public ConnectionState ConnectionState => model.ConnectionState;

        private string ipString;

        public string IPString {
            get => ipString;
            set {
                ipString = value;
                OnPropertyChanged(nameof(IPString));
            }
        }

        private string portString;

        public string PortString {
            get => portString;
            set {
                portString = value;
                OnPropertyChanged(nameof(PortString));
            }
        }

        public ConnectionViewModel(ChatModel model) {
            this.model = model;
            ipString = "127.0.0.1";
            portString = "8005";
            model.ConnectionStateChanged += HandleConnectionStateChanged;
        }

        private RelayCommand cmdConnect;

        public RelayCommand CmdConnect {
            get {
                cmdConnect ??= new(_ => Connect(), _ => CanConnect());
                return cmdConnect;
            }
        }

        private bool CanConnect() {
            return ValidateIP(IPString) && ushort.TryParse(PortString, out _);
        }

        private bool ValidateIP(string address) {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            var splitValues = address.Split('.');
            if (splitValues.Length != 4)
                return false;

            return splitValues.All(s => byte.TryParse(s, out _));
        }

        private void Connect() {
            model.Connect(IPString, int.Parse(PortString));
            OnPropertyChanged(nameof(ConnectionState));
        }


        private RelayCommand cmdDisconnect;

        public ICommand CmdDisconnect {
            get {
                cmdDisconnect ??= new(_ => Disconnect(), _ => CanDisconnect());
                return cmdDisconnect;
            }
        }

        private bool CanDisconnect() {
            return ConnectionState is ConnectionState.Connected or ConnectionState.LoggedIn;
        }

        private void Disconnect() {
            model.Disconnect();
        }

        private void HandleConnectionStateChanged(object sender, EventArgs e) {
            OnPropertyChanged(nameof(ConnectionState));
        }
    }
}
