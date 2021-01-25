using System;
using System.Collections.Generic;

namespace CheckForPhotoInDirectory
{
    class CheckForPhoto
    {
        static void Main(string[] args)
        {
            //Path to extract zip file
            string extractPath = @".\extractPath"; 

            try
            { 
                List<FileToBeCheckedForPhoto> listOfFileObjects = new List<FileToBeCheckedForPhoto>();

                if (args.Length == 0)
                {
                    Console.WriteLine("Podaj ścieżkę do pliku");
                }
                // If the given directory is Zip archive, unpack it and create list of objects of all files that it contains
                else if (Utilities.CheckIfFileIsZipArchive(args[0]))
                {
                    Utilities.UnPackZipArchive(args[0], extractPath);
                    String[] listOfFilesDirectories = Utilities.GetListOfAllFilesInFolderAndSubfolder(extractPath);
                    listOfFileObjects = Utilities.CreateListOfFileObjects(listOfFilesDirectories);
                }
                // If it's not zip archive then it's signle file that needs to be checked
                else
                {
                    listOfFileObjects.Add(new FileToBeCheckedForPhoto(args[0]));
                }
                // All files are being checked for being jpg or png image or did they contain jpg or png image
                foreach (var item in listOfFileObjects)
                {
                    if (item.CheckForImageInDocFile() || item.CheckIfFileIsImage() || 
                        item.CheckForImageInPowerPointFile() || item.CheckForImageInSpreadsheetFile()){}
                    Console.WriteLine(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Delete prevoriusly created folder
                Utilities.CleanDirectoryIfFolderExists(extractPath);
            }
        }
    }
}
