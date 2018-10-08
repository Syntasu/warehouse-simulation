using AmazonSimulator.Commands;
using AmazonSimulator.Framework;
using AmazonSimulator.Framework.Patterns;
using AmazonSimulator.Game.Commands;
using AmazonSimulator.Game.Controllers;
using AmazonSimulator.Models;
using System;
using System.Threading;

namespace AmazonSimulator.Controllers
{
    public class SimulationController : Controller
    {
        /// <summary>
        ///     Wether the controller is or should still be running.
        /// </summary>
        private bool isRunning = false;

        /// <summary>
        ///     Counts how many ticks there have been.
        /// </summary>
        private ulong tickCount = 0;
        
        /// <summary>
        ///     How many ticks we should run at.
        /// </summary>
        private int tickRate = 20;

        /// <summary>
        ///     The thread we run the game logic on.
        /// </summary>
        private Thread simulationThread;

        /// <summary>
        ///     SimulationLogic hold all the logic related to the game.
        /// </summary>
        private SimulationLogic game;


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

                        int sleepTime = (int)Math.Round(1000.0 / tickRate);
                        Thread.Sleep(sleepTime);
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
        ///     Reset the simulation, todo..
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
            WorldModel world = GetModel<WorldModel>();

            //  Initialize the game logic.
            if (game == null)
            {
                game = new SimulationLogic(world, 2, 10);
            }
            else
            {
                game.ProcessTick();
            }
        }

        /// <summary>
        ///     Intercept any changes from the view or model.
        /// </summary>
        /// <param name="observable">The observable that was changed</param>
        /// <param name="command">The actual data that got changed + some metadata.</param>
        public override void ObservableChanged(Observable observable, ObservableArgs args)
        {
            //We received an update from the model.
            if(args is ObservableModelArgs)
            {
                //Convert ModelArgs to an actual command, using the command factory,
                //  The CommandFactory determines which command we want to send.s
                ObservableModelArgs modelArgs = args as ObservableModelArgs;
                Command networkCommand = CommandFactory.GetNetCommandFromModel(modelArgs);

                //To let the view know we have a new command, we need to notify the view.
                // The command we just made, we convert it to a string we want to send of the network.
                // And the string we get we pass as arguments.
                ObservableArgs commandArgs = new ObservableArgs
                {
                    Content = networkCommand.ToNet()
                };

                //Let the view know we have changed!
                Notify(commandArgs);
            }

            //We received an update from the view.
            //if(args is ObservableViewArgs)
        }
    }
}
