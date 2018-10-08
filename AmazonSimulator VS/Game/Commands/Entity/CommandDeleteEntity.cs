using AmazonSimulator.Commands;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            IDictionary<string, string> command = new Dictionary<string, string>()
            {
                { "command", CommandName },
                { "id", Id.ToString() },
            };

            return JsonConvert.SerializeObject(command);
        }
    }
}
