using AmazonSimulator.Data;
using AmazonSimulator.Framework.Patterns;
using System;

namespace AmazonSimulator.Game.Data
{
    public class Truck : Entity
    {
        private bool HasDriveToTask = false;
        private Vector3 target = Vector3.Zero;
        private float speed = 0.0f;

        public Truck(ushort id) : base(id, EntityType.Truck) { }


        public void TaskDriveTo(Vector3 target, float speed)
        {
            HasDriveToTask = true;

            this.target = target;
            this.speed = speed;
        }

        public void Tick()
        {
            if(HasDriveToTask)
            {
                double distance = Vector3.Magnitude(Position, target);

                if(distance > 0.5f)
                {
                    Position.X += speed;
                }
                else
                {
                    HasDriveToTask = false;
                    target = Vector3.Zero;
                    speed = 0.0f;
                }
            }
        }
    }
}
