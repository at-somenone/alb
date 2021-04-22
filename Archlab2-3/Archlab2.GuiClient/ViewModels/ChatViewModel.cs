using System;
using System.Windows.Input;

namespace Archlab2.GuiClient
{
    public class ChatViewModel: BaseViewModel
    {
        private ChatModel model;

        private string _messages;

        public string Messages {
            get => _messages;
            set {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        private string messageString;

        public string MessageString {
            get => messageString;
            set {
                messageString = value;
                OnPropertyChanged(MessageString);
            }
        } // TODO
        // i forgot what the todo is for

        public ChatViewModel(ChatModel model) {
            this.model = model;
            model.MessageReceived += MessageReceivedHandler;
            model.ConnectionStateChanged += HandleConnectionStateChanged;
        }

        private void HandleConnectionStateChanged(object sender, EventArgs e) {
            if (model.ConnectionState is ConnectionState.Connected)
                Messages = "";

            Messages += model.ConnectionState switch {
                ConnectionState.Disconnected => "[ disconnected ]\n",
                ConnectionState.Connected    => "[ connected ]\n",
                ConnectionState.LoggedIn     => "[ logged in ]\n",
                ConnectionState.Errored      => "[ connection error ]\n",
                _                            => throw new ArgumentOutOfRangeException()
            };
        }

        private void MessageReceivedHandler(object sender, MessageReceivedEventArgs eventArgs) {
            Messages += eventArgs.Message + '\n';
        }


        private RelayCommand cmdSend;

        public ICommand CmdSendMessage {
            get {
                cmdSend ??= new(_ => Send(), _ => CanSend());
                return cmdSend;
            }
        }

        private bool CanSend() {
            return MessageString is { Length: > 0 and <= 512 }
                && model.ConnectionState is ConnectionState.LoggedIn;
        }

        private void Send() {
            var t = MessageString.Trim();
            model.Send(t);
            MessageString = "";
        }
    }
}
