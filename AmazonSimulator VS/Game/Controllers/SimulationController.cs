using AmazonSimulator.Data;
using AmazonSimulator.Data.Entities;
using AmazonSimulator.Framework;
using AmazonSimulator.Framework.Patterns;
using AmazonSimulator.Models;
using System;
using System.Threading;

namespace AmazonSimulator.Controllers
{
    public class SimulationController : Controller
    {
        private bool isRunning = false;
        private ulong tickCount = 0;
        private Thread simulationThread;

        /// <summary>
        ///     Start the simulation.
        /// </summary>
        public void Start()
        {
            //If we do not have a thread, create one w/ lambda function.
            if(simulationThread == null)
            {
                simulationThread = new Thread(() =>
                {
                    isRunning = true;
                    while (isRunning)
                    {
                        ProcessFrame();
                        tickCount++;
                        Thread.Sleep(1000/3);
                    }
                });

                simulationThread.Start();
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

        ushort robotId = 0;

        /// <summary>
        ///     Process one frame of the simulation
        /// </summary>
        public void ProcessFrame()
        {
            if (tickCount == 0)
            {
                InitializeSimulation();
                return;
            }

            WorldModel world = GetModel<WorldModel>();

            Robot robot = world.GetEntity<Robot>(robotId);

            Vector3 robotPos = robot.Position;
            robotPos.Y += 0.1f;

            world.UpdateEntity(robot);
        }

        /// <summary>
        ///     Start the simulation.
        /// </summary>
        private void InitializeSimulation()
        {
            WorldModel world = GetModel<WorldModel>();

            robotId = world.AddEntity<Robot>(Vector3.Zero, Vector3.Zero);
            world.AddEntity<Truck>(Vector3.Zero, Vector3.Zero);
            world.AddEntity<Rack>(Vector3.Zero, Vector3.Zero);
        }

        /// <summary>
        ///     Intercept any changes from the view or model.
        /// </summary>
        /// <param name="observable">The observable that was changed</param>
        /// <param name="command">The actual data that got changed + some metadata.</param>
        public override void ObservableChanged(Observable observable, ObservableArgs args)
        {
            /// We received an update from the model.
            if(args is ObservableModelArgs)
            {
                ObservableModelArgs modelArgs = args as ObservableModelArgs;
                Notify(modelArgs);

                Console.WriteLine($"\nThe changed model is [{modelArgs.Model.ToUpper()}] and field [{modelArgs.Field.ToUpper()}]" +
                    $"\n The action performed was [{modelArgs.Action.ToUpper()}] with this data: {modelArgs.Content}\n");
            }
        }
    }
}
