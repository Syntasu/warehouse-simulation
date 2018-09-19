using Newtonsoft.Json;
using System;

namespace AmazonSimulator.Commands
{
    public enum CommandOpCodes : byte
    {
        Created,
        Modified,
        Deleted,
    }

    public class Command
    {
        public dynamic Contents { get; set; }
        public CommandOpCodes Operation { get; set; }

        public Command(dynamic contents, CommandOpCodes operation)
        {
            Contents = contents;
            Operation = operation;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new string[]
            {
                Contents,
                Operation.ToString(),
            });
        }
    }

    public static class CommandFactory
    {
        public static Command Create(string json)
        {
            string[] values = JsonConvert.DeserializeObject<string[]>(json);

            dynamic content = values[0];
            CommandOpCodes opCode = (CommandOpCodes)Enum.Parse(typeof(CommandOpCodes), values[1]);

            return new Command(content, opCode);
        }
    }
}
