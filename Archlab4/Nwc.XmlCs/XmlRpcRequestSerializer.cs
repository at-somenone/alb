using System.Xml;

namespace Nwc.XmlRpc
{
    /// <summary>Class responsible for serializing an XML-RPC request.</summary>
    /// <remarks>
    ///     This class handles the request envelope, depending on <c>XmlRpcSerializer</c>
    ///     to serialize the payload.
    /// </remarks>
    /// <seealso cref="XmlRpcSerializer" />
    public class XmlRpcRequestSerializer: XmlRpcSerializer
    {
        private static XmlRpcRequestSerializer _singleton;

        /// <summary>A static singleton instance of this deserializer.</summary>
        public static XmlRpcRequestSerializer Singleton {
            get {
                if (_singleton == null)
                    _singleton = new();

                return _singleton;
            }
        }

        /// <summary>Serialize the <c>XmlRpcRequest</c> to the output stream.</summary>
        /// <param name="output">An <c>XmlTextWriter</c> stream to write data to.</param>
        /// <param name="obj">An <c>XmlRpcRequest</c> to serialize.</param>
        /// <seealso cref="XmlRpcRequest" />
        public override void Serialize(XmlTextWriter output, object obj) {
            var request = (XmlRpcRequest) obj;
            output.WriteStartDocument();
            output.WriteStartElement(METHOD_CALL);
            output.WriteElementString(METHOD_NAME, request.MethodName);
            output.WriteStartElement(PARAMS);
            foreach (var param in request.Params) {
                output.WriteStartElement(PARAM);
                output.WriteStartElement(VALUE);
                SerializeObject(output, param);
                output.WriteEndElement();
                output.WriteEndElement();
            }

            output.WriteEndElement();
            output.WriteEndElement();
        }
    }
}
