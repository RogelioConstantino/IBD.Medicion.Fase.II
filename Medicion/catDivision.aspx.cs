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
    public partial class division : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        StringBuilder strHTMLGroup = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            clsDivision clsBussinesDivision = new clsDivision();
            try
            {
                if (!IsPostBack)
                {
                    DataTable dtG = clsBussinesDivision.GetAllDivision();
                    Session["dtG"] = dtG;
                    strHTMLGroup = clsBussinesDivision.ReturnHTMLDivision(dtG);
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