using AmazonSimulator.Data;
using AmazonSimulator.Models;

namespace Models
{
    public class RobotModel : EntityModel
    {
        public RobotModel(string type, float x, float y, float z, float rx, float ry, float rz)
        {
            SetEntityPosition(new Vector3(x, y, z));
            SetEntityRotation(new Vector3(rx, ry, rz));
        }

        //Do robot thingies...
    }
}