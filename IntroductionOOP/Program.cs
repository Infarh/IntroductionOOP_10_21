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

            //var car = new Car();
            //car.Location = car.Location.Add(new(5, 0));
            //location.X += 10;

            var car = new Car("Lada")
            {
                Speed = new(5,7),
                Acceleration = new(0.5, 0.7)
            };

            //car.Speed = new(5, 7);

            const double dt = 0.01;
            for (var t = 0.0; t < 10; t += dt)
            {
                car.Acceleration = new(1 / (1 + Math.Exp(-(t - 5) / 1)), 0);
                car.Move(dt);
            }
        }
    }

    public class Car
    {
        private Vector2D _Location;
        private Vector2D _Speed;
        private Vector2D _Acceleration;

        //private readonly string _Name; // 4 байта

        public Vector2D Location { get => _Location; set => _Location = value; }

        public Vector2D Speed { get => _Speed; set => _Speed = value; }
        //public Vector2D Speed { get => _Speed; init => _Speed = value; }

        public Vector2D Acceleration { get => _Acceleration; set => _Acceleration = value; }

        public string Name { get; }

        public Car(string Name, Vector2D Location = default)
        {
            this.Name = Name;
            _Location = Location;
        }

        /// <summary>Метод перемещения машины в пространстве</summary>
        /// <param name="dt">Интервал времени, который машина двигалась с равным ускорением</param>
        public Vector2D Move(double dt)
        {
            var speed = _Speed.Add(_Acceleration.Mul(dt));
            var location = _Location.Add(_Speed.Mul(dt));
            location = location.Add(_Acceleration.Mul(dt * dt / 2));

            _Speed = speed;
            _Location = location;

            return location;
        }
    }

    /// <summary>Двумерный вектор</summary>
    /// <remarks>16 байт</remarks>
    public readonly /*ref*/ struct Vector2D
    {
        /// <summary>Значение координаты X</summary>
        /// <remarks>8 байт</remarks>
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

        //public double Length => Math.Sqrt(Math.Pow(_X, 2) + Math.Pow(_Y, 2));

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

        public Vector2D Mul(Vector2D other)
        {
            return new(_X * other._X, _Y * other._Y);
        }

        public Vector2D Mul(double c) => new(_X * c, _Y * c);

        public Vector2D GetLager(Vector2D other)
        {
            if (Length > other.Length)
                return this;
            return other;
        }

        public override string ToString() => $"({_X};{_Y})";
    }

}
