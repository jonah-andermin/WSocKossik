using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace WSocKossik
{
    //see: https://docs.microsoft.com/en-us/dotnet/api/system.net.websockets.websocket?view=net-6.0
    //see: https://github.com/microsoft/referencesource/blob/master/System/net/System/Net/WebSockets/ClientWebSocket.cs
    //see: https://datatracker.ietf.org/doc/html/rfc6455
    //see: https://html.spec.whatwg.org/multipage/web-sockets.html#the-websocket-interface

    enum KState
    {
        created = 0,
        connecting = 1,
        connected = 2,
        disposed = 3
    }

    public class WSocKossik : WebSocket
    {
        #region member variables
        private readonly ClientWebSocketOptions options;
        private readonly CancellationTokenSource cts;
        private WebSocketCloseStatus? closeStatus;
        private string closeStatusDescription;
        private string subProtocol;
        private WebSocketState state;
        private KState kState;
        private TcpListener listener;
        private TcpClient client;
        private Thread tcpThread;
        private bool AbortClient = false;
        private object stateLock = new object(); 
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

        public WSocKossik(string ipAddress = null, int listenPort = 80, int clientPort = 80)
        {
            IPAddress addr;
            try
            {
                addr = IPAddress.Parse(ipAddress);
            }
            catch (FormatException)
            {
                addr = IPAddress.Parse(Dns.GetHostAddresses(Environment.MachineName)[0].ToString());
            }

            kState = KState.created;
            state = WebSocketState.None;
            //options = new ClientWebSocketOptions();
            cts = new CancellationTokenSource();
            
            
                
            listener = new TcpListener(addr, listenPort);
            tcpThread = new Thread(AcceptClient);
        }
        #endregion

        public void Listen()
        {
            tcpThread.Start();
        }

        private void AcceptClient()
        {
            lock (stateLock)
            {
                if (this.kState > KState.created)
                {
                    return;
                }
                this.kState = KState.connecting;
            }
            
            client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            this.kState = KState.connected;
            Byte[] message = new byte[1024];
            Byte[] buffer;
            int messageSize = 0;
            int maxMessageSize = 1024;
            while (true)
            {
                if (this.AbortClient)
                {
                    closeStatusDescription = "Aborted Listener.";
                    goto DONELISTENING;
                }
                if (messageSize > 0 && CompletedMessage(message))
                buffer = new Byte[1024];
                while (!stream.DataAvailable)
                {
                    if (this.AbortClient)
                    {
                        closeStatusDescription = "Aborted Listener.";
                        goto DONELISTENING;
                    }
                }
                int available = client.Available;
                int bSize = (available > 1024) ? 1024 : available;
                Byte[] bytes = new Byte[bSize];
                int oldMessageSize = messageSize;
                messageSize += bSize;
                if(messageSize > maxMessageSize)
                {
                    maxMessageSize += 1024;
                    Byte[] newMessage = new Byte[maxMessageSize];
                    message.CopyTo(newMessage, 0);
                    message = newMessage;
                }
                try
                {
                    stream.Read(bytes, 0, bytes.Length);
                }
                catch(Exception)
                {
                    closeStatusDescription = "Read Error, did the stream close?";
                    goto DONELISTENING;
                }
                bytes.CopyTo(message, oldMessageSize);
            }
            DONELISTENING:
            return;
        }

        private bool CompletedMessage(Byte[] message)
        {
            throw new NotImplementedException();
        }

        public override void Abort()
        {
            listener.Stop();
            tcpThread.Abort();
            throw new NotImplementedException();
        }

        public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        {
            listener.Stop();
            throw new NotImplementedException();
        }

        public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            listener.Stop();
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
