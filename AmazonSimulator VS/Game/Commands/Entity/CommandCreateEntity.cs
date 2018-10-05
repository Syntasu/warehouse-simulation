using AmazonSimulator.Commands;
using Newtonsoft.Json;

namespace AmazonSimulator.Game.Commands.Entity
{
    public class CommandCreateEntity : Command
    {
        public CommandCreateEntity() : base("create_entity") { }

        public override string ToNet()
        {
            return JsonConvert.SerializeObject(new string[]{
                CommandName,

            }, Formatting.None); 
        }
    }
}
