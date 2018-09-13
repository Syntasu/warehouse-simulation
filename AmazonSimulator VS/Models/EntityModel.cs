using AmazonSimulator.Data;
using AmazonSimulator.Framework;
using System;

namespace AmazonSimulator.Models
{
    public enum EntityType
    {
        Entity,
        Robot,
        Truck,
        Shelf
    }

    public class EntityModel : Model
    {
        protected ModelData Id = new ModelData("id", Guid.NewGuid());
        protected ModelData Type = new ModelData("type", EntityType.Entity);
        protected ModelData Position = new ModelData("position", new Vector3());
        protected ModelData Rotation = new ModelData("rotation", new Vector3());

        public EntityModel(EntityType type = EntityType.Entity)
        {
            Type.Value = type;
            RegisterModelData(Id, Type, Position, Rotation);
        }

        public void SetEntityPosition(Vector3 position)
        {
            Position.Value = position;
        }

        public void SetEntityRotation(Vector3 rotation)
        {
            Rotation.Value = rotation;
        }
    }
}
