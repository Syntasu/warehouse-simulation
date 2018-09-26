using AmazonSimulator.Framework;
using AmazonSimulator.Framework.Patterns;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmazonSimulator.Views
{
    public class NetworkView : View
    {
        /// <summary>
        ///     The websocket we obtain via the HandleRequest method.
        /// </summary>
        private WebSocket socket;

        public async Task HandleRequest(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path == "/connect_client")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    socket = await context.WebSockets.AcceptWebSocketAsync();
                    await Receive();
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            }
            else
            {
                await next();
            }
        }

        /// <summary>
        ///     Intercept any incoming changes from the controller/model.
        /// </summary>
        /// <param name="observable">The observable that got changed (presumably a controller)</param>
        /// <param name="arguments">The arguments for the changing piece of data.</param>
        public override void ObservableChanged(Observable observable, ObservableArgs arguments)
        {
            if (arguments is ObservableModelArgs)
            {
                ObservableModelArgs args = arguments as ObservableModelArgs;
                string content = args.ToString();

                if (!String.IsNullOrEmpty(content))
                {
                    Task.Run(async () => {
                        await Send(content);
                    });
                }
            }
        }

        public async Task Receive()
        {
            byte[] buffer = new byte[4096];

            WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                Console.WriteLine("Received the following information from client: " + Encoding.UTF8.GetString(buffer));

                string json = Encoding.UTF8.GetString(buffer);
                //NetCommand command = Command.FromJson(json);

            }

            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private async Task Send(string message)
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
    }
}
