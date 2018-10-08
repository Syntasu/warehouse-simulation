using AmazonSimulator.Commands;
using AmazonSimulator.Framework.Patterns;
using AmazonSimulator.Game.Data;
using Newtonsoft.Json;

namespace AmazonSimulator.Game.Commands
{
    public static class CommandFactory
    {
        //TODO: A less weird way of converting ModelArgs -> NetworkCommand...
        public static Command GetNetCommandFromModel(ObservableModelArgs args)
        {
            //Handle all world commands
            if(args.Model == "world")
            {
                if(args.Field == "entities")
                {
                    return HandleEntityEvents(args);
                }
            }

            return default(Command);
        }

        private static Command HandleEntityEvents(ObservableModelArgs args)
        {
            Command cmd = null;
            Entity e = Entity.FromJson(args.Content);

            if (args.Action == "add")
            {
                cmd = new CommandCreateEntity(e.Id, e.Type, e.Position, e.Rotation);
            }
            else if(args.Action == "modified")
            {
                cmd = new CommandUpdateEntity(e.Id, e.Position, e.Rotation);
            }
            else if (args.Action == "remove")
            {
                cmd = new CommandDeleteEntity(e.Id);
            }

            return cmd;
        }
    }
}
