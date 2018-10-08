using AmazonSimulator.Data;
using AmazonSimulator.Game.Data;
using AmazonSimulator.Models;
using System;

namespace AmazonSimulator.Game.Controllers
{
    public class SimulationLogic
    {
        /// <summary>
        ///     Reference to the world model.
        /// </summary>
        private WorldModel world;

        /// <summary>
        ///     How many robots would we need?
        /// </summary>
        private int robotCount = 1;

        /// <summary>
        ///     How many trucks do we need to spawn? (1 means 1 per minute.)
        /// </summary>
        private int truckRate = 1;

        /// <summary>
        ///     Returns the elapsed time since the game was create
        ///     (when new SimulationLogic was called!).
        /// </summary>
        private DateTime startTime;
        private double gameTimeInMs
        {
            get
            {
                return (DateTime.Now - startTime).TotalMilliseconds;
            }
        }

        public SimulationLogic(WorldModel world, int robotCount, int truckRate)
        {
            this.world = world;
            this.robotCount = robotCount;
            this.truckRate = truckRate;
            this.startTime = DateTime.Now;

            ProcessIntialization();
        }

        private void ProcessIntialization()
        {
            for (int i = 0; i < robotCount; i++)
            {
                world.AddEntity<Robot>(new Vector3(10, 10, 10), Vector3.Zero);
            }
        }

        public void ProcessTick()
        {

        }

    }
}
