using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medicion.Class.LogError;
using Medicion.Class.Business;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;

namespace Medicion
{
    public partial class Centrales : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        StringBuilder strHTMLCentral = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            clsCentral clsBussCentral = new clsCentral();

            try
            {
                if (!IsPostBack)
                {
                    DataTable dtC = clsBussCentral.GetAllCentrales();
                    Session["dtC"] = dtC;
                    strHTMLCentral = clsBussCentral.ReturnHTMLCentral(dtC);
                    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLCentral.ToString() });

                }
                this.Dispose();
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Page_LoadCentrales";
                clsError.LogWrite();
            }
        }
    }
}