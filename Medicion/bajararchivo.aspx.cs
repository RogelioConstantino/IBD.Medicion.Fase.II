using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medicion.Class.LogError;

namespace Medicion
{
    public partial class bajararchivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Business.clsMeters oclsMeters = new Class.Business.clsMeters();
            Class.Business.PropertiesDocuments oclsPropDoc = new Class.Business.PropertiesDocuments();
            LogErrorMedicion clsError = new LogErrorMedicion();
           
            if (!IsPostBack) 
            {
                try 
                {
                    string strEmail = (string)Session["email"];
                    if (string.IsNullOrEmpty(strEmail)) { Response.Redirect("Default.aspx"); }

                    string strFile = (string)Request.QueryString["doc"];
                    if (!string.IsNullOrEmpty(strFile))
                    {
                        string strPathMeter = oclsMeters.GetPathUploadMeters();
                        strPathMeter = strPathMeter.Replace("~", "");
                        string strFileWithReplace = strFile.Replace(" ", "+");
                        oclsPropDoc = oclsMeters.DownloadFile(strFileWithReplace);
                        if (!string.IsNullOrEmpty(oclsPropDoc.strFileName.ToString()))
                        {
                            Boolean bmsg = oclsMeters.InsertDownloadLogFile(strFileWithReplace, strEmail);
                            string filePath = "\\" + strPathMeter + "\\" + oclsPropDoc.strRPU + "\\" + strFileWithReplace + oclsPropDoc.strFileExtension.ToString();
                            Response.ContentType = oclsPropDoc.strMimeType;
                            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + oclsPropDoc.strFileName.ToString() + "\"");
                            Response.TransmitFile(Server.MapPath(filePath));
                            Response.End();
                        }
                        

                    }
                }
                catch (Exception ex)
                {
                    clsError.logMessage = ex.ToString();
                    clsError.logModule = "Page_Load";
                    clsError.LogWrite();
                }

                
            }
        }
    }
}