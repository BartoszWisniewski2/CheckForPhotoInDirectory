using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CheckForPhotoInDirectory
{
    public static class Utilities
    {
        public static Boolean checkIfFileIsZipArchive(String filePath)
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
        }
        public static void unPack(String archivePatch, string extractPath)
        {
            cleanDirectoryIfFolderExists(extractPath);
            ZipFile.ExtractToDirectory(archivePatch, extractPath);
        }
        public static void cleanDirectoryIfFolderExists(string extractPath)
        {
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, recursive: true);
            }
        }
        public static string[] getListOfAllFilesDirectories(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            return files;
        }

    }
}
