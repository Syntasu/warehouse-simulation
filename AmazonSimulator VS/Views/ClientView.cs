using System;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Text;
using AmazonSimulator.Commands;

namespace Views
{
    public class ClientView : IObserver<NetCommand>
    {
        private WebSocket socket;

        public ClientView(WebSocket socket)
        {
            this.socket = socket;
        }

        public async Task StartReceiving()
        {
            var buffer = new byte[1024 * 4];

            Console.WriteLine("ClientView connection started");

            WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                Console.WriteLine("Received the following information from client: " + Encoding.UTF8.GetString(buffer));

                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            Console.WriteLine("ClientView has disconnected");

            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private async void SendMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            try
            {
                await socket.SendAsync(new ArraySegment<byte>(buffer, 0, message.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception)
            {
                Console.WriteLine("Error while sending information to client, probably a Socket disconnect");
            }
        }

        public void SendCommand(NetCommand c)
        {
            SendMessage(c.ToJson());
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(NetCommand value)
        {
            SendCommand(value);
        }
    }
}