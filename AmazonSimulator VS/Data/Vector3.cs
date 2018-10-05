namespace AmazonSimulator.Data
{
    /// <summary>
    ///     A 3 dimensional vector.
    /// </summary>
    public class Vector3
    {
        /// <summary>
        ///     Short hand notation to create a new vector at 0,0,0.
        /// </summary>
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

        public void ToStringList(out string sx, out string sy, out string sz)
        {
            sx = X.ToString();
            sy = Y.ToString();
            sz = Z.ToString();
        }
    }
}
