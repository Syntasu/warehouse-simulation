using AmazonSimulator.Data;

namespace AmazonSimulator.Game.Data
{
    public class Truck : Entity
    {
        private bool hasTaskDriveTo = false;
        private Vector3 target = Vector3.Zero;
        private float speed = 0.0f;

        public Truck(ushort id) : base(id, EntityType.Truck) { }

        /// <summary>
        ///     Create a new task for the truck to drive to a coordinate.
        /// </summary>
        /// <param name="target">Where we want to drive to.</param>
        /// <param name="speed">The rate at we want to move.</param>
        public void TaskDriveTo(Vector3 target, float speed)
        {
            this.hasTaskDriveTo = true;
            this.target = target;
            this.speed = speed;
        }

        /// <summary>
        ///     Update the truck.
        /// </summary>
        public void Tick()
        {
            if(hasTaskDriveTo)
            {
                double distance = Vector3.Magnitude(Position, target);

                if(distance > 0.5f)
                {
                    Vector3 p = Position;
                    p.X += speed;

                    SetEntityPosition(p);
                }
                else
                {
                    hasTaskDriveTo = false;
                    target = Vector3.Zero;
                    speed = 0.0f;
                }
            }
        }
    }
}
