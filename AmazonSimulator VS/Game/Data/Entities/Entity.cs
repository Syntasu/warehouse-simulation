using System;

namespace AmazonSimulator.Data
{
    public enum EntityType : byte
    {
        Entity,
        Robot,
        Truck,
        Rack
    }

    public class Entity
    {
        protected ushort Id = 0;
        protected EntityType Type = EntityType.Entity;
        protected Vector3 Position = Vector3.Zero;
        protected Vector3 Rotation = Vector3.Zero;

        public Entity(ushort id, EntityType type)
        {
            Id = id;
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
