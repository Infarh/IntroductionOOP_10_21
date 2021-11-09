global using System;
global using System.Linq;
using System.Collections.Generic;
using System.IO;
using IntroductionOOP.Extensions;
using Utilities.Entities;

//string text;
//new StreamReader("Names.txt").DisposeAfter(
//    reader =>
//    {
//        text = reader.ReadToEnd();
//    });


var text = new StreamReader("Names.txt").DisposeAfter(reder => reder.EnumLines().ToArray());