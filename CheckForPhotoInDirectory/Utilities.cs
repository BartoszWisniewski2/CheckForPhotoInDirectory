using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace CheckForPhotoInDirectory
{
    public static class Utilities
    {
        private enum FileType
        {
            Jpeg,
            Png
        }

        private static readonly Dictionary<FileType, byte[]> knownFileHeaders = new Dictionary<FileType, byte[]>()
        {
            { FileType.Jpeg, new byte[]{ 0xFF, 0xD8 }},
            { FileType.Png, new byte[]{ 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }},
        };

        public static Boolean CheckIfFileIsZipArchive(String filePath)
        {
            try
            {
                using (var zipFile = ZipFile.OpenRead(filePath))
                {
                    var entries = zipFile.Entries;
                    return true;
                }
            }
            catch (InvalidDataException)
            {
                return false;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Brak pliku w podanej lokalizacji");
                return false;
            }
        }
        public static void UnPackZipArchive(String archivePatch, string extractPath)
        {
            CleanDirectoryIfFolderExists(extractPath);
            ZipFile.ExtractToDirectory(archivePatch, extractPath);
        }
        public static void CleanDirectoryIfFolderExists(string extractPath)
        {
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, recursive: true);
            }
        }
        public static string[] GetListOfAllFilesInFolderAndSubfolder(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            return files;
        }
        public static List<FileToBeCheckedForPhoto> CreateListOfFileObjects(String[] listOfDirectories)
        {
            List<FileToBeCheckedForPhoto> listOfFiles = new List<FileToBeCheckedForPhoto>();
            foreach (var item in listOfDirectories)
            {
                listOfFiles.Add(new FileToBeCheckedForPhoto(item));
            }
            return listOfFiles;
        }
        public static Boolean CheckIfFileIsImageType(FileStream filestream)
        {
            foreach (var fileHeader in knownFileHeaders)
            {
                filestream.Seek(0, SeekOrigin.Begin);

                var slice = new byte[fileHeader.Value.Length];
                filestream.Read(slice, 0, fileHeader.Value.Length);
                if (slice.SequenceEqual(fileHeader.Value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
