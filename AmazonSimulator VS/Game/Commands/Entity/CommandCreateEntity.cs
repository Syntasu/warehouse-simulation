using AmazonSimulator.Commands;
using AmazonSimulator.Data;
using AmazonSimulator.Game.Data;
using Newtonsoft.Json;

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

            return JsonConvert.SerializeObject(new string[]{
                CommandName,
                Id.ToString(),
                Type.ToString(),
                px, py, pz,
                rx, ry, rz
            }, Formatting.None); 
        }
    }
}
