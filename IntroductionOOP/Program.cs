using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IntroductionOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector2D r1 = new();
            //r1.X = 5;
            //var r2 = r1;
            Console.WriteLine(r1.X);
        }
    }

    public class Car
    {
        private Vector2D _Location;
        private Vector2D _Speed;
        private Vector2D _Acceleration;

        private string _Name;


    }

    /// <summary>Двумерный вектор</summary>
    public readonly /*ref*/ struct Vector2D
    {
        /// <summary>Значение координаты X</summary>
        private readonly double _X;

        /// <summary>Значение координаты Y</summary>
        private readonly double _Y;

        //public double get_X()
        //{
        //    return _X;
        //}

        //public void set_X(double value)
        //{
        //    _X = value;
        //}

        public double X
        {
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _X;
            }
            //[MethodImpl(MethodImplOptions.Synchronized)]
            //set
            //{
            //    _X = value;
            //}
        }

        //public double Y { get => _Y; set => _Y = value; }
        public double Y => _Y;

        ////private double <TestValue>k__BackingField;
        //public double TestValue
        //{
        //    [DebuggerStepThrough]
        //    get;
        //    private set;
        //}

        //public double Length => Math.Sqrt(Math.Pow(_X) + Math.Pow(_Y, 2));

        /// <summary>Длина вектора</summary>
        public double Length => Math.Sqrt(_X * _X + _Y * _Y);

        /// <summary>Угол вектора от оси OX</summary>
        public double Angle => Math.Atan2(_Y, _X);

        public Vector2D(double Length) 
            : this(Math.Sqrt(Length) / 2, Math.Sqrt(Length) / 2)
        {
            //_X = _Y = Math.Sqrt(Length) / 2;
        }

        public Vector2D(double X, double Y)
        {
            _X = X;
            _Y = Y;
        }

        public static Vector2D Sum(Vector2D a, Vector2D b)
        {
            var c = new Vector2D(a._X + b._X, a._Y + b._Y);
            //c._X = a._X + b._X;
            //c._Y = a._Y + b._Y;

            return c;
        }

        public Vector2D Add(Vector2D other)
        {
            return new(_X + other._X, _Y + other._Y);
        }

        public Vector2D GetLager(Vector2D other)
        {
            if (Length > other.Length)
                return this;
            return other;
        }
    }

}
