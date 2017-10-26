using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medicion
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Encrypt clsEncrypt = new Class.Encrypt();

            string strEmail = Convert.ToString(Session["IdUsuario"]);
            if (string.IsNullOrEmpty(strEmail))
            {
                Response.Redirect("Default.aspx");                
            }
            else {
                clsEncrypt.strData = Session["Fullname"].ToString();
                lblUserName.Text = clsEncrypt.DecryptData();
            }

            
                        
        }
    }
}