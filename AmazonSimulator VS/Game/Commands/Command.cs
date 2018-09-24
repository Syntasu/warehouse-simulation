using AmazonSimulator.Data;
using AmazonSimulator.Framework.Patterns.Serialization;
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
        public string CommandName { get; set; } = "Command";
        public string Contents { get; set; }

        public Command(string commandName, IJsonSerializable content)
        {
            CommandName = commandName;
            Contents = content.ToJson();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new []
            {
                Contents,
            });
        }
    }

    public static class CommandFactory
    {
        public static Command FromJson(string json)
        {
            return new Command("command", new Entity(ushort.MaxValue));
        }
    }
}
