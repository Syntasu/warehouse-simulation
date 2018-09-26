﻿using AmazonSimulator.Data;
using AmazonSimulator.Framework;
using AmazonSimulator.Framework.Patterns;
using System;

namespace AmazonSimulator.Models
{
    public class WorldModel : Model
    {
        /// <summary>
        ///     A counter to assign a unique ID to every entity in the world.
        /// </summary>
        private ushort entityIdCounter = 0;

        /// <summary>
        ///     A list of all the entities in the system.
        /// </summary>
        public ObservableList<Entity> Entities = new ObservableList<Entity>();
        public ObservableProperty<string> WorldName = new ObservableProperty<string>();

        public WorldModel() : base("world")
        {
            ModelObserveData(nameof(Entities), Entities);
            ModelObserveData(nameof(WorldName), WorldName);
        }

        public void AddEntity<T>(Vector3 position, Vector3 rotation) where T : Entity
        {
            entityIdCounter++;

            T entity = (T)Activator.CreateInstance(typeof(T), new object[] { entityIdCounter });
            entity.SetEntityPosition(position);
            entity.SetEntityRotation(rotation);

            Entities.Add(entity);
        }

        public void SetWorldName(string name)
        {
            WorldName.Value = name;
        }

    }
}