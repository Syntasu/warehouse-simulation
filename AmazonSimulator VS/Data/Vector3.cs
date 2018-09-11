namespace AmazonSimulator.Data
{
    public class Vector3
    {
        public float X = 0.0f;
        public float Y = 0.0f;
        public float Z = 0.0f;

        public Vector3() { }

        public Vector3(float x, float y, float z)
        {
            Set(x, y, z);
        }

        public void Set(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
