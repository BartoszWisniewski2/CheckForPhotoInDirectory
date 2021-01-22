using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Spire.Doc.Documents;
using Spire.Doc;


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
        //Method to check if doc file contains Image using Spire.Doc
        public Boolean checkForImageInDocFile()
        {
            try
            {
                Document document = new Document(this.FileDirectory);
                {
                    foreach (Section section in document.Sections)
                    {
                        //Get Each Paragraph of Section
                        foreach (Paragraph paragraph in section.Paragraphs)
                        {
                            //Get Each Document Object of Paragraph Items
                            foreach (DocumentObject docObject in paragraph.ChildObjects)
                            {
                                //If Type of Document Object is Picture set ContainsPhoto property to true and return true.
                                if (docObject.DocumentObjectType == DocumentObjectType.Picture)
                                {
                                    this.ContainsPhoto = true;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            return false;
        }
        public void checkIfFileIsImage()
        {
            try
            {
                using (FileStream data = File.OpenRead(this.FileDirectory))
                {
                    if (Utilities.CheckIfFileIsImageType(data))
                    {
                        this.ContainsPhoto = true;
                    }
                }
            }
            catch (Exception) { }
        }
        public override string ToString()
        {
            return FileName + " | " + ContainsPhoto;
        }

    }
}
