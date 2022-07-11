
using System;
using System.Collections.Generic;
using Utilities.Entities;

var students = new List<Student>
{
    new()
    {
        LastName = "Иванов",
        Name = "Иван",
        Patronymic = "Иванович",
        Birthday = DateTime.Now.AddYears(-23)
    }
};

var test_student = new Student
{
    LastName = "Иванов",
    Name = "Иван",
    Patronymic = "Иванович",
    Birthday = DateTime.Now.AddYears(-23)
};

var test_hash_set = new HashSet<Student>();
test_hash_set.Add(test_student);

test_hash_set.Add(students[0]);

var contains = students.Contains(test_student);

//var group = new StudentGroup("GroupName");
//group.Add(new Student
//{
//    LastName = "Иванов",
//    Name = "Иван",9=
//    Patronymic = "Иванович",
//    Birthday = DateTime.Now.AddYears(-23)
//});
//group.Add("Петров", "Пётр", "Петрович", DateTime.Now.AddYears(-18));
var group = new StudentGroup("GroupName")
{
    { new() { LastName = "Иванов", Name = "Иван", Patronymic = "Иванович", Birthday = DateTime.Now.AddYears(-23) } },
    { "Петров", "Пётр", "Петрович", DateTime.Now.AddYears(-18) },
    //Description = "123"
};

group.Description = "Тестовая группа";

var decanat = new Decanat
{
    new Cource { Name = "Математика" },
    new Lector { LastName = "Вестяк" },
    { new Student { LastName = "Сидоров" }, "Группа-2" },
    "123"
};
