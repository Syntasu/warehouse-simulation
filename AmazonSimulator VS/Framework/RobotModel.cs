using AmazonSimulator.Data;

namespace AmazonSimulator.Framework
{
    public class RobotModel : Model
    {
        public ModelData Position = new ModelData("position", "{0,0,0}");

        public RobotModel()
        {
            RegisterData(Position);
        }

    }
}
