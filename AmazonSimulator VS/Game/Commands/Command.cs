namespace AmazonSimulator.Commands
{
    public abstract class Command
    {
        public string CommandName { get; protected set; } = "invalid_command";

        protected Command(string commandName)
        {
            CommandName = commandName; 
        }

        public abstract string ToNet();
    }
}
