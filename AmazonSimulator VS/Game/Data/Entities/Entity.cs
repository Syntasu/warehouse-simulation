using AmazonSimulator.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AmazonSimulator.Game.Data
{
    public class Entity
    {
        public ushort Id { get; protected set; } = 0;

        [JsonConverter(typeof(StringEnumConverter))] //NOTE: Use default StringEnumConverter to make serializaion possible.
        public EntityType Type { get; protected set; } = EntityType.Entity;

        public Vector3 Position { get; protected set; } = Vector3.Zero;
        public Vector3 Rotation { get; protected set; } = Vector3.Zero;

        public Entity(ushort entityId, EntityType type)
        {
            Id = entityId;
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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Entity FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Entity>(json);
        }
    }
}
