using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CheckForPhotoInDirectory.Tests
{
    public class UtilitiesShould
    {
        //CheckIfFileIsZipArchive
        [Fact]
        public void ShouldDetectZipArchiveWithoutExtension()
        {
            String filePath = @".\Data\zipArchiveWithFiles";
            var result = Utilities.CheckIfFileIsZipArchive(filePath);
            Assert.True(result);
        }
        [Fact]
        public void ShouldDetectZipArchiveWithExtension()
        {
            String filePath = @".\Data\zipArchiveWithFiles.zip";
            var result = Utilities.CheckIfFileIsZipArchive(filePath);
            Assert.True(result);
        }
        [Fact]
        public void ShouldDetectNotZipArchiveWithoutExtension()
        {
            String filePath = @".\Data\tif";
            var result = Utilities.CheckIfFileIsZipArchive(filePath);
            Assert.False(result);
        }
        [Fact]
        public void ShouldDetectNotZipArchiveWithExtension()
        {
            String filePath = @".\Data\tif.tif";
            var result = Utilities.CheckIfFileIsZipArchive(filePath);
            Assert.False(result);
        }

        //UnPackZipArchive
        [Fact]
        public void ShouldUnpackZipArchiveWithoutExtension()
        {
            String filePath = @".\Data\zipArchiveWithFiles";
            String extractPath = @".\Data\ExtractTemp";

            Utilities.UnPackZipArchive(filePath, extractPath);
            var result = Directory.Exists(extractPath);
            Utilities.CleanDirectoryIfFolderExists(extractPath);

            Assert.True(result);
        }
        [Fact]
        public void ShouldUnpackZipArchiveWithExtension()
        {
            String filePath = @".\Data\zipArchiveWithFiles";
            String extractPath = @".\Data\ExtractTemp.zip";

            Utilities.UnPackZipArchive(filePath, extractPath);
            var result = Directory.Exists(extractPath);
            Utilities.CleanDirectoryIfFolderExists(extractPath);

            Assert.True(result);
        }

        //CleanDirectoryIfFolderExists
        [Fact]
        public void ShouldCleanDirectory()
        {
            String directory = @".\Data\Directory\Test";
            Directory.CreateDirectory(directory);

            Utilities.CleanDirectoryIfFolderExists(directory);
            var result = !Directory.Exists(directory);

            Assert.True(result);
        }

        //GetListOfAllFilesInFolderAndSubfolder
        [Fact]
        public void SchouldGetListOfAllFiles()
        {
            String directory = @".\Data\folderWithFiles";
            string[] expectedResult = new string[] { ".\\Data\\folderWithFiles\\docmEmpty",
                ".\\Data\\folderWithFiles\\docmEmpty.docm", ".\\Data\\folderWithFiles\\subFolder\\jpg" };

            var result = Utilities.GetListOfAllFilesInFolderAndSubfolder(directory);

            Assert.Equal(expectedResult, result);
        }

        //CreateListOfFileObjects
        [Fact]
        public void SchouldCreateListOfFileObjects()
        {
            String[] directories = new string[] { @".\Data\one", @".\Data\two", @".\Data\three" };

            var result = Utilities.CreateListOfFileObjects(directories).Count;
            
            Assert.Equal(3, result);
        }

    }
}
