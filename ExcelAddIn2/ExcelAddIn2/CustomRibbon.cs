using Microsoft.Office.Tools.Ribbon;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
namespace ExcelAddIn2
{
    public partial class CustomRibbon
    {
        private readonly Dictionary<string, object> originalValues = new Dictionary<string, object>();
        private void CustomRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void btnConvertAlpha_Click(object sender, RibbonControlEventArgs e)
        {
            Excel.Application application = Globals.ThisAddIn.Application;
            Excel.Range selectedRange = application.Selection as Excel.Range;

            if (selectedRange == null)
            {
                return;
            }

            originalValues.Clear(); // reset 

            foreach (Excel.Range cell in selectedRange.Cells)
            {
                if (cell.Value2 != null )
                {
                    originalValues[cell.Address] = cell.Value2;
                    //Clearing Non-Alphanumeric presence in the cell value 
                    cell.Value2 = Regex.Replace(cell.Value2, "[^a-zA-Z0-9]", "");
                }
            }
            
        }

        private void btnUndo_Click(object sender, RibbonControlEventArgs e)
        {
            Excel.Application app = Globals.ThisAddIn.Application;
            Excel.Worksheet activeSheet = app.ActiveSheet;

            foreach(var keyValue in originalValues)
            {
                // select current address (Key) in stored value
                Excel.Range cell = activeSheet.Range[keyValue];
                // restore stored value of corresponding address
                cell.Value2 = keyValue.Value;   
            }
        }
    }
}
