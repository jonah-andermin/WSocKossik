using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;

namespace WSocKossik
{
    //see: https://docs.microsoft.com/en-us/dotnet/api/system.net.websockets.websocket?view=net-6.0
    //see: https://github.com/microsoft/referencesource/blob/master/System/net/System/Net/WebSockets/ClientWebSocket.cs
    //see: https://datatracker.ietf.org/doc/html/rfc6455
    //see: https://html.spec.whatwg.org/multipage/web-sockets.html#the-websocket-interface
    class WSocKossik : WebSocket
    {
        #region member variables
        private readonly ClientWebSocketOptions options;
        private readonly CancellationTokenSource cts;
        private WebSocketCloseStatus? closeStatus;
        private string closeStatusDescription;
        private string subProtocol;
        private WebSocketState state;
        #endregion
        #region properties
        public override WebSocketCloseStatus? CloseStatus => closeStatus;

        public override string CloseStatusDescription => closeStatusDescription;

        public override string SubProtocol => subProtocol;

        public override WebSocketState State => state;
        #endregion
        #region Constructors
        static WSocKossik()
        {
            // Register ws: and wss: with WebRequest.Register so that WebRequest.Create returns a 
            // WebSocket capable HttpWebRequest instance.
            WebSocket.RegisterPrefixes();
        }

        public WSocKossik()
        {
            /*
            if (Logging.On) Logging.Enter(Logging.WebSockets, this, ".ctor", null);

            if (!WebSocketProtocolComponent.IsSupported)
            {
                WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
            }

            state = created;
            options = new ClientWebSocketOptions();
            cts = new CancellationTokenSource();

            if (Logging.On) Logging.Exit(Logging.WebSockets, this, ".ctor", null);
            */
        }
        #endregion

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
