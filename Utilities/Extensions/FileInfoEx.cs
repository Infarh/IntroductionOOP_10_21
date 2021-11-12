using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
    public static class FileInfoEx
    {
        public static BinaryReader OpenBinary(this FileInfo file) => new(file.OpenRead());
        public static BinaryReader OpenBinary(this FileInfo file, Encoding? encoding) => new(file.OpenRead(), encoding ?? Encoding.UTF8);

        public static Process? Execute(this FileInfo file, bool UseShellExecute = false)
        {
            var info = new ProcessStartInfo(file.FullName)
            {
                UseShellExecute = UseShellExecute,
                //Arguments = "/help",
                //UserName = "admin",
            };

            var process = Process.Start(info);

            return process;
        }

        public static Process? ExecuteAsAdmin(this FileInfo file, string Args = "", bool UseShellExecute = true)
        {
            var info = new ProcessStartInfo(file.FullName, Args)
            {
                UseShellExecute = UseShellExecute,
                Verb = "runas"
            };

            var process = Process.Start(info);

            return process;
        }

        public static Process? ShowInExplorer(this FileInfo file) => Process.Start("explorer", $"/select,{file.FullName}");

        public static byte[] ComputeSHA256(this FileInfo file)
        {
            using var stream = file.OpenRead();
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(stream);
        }

        public static byte[] ComputeMD5(this FileInfo file)
        {
            using var stream = file.OpenRead();
            using var md5 = MD5.Create();
            return md5.ComputeHash(stream);
        }

        public static IEnumerable<string> EnumLines(this FileInfo file)
        {
            using var reader = file.OpenText();
            while (!reader.EndOfStream)
                yield return reader.ReadLine() ?? throw new InvalidOperationException("Что-то пошло не так");
        }

        public static async IAsyncEnumerable<string> EnumLinesAsync(this FileInfo file, [EnumeratorCancellation] CancellationToken Cancel = default)
        {
            if (file is null) throw new ArgumentNullException(nameof(file));

            using var reader = file.OpenText();
            while (!reader.EndOfStream)
            {
                Cancel.ThrowIfCancellationRequested();
                yield return (await reader.ReadLineAsync().ConfigureAwait(false))!;
            }
        }

        public static FileInfo Zip(this FileInfo file, string? ArchiveFileName = null, bool Override = true)
        {
            ArchiveFileName ??= $"{file.FullName}.zip";

            using var zip_stream = File.Open(ArchiveFileName, FileMode.Create, FileAccess.Write);
            using var zip = new ZipArchive(zip_stream, ZipArchiveMode.Create);

            //var file_entry = zip.GetEntry(file.Name);
            //if (file_entry != null)
            //{
            //    if (!Override)
            //        return new(ArchiveFileName);
            //    file_entry.Delete();
            //}

            using var file_entry_stream = zip.CreateEntry(file.Name).Open();
            using var file_stream = file.OpenRead();
            file_stream.CopyTo(file_entry_stream);

            return new(ArchiveFileName);
        }

        public static async Task<FileInfo> ZipAsync(this FileInfo file, string? ArchiveFileName = null, bool Override = true, CancellationToken Cancel = default)
        {
            ArchiveFileName ??= $"{file.FullName}.zip";

            await using var zip_stream = File.Open(ArchiveFileName, FileMode.Create, FileAccess.Write);
            using var zip = new ZipArchive(zip_stream, ZipArchiveMode.Create);

            //var file_entry = zip.GetEntry(file.Name);
            //if (file_entry != null)
            //{
            //    if (!Override)
            //        return new(ArchiveFileName);
            //    file_entry.Delete();
            //}

            await using var file_entry_stream = zip.CreateEntry(file.Name).Open();
            await using var file_stream = file.OpenRead();
            await file_stream.CopyToAsync(file_entry_stream, Cancel).ConfigureAwait(false);

            return new(ArchiveFileName);
        }
    }
}
