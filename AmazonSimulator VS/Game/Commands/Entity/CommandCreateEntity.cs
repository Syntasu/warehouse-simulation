using AmazonSimulator.Commands;
using AmazonSimulator.Data;
using AmazonSimulator.Game.Data;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace AmazonSimulator.Game.Commands
{
    public class CommandCreateEntity : Command
    {
        //NOTE: ushort.MaxValue indicates an errornous condition.
        private ushort Id = ushort.MaxValue;
        private EntityType Type = EntityType.Entity;
        private Vector3 Position = Vector3.Zero;
        private Vector3 Rotation = Vector3.Zero;


        public CommandCreateEntity(ushort id, EntityType type, Vector3 position, Vector3 rotation) : base("create_entity")
        {
            Id = id;
            Type = type;
            Position = position;
            Rotation = rotation;
        }

        public override string ToNet()
        {
            Position.ToStringList(out string px, out string py, out string pz);
            Rotation.ToStringList(out string rx, out string ry, out string rz);

            IDictionary<string, string> command = new Dictionary<string, string>()
            {
                { "command", CommandName },
                { "id", Id.ToString() },
                { "type", Type.ToString() },
                { "px", px },
                { "py", py },
                { "pz", pz },
                { "rx", rx },
                { "ry", ry },
                { "rz", rz },
            };

            return JsonConvert.SerializeObject(command); 
        }
    }
}
