using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

using AmazonSimulator.Controllers;
using AmazonSimulator.Models;
using AmazonSimulator.Views;

using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace AmazonSimulator_VS
{
    public class Startup
    {
        private SimulationController simulationController;
        private NetworkView networkView = new NetworkView();

        public Startup(IConfiguration configuration)
        {
            simulationController = new SimulationController();
            simulationController.AddModel(new WorldModel());
            simulationController.AddView(networkView);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".mtl"] = "text/plain";
            provider.Mappings[".obj"] = "text/plain";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });

            app.UseWebSockets();
            app.Use(HandleRequests);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }

        /// <summary>
        ///     Handle any incoming request of the server.
        /// </summary>
        private async Task HandleRequests(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path == "/connect_client")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();
                    networkView.Socket = socket;
                    
                    //Start the simulation after n msec, we do this in parallel
                    //  so we don't block the await Receive and miss out on some receives.
                    //  Doing this syncronously will cause dropped packets.
                    Parallel.Invoke(StartSimulation);

                    await networkView.Receive();
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
        ///     Wait a bit for the simulation to start
        ///     Prevents interfering with the startup process.
        /// </summary>
        private async void StartSimulation()
        {
            await Task.Delay(1000);
            simulationController.Start();
        }
    }
}
