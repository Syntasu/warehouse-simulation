using AmazonSimulator.Framework;
using AmazonSimulator.Framework.Patterns;
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
        public WebSocket Socket { get; set; }

        /// <summary>
        ///     Intercept any incoming changes from the controller/model.
        /// </summary>
        /// <param name="observable">The observable that got changed (presumably a controller)</param>
        /// <param name="arguments">The arguments for the changing piece of data.</param>
        public override void ObservableChanged(Observable observable, ObservableArgs arguments)
        {
            //Get the content from the arguments.
            dynamic content = arguments.Content;

            //If we have a string...
            if(content is string)
            {
                //Don't bother sending empty shit.
                if (!string.IsNullOrEmpty(content))
                {
                    //Send it!
                    //NOTE: This is wrapped in Task.Run, so we don't
                    //      have to make literally _everything_ async...
                    Task.Run(async () => {
                        await Send(content);
                    });
                }
            }
            else
            {
                Console.WriteLine("Trying to send a non-string over net, that's bad!");
            }
        }

        /// <summary>
        ///     Receive new data from the client(s).
        /// </summary>
        /// <returns>A void task, not of use.</returns>
        public async Task Receive()
        {
            byte[] buffer = new byte[4096];

            WebSocketReceiveResult result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                Console.WriteLine("Received the following information from client: " + Encoding.UTF8.GetString(buffer));

                string json = Encoding.UTF8.GetString(buffer);
                //TODO: Receive from view.
            }

            await Socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        /// <summary>
        ///     Send a new message to the client.
        /// </summary>
        /// <param name="message">The payload we want to send.</param>
        /// <returns>A void task, not of use.</returns>
        private async Task Send(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            ArraySegment<byte> messageSegment = new ArraySegment<byte>(buffer, 0, message.Length);

            //NOTE: Workaround, don't shit yourself when receiving > 50 messages/second
            Monitor.Enter(Socket);

            try
            {
                try
                {
                    await Socket.SendAsync(messageSegment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while sending information to client, probably a Socket disconnect..." + e.Message);
                }
            }
            finally
            {
                Monitor.Exit(Socket);
            }
        }
    }
}
