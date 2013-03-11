using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualStudio.Zip;
using ZipEntry = ICSharpCode.SharpZipLib.Zip.ZipEntry;

namespace Library.Scan
{
    public class ZipUtil
    {
        public static void CompressFolder(string folder, string zipPath, string pwd)
        {
            using (var s = new ZipOutputStream(File.Create(zipPath)))
            {
                if (!String.IsNullOrEmpty(pwd))
                    s.Password = pwd;

                s.SetLevel(9); // 0 - store only to 9 - means best compression
                var buffer = new byte[4096];
                var files = Directory.GetFiles(folder);
                foreach (var file in files)
                {
                    var entry = new ZipEntry(Path.GetFileName(file));
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);
                    using (var fs = File.OpenRead(file))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                s.Finish();
                s.Close();
            }
        }

        public static void CompressFolder(string folder, string zipPath)
        {
            var files = Directory.GetFiles(folder);
            for(var i = 0; i < files.Length; i++)
                files[i] = Path.GetFileName(files[i]);
            var zipFileComp = new ZipFileCompressor(zipPath, folder, files, false, false);
        }

        public static void Uncompress(string zipFile, string folder)
        {
            new ZipFileDecompressor(zipFile, false).UncompressToFolder(folder, false);
        }
    }
}
