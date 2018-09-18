using AmazonSimulator.Data;
using AmazonSimulator.Framework;
using System;

namespace AmazonSimulator.Data
{
    public enum EntityType
    {
        Entity,
        Robot,
        Truck,
        Shelf
    }

    public class Entity
    {
        protected Guid Id = Guid.NewGuid();
        protected EntityType Type = EntityType.Entity;
        protected Vector3 Position = Vector3.Zero;
        protected Vector3 Rotation = Vector3.Zero;

        public Entity(EntityType type)
        {
            Type = type;
        }

        public void SetEntityPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetEntityRotation(Vector3 rotation)
        {
            Rotation = rotation;
        }
    }
}
