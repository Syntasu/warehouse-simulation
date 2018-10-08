using System;

namespace AmazonSimulator.Data
{
    /// <summary>
    ///     A 3 dimensional vector.
    /// </summary>
    [System.Serializable]
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

        public Vector3(string vectorStr)
        {
            string[] fields = vectorStr.Split(',');

            if (fields.Length == 3)
            {
                X = float.Parse(fields[0]);
                Y = float.Parse(fields[1]);
                Z = float.Parse(fields[2]);
            }
            else
            {
                Console.WriteLine("Cannot create vector3 from malformed string");
            }
        }

        public void Set(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"{X.ToString()}, {Y.ToString()}, {Z.ToString()}";
        }

        public void ToStringList(out string sx, out string sy, out string sz)
        {
            sx = X.ToString();
            sy = Y.ToString();
            sz = Z.ToString();
        }

        public static double Magnitude(Vector3 a, Vector3 b)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2) + Math.Pow((a.Z - b.Z), 2));
        }
    }
}
