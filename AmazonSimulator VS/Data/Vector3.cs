namespace AmazonSimulator.Data
{
    /// <summary>
    ///     A 3 dimensional vector.
    /// </summary>
    public class Vector3
    {
        public static Vector3 Zero => new Vector3(0.0f, 0.0f, 0.0f);

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
