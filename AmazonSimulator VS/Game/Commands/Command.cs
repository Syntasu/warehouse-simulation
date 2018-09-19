using Newtonsoft.Json;

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
        public string Operation { get; set; }

        public Command(string contents, string operation)
        {
            Contents = contents;
            Operation = operation;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new string[]
            {
                Contents,
                Operation
            });
        }
    }

    public static class CommandFactory
    {
        public static Command Create(string json)
        {
            string[] values = JsonConvert.DeserializeObject<string[]>(json);
            return new Command(values[0], values[1]);
        }
    }

}
