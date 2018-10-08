using AmazonSimulator.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace AmazonSimulator.Game.Data
{
    public class Entity
    {
        public ushort Id { get; protected set; } = 0;

        [JsonConverter(typeof(StringEnumConverter))] //NOTE: Use default StringEnumConverter to make serializaion possible.
        public EntityType Type { get; protected set; } = EntityType.Entity;

        public Vector3 Position { get; protected set; } = Vector3.Zero;
        public Vector3 Rotation { get; protected set; } = Vector3.Zero;

        public Entity(ushort entityId, EntityType type, Vector3 position = null, Vector3 rotation = null)
        {
            Id = entityId;
            Type = type;
            Position = position;
            Rotation = rotation;
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
            return JsonConvert.SerializeObject(new object[]
            {
                Id,
                Type.ToString(),
                Position.ToString(),
                Rotation.ToString()
            });
        }

        public static Entity FromJson(string json)
        {
            string[] deserialized = JsonConvert.DeserializeObject<string[]>(json);

            //HACK: Stupid ass work around because JSON.net seems to fail to serialize Entity class properly...
            //      Or rather deserializing it from a json string 
            //      which is litterally what rolled out of JsonConvert.SerializeObject(this) ???
            ushort id = ushort.Parse(deserialized[0], System.Globalization.NumberStyles.None);
            EntityType type = (EntityType)Enum.Parse(typeof(EntityType), deserialized[1]);
            Vector3 position = new Vector3(deserialized[2]);
            Vector3 rotation = new Vector3(deserialized[3]);

            return new Entity(id, type, position, rotation);
        }
    }
}
