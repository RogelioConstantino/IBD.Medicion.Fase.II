using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Text;
using Medicion.Class;
namespace Medicion.Class.Business
{
    public class clsPorteo : Class.porteoautomatico.clsPropiertiesPorteoAutomatico
    {
        public DataTable  Import_To_Grid()
        {
            DataTable dt = new DataTable();
            try 
            {
                string conStr = "";
                switch (strExtension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                                 .ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                                  .ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, strFilePath, strIsHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
               
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                connExcel.Close();



                //Read Data from First Sheet
                connExcel.Open();
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                //string SheetName = "Sheet1$";
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                //Bind Data to GridView
                //GridView1.Caption = (FilePath);
                //GridView1.DataSource = dt;
                //GridView1.DataBind();
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Import_To_Grid";
                clsError.LogWrite();
            }

            

            return dt;
           
        }

        public StringBuilder CreateTableHTML()
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try 
            {
                
                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtShiping.Columns)
                {
                    html.Append("<th class='text-uppercase'>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtShiping.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtShiping.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
                html.Append("</tbody>");
               
 
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CreateTableHTML";
                clsError.LogWrite();
            }
            
            return html;
        }

    }
}