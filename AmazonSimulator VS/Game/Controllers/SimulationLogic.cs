﻿using AmazonSimulator.Data;
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
        ///     Contains static data about the simulation.
        /// </summary>
        private SimulationData data = new SimulationData();

        /// <summary>
        ///     How many robots would we need?
        /// </summary>
        private int robotCount = 1;

        /// <summary>
        ///     How many trucks do we need to spawn? (1 means 1 per minute.)
        /// </summary>
        private int truckRate = 60;

        /// <summary>
        ///     When the last truck was spawned.
        /// </summary>
        private double lastTruckSpawn = 0;

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

        private ushort mrroboto = 0;
        private void ProcessIntialization()
        {
            for (int i = 0; i < robotCount; i++)
            {
                mrroboto = world.AddEntity<Robot>(new Vector3(1, 0, 0), Vector3.Zero);
            }
        }

        public void ProcessTick()
        {
            ProcessTrucks();
        }

        /// <summary>
        ///     Function to spawn the trucks.
        /// </summary>
        private void ProcessTrucks()
        {
            // 1000ms -> 1 second * 60 == 1 minute.
            double truckSpawnInterval = (1000 * 60) / truckRate;
            double truckSpawnDelta = gameTimeInMs - lastTruckSpawn;

            if(truckSpawnDelta >= truckSpawnInterval)
            {
                world.AddEntity<Truck>(data.TruckSpawnpoint, new Vector3(0, 90, 0));
                lastTruckSpawn = gameTimeInMs;
            }
        }
    }
}
