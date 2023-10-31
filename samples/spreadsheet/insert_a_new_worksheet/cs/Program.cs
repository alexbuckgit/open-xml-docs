#nullable disable

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

// Given a document name, inserts a new worksheet.
static void InsertWorksheet(string docName)
{
    // Open the document for editing.
    using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docName, true))
    {
        // Add a blank WorksheetPart.
        WorksheetPart newWorksheetPart = spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
        newWorksheetPart.Worksheet = new Worksheet(new SheetData());

        Sheets sheets = spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();
        string relationshipId = spreadSheet.WorkbookPart.GetIdOfPart(newWorksheetPart);

        // Get a unique ID for the new worksheet.
        uint sheetId = 1;
        if (sheets.Elements<Sheet>().Count() > 0)
        {
            sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
        }

        // Give the new worksheet a name.
        string sheetName = "Sheet" + sheetId;

        // Append the new worksheet and associate it with the workbook.
        Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
        sheets.Append(sheet);
    }
}