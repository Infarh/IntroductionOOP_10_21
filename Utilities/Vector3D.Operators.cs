using System;

namespace Utilities
{
    public readonly partial struct Vector3D
    {
        public static Vector3D operator +(Vector3D a, double b) => new(a._X + b, a._Y + b, a._Z + b);
        public static Vector3D operator -(Vector3D a, double b) => new(a._X - b, a._Y - b, a._Z - b);
        public static Vector3D operator *(Vector3D a, double b) => new(a._X * b, a._Y * b, a._Z * b);
        public static Vector3D operator /(Vector3D a, double b) => new(a._X / b, a._Y / b, a._Z / b);

        public static Vector3D operator +(Vector3D a, int b) => new(a._X + b, a._Y + b, a._Z + b);
        public static Vector3D operator -(Vector3D a, int b) => new(a._X - b, a._Y - b, a._Z - b);
        public static Vector3D operator *(Vector3D a, int b) => new(a._X * b, a._Y * b, a._Z * b);
        public static Vector3D operator /(Vector3D a, int b) => new(a._X / b, a._Y / b, a._Z / b);

        public static Vector3D operator +(double a, Vector3D b) => new(a + b._X, a + b._Y, a + b._Z);
        public static Vector3D operator -(double a, Vector3D b) => new(a - b._X, a - b._Y, a - b._Z);
        public static Vector3D operator *(double a, Vector3D b) => new(a * b._X, a * b._Y, a * b._Z);
        public static Vector3D operator /(double a, Vector3D b) => new(a / b._X, a / b._Y, a / b._Z);

        public static Vector3D operator +(int a, Vector3D b) => new(a + b._X, a + b._Y, a + b._Z);
        public static Vector3D operator -(int a, Vector3D b) => new(a - b._X, a - b._Y, a - b._Z);
        public static Vector3D operator *(int a, Vector3D b) => new(a * b._X, a * b._Y, a * b._Z);
        public static Vector3D operator /(int a, Vector3D b) => new(a / b._X, a / b._Y, a / b._Z);

        public static Vector3D operator +(Vector3D a, Vector3D b) => new(a._X + b._X, a._Y + b._Y, a._Z + b._Z);
        public static Vector3D operator -(Vector3D a, Vector3D b) => new(a._X - b._X, a._Y - b._Y, a._Z - b._Z);

        public static double operator *(Vector3D a, Vector3D b) => a._X * b._X + a._Y * b._Y + a._Z * b._Z;

        public static bool operator ==(Vector3D a, Vector3D b) => a._X == b._X && a._Y == b._Y && a._Z == b._Z;
        public static bool operator !=(Vector3D a, Vector3D b) => !(a == b);

        //public override bool Equals(object obj) { return base.Equals(obj); } // надо переопределить

        //public override int GetHashCode() { return base.GetHashCode(); } // надо переопределить

        public static Vector3D VectorProduct(Vector3D a, Vector3D b) => throw new NotImplementedException();

        public static explicit operator double(Vector3D v) => v.Length;
    }
}
