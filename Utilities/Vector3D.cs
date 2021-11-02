using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public readonly partial struct Vector3D
    {
        private readonly double _X;
        private readonly double _Y;
        private readonly double _Z;

        public double X => _X;
        public double Y => _Y;
        public double Z => _Z;

        public double Length => Math.Sqrt(_X * _X + _Y * _Y + _Z * _Z);

        private static (double x, double y, double z) GetRandomVector(double Length)
        {
            var rnd = new Random();
            var x = rnd.NextDouble();
            var y = rnd.NextDouble();
            var z = rnd.NextDouble();

            var len = Math.Sqrt(x * x + y * y + z * z);

            var k = Length / len;

            return (x * k, y * k, z * k);
        }

        public Vector3D(double Length) : this(GetRandomVector(Length))
        {

        }

        private Vector3D((double x, double y, double z) value) : this(value.x, value.y, value.z) { }

        public Vector3D(double X, double Y, double Z)
        {
            //(_X, _Y, _Z) = (X, Y, Z);
            _X = X;
            _Y = Y;
            _Z = Z;
        }

       

        public void Deconstruct(out double X, out double Y, out double Z) => (X, Y, Z) = (_X, _Y, _Z);

    }
}
