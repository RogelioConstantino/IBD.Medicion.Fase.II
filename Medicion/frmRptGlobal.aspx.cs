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

namespace Medicion
{
    public partial class frmRptGlobal : System.Web.UI.Page
    {
        StringBuilder strHTMLElectric = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            clsGeneralReport oclsGeneralReport = new clsGeneralReport();
            if (!IsPostBack)
            {
                dtGR = oclsGeneralReport.GetGeneralReport("1", "", "1", "1", "0","0","0","0","0");
                if (dtGR != null && (dtGR.Rows.Count > 0))
                {
                    Session["dtGR"] = dtGR;
                    strHTMLElectric = oclsGeneralReport.CreateTableHTML(dtGR);
                    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                }
            }
        }
    }
}