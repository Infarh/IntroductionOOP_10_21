//#define IncludeInternal

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;

namespace IntroductionOOP
{
    public record Person(string LastName, string FirstName, string Patronymic);

    class Program
    {
        static void Main(string[] args)
        {
            const string file_name = "Names.txt";

            ReadData(file_name, out var last, out var first, out var patron);

            var rnd = new Random(5);
            var persons = CreatePersons(1000, first, last, patron, rnd).ToArray();
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

                last.Add(components[0]);
                first.Add(components[1]);
                patron.Add(components[2]);
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
}
