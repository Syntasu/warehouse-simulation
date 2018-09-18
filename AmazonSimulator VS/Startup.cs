using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

using Views;

using AmazonSimulator.Controllers;
using AmazonSimulator.Models;
using AmazonSimulator.Views;

namespace AmazonSimulator_VS
{
    public class Startup
    {
        public static _SimulationController _simulationController;

        private static SimulationController simulationController;
        private NetworkView networkView = new NetworkView();

        public Startup(IConfiguration configuration)
        {
            simulationController = new SimulationController();
            simulationController.AddModel(new WorldModel());
            simulationController.AddView(networkView);
            simulationController.Start();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.Use(networkView.HandleRequest);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
