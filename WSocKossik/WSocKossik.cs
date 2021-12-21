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
    class WSocKossik : WebSocket
    {
        private WebSocketCloseStatus? closeStatus;
        private string closeStatusDescription;
        private string subProtocol;
        private WebSocketState state;

        public override WebSocketCloseStatus? CloseStatus => closeStatus;

        public override string CloseStatusDescription => closeStatusDescription;

        public override string SubProtocol => subProtocol;

        public override WebSocketState State => state;

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
