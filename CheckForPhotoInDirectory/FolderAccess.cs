using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CheckForPhotoInDirectory
{
    class FolderAccess
    {
        /// <summary>
        /// Deletes a directory with all folders and files in it if it exists
        /// </summary>
        /// <param name="directoryPath">Directory to the folder</param>
        public void CleanDirectoryIfFolderExists(string directoryPath)
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
        public string[] GetListOfAllFilesInFolderAndSubfolder(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            return files;
        }
    }
}
