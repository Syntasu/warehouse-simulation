
using AmazonSimulator.Framework.Patterns.Serialization;
using Newtonsoft.Json;

namespace AmazonSimulator.Data
{
    public enum EntityType
    {
        Entity,
        Robot,
        Truck,
        Rack
    }

    public class Entity : IJsonSerializable
    {
        protected ushort Id = 0;
        protected Vector3 Position = Vector3.Zero;
        protected Vector3 Rotation = Vector3.Zero;

        public Entity(ushort id)
        {
            Id = id;
        }

        public void SetEntityPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetEntityRotation(Vector3 rotation)
        {
            Rotation = rotation;
        }

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(new object[]{
                Id,
                Position,
                Rotation
            });
        }
    }
}
