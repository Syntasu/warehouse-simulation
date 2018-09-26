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

    public class Entity
    {
        public ushort EntityId { get; protected set; } = 0;
        public Vector3 Position { get; protected set; } = Vector3.Zero;
        public Vector3 Rotation { get; protected set; } = Vector3.Zero;

        public Entity(ushort entityId)
        {
            EntityId = entityId;
        }

        public void SetEntityPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetEntityRotation(Vector3 rotation)
        {
            Rotation = rotation;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new object[]{
                EntityId,
                Position,
                Rotation
            });
        }
    }
}
