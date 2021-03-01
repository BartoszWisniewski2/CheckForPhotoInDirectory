using System.IO;

namespace CheckForPhotoInDirectory
{
    public interface IFileOpener
    {
        FileStream OpenFileStream(string filePath);
    }
}