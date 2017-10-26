using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;

using Medicion.Class.Business;
using Medicion.Class.LogError;

using System.Text;

//using Microsoft.Office.Interop.Excel;

using ClosedXML.Excel;

using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;


namespace Medicion
{
    public partial class resumengeneral : System.Web.UI.Page
    {
        StringBuilder strHTMLElectric = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                FillGestorMedicion();

                //buscar();

                //   System.Data.DataTable dtGR = new System.Data.DataTable();
                //clsGeneralReport oclsGeneralReport = new clsGeneralReport();
                //if (!IsPostBack)
                //{
                //    dtGR = oclsGeneralReport.GetGeneralReport( "1","", toggleConvenios.Checked?"0":"1",   toggleComunicacion.Checked ? "0" : "1", toggleMedicion.Checked ? "0" : "1");
                //    if  (dtGR != null &&   (dtGR.Rows.Count > 0))
                //    {
                //        Session["dtGR"] = dtGR;
                //        strHTMLElectric = oclsGeneralReport.CreateTableHTML(dtGR);
                //       DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                //    }
            }
        }
                    
            
        protected void btnSave_Click(object sender, EventArgs e) {


            LogErrorMedicion clsError = new LogErrorMedicion();


            clsError.logMessage = "btnSave_Click entra";
            clsError.logModule = "btnSave_Click";
            clsError.LogWrite();



            string strGrupo = ddl_Grupos.Items[ddl_Grupos.SelectedIndex].Value;
            string strEstatus = ddl_Estaus.Items[ddl_Estaus.SelectedIndex].Value;
            String strCentral = ddl_Central.Items[ddl_Central.SelectedIndex].Value;
            String strGestor = cboGestorMedicion.Items[cboGestorMedicion.SelectedIndex].Value;

            System.Data.DataTable dtGR = new System.Data.DataTable();
            clsGeneralReport oclsGeneralReport = new clsGeneralReport();
            dtGR = oclsGeneralReport.GetGeneralReport("1"
                                                    , ""
                                                    , toggleConvenios.Checked ? "0" : "1"
                                                    , toggleComunicacion.Checked ? "0" : "1"
                                                    , toggleMedicion.Checked ? "0" : "1"
                                                    , strGrupo == "" ? "0" : strGrupo
                                                    , strCentral == "" ? "0" : strCentral
                                                    , strEstatus == "" ? "0" : strEstatus
                                                    ,strGestor == "" ? "0" : strGestor
                                                    );

                        
            DataSet dsGR =new DataSet();
            try
            {
                //dtGR = (System.Data.DataTable)Session["dtGR"];
                if (dtGR.Rows.Count > 0)
                {

                    //ExporttoExcel(dtGR);
                    dsGR.Tables.Add(dtGR);

                    clsError.logMessage =  "if (dtGR.Rows.Count > 0) ";
                    clsError.logModule = "btnSave_Click";
                    clsError.LogWrite();

                    ExporttoExcelClosedXML(dsGR);

                    //ExporttoExcelXML(dsGR);

                   // ExportDataSetToExcel(dsGR);
                    //UploadDataTableToExcel(dtGR);
                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSave_Click";
                clsError.LogWrite();
            }
            finally
            {
               // Response.Redirect("resumengeneral.aspx");
            }
        }

        //private Microsoft.Office.Interop.Excel.Worksheet setIcon(Microsoft.Office.Interop.Excel.Worksheet excelWorksheet, Microsoft.Office.Interop.Excel.Workbook excelWorkBook, string cell, int X, int Y)
        //{
        //    Microsoft.Office.Interop.Excel.Worksheet es = new Microsoft.Office.Interop.Excel.Worksheet();
        //    try
        //    {
        //        var c = (Microsoft.Office.Interop.Excel.IconSetCondition)excelWorksheet.get_Range((Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[X, Y], (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[X, Y]).FormatConditions.AddIconSetCondition();
        //        c.SetFirstPriority();
        //        c.ShowIconOnly = true;
        //        c.IconSet = excelWorkBook.IconSets[Microsoft.Office.Interop.Excel.XlIconSet.xl3Symbols];
        //        var yellowIcon = c.IconCriteria[2];
        //        yellowIcon.Type = Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueNumber;
        //        yellowIcon.Value = Convert.ToDouble(2);
        //        yellowIcon.Operator = (int)Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlGreaterEqual;

        //        var greenIcon = c.IconCriteria[0];
        //        greenIcon.Type = Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueNumber;
        //        greenIcon.Value = Convert.ToDouble(1);
        //        greenIcon.Operator = (int)Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlLessEqual;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //    return excelWorksheet;
        //}

        protected void UploadDataTableToExcel(System.Data.DataTable dtRecords)
        {
            LogErrorMedicion clsError = new LogErrorMedicion();
            string XlsPath = Server.MapPath(@"C:\Org.xlsx");
            string attachment = string.Empty;
            if (XlsPath.IndexOf("\\") != -1)
            {
                string[] strFileName = XlsPath.Split(new char[] { '\\' });
                attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
            }
            else
                attachment = "attachment; filename=" + XlsPath;
            try
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = string.Empty;

                foreach (DataColumn datacol in dtRecords.Columns)
                {
                    Response.Write(tab + datacol.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");

                foreach (DataRow dr in dtRecords.Rows)
                {
                    tab = "";
                    for (int j = 0; j < dtRecords.Columns.Count; j++)
                    {
                        Response.Write(tab + Convert.ToString(dr[j]));
                        tab = "\t";
                    }

                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSave_Click";
                clsError.LogWrite();
            }
        }

        public String GetPathUploadReports()
        {
            return ConfigurationManager.AppSettings["GuardarReporteGeneral"].ToString();
        }

        /// <summary>
        /// This method takes DataSet as input paramenter and it exports the same to excel
        /// </summary>
        /// <param name="ds"></param>
       
        protected void Button1_Click(object sender, EventArgs e)
        {
            buscar();
            //System.Data.DataTable dtGR = new System.Data.DataTable();
            //clsGeneralReport oclsGeneralReport = new clsGeneralReport();
            //dtGR = oclsGeneralReport.GetGeneralReport("1", "", toggleConvenios.Checked ? "0" : "1", toggleComunicacion.Checked ? "0" : "1", toggleMedicion.Checked ? "0" : "1");
            //if (dtGR != null && (dtGR.Rows.Count > 0))
            //{
            //    Session["dtGR"] = dtGR;
            //    strHTMLElectric = oclsGeneralReport.CreateTableHTML(dtGR);
            //    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
            //}
        }

        private void ExporttoExcelXML(DataSet ds) {

            string strPathReports = GetPathUploadReports();
            string strNamefile = DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
            string strFullPath = Server.MapPath(strPathReports) + strNamefile;

            var stream = new MemoryStream();
            var document = SpreadsheetDocument.Create(strFullPath, SpreadsheetDocumentType.Workbook);

            
            var workbookpart = document.AddWorkbookPart();
            workbookpart.Workbook = new  DocumentFormat.OpenXml.Spreadsheet.Workbook();
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();

            Worksheet ws = new Worksheet();
            WorkbookStylesPart wbsp = workbookpart.AddNewPart<WorkbookStylesPart>();
            // add styles to sheet
            wbsp.Stylesheet = CreateStylesheet();
            wbsp.Stylesheet.Save();

            var sheetData = new SheetData();            

            worksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

            var sheets = document.WorkbookPart.Workbook.
                AppendChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

            var sheet = new Sheet()
            {
                Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Resumen General" //data.SheetName ?? "Sheet 1"
            };
            sheets.AppendChild(sheet);

            
            

            UInt32 rowIdex = 0;
            var row = new Row { RowIndex = ++rowIdex };

            Fonts fts = new Fonts();
            DocumentFormat.OpenXml.Spreadsheet.Font ft = new DocumentFormat.OpenXml.Spreadsheet.Font();
            Bold fbld = new Bold();
            FontName ftn = new FontName();
            ftn.Val = "Calibri";
            DocumentFormat.OpenXml.Spreadsheet.FontSize ftsz = new DocumentFormat.OpenXml.Spreadsheet.FontSize();
            ftsz.Val = 11;
            ft.FontName = ftn;
            ft.FontSize = ftsz;
            ft.Bold = fbld;
            fts.Append(ft);
            fts.Count = (uint)fts.ChildElements.Count;
            row.Append(fts);
            
            sheetData.AppendChild(row);
            row.StyleIndex = (UInt32Value)1U;

            var cellIdex = 0;
            

            int intCount = 0;
            string strX = string.Empty;
            string strY = string.Empty;
            foreach (System.Data.DataTable table in ds.Tables)
            {                
                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    //excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName.ToString().ToUpper();

                    Cell cel = CreateTextCellHeader(ColumnLetter(cellIdex++), rowIdex, table.Columns[i - 1].ColumnName.ToString().ToUpper() ?? string.Empty);
                    if ( i < 9 )
                        cel.StyleIndex = (UInt32Value)1U;

                    if (i > 10 && i <15)
                        cel.StyleIndex = (UInt32Value)2U;

                    if (i > 15 && i < 20) 
                        cel.StyleIndex = (UInt32Value)4;

                    if (i > 20 && i < 25)
                        cel.StyleIndex = (UInt32Value)5U;

                    if (i > 30 && i < 35)
                        cel.StyleIndex = (UInt32Value)6U;


                    row.AppendChild(cel);
                    
                    //((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[1, i]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                    //((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[1, i]).Font.Bold = true;
                    //((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[1, i]).Font.Name = "Calibir";
                    //((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[1, i]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //  REQUIERE TC Y TP    
                    if ((table.Columns[i - 1].ColumnName.ToString().ToUpper() == "REQUIERE TC Y TP")
                        ||
                        (table.Columns[i - 1].ColumnName.ToString().ToUpper() == "REQUIERE REUBICACIÓN")
                        &&
                        intCount == 0)
                    {
                        intCount = i;
                    }
                    //((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[1, i]).Style.Name = "Normal";
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {                                       
                    //rowIdex = 1;
                    row = new Row { RowIndex = ++rowIdex   };

                    
                    sheetData.AppendChild(row);
                    cellIdex = 0;
                    
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        if ((k == 2))
                        {
                            //((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[j + 2, k + 1]).NumberFormat = "#####";
                        }
                        if ((k == 4))
                        {
                        //    ((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[j + 2, k + 1]).NumberFormat = "##,###.##";
                        }
                        //excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++), rowIdex, table.Rows[j].ItemArray[k].ToString() ?? string.Empty));
                    }
                }

            }
                                 

            workbookpart.Workbook.Save();
            document.Close();
            string strUrl = "Bajarresumengral.aspx?n=" + strNamefile;
            Response.Redirect(strUrl, true);                       
            
        }

        private string ColumnLetter(int intCol)
        {
            var intFirstLetter = ((intCol) / 676) + 64;
            var intSecondLetter = ((intCol % 676) / 26) + 64;
            var intThirdLetter = (intCol % 26) + 65;

            var firstLetter = (intFirstLetter > 64) ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64) ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter, thirdLetter).Trim();
        }

        private Cell CreateTextCellHeader(string header, UInt32 index, string text)
        {          
            var cell = new Cell
            {
                DataType = CellValues.InlineString,
                CellReference = header + index                
            };  
            var istring = new InlineString();
            var t = new Text { Text = text };
            istring.AppendChild(t);
            cell.AppendChild(istring);
            return cell;
        }


        private Cell CreateTextCell(string header, UInt32 index, string text)
        {
            var cell = new Cell
            {
                DataType = CellValues.InlineString,
                CellReference = header + index
            };
            var istring = new InlineString();
            var t = new Text { Text = text };
            istring.AppendChild(t);
            cell.AppendChild(istring);
            return cell;
        }


        private static Stylesheet CreateStylesheet()
        {
            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts()
            {
                Count = (UInt32Value)2U,
                KnownFonts
             = true
            };
            //Normal Font
            DocumentFormat.OpenXml.Spreadsheet.Font font1 =
            new DocumentFormat.OpenXml.Spreadsheet.Font();
            DocumentFormat.OpenXml.Spreadsheet.FontSize fontSize1 =
            new DocumentFormat.OpenXml.Spreadsheet.FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color color1 =
            new DocumentFormat.OpenXml.Spreadsheet.Color()
            { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 =
            new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme()
            { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);
            fonts1.Append(font1);

            //Bold Font
            DocumentFormat.OpenXml.Spreadsheet.Font bFont =
            new DocumentFormat.OpenXml.Spreadsheet.Font();
            DocumentFormat.OpenXml.Spreadsheet.FontSize bfontSize =
            new DocumentFormat.OpenXml.Spreadsheet.FontSize() { Val = 11D };
            DocumentFormat.OpenXml.Spreadsheet.Color bcolor =
            new DocumentFormat.OpenXml.Spreadsheet.Color()
            { Theme = (UInt32Value)1U };
            FontName bfontName = new FontName() { Val = "Calibri" };
            FontFamilyNumbering bfontFamilyNumbering =
            new FontFamilyNumbering() { Val = 2 };
            FontScheme bfontScheme = new FontScheme()
            { Val = FontSchemeValues.Minor };
            Bold bFontBold = new Bold();

            bFont.Append(bfontSize);
            bFont.Append(bcolor);
            bFont.Append(bfontName);
            bFont.Append(bfontFamilyNumbering);
            bFont.Append(bfontScheme);
            bFont.Append(bFontBold);

            fonts1.Append(bFont);


            Fills fills1 = new Fills() { Count = (UInt32Value)6U };

            // FillId = 0
            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };
            fill1.Append(patternFill1);

            // FillId = 1
            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };
            fill2.Append(patternFill2);

            // FillId = 2,RED
            Fill fill3 = new Fill();     
            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "5c881a" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U }; //
            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);
            fill3.Append(patternFill3);

            // FillId = 3,BLUE
            Fill fill4 = new Fill();
            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Rgb = "0070c0" };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);
            fill4.Append(patternFill4);

            // FillId = 4,YELLO
            Fill fill5 = new Fill();
            PatternFill patternFill5 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb = "FFFFFF00" };
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill5.Append(foregroundColor3);
            patternFill5.Append(backgroundColor3);
            fill5.Append(patternFill5);

            // FillId = 5,RED and BOLD Text             
            Fill fill6 = new Fill();
            PatternFill patternFill6 = new PatternFill()
            { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor4 = new ForegroundColor()            { Rgb = "5c881a" };
            BackgroundColor backgroundColor4 = new BackgroundColor()            { Indexed = (UInt32Value)64U };
            Bold bold1 = new Bold();
            patternFill6.Append(bold1);
            patternFill6.Append(foregroundColor4);
            patternFill6.Append(backgroundColor4);
            fill6.Append(patternFill6);


            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);
            fills1.Append(fill5);
            fills1.Append(fill6);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)4U };

            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleMedium9" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            X14.SlicerStyles slicerStyles1 = new X14.SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" };

            stylesheetExtension1.Append(slicerStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);
            return stylesheet1;
        }


        private void ExporttoExcelClosedXML(DataSet ds)
        {
            LogErrorMedicion clsError = new LogErrorMedicion();

            var wb = new XLWorkbook();
            
            var ws = wb.Worksheets.Add("Resumen General");
            var cellIdex = 0;
            int intCount = 0;
            string strX = string.Empty;
            string strY = string.Empty;

            int intComunicacion = 0;
            int intMedicion = 0;
            int intConvenios = 0;

            UInt32 rowIdex = 1;
            try
            {
                foreach (System.Data.DataTable table in ds.Tables)
                {


                    //// From worksheet
                    var rngTable = ws.Range("A1:" + ColumnLetter(table.Columns.Count - 1) + "1");
                    var rngHeaders = rngTable.Range("A1:" + ColumnLetter(table.Columns.Count - 1) + "1"); // The address is relative to rngTable (NOT the worksheet)
                    rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    rngHeaders.Style.Font.Bold = true;
                    rngHeaders.Style.Fill.BackgroundColor = XLColor.FromArgb(155, 187, 89);



                    // headers
                    for (int i = 1; i < table.Columns.Count + 1; i++)
                    {                        
                        ws.Cell(ColumnLetter(cellIdex++) + rowIdex).Value = table.Columns[i - 1].ColumnName.ToString().ToUpper() ?? string.Empty;

                        //  REQUIERE TC Y TP            // Comunicación
                        if (table.Columns[i - 1].ColumnName.ToString().ToUpper() == "REQUIERE TC Y TP")
                        {
                            intCount = i;
                            intComunicacion = i;
                        } 
                        //  REQUIERE REUBICACIÓN                // Medidores
                        if (table.Columns[i - 1].ColumnName.ToString().ToUpper() == "REQUIERE REUBICACIÓN")
                        {
                            intMedicion = i;
                            intCount = i;
                        }
                        //if (table.Columns[i - 1].ColumnName.ToString().ToUpper() == "PRELACION")
                        //{
                        //    intMedicion = i;
                        //    intCount = i;
                        //}

                        if (i > 12 && intCount == 0 && i < (table.Columns.Count))
                        {
                            string sColor = "";
                            sColor = get_ConveniorColor(table.Columns[i - 1].ColumnName.ToString() ?? string.Empty);
                            sColor = (sColor == "") ? "FFFF00" : sColor;
                            ws.Cell(ColumnLetter(cellIdex -1) + rowIdex).Style.Fill.BackgroundColor = XLColor.FromHtml("#" + sColor + "15");
                            intConvenios = i;
                        }
                        else {
                        }

                    }


                    if (intCount != 0)
                    {
                        var rngHeaders2 = rngTable.Range(ColumnLetter(intCount - 1) + "1:" + ColumnLetter(cellIdex - 1) + "1"); // The address is relative to rngTable (NOT the worksheet)
                        rngHeaders2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rngHeaders2.Style.Font.Bold = true;
                        rngHeaders2.Style.Fill.BackgroundColor = XLColor.FromArgb(155, 187, 89);
                    }
                    //var rngConvenios = rngTable.Range("K1:" + ColumnLetter(intCount ) + "1"); // The address is relative to rngTable (NOT the worksheet)
                    //rngConvenios.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //rngConvenios.Style.Font.Bold = true;
                    //rngConvenios.Style.Fill.BackgroundColor = XLColor.FromArgb(196, 215, 155);

                    rowIdex = 2;

                    // datos
                    int k = 0;
                    int j = 0;
                    for (j = 0; j < table.Rows.Count; j++)
                    {
                        for (k = 0; k < table.Columns.Count; k++)
                        {
                            if ((k == 2))
                            {
                                //((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[j + 2, k + 1]).NumberFormat = "#####";
                            }
                            if ((k == 4))
                            {
                                //    ((Microsoft.Office.Interop.Excel.Range)excelWorkSheet.Cells[j + 2, k + 1]).NumberFormat = "##,###.##";
                            }
                            //excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                            //row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++), rowIdex, table.Rows[j].ItemArray[k].ToString() ?? string.Empty));
                            ws.Cell(ColumnLetter(k) + (j + 2)).Value = table.Rows[j].ItemArray[k].ToString().ToUpper() ?? string.Empty;
                        }
                    }

                    var rngTableAll = ws.Range("A1:" + ColumnLetter(k - 1) + (j + 1));
                    //Add a thick outside border
                    rngTableAll.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;



                    //ws.Column(9).Hide();
                    // ws.Column(10).Hide();

                    ws.Column(1).Width = 5;

                    var col2 = ws.Column(1);
                    col2.Width = 5;

                    var col3 = ws.Column("B");
                    col3.Width = 5;

                    rngTableAll.SetAutoFilter();




                    //You can also specify the border for each side with:
                    rngTableAll.FirstColumn().Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                    rngTableAll.LastColumn().Style.Border.RightBorder = XLBorderStyleValues.Thick;
                    rngTableAll.FirstRow().Style.Border.TopBorder = XLBorderStyleValues.Thick;
                    rngTableAll.LastRow().Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    rngTableAll.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    var rngCapacidad = rngTableAll.Range("F2:F" + (j + 1)); // The address is relative to rngTable (NOT the worksheet)
                    rngCapacidad.Style.NumberFormat.Format = "#,##0.00";
                                       

                    var rngCapacidadRPU = rngTableAll.Range("C2:C" + (j +1)); // The address is relative to rngTable (NOT the worksheet)
                    rngCapacidadRPU.Style.NumberFormat.Format = "###";

                    
                    if (!toggleConvenios.Checked)
                    {
                        var rngConvenios = rngTableAll.Range("L2:" + ColumnLetter(intConvenios-1) + (j + 1)); // The address is relative to rngTable (NOT the worksheet)
                            rngConvenios.Style.NumberFormat.Format = "###";
                    }




                    // iconos 


                    //var rngPrelacion = rngTableAll.Range("J" + "2:J" + (j + 1)); // The address is relative to rngTable (NOT the worksheet)
                    //rngPrelacion.AddConditionalFormat().IconSet(XLIconSetStyle.ThreeSymbols, false, true)
                    //            .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 0, XLCFContentType.Number)
                    //            .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 1, XLCFContentType.Number);
                    
                    if ((intCount != 0))
                    {
                        if ((intMedicion != 0))    //&& !toggleConvenios.Checked 
                        {
                            var rngIconos = rngTableAll.Range(ColumnLetter(intMedicion - 1) + "2:" + ColumnLetter(intMedicion+10) + (j + 1)); // The address is relative to rngTable (NOT the worksheet)
                            rngIconos.AddConditionalFormat().IconSet(XLIconSetStyle.ThreeSymbols, false, true)
                                        .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 0, XLCFContentType.Number)
                                        .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 1, XLCFContentType.Number);
                                        //.AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 2, XLCFContentType.Number);
                       
                            var rngIconos2 = rngTableAll.Range(ColumnLetter((intMedicion + 13)) + "2:" + ColumnLetter((intMedicion + 15)) + (j+1)); // The address is relative to rngTable (NOT the worksheet)
                            rngIconos2.AddConditionalFormat().IconSet(XLIconSetStyle.ThreeSymbols, false, true)
                                    .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 0, XLCFContentType.Number)
                                    .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 1, XLCFContentType.Number);
                                    //.AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 2, XLCFContentType.Number);
                        }
                        if ((intComunicacion!= 0))
                        {
                            var rngIconos3 = rngTableAll.Range(ColumnLetter((intComunicacion-1)) + "2:" + ColumnLetter(intComunicacion ) + (j + 1)); // The address is relative to rngTable (NOT the worksheet)                    
                            rngIconos3.AddConditionalFormat().IconSet(XLIconSetStyle.ThreeSymbols, false, true)
                                .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 0, XLCFContentType.Number)
                                .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 1, XLCFContentType.Number);                           
                        }

                        var rngIconos4 = rngTableAll.Range(ColumnLetter(9) + "2:" + ColumnLetter(9) + (j + 1)); // The address is relative to rngTable (NOT the worksheet)
                        rngIconos4.AddConditionalFormat().IconSet(XLIconSetStyle.ThreeSymbols, false, true)
                                    .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 0, XLCFContentType.Number)
                                    .AddValue(XLCFIconSetOperator.EqualOrGreaterThan, 1, XLCFContentType.Number);
                        
                    }
                ws.Columns(1, k ).AdjustToContents();
            }
                        
            ws.SheetView.FreezeRows(1);
            ws.SheetView.FreezeColumns(3);
            
            string strPathReports = GetPathUploadReports();
            string strNamefile = DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
            string strFullPath = Server.MapPath(strPathReports) + strNamefile;
            
            wb.SaveAs(strFullPath);
            
            string strUrl = "Bajarresumengral.aspx?n=" + strNamefile;
            Response.Redirect(strUrl, true);
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSave_Click";
                clsError.LogWrite();
            }
        }


        private string  get_ConveniorColor( string sConvenio)
        {
            string query = "SELECT ce.Color   FROM convenios c     JOIN ConveniosEstatus ce       ON c.IdEstatus = ce.IdEstatus WHERE concat(   Convenio,' (',		ce.Descripcion,')') = '" + sConvenio+ "' ";            
            string constr = ConfigurationManager.AppSettings["appConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                                return dt.Rows[0][0].ToString();
                            else
                                return  "";
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (ddl_Grupos.Text == "")
            //{ 
            //ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Seleccione un grupo','error');", true); 
            ////}
            ////else if (ddl_Central.Text =="")
            ////{
            ////     ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Seleccione una central','error');", true); 
            //}else
            //{
            //    buscar();
            //}
            buscar();

        }


        private void buscar()
        {

            string strGrupo = ddl_Grupos.Items[ddl_Grupos.SelectedIndex].Value;
            string strEstatus = ddl_Estaus.Items[ddl_Estaus.SelectedIndex].Value;
            String strCentral = ddl_Central.Items[ddl_Central.SelectedIndex].Value;
            String strGestor = cboGestorMedicion.Items[cboGestorMedicion.SelectedIndex].Value;

            if (strGestor == "-- TODOS --" ) strGestor ="0" ;
            

System.Data.DataTable dtGR = new System.Data.DataTable();
            clsGeneralReport oclsGeneralReport = new clsGeneralReport();
            dtGR = oclsGeneralReport.GetGeneralReport("1"
                                                    , ""
                                                    , toggleConvenios.Checked ? "0" : "1"
                                                    , toggleComunicacion.Checked ? "0" : "1"
                                                    , toggleMedicion.Checked ? "0" : "1"
                                                    , strGrupo == "" ? "0" : strGrupo
                                                    , strCentral == "" ? "0" : strCentral
                                                    , strEstatus == "" ? "0" : strEstatus
                                                     , strGestor == "" ? "0" : strGestor);
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                Session["dtGR"] = dtGR;
                strHTMLElectric = oclsGeneralReport.CreateTableHTML(dtGR);
                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
            }
        }



        private void FillGestorMedicion()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetGestorMedicion();
                cboGestorMedicion.DataSource = dtCommunication;
                cboGestorMedicion.DataTextField = "GestorMedicion";
                cboGestorMedicion.DataValueField = "IdGestor";
                cboGestorMedicion.DataBind();
                cboGestorMedicion.Items.Add("-- TODOS --");
                cboGestorMedicion.SelectedValue = "-- TODOS --";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillGestorMedicion";
                clsError.LogWrite();
            }
        }


    }
}