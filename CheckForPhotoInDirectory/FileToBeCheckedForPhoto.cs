using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CheckForPhotoInDirectory
{
    public class FileToBeCheckedForPhoto
    {
        public String FileDirectory { get;}
        public String FileName { get; }
        public Boolean ContainsPhoto { get; private set; }

        public FileToBeCheckedForPhoto(String filePath)
        {
            FileDirectory = filePath;
            FileName = Path.GetFileName(filePath);
            ContainsPhoto = false;
        }
        public Boolean CheckForImageInDocFile()
        {
            List<String> imageFormats = new List<string> { "image/jpeg", "image/png" };
            try
            {
                using (FileStream fileStream = File.Open(this.FileDirectory, FileMode.Open))
                {
                    WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(fileStream, true);
                    var images = wordprocessingDocument.MainDocumentPart.ImageParts;
                    foreach (var image in images)
                    {
                        if (image != null && imageFormats.Contains(image.ContentType))
                        {
                            this.ContainsPhoto = true;
                            return true;
                        }
                    }
                }
            }
            catch (Exception) { }
            return false;
        }
        public Boolean CheckForImageInPowerPointFile()
        {
            List<String> imageFormats = new List<string> { "image/jpeg", "image/png" };
            try
            {
                using (FileStream fileStream = File.Open(this.FileDirectory, FileMode.Open))
                {
                    PresentationDocument presentationDocument = PresentationDocument.Open(fileStream, true);
                    PresentationPart presentationPart = presentationDocument.PresentationPart;
                    foreach (SlidePart slidePart in presentationPart.GetPartsOfType<SlidePart>())
                    {
                        ImagePart imagePart = slidePart.GetPartsOfType<ImagePart>().FirstOrDefault();
                        if (imagePart != null && imageFormats.Contains(imagePart.ContentType))
                        {
                            this.ContainsPhoto = true;
                            return true;
                        }
                    }
                }

            }
            catch (Exception) { }
            return false;
        }
        public Boolean CheckForImageInSpreadsheetFile()
        {
            List<String> imageFormats = new List<string> { "Jpeg", "Png" };
            try
            {
                using (FileStream fileStream = File.Open(this.FileDirectory, FileMode.Open))
                {
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
                                this.ContainsPhoto = true;
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            return false;
        }

        public Boolean CheckIfFileIsImage()
        {
            try
            {
                using (FileStream fileStream = File.Open(this.FileDirectory, FileMode.Open))
                {
                    if (Utilities.CheckIfFileIsImageType(fileStream))
                    {
                        this.ContainsPhoto = true;
                        return true;
                    }
                }
            }
            catch (Exception) { }
            return false;
        }
        public override string ToString()
        {
            return FileName + " | " + ContainsPhoto;
        }

    }
}
