using AmazonSimulator.Commands;
using Newtonsoft.Json;

namespace AmazonSimulator.Game.Commands
{
    public class CommandDeleteEntity : Command
    {
        private ushort Id = ushort.MaxValue;

        public CommandDeleteEntity(ushort id) : base("delete_entity")
        {
            Id = id;
        }

        public override string ToNet()
        {
            return JsonConvert.SerializeObject(new string[]{
                CommandName,
                Id.ToString()
            });
        }
    }
}
