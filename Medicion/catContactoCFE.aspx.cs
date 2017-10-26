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


namespace Medicion
{
    public partial class catContactoCFE : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["__EVENTTARGET"] == "MiFuncion" &&
            Request["__EVENTARGUMENT"] == "")
            {
                buscar();
            
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            buscar();

        }

        private void buscar() {

            clsZone oClsZone = new clsZone();
            clsContactCFE oClsContactoCFE = new clsContactCFE();
            DataTable dtZone;
            StringBuilder strHTMLGroup = new StringBuilder();


            try
            {
                msgErrorSearch.InnerText = "";
                msgErrorSearch.Style.Add("display", "none");
                string strDivision = ddl_Divisiones.Items[ddl_Divisiones.SelectedIndex].Value;
                string strZone = string.Empty;
                if (ddl_Zonas.SelectedIndex != -1)
                {
                    strZone = ddl_Zonas.Items[ddl_Zonas.SelectedIndex].Value;
                }

                dtZone = oClsContactoCFE.ValidateFilters(strDivision, strZone);
                if (dtZone == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','warning');", true);

                    //msgErrorSearch.InnerText = "";
                    //msgErrorSearch.Style.Add("display", "inline");
                    //msgErrorSearch.InnerText = "Error al recuperar los datos";
                }
                else
                {
                    if (dtZone.Rows.Count > 0)
                    {
                        strHTMLGroup = oClsContactoCFE.ReturnHTMLDivision(dtZone);
                        DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLGroup.ToString() });
                    }
                    else
                    {

                        //msgErrorSearch.InnerText = "";
                        //msgErrorSearch.Style.Add("display", "inline");
                        //msgErrorSearch.InnerText = "No hay datos para mostrar";
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','No hay datos para mostrar','warning');", true);

                    }

                }

            }
            catch (Exception ex)
            {

                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSearch_Click";
                clsError.LogWrite();

                msgErrorSearch.InnerText = "";
                msgErrorSearch.Style.Add("display", "inline");
                msgErrorSearch.InnerHtml = ex.ToString();

            }
        }


        protected void btnaddContac_Click(object sender, EventArgs e)
        {

            //buscar();
            msgErrNew.Visible = false;
            msgErrNewDivision.Visible = false;
            cmbTitle.Text = "";
            txtName.Text = "";
            txtFN.Text = "";
            txtLN.Text = "";
            txtCharge.Text = "";
            
            txtEmail.Text = "";
            txtWorkTel.Text = "";
            txtExt.Text = "";
            txtCel.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);
            //buscar();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                return;
            }
            else
            {
                clsContactCFE oClsContactCFE = new clsContactCFE();
                Boolean bmsg = true;
                try
                {
                    msgErrNew.InnerText = "";
                    msgErrNew.Style.Add("display", "none");

                    string strTitle = cmbTitle.Text;
                    string strDivision = ddl_Divisiones_New.Items[ddl_Divisiones_New.SelectedIndex].Value;
                    string strZone = ddl_Zonas_New.Items[ddl_Zonas_New.SelectedIndex].Value;
                    string strName = txtName.Text;
                    string strFirstName = txtFN.Text;
                    string strLastName = txtLN.Text;
                    string strCharge = txtCharge.Text;
                    string strWorkTel = txtWorkTel.Text;
                    string strExt = txtExt.Text;
                    string strCel = txtCel.Text;
                    string strPuesto = txtCharge.Text;
                    string strEmail = txtEmail.Text;

                    if (string.IsNullOrEmpty(strEmail))
                    {
                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta capturar el correo electrónico.','warning');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);
                        //msgErrNewDivision.InnerText = "Falta capturar el correo electrónico.";
                        bmsg = false;
                    }
                    if (string.IsNullOrEmpty(strFirstName))
                    {
                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta capturar el apellido paterno.','warning');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);
                        //msgErrNewDivision.InnerText = "Falta capturar el apellido paterno.";
                        bmsg = false;
                    }
                    if (string.IsNullOrEmpty(strName))
                    {
                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        //msgErrNewDivision.InnerText = "Falta capturar el nombre.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta capturar el nombre.','warning');", true);

                        bmsg = false;
                    }
                    if (string.IsNullOrEmpty(strZone))
                    {
                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta seleccionar la zona.','warning');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);
                        //msgErrNewDivision.InnerText = "Falta seleccionar la zona.";
                        bmsg = false;
                    }
                    if (string.IsNullOrEmpty(strDivision))
                    {
                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta seleccionar la división y la zona','warning');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);
                        //msgErrNewDivision.InnerText = "Falta seleccionar La división y la zona";
                        bmsg = false;
                    }


                    if (bmsg)
                    {
                        string sResp = "";

                        sResp = oClsContactCFE.NewContactCFE(strTitle, strDivision, strZone, strName, strFirstName, strLastName, strCharge, strWorkTel, strExt, strCel, strEmail, strPuesto);

                        string[] aResp = sResp.Split('-');

                        if (aResp[0] == "1")
                        {

                            //msgErrNewDivision.InnerHtml = "<strong>" + aResp[1] + "</strong> .";
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','" + aResp[1] + "','success');", true);

                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            //sb.Append("<script type = 'text/javascript'>");
                            //sb.Append("window.onload=function(){");
                            ////sb.Append("  $('#msgErrNewDivision').show(); $('#btnaddContac').click();  };");
                            //sb.Append("  $('#msgErrNewDivision').removeAttr('style');");
                            //sb.Append("  $('#msgErrNewDivision').addClass('alert alert-success text-center');");
                            //sb.Append("  $('#msgErrNewDivision').removeClass('alert alert-danger text-center').addClass('alert alert-success text-center');");
                            //sb.Append("  $('#msgErrNewDivision').show();");
                            //sb.Append("    $('#btnCloseNewx').click(); ");
                            //sb.Append("</script>");
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                            buscar();

                        }
                        else
                        {

                            //msgErrNewDivision.InnerHtml = "<strong>" + aResp[1] + "</strong> .";
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','" + aResp[1] + "','error');", true);
                            buscar();
                            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);

                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            //sb.Append("<script type = 'text/javascript'>");
                            //sb.Append("window.onload=function(){");
                            ////sb.Append("  $('#msgErrNewDivision').show(); $('#btnaddContac').click();  };");
                            //sb.Append("  $('#msgErrNewDivision').removeAttr('style');");
                            //sb.Append("  $('#msgErrNewDivision').addClass('alert alert-danger text-center');");
                            //sb.Append("  $('#msgErrNewDivision').removeClass('alert alert-success text-center').addClass('alert alert-danger text-center');");
                            //sb.Append("  $('#msgErrNewDivision').show();");
                            ////sb.Append("  setTimeout(function() { window.location.reload(1); }, 2000); ");
                            //sb.Append("</script>");
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                        }
                    }
                    else
                    {

                        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        //sb.Append("<script type = 'text/javascript'>");
                        //sb.Append("window.onload=function(){");
                        ////sb.Append("  $('#msgErrNewDivision').show(); $('#btnaddContac').click();  };");
                        //sb.Append("</script>");
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

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