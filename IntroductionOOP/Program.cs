global using System;
global using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using IntroductionOOP.Extensions;
using Microsoft.VisualBasic;
using Utilities.Entities;
using Utilities.Extensions;

if (args.Contains("/admin"))
{
    Console.WriteLine("As admin");
    Console.ReadLine();
    return;
}

var file = new FileInfo("Names.txt");

var large_files_dir = new DirectoryInfo(@"C:\123\123");
(await large_files_dir.ZipAsync()).ShowInExplorer();
Console.WriteLine("Упаковано!");

Console.ReadLine();


var names_zip = await file.ZipAsync();
names_zip.ShowInExplorer();

Console.ReadLine();


//var lines_count = file.EnumLinesAsync().CountAsync();

//await Task.Run(
//    async () =>
//    {
//        await foreach (var line in file.EnumLinesAsync().ConfigureAwait(false))
//        {
//            //var thread_id = Thread.CurrentThread.ManagedThreadId;
//            var thread_id = Environment.CurrentManagedThreadId;
//            Console.WriteLine("thread id:{0} - {1}", thread_id, line);
//        }
//    });

//var lines_count = file.EnumLines().Count(l => l.Length > 5);
//var words_count = file.EnumLines().Sum(l => l.Split(' ').Length);

var md5 = file.ComputeMD5();

//var md5_str = Convert.ToBase64String(md5);
var md5_str = string.Join("", md5.Select(b => b.ToString("X2")));

Console.WriteLine(md5_str);

//file.ShowInExplorer();

Console.ReadLine();


//file.Execute(UseShellExecute: true);

//Console.ReadLine();

//var text_reader = file.OpenText();
//var bin_reader = file.OpenBinary(Encoding.ASCII);

var dir = new DirectoryInfo("c:\\123");

//var length_task = Task.Run(() => dir.EnumerateFiles("*.txt", SearchOption.AllDirectories)
//   .Sum(f => f.Length));
//// делаем что-то пока считается размер
//var length = await length_task;

//FileSystemInfo fsi = file;

//var dirs = dir.EnumerateFileSystemInfos().OfType<DirectoryInfo>();
//var files = dir.EnumerateFileSystemInfos().OfType<FileInfo>();

var drive_c = new DriveInfo("c:");
var drives = DriveInfo.GetDrives();

//drive_c.RootDirectory

//var watcher = new FileSystemWatcher(@"c:\\123", "*.txt");
//watcher.InternalBufferSize = 1024;

//void OnFileChanged(object s, FileSystemEventArgs e)
//{
//    var file = new FileInfo(e.FullPath);
//    for(var i = 0; i < 5; i++)
//        try
//        {
//            using var reader = file.OpenText();
//            while (!reader.EndOfStream)
//            {
//                var line = reader.ReadLine();
//                Console.WriteLine(line);
//            }
//        }
//        catch (IOException)
//        {
//            Console.WriteLine("Ошибка доступа к файлу {0}", file.FullName);
//            if (i == 4)
//                throw;
//        }
//}

//watcher.Created += OnFileChanged;
//watcher.Changed += OnFileChanged;

//watcher.EnableRaisingEvents = true;

//var gif_cam = new FileInfo(@"c:\service\GifCam.exe");

var new_file_ext = Path.ChangeExtension("c:\\123\test.txt", ".exe");
Path.GetFileNameWithoutExtension("c:\\123\test.txt"); // "c:\\123\test" + ".zip"

//Path.Combine()
new FileInfo(Path.ChangeExtension(typeof(Program).Assembly.Location, ".exe"))
   .ExecuteAsAdmin("/admin");

//var gif_cam_process = gif_cam.ExecuteAsAdmin();

//Console.ReadLine();

//gif_cam_process.CloseMainWindow();

Console.ReadLine();
