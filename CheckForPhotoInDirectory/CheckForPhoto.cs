using System;
using System.Collections.Generic;
using System.IO;

namespace CheckForPhotoInDirectory
{
    class CheckForPhoto
    {
        static void Main(string[] args)
        {
            //Path to extract zip file
            string extractPath = @".\extractPath";
            FileOpener fileOpener = new FileOpener();
            ZipArchive zipArchive = new ZipArchive();
            FolderAccess folder = new FolderAccess();
            LoggerToConsole logger = new LoggerToConsole();
            CheckForImageInFile checkForImageInFile = new CheckForImageInFile(fileOpener);
            List<String> listOfFilePaths = new List<string>();
            try
            {
                folder.CleanDirectoryIfFolderExists(extractPath);

                if (args.Length == 0)
                {
                    Console.WriteLine("Podaj ścieżkę do pliku");
                }
                // If the given directory is Zip archive, unpack it and create list of objects of all files that it contains
                else if (zipArchive.IsArchive(args[0]))
                {
                    zipArchive.UnPackArchive(args[0], extractPath);
                    foreach (var item in folder.GetListOfAllFilesInFolderAndSubfolder(extractPath))
                    {
                        listOfFilePaths.Add(item);
                    }
                }
                // If it's not zip archive then it's signle file that needs to be checked
                else
                {
                    listOfFilePaths.Add(args[0]);
                }
                // All files are being checked for being jpg or png image or did they contain jpg or png image
                foreach (var item in listOfFilePaths)
                {
                    if (checkForImageInFile.CheckForImageInDocFile(item) || checkForImageInFile.CheckIfFileIsImage(item) ||
                        checkForImageInFile.CheckForImageInPowerPointFile(item) || checkForImageInFile.CheckForImageInSpreadsheetFile(item))
                    {
                        logger.Log(Path.GetFileName(item) + " | " + "True");
                    }
                    else
                    {
                        Console.WriteLine(Path.GetFileName(item) + " | " + "False");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Delete prevoriusly created folder
                folder.CleanDirectoryIfFolderExists(extractPath);
            }
        }
    }
}
