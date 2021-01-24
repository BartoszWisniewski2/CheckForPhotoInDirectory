using System;
using Xunit;

namespace CheckForPhotoInDirectory.Tests
{
    public class FileToBeCheckedForPhotoShould
    {
        [Theory]
        [InlineData(@"C:\Users\fileOne.txt", "fileOne.txt")]
        [InlineData(@"C:\Users\fileTwo", "fileTwo")]
        public void FileNameIsCorrectlyAssigned(string fileDirectory, string expectedFileName)
        {
            var file = new FileToBeCheckedForPhoto(fileDirectory);
            Assert.Equal(file.FileName, expectedFileName);
        }
        [Theory]
        [InlineData(@"C:\Users\fileOne.txt", @"C:\Users\fileOne.txt")]
        [InlineData(@"C:\Users\fileTwo", @"C:\Users\fileTwo")]
        public void FileDirectoryIsCorrectlyAssigned(string fileDirectory, string expectedDirectory)
        {
            var file = new FileToBeCheckedForPhoto(fileDirectory);
            Assert.Equal(file.FileDirectory, expectedDirectory);
        }
        [Theory]
        [InlineData(@"C:\Users\fileOne.txt")]
        [InlineData(@"C:\Users\fileTwo")]
        public void FileContainsPhotoPropertyIsFalseByDefault(string fileDirectory)
        {
            var file = new FileToBeCheckedForPhoto(fileDirectory);
            Assert.False(file.ContainsPhoto);
        }

        //jpg
        [Fact]
        public void FileCanDetectJpgImageWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\jpg");
            file.CheckIfFileIsImage();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\jpg.jpg");
            file.CheckIfFileIsImage();
            Assert.True(file.ContainsPhoto);
        }

        //png
        [Fact]
        public void FileCanDetectPngImageWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\png");
            file.CheckIfFileIsImage();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\png.png");
            file.CheckIfFileIsImage();
            Assert.True(file.ContainsPhoto);
        }

        //tif
        [Fact]
        public void FileCantDetectTifImageWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\tif");
            file.CheckIfFileIsImage();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCantDetectTifImageWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\tif.tif");
            file.CheckIfFileIsImage();
            Assert.False(file.ContainsPhoto);
        }

        //xlsm
        [Fact]
        public void FileCantDetectImageInEmptyXlsmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsmEmpty");
            file.CheckForImageInSpreadsheetFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCantDetectImageInEmptyXlsmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsmEmpty.xlsm");
            file.CheckForImageInSpreadsheetFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInXlsmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsmWithJpgOnSecondSheet");
            file.CheckForImageInSpreadsheetFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInXlsmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsmWithJpgOnSecondSheet.xlsm");
            file.CheckForImageInSpreadsheetFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInXlsmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsmWithPngOnSecondSheet");
            file.CheckForImageInSpreadsheetFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInXlsmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsmWithPngOnSecondSheet.xlsm");
            file.CheckForImageInSpreadsheetFile();
            Assert.True(file.ContainsPhoto);
        }

        //xlsx
        [Fact]
        public void FileCantDetectImageInEmptyXlsxFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsxEmpty");
            file.CheckForImageInSpreadsheetFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCantDetectImageInEmptyXlsxFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsxEmpty.xlsx");
            file.CheckForImageInSpreadsheetFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInXlsxFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsxWithPngOnSecondSheet");
            file.CheckForImageInSpreadsheetFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInXlsxFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\xlsxWithPngOnSecondSheet.xlsx");
            file.CheckForImageInSpreadsheetFile();
            Assert.True(file.ContainsPhoto);
        }

        //docx
        [Fact]
        public void FileCantDetectImageInEmptyDocxFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docxEmpty");
            file.CheckForImageInDocFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCantDetectImageInEmptyDocxFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docxEmpty.docx");
            file.CheckForImageInDocFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInDocxFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docxWithJpgOnSecondPage");
            file.CheckForImageInDocFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInDocxFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docxWithJpgOnSecondPage.docx");
            file.CheckForImageInDocFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInDocxFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docxWithPngOnSecondPage");
            file.CheckForImageInDocFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInDocxFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docxWithPngOnSecondPage.docx");
            file.CheckForImageInDocFile();
            Assert.True(file.ContainsPhoto);
        }

        //docm
        [Fact]
        public void FileCantDetectImageInEmptyDocmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docmEmpty");
            file.CheckForImageInDocFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCantDetectImageInEmptyDocmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docmEmpty.docm");
            file.CheckForImageInDocFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInDocmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docmWithJpgOnSecondPage");
            file.CheckForImageInDocFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInDocmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docmWithJpgOnSecondPage.docm");
            file.CheckForImageInDocFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInDocmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docmWithPngOnSecondPage");
            file.CheckForImageInDocFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInDocmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\docmWithPngOnSecondPage.docm");
            file.CheckForImageInDocFile();
            Assert.True(file.ContainsPhoto);
        }

        //pptm
        [Fact]
        public void FileCantDetectImageInEmptyPptmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptmEmpty");
            file.CheckForImageInPowerPointFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCantDetectImageInEmptyPptmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptmEmpty.pptm");
            file.CheckForImageInPowerPointFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInPptmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptmWithJpgOnSecondSlide");
            file.CheckForImageInPowerPointFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInPptmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptmWithJpgOnSecondSlide.pptm");
            file.CheckForImageInPowerPointFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInPptmFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptmWithPngOnSecondSlide");
            file.CheckForImageInPowerPointFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInPptmFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptmWithPngOnSecondSlide.pptm");
            file.CheckForImageInPowerPointFile();
            Assert.True(file.ContainsPhoto);
        }

        //pptx
        [Fact]
        public void FileCantDetectImageInEmptyPptxFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptxEmpty");
            file.CheckForImageInPowerPointFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCantDetectImageInEmptyPptxFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptxEmpty.pptx");
            file.CheckForImageInPowerPointFile();
            Assert.False(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInPptxFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptxWithJpgOnSecondSlide");
            file.CheckForImageInPowerPointFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectJpgImageInPptxFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptxWithJpgOnSecondSlide.pptx");
            file.CheckForImageInPowerPointFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInPptxFileWithoutExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptxWithPngOnSecondSlide");
            file.CheckForImageInPowerPointFile();
            Assert.True(file.ContainsPhoto);
        }
        [Fact]
        public void FileCanDetectPngImageInPptxFileWithExtension()
        {
            var file = new FileToBeCheckedForPhoto(@".\Data\pptxWithPngOnSecondSlide.pptx");
            file.CheckForImageInPowerPointFile();
            Assert.True(file.ContainsPhoto);
        }
    }
}
