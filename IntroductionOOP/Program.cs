#nullable enable
using System;
using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace IntroductionOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            string? name = "Ivanov;";

            name = null!;

            //Console.WriteLine(name);

            //name = null;

            //if (name!.Length == 0)
            //{

            //}

            //var car_name = "123";
            //var location = new Vector2D();
            //Console.WriteLine("Готов к старту");
            //Console.ReadLine();

            //for (var i = 0; i < 1000_000_000; i++)
            //{
            //    //var vector = new Vector2D();
            //    var car = new Car(car_name, location);

            //}

            //GC.Collect();

            var v1 = new Vector2D(5, 7);
            var v2 = new Vector2D(4, 6);
            var v3 = new Vector2D(5, 7);

            Print(v1);
            Print2(ref v1);
            CreateVector(out v1);

            //if (Equals(v1, v2))
            //{

            //}

            Vector2D? null_v1 = v1;
            Nullable<Vector2D> null_v2 = v2;

            if (v1.Equals(v2))
            {

            }

            var car1 = new Car("123", default);
            car1.Pilot = new Pilot { Name = "Иванов" };
            //car1 = default;
            //car1 = null;
            var car2 = (Car)car1.Clone();

            var is_equals_name = ReferenceEquals(car1.Name, car2.Name);
            var is_equals_pilot = ReferenceEquals(car1.Pilot, car2.Pilot);

            //Memory<>
            //Span<>

            Vector2D[] vectors =
            {
                new(5,7),
                new(3,1),
                new(10,13),
                new(4,5)
            };

            ref var max_vector = ref GetMaxValue(vectors);

            max_vector = new(0, 0);

            GetMaxValue(vectors) = new(-1, -1);

            var (get_max, set_max) = GetMaxValue2(vectors);
            var max = get_max();
            set_max(new(5, 5));

            var str = "Hello World!";

            var str_span = str.AsSpan();

            str = "123";

            var fragment1 = str_span.Slice(4);
            var fragment2 = str_span.Slice(1, 4);
            var fragment3 = str_span[1..5];

            var fragment4 = str[1..5];

            var str2 = new string(fragment2);

            //var vectors_span = Span < Vector2D >.Empty

            ValueTuple<string, int> v = ("123", 123);

            Tuple<string, int> ref_v = new ("123", 123);

            Console.WriteLine("Готово");
            Console.ReadLine();
        }

        private static void Print(in Vector2D vector)
        {
            Console.WriteLine($"{vector.X}:{vector.Y}");
        }

        public static void Print2(ref Vector2D vector)
        {
            vector = new();
            Console.WriteLine($"{vector.X}:{vector.Y}");
        }

        public static void CreateVector(out Vector2D vector)
        {
            vector = new(5, 7);
        }

        public static ref Vector2D GetMaxValue(Vector2D[] vectors)
        {
            var max = double.NegativeInfinity;
            var max_index = 0;

            for (var i = 0; i < vectors.Length; i++)
            {
                var length = vectors[i].Length;
                if (length > max)
                {
                    max = length;
                    max_index = i;
                }
            }

            return ref vectors[max_index];
        }

        public static (Func<Vector2D> Getter, Action<Vector2D> Setter) GetMaxValue2(Vector2D[] vectors)
        {
            var max = double.NegativeInfinity;
            var max_index = 0;

            for (var i = 0; i < vectors.Length; i++)
            {
                var length = vectors[i].Length;
                if (length > max)
                {
                    max = length;
                    max_index = i;
                }
            }

            return (() => vectors[max_index], v => vectors[max_index] = v);
        }
    }

    public record class Student(string LastName, string Name, string Patronymic);

    public record struct Student2(string LastName, string Name, string Patronymic);

    public class Pilot : ICloneable
    {
        public string Name { get; set; }

        public object Clone() => MemberwiseClone();
    }

    public class Car : IEquatable<Car>, ICloneable
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

        public Pilot Pilot { get; set; }

        public Car(string Name, Vector2D Location = default)
        {
            this.Name = Name;
            _Location = Location;
        }

        public Car(Car other)
        {
            _Speed = other._Speed;
            _Acceleration = other._Acceleration;
            _Location = other._Location;
            Name = other.Name;
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

        public bool Equals(Car other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _Location.Equals(other._Location)
                && _Speed.Equals(other._Speed)
                && _Acceleration.Equals(other._Acceleration)
                && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            //if (obj.GetType() != this.GetType()) return false;
            //return Equals((Car)obj);
            return obj is Car car && Equals(car);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_Location, _Speed, _Acceleration, Name);
        }

        public static bool operator ==(Car left, Car right) => Equals(left, right);
        public static bool operator !=(Car left, Car right) => !Equals(left, right);

        public object Clone()
        {
            var result = (Car)MemberwiseClone();
            result.Pilot = (Pilot)Pilot.Clone();
            return result;
        }
    }

    /// <summary>Двумерный вектор</summary>
    /// <remarks>16 байт</remarks>
    public readonly /*ref*/ struct Vector2D : IEquatable<Vector2D> //IStructuralEquatable
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

        public override bool Equals(object obj)
        {
            //var other = (Vector2D)obj;
            //return _X == other._X && _Y == other._Y;
            return obj is Vector2D other && Equals(other);
        }

        public bool Equals(Vector2D other)
        {
            return _X == other._X && _Y == other._Y;
        }

        public static bool operator ==(Vector2D v1, Vector2D v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector2D v1, Vector2D v2)
        {
            return !(v1 == v2);
        }

        public override int GetHashCode()
        {
            //int hash;
            //unchecked
            //{
            //    hash = _X.GetHashCode();
            //    hash = (hash * 139) ^ _Y.GetHashCode();
            //}

            //return hash;
            return HashCode.Combine(_X, _Y);
        }
    }

}
