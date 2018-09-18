using Newtonsoft.Json;

namespace AmazonSimulator.Commands
{
    public class NetCommand
    {
        private NetCommandType type;
        protected dynamic contents;

        public NetCommand(NetCommandType type, dynamic contents)
        {
            this.type = type;
            this.contents = contents;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static NetCommand FromJson(string json)
        {
            return JsonConvert.DeserializeObject<NetCommand>(json);
        }
    }
}