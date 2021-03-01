using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CheckForPhotoInDirectory
{
    class ZipArchive
    {
        /// <summary>
        /// Checks if the file is a zip archive
        /// </summary>
        /// <param name="filePath">Path to the zip archive</param>
        public Boolean IsArchive(String filePath)
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
        /// <param name="archivePath">Path to the zip archive</param>
        /// <param name="extractPath">Path where the zip archive should be extracted</param>
        public void UnPackArchive(String archivePath, string extractPath)
        {
            ZipFile.ExtractToDirectory(archivePath, extractPath);
        }
    }
}
