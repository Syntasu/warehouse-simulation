using AmazonSimulator.Commands;
using AmazonSimulator.Data;
using Newtonsoft.Json;
using System;

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

            return JsonConvert.SerializeObject(new string[] {
                CommandName,
                Id.ToString(),
                px, py, pz,
                rx, ry, rz
            });
        }
    }
}
