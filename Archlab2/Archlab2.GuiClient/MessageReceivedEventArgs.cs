using System;

namespace Archlab2.GuiClient
{
    public class MessageReceivedEventArgs: EventArgs
    {
        public string Message { get; }

        public MessageReceivedEventArgs(string message) {
            Message = message;
        }
    }
}
