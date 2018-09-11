using System;

namespace Models
{
    public class Robot : IUpdatable
    {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _rX = 0;
        private double _rY = 0;
        private double _rZ = 0;

        public string Type { get; }
        public Guid Id { get; }
        public double X { get { return _x; } }
        public double Y { get { return _y; } }
        public double Z { get { return _z; } }
        public double RotationX { get { return _rX; } }
        public double RotationY { get { return _rY; } }
        public double RotationZ { get { return _rZ; } }

        public bool needsUpdate = true;

        public Robot(string type, double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            Type = type;
            Id = Guid.NewGuid();

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;
        }

        public virtual void Move(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;

            needsUpdate = true;
        }

        public virtual void Rotate(double rotationX, double rotationY, double rotationZ)
        {
            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;

            needsUpdate = true;
        }

        public virtual bool Update(int tick)
        {
            if (needsUpdate)
            {
                needsUpdate = false;
                return true;
            }
            return false;
        }
    }
}