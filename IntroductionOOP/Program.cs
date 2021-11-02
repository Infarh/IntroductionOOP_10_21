//#define IncludeInternal

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Utilities;
using Utilities.Extensions;

namespace IntroductionOOP
{
    [Serializable]
    public record Person(string LastName, string FirstName, string Patronymic)
    {
        public Person() : this("", "", "")
        {
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string file_name = "Names.txt";

            ReadData(file_name, out var last, out var first, out var patron);

            var rnd = new Random(5);
            var persons = CreatePersons(1000, first, last, patron, rnd).ToArray();

            var xml_serializer = new XmlDataSerializer<Person>();
            var json_serializer = new JsonDataSerializer<Person>();
            var bin_serializer = new BinDataSerializer<Person>();

            Serialize("FirstPerson.xml", persons[0], xml_serializer);
            Serialize("SecondPerson.json", persons[1], json_serializer);
            Serialize("Person3.bin", persons[3], bin_serializer);

            var vector = new Vector3D(1, 2, 3);

            var v2 = vector * 5;
            var v3 = 6 * vector;

            var len = (double)vector;

            var (x, y, z) = vector;
        }

        private static void Serialize(string FileName, Person person, DataSerializer<Person> serializer)
        {
            using var file = File.Create(FileName);
            serializer.Serialize(person, file);
        }

        private static void ReadData(
            string FileName,
            out string[] LastNames,
            out string[] FirstNames,
            out string[] Patronymics)
        {
            var last = new List<string>(100);
            var first = new List<string>(100);
            var patron = new List<string>(100);

            foreach (var line in ReadLines(FileName).Where(line => line.Length > 0))
            {
                var components = line.Split(' ');
                if (components.Length < 3)
                    continue;

                //var (f, l, p) = components;
                string last_name;
                (var first_name, last_name, var patronymic_name) = components;
                //components.Deconstruct(out var first_name, out last_name, out var patronymic_name);

                last.Add(last_name);
                first.Add(first_name);
                patron.Add(patronymic_name);
            }

            //last.Capacity = last.Count;
            //last.TrimExcess();

            LastNames = last.ToArray();
            FirstNames = first.ToArray();
            Patronymics = patron.ToArray();
        }

        private static IEnumerable<string> ReadLines(string FileName)
        {
            var file = new FileInfo(FileName);
            if (!file.Exists) throw new FileNotFoundException("Файл данных не найден", FileName);

            using var reader = file.OpenText();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                yield return line;
            }
        }

        private static IEnumerable<Person> CreatePersons(
            int Count,
            string[] LastNames,
            string[] FirstNames,
            string[] Patronymics,
            Random rnd = null)
        {
            rnd ??= new();

            //var rnd = new Random((int)DateTime.Now.Ticks);

            //var result = new Person[Count];

            ////

            //return result;

            for (var i = 0; i < Count; i++)
            {
                var last_name = LastNames[rnd.Next(0, LastNames.Length)];
                var first_name = FirstNames[rnd.Next(0, LastNames.Length)];
                var patronymic = Patronymics[rnd.Next(0, LastNames.Length)];

                yield return new Person(last_name, first_name, patronymic);
            }
        }
    }

    public abstract class DataSerializer<T>
    {
        public abstract void Serialize(T value, Stream stream);

        public abstract T Deserialize(Stream stream);
    }

    public static class XmlSerializersPool
    {
        private static readonly ConcurrentDictionary<Type, XmlSerializer> __Serializers = new();

        public static XmlSerializer GetSerializer(Type type) => __Serializers.GetOrAdd(type, t => new XmlSerializer(t));

        public static XmlSerializer GetSerializer<T>() => GetSerializer(typeof(T));

        public static XmlSerializer GetSerializer(object value) => GetSerializer(value.GetType());

        public static void Serialize(object value, Stream stream) => GetSerializer(value).Serialize(stream, value);

        public static T Deserialize<T>(Stream stream) => (T)GetSerializer<T>().Deserialize(stream);
    }

    public class XmlDataSerializer<T> : DataSerializer<T>
    {
        //private XmlSerializer _Serializer = XmlSerializersPool.GetSerializer<T>();

        //public XmlDataSerializer()
        //{
        //    _Serializer = XmlSerializersPool.GetSerializer(typeof(T));
        //}

        public override void Serialize(T value, Stream stream) => XmlSerializersPool.Serialize(value, stream);

        public override T Deserialize(Stream stream) => XmlSerializersPool.Deserialize<T>(stream);
    }

    public class JsonDataSerializer<T> : DataSerializer<T>
    {
        private readonly Encoding _Encoding;

        //public JsonDataSerializer() => _Encoding = Encoding.UTF8;
        public JsonDataSerializer() : this(Encoding.UTF8) { }

        public JsonDataSerializer(Encoding encoding) => _Encoding = encoding;

        public override void Serialize(T value, Stream stream)
        {
            var str = JsonSerializer.Serialize(value);
            //var encoding = Encoding.UTF8;
            var bytes = _Encoding.GetBytes(str);
            stream.Write(bytes);
        }

        public override T Deserialize(Stream stream)
        {
            //using var other_reader = new BinaryReader(stream, _Encoding, true);
            var reader = new StreamReader(stream, _Encoding);
            var str = reader.ReadToEnd();
            var result = (T)JsonSerializer.Deserialize(str, typeof(T));
            return result;

            //using (var reader = new StreamReader(stream))
            //{
            //    //...
            //}

            //StreamReader reader = null;
            //try
            //{
            //    reader = new StreamReader(stream);

            //    // ...
            //}
            //finally
            //{
            //    reader?.Dispose();
            //}

        }
    }

    public class BinDataSerializer<T> : DataSerializer<T>
    {
        private BinaryFormatter _Formatter = new();

        public override void Serialize(T value, Stream stream)
        {
#pragma warning disable SYSLIB0011 // Тип или член устарел
#pragma warning disable 618
            _Formatter.Serialize(stream, value);
#pragma warning restore 618
#pragma warning restore SYSLIB0011 // Тип или член устарел
        }

        public override T Deserialize(Stream stream)
        {
#pragma warning disable SYSLIB0011 // Тип или член устарел
#pragma warning disable 618
            return (T)_Formatter.Deserialize(stream);
#pragma warning restore 618
#pragma warning restore SYSLIB0011 // Тип или член устарел
        }
    }
}
