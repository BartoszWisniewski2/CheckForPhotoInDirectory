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

        /// <summary>
        /// Checks if the file is a zip archive
        /// </summary>
        /// <param name="filePath">Path to the zip archive</param>
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
        /// <summary>
        /// Unpacks the zip archive
        /// </summary>
        /// <param name="archivePatch">Patch to the zip archive</param>
        /// <param name="extractPath">Patch where the zip archive should be extracted</param>
        public static void UnPackZipArchive(String archivePatch, string extractPath)
        {
            CleanDirectoryIfFolderExists(extractPath);
            ZipFile.ExtractToDirectory(archivePatch, extractPath);
        }
        /// <summary>
        /// Deletes a directory with all folders and files in it if it exists
        /// </summary>
        /// <param name="directoryPath">Directory to the folder</param>
        public static void CleanDirectoryIfFolderExists(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, recursive: true);
            }
        }

        /// <summary>
        /// Returns all files in fiven directory and subfolders
        /// </summary>
        /// <param name="directoryPath">Directory to be checked for files</param>
        /// <returns>Table of File Paths</returns>
        public static string[] GetListOfAllFilesInFolderAndSubfolder(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            return files;
        }
        
        /// <summary>
        /// Creates List of FileToBeCheckedForPhoto class objects
        /// </summary>
        /// <param name="listOfDirectories">Table of file paths</param>
        /// <returns>List of FileToBeCheckedForPhoto objects</returns>
        public static List<FileToBeCheckedForPhoto> CreateListOfFileObjects(String[] listOfDirectories)
        {
            List<FileToBeCheckedForPhoto> listOfFiles = new List<FileToBeCheckedForPhoto>();
            foreach (var item in listOfDirectories)
            {
                listOfFiles.Add(new FileToBeCheckedForPhoto(item));
            }
            return listOfFiles;
        }

        /// <summary>
        /// Checks if given file is Jpg or Png Image
        /// </summary>
        /// <param name="filestream">Filestream of a file to be checked</param>
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
