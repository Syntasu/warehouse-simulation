using System;
using System.Collections.Generic;
using System.Threading;
using Models;
using Views;

namespace AmazonSimulator.Controllers
{
    struct ObservingClient
    {
        public ClientView cv;
        public IDisposable unsubscribe;
    }

    public class SimulationController
    {
        private WorldModel world;
        private List<ObservingClient> views = new List<ObservingClient>();
        private bool running = false;
        private int tickRate = 50;

        public SimulationController(WorldModel w)
        {
            world = w;
        }

        public void AddView(ClientView v)
        {
            ObservingClient oc = new ObservingClient();

            oc.unsubscribe = world.Subscribe(v);
            oc.cv = v;

            views.Add(oc);
        }

        public void RemoveView(ClientView v)
        {
            for (int i = 0; i < views.Count; i++)
            {
                ObservingClient currentOC = views[i];

                if (currentOC.cv == v)
                {
                    views.Remove(currentOC);
                    currentOC.unsubscribe.Dispose();
                }
            }
        }

        public void Simulate()
        {
            running = true;

            while (running)
            {
                world.Update(tickRate);
                Thread.Sleep(tickRate);
            }
        }

        public void EndSimulation()
        {
            running = false;
        }
    }
}