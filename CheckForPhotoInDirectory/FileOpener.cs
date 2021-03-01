using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CheckForPhotoInDirectory
{   class FileOpener : IFileOpener
    {
        public FileStream OpenFileStream(string filePath)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.Open(filePath, FileMode.Open);
                return fileStream;
            }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx);
            }
            return fileStream;
        }
    }
}