using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medicion.Class.LogError;
using Medicion.Class.Business;
using System.Text;
using System.Data;
namespace Medicion
{
    public partial class estatus : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        StringBuilder strHTMLGroup = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            clsStatus clsBussinesStatus = new clsStatus();

            try
            {
                if (!IsPostBack)
                {
                    DataTable dtG = clsBussinesStatus.GetAllStatus();
                    Session["dtG"] = dtG;
                    strHTMLGroup = clsBussinesStatus.ReturnHTMLDivision(dtG);
                    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLGroup.ToString() });

                }
                this.Dispose();
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