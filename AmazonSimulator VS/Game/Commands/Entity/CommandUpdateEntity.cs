using AmazonSimulator.Commands;
using AmazonSimulator.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AmazonSimulator.Game.Commands
{
    public class CommandUpdateEntity : Command
    {
        private ushort Id = ushort.MaxValue;
        private Vector3 Position = Vector3.Zero;
        private Vector3 Rotation = Vector3.Zero;

        public CommandUpdateEntity(ushort id, Vector3 position, Vector3 rotation) : base("update_entity")
        {
            Id = id;
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
