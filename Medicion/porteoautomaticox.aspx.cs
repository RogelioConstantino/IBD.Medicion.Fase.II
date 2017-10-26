using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Text;
using Medicion.Class.porteoautomatico;
using Medicion.Class.Business;
using Medicion.Class.LogError;
namespace Medicion
{

    public partial class porteoautomatico : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        DataTable dtTemp;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtTemp = new DataTable();
            }
            else 
            {
               
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            DataTable dtRsponseImport2Grid =new DataTable();
            clsPorteo clsBusinessPorteo = new clsPorteo();
            StringBuilder strHtmlTablePorteo;
            if (IsPostBack)
            {
                clsError.logMessage = "Entra ISPOSTBACK";
                clsError.LogWrite();
                try
                {
                    if (FileUpload1.HasFile)
                    {
                        clsError.logMessage = "Entra FileUpload1.HasFile";
                        clsError.LogWrite();
                        clsBusinessPorteo.strFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        if (!string.IsNullOrEmpty(clsBusinessPorteo.strFileName))
                        {
                            clsError.logMessage = "Entra clsBusinessPorteo.strFileName " + clsBusinessPorteo.strFileName;
                            clsError.LogWrite();
                            clsBusinessPorteo.strExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                            //string nombreArchivo = FileUpload1.FileName;
                            clsBusinessPorteo.strFilePath = Server.MapPath(clsBusinessPorteo.strFileName);
                            //GridView1.Caption = FilePath;
                            FileUpload1.SaveAs( clsBusinessPorteo.strFilePath);
                            clsBusinessPorteo.strIsHDR = ExcelHasHeader.Yes;
                            dtRsponseImport2Grid = clsBusinessPorteo.Import_To_Grid();
                            //Import_To_Grid(clsBusinessPorteo.strFilePath, clsBusinessPorteo.strFileName, "Yes");
                            if (dtRsponseImport2Grid.Rows.Count > 0)
                            {
                                Session["dt_Sess_Porteo"] = dtRsponseImport2Grid;
                                clsBusinessPorteo.dtShiping = dtRsponseImport2Grid;
                                strHtmlTablePorteo = clsBusinessPorteo.CreateTableHTML();
                                //CreateTableHTML(dtRsponseImport2Grid);
                                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHtmlTablePorteo.ToString() });

                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    clsError.logMessage = ex.ToString();
                    clsError.logModule = "btnUpload_Click";
                    clsError.LogWrite();
                }
                finally
                {
                    dtRsponseImport2Grid = null;
                    clsBusinessPorteo = null;
                    strHtmlTablePorteo = null;
                }
                
            }
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            if (IsPostBack) 
            {
                clsPorteoAutomatico clsShiping = new clsPorteoAutomatico();
                
                try
                {
                    clsShiping.dtShiping = (DataTable)Session["dt_Sess_Porteo"];
                    clsShiping.strRPUrepeated = clsShiping.RPU();
                    if (clsShiping.strRPUrepeated.Length > 0)
                    {
                        ErrorMsg.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Los siguientes RPU's ya están en la base de datos y no se importaron: <br></strong> " + clsShiping.strRPUrepeated + "</div>";
                    }
                }
                catch (Exception ex)
                {
                    clsError.logMessage = ex.ToString();
                    clsError.logModule = "btnUpload_Click";
                    clsError.LogWrite();
                }
                finally {
                    Session["dt_Sess_Porteo"] = null;
                    clsShiping = null;
                }
                
            }
           
        }



    }
}