using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Medicion.Class.LogError;
using System.Data;

namespace Medicion
{
    public partial class _Default : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        DataTable Usr;
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtUsrEmail.autoFocus = True;
            txtUsrEmail.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strEmailusr = string.Empty;
            string strIdUsuario = string.Empty;
            string strIdRol = string.Empty;

            try 
            {
                string FullName = string.Empty;
                if (IsPostBack)
                {

                    if (txtUsrEmail.Text =="")
                    {ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta usuario ','error');", true);
                        txtUsrEmail.Focus();}
                    else if (txtPassword.Text == "")
                    {ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta contraseña ','error');", true);
                        txtPassword.Focus();}
                    Class.Login Exists = new Class.Login();
                    Class.Encrypt clsEncrypt = new Class.Encrypt();
                    clsEncrypt.strData = txtPassword.Text.ToString();
                    Exists.UserName = txtUsrEmail.Text.ToString();
                    Exists.Password = clsEncrypt.EncryptData();

                    DataTable Usr = Exists.GetUser();

                    if (Usr.Rows.Count > 0)
                    {
                        foreach (DataRow row in Usr.Rows)
                        {
                            FullName = Convert.ToString(row["FirstName"]) + " " + Convert.ToString(row["LastName"]);
                            strEmailusr = Convert.ToString(row["Email"]);
                            strIdUsuario = Convert.ToString(row["IdGestor"]);
                            strIdRol = Convert.ToString(row["IdRol"]);
                        }

                    }

                    if (!string.IsNullOrWhiteSpace(FullName))
                    {
                        clsEncrypt.strData = FullName;
                        Session["Fullname"] = clsEncrypt.EncryptData(); 
                        clsEncrypt.strData = txtUsrEmail.Text;
                        Session["email"] = strEmailusr;
                        Session["IdUsuario"] = strIdUsuario;
                        Session["Rol"] = strIdRol;
                        Response.Redirect("principal.aspx");

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Usuario o contraseña inválido ','error');", true);
                        txtUsrEmail.Text = "";
                       // ErrorMsg.InnerHtml = "<div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Error:</strong> </div>";
                    }
                }
                
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Button1_Click";
                clsError.LogWrite();
            }
            
        }
    }
}
