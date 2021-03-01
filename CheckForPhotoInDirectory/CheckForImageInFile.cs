using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CheckForPhotoInDirectory
{
    public class CheckForImageInFile
    {
        private IFileOpener _fileOpener;

        public CheckForImageInFile(IFileOpener fileOpener)
        {
            _fileOpener = fileOpener;
        }

        /// <summary>
        /// Checks if the file is a Doc document and contains Jpg or Png Image 
        /// </summary>
        public Boolean CheckForImageInDocFile(string filePath)
        {
            FileStream fileStream = null;
            List<String> imageFormats = new List<string> { "image/jpeg", "image/png" };
            try
            {
                fileStream = _fileOpener.OpenFileStream(filePath);
                if (fileStream == null) return false;
                WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(fileStream, true);
                var images = wordprocessingDocument.MainDocumentPart.ImageParts;
                foreach (var image in images)
                {
                    if (image != null && imageFormats.Contains(image.ContentType))
                    {
                        return true;
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                fileStream.Dispose();
            }
            return false;
        }

        /// <summary>
        /// Checks if the file is a Presentation document and contains Jpg or Png Image on any of the slides 
        /// </summary>
        public Boolean CheckForImageInPowerPointFile(string filePath)
        {

            FileStream fileStream = null;
            List<String> imageFormats = new List<string> { "image/jpeg", "image/png" };
            try
            {
                fileStream = _fileOpener.OpenFileStream(filePath);
                if (fileStream == null) return false;
                PresentationDocument presentationDocument = PresentationDocument.Open(fileStream, true);
                PresentationPart presentationPart = presentationDocument.PresentationPart;
                foreach (SlidePart slidePart in presentationPart.GetPartsOfType<SlidePart>())
                {
                    ImagePart imagePart = slidePart.GetPartsOfType<ImagePart>().FirstOrDefault();
                    if (imagePart != null && imageFormats.Contains(imagePart.ContentType))
                    {
                        return true;
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                fileStream.Dispose();
            }
            return false;
        }

        /// <summary>
        /// Checks if the file is a Spreadsheet document and contains Jpg or Png Image on any of the worksheets 
        /// </summary>
        /// 
        public Boolean CheckForImageInSpreadsheetFile(string filePath)
        {
            FileStream fileStream = null;
            List<String> imageFormats = new List<string> { "Jpeg", "Png" };
            try
            {
                fileStream = _fileOpener.OpenFileStream(filePath);
                if (fileStream == null) return false;
                XLWorkbook workbook = new XLWorkbook(fileStream);
                IXLWorksheets worksheets = workbook.Worksheets;
                foreach (IXLWorksheet worksheet in worksheets)
                {
                    if (!worksheet.Pictures.Any())
                    {
                        continue;
                    }
                    foreach (IXLPicture picture in worksheet.Pictures)
                    {
                        if (imageFormats.Contains(picture.Format.ToString()))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                fileStream.Dispose();
            }
            return false;
        }

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
        public Boolean CheckIfFileIsImage(string filePath)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = _fileOpener.OpenFileStream(filePath);
                if (fileStream == null) return false;
                if (CheckIfFileIsImageType(fileStream))
                {
                    return true;
                }
            }
            catch (Exception) { }
            finally
            {
                fileStream.Dispose();
            }
            return false;
        }
        /// <summary>
        /// Checks if given filestream is Jpg or Png Image
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
