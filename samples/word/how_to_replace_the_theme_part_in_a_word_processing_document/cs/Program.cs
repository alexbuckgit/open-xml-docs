
    using System.IO;
    using DocumentFormat.OpenXml.Packaging;

    // This method can be used to replace the theme part in a package.
    public static void ReplaceTheme(string document, string themeFile)
    {
        using (WordprocessingDocument wordDoc = 
            WordprocessingDocument.Open(document, true))
        {
            MainDocumentPart mainPart = wordDoc.MainDocumentPart;
            
            // Delete the old document part.
            mainPart.DeletePart(mainPart.ThemePart);
            
            // Add a new document part and then add content.
            ThemePart themePart = mainPart.AddNewPart<ThemePart>();

            using (StreamReader streamReader = new StreamReader(themeFile))
            using (StreamWriter streamWriter = 
                new StreamWriter(themePart.GetStream(FileMode.Create)))
            {
                streamWriter.Write(streamReader.ReadToEnd());
            }
        }
    }