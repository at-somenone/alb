using System;

namespace Archlab2.Common
{
    public class RequestReceivedEventArgs: EventArgs
    {
        public string Type { get; }
        public string Content { get; }

        public RequestReceivedEventArgs(string type, string content) {
            Content = content;
            Type = type;
        }
    }
}
