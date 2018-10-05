using AmazonSimulator.Commands;
using AmazonSimulator.Framework.Patterns;
using AmazonSimulator.Game.Commands.Entity;

namespace AmazonSimulator.Game.Commands
{
    public static class CommandFactory
    {
        //TODO: A less weird way of converting ModelArgs -> NetworkCommand...
        public static Command GetCommandFromModel(ObservableModelArgs args)
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
            if(args.Action == "add")
            {
                CommandCreateEntity createEntityCommand = new CommandCreateEntity();

            }

            return new CommandCreateEntity();
        }
    }
}
