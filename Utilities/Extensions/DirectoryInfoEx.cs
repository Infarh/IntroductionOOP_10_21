using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities.Extensions
{
    public static class DirectoryInfoEx
    {
        public static long GetSize(this DirectoryInfo dir)
        {
            return dir.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(f => f.Length);
        }

        public static int GetFilesCount(this DirectoryInfo dir) => dir.EnumerateFiles("*.*", SearchOption.AllDirectories).Count();

        public static FileSystemWatcher GetWatcher(this DirectoryInfo dir, string? Filter = null) =>
            Filter is { Length: > 0 }
                ? new(dir.FullName, Filter)/* { EnableRaisingEvents = true }*/
                : new(dir.FullName)/* { EnableRaisingEvents = true }*/;

        public static FileInfo Zip(this DirectoryInfo dir, string? ArchiveFileName = null)
        {
            ArchiveFileName ??= $"{dir.FullName}.zip";

            using var zip = File.Exists(ArchiveFileName)
                ? new ZipArchive(File.OpenRead(ArchiveFileName), ZipArchiveMode.Update)
                : new ZipArchive(File.Create(ArchiveFileName), ZipArchiveMode.Create);

            var dir_path = dir.FullName;
            foreach (var file in dir.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                var relative_file_path = file.FullName[dir_path.Length..];
                var file_entry = zip.CreateEntry(relative_file_path, CompressionLevel.SmallestSize);
                using var zip_stream = file_entry.Open();
                using var file_stream = file.OpenRead();
                file_stream.CopyTo(zip_stream);
            }

            return new(ArchiveFileName);
        }

        public static async Task<FileInfo> ZipAsync(this DirectoryInfo dir, string? ArchiveFileName = null, CancellationToken Cancel = default)
        {
            await Task.Yield();

            ArchiveFileName ??= $"{dir.FullName}.zip";

            using var zip = File.Exists(ArchiveFileName)
                ? new ZipArchive(File.OpenRead(ArchiveFileName), ZipArchiveMode.Update)
                : new ZipArchive(File.Create(ArchiveFileName), ZipArchiveMode.Create);

            var dir_path = dir.FullName;
            foreach (var file in dir.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                var relative_file_path = file.FullName[(dir_path.Length + 1)..];
                var file_entry = zip.CreateEntry(relative_file_path, CompressionLevel.SmallestSize);
                await using var zip_stream = file_entry.Open();
                await using var file_stream = file.OpenRead();
                await file_stream.CopyToAsync(zip_stream, Cancel).ConfigureAwait(false);
            }

            return new(ArchiveFileName);
        }
    }
}
