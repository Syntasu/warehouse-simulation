﻿using AmazonSimulator.Framework;
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
        private WebSocket socket;

        public async Task HandleRequest(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path == "/connect_client")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    socket = await context.WebSockets.AcceptWebSocketAsync();
                    await StartReceiving();
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

    }
}
