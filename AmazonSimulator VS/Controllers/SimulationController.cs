using AmazonSimulator.Framework;
using AmazonSimulator.Models;
using System.Threading;

namespace AmazonSimulator.Controllers
{
    public class SimulationController : Controller
    {
        private bool isRunning = false;
        private float tickRate = 1000 / 60;
        private Thread simulationThread;

        /// <summary>
        ///     Start the simulation.
        /// </summary>
        public void Start()
        {
            if(simulationThread == null)
            {
                simulationThread = new Thread(() =>
                {
                    isRunning = true;
                    while (isRunning)
                    {
                        ProcessFrame();
                    }
                });
            }

            if(simulationThread.ThreadState != ThreadState.Running)
            {
                simulationThread.Start();
                isRunning = true;
            }
        }

        /// <summary>
        ///     Stop the simulation
        /// </summary>
        public void Stop()
        {
            if(simulationThread.ThreadState == ThreadState.Running)
            {
                isRunning = false;
                simulationThread.Join();
            }

            simulationThread = null;
        }

        /// <summary>
        ///     Reset the simulation.
        /// </summary>
        public void Reset()
        {
            // TODO: Reset data?
        }

        /// <summary>
        ///     Process one frame of the simulation
        /// </summary>
        public void ProcessFrame()
        {
            WorldModel model = GetModel<WorldModel>();
            //model.RegisterModelData
            //world.Update(tickRate);
            //Thread.Sleep(tickRate);
        }
    }
}
