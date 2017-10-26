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
    public partial class catGestores : System.Web.UI.Page
    {

        LogErrorMedicion clsError = new LogErrorMedicion();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["__EVENTTARGET"] == "MiFuncion" &&
                  Request["__EVENTARGUMENT"] == "")
                {
                    buscar();

                }
                if (!IsPostBack)
                {
                    buscar();
                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Page_Load";
                clsError.LogWrite();
            }

        }
        private void buscar()
        {
            clsGestores clsGes = new clsGestores();
            StringBuilder strHTML = new StringBuilder();

            DataTable dtC = clsGes.dtGetAll();
            Session["dtC"] = dtC;
            strHTML = clsGes.ReturnHTML(dtC);
            DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTML.ToString() });


        }

        protected void btnAddGestor_Click(object sender, EventArgs e)
        {

            clsGestores oCls = new clsGestores();
            Boolean bmsg = true;
            try
            {                
                string strDivision = ddl_Tipo_New.Items[ddl_Tipo_New.SelectedIndex].Value;
                string strZone = ddl_Rol_New.Items[ddl_Rol_New.SelectedIndex].Value;
                string strNumEmpleado = txtNumempleado.Text;
                string strName = txtName.Text;
                string strFirstName = txtAP.Text;
                string strLastName = txtAM.Text;
                string strIniciales = txtIniciales.Text;
                if (string.IsNullOrEmpty(strIniciales))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta capturar las iniciales del Gestor','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);

                    //msgErrNew.InnerText = "Falta capturar las iniciales del Gestor";
                    bmsg = false;
                }
                if (string.IsNullOrEmpty(strNumEmpleado))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta capturar el número de empleado del gestor','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);

                    //msgErrNew.InnerText = "Falta capturar el número de empleado del gestor";
                    bmsg = false;
                }
                if (string.IsNullOrEmpty(strZone))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta seleccionar el Rol del Gestor','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);

                    //msgErrNew.InnerText = "Falta seleccionar el Rol del Gestor";
                    bmsg = false;
                }
                if (string.IsNullOrEmpty(strDivision))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta seleccionar el Tipo de Gestor','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);

                    //msgErrNew.InnerText = "Falta seleccionar el Tipo de Gestor";
                    bmsg = false;
                }
                if (string.IsNullOrEmpty(strName))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta capturar el nombre del gestor','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);

                    //msgErrNew.InnerText = "Falta capturar el nombre del gestor";
                    bmsg = false;
                }
                if (string.IsNullOrEmpty(strFirstName))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta capturar el apellido paterno del Gestor','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);

                    //msgErrNew.InnerText = "Falta capturar el apellido paterno del Gestor";
                    bmsg = false;
                }
                //if (string.IsNullOrEmpty(strLastName))
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Falta capturar el apellido materno del Gestor','warning');", true);
                //    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);

                //    //msgErrNew.InnerText = "Falta capturar el apellido paterno del Gestor";
                //    bmsg = false;
                //}
                


                if (bmsg)
                {
                    string sResp = "";

                    sResp = oCls.New(strZone, strName, strFirstName, strLastName, strNumEmpleado,  strIniciales);

                    string[] aResp = sResp.Split('-');

                    if (aResp[0] == "1")
                    {
                        buscar();

                        //msgErrNew.InnerText = "";
                        //msgErrNew.InnerHtml = "<strong>" + aResp[1] + "</strong> .";
                       ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','" + aResp[1] + "','success');", true);

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("  $('#msgErrNew').show(); $('#btnaddGestor').click();  };");
                        sb.Append("  $('#msgErrNew').removeAttr('style');");
                        sb.Append("  $('#msgErrNew').addClass('alert alert-success text-center');");
                        sb.Append("  $('#msgErrNew').removeClass('alert alert-danger text-center').addClass('alert alert-success text-center');");
                        sb.Append("  $('#msgErrNew').show();");
                        sb.Append("    $('#btnCloseNewx').click(); ");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }
                    else
                    {
                        buscar();

                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','" + aResp[1] + "','error');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);

                        //msgErrNew.InnerHtml = "<strong>" + aResp[1] + "</strong> .";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("  $('#msgErrNew').show(); $('#btnaddGestor').click();  };");
                        sb.Append("  $('#msgErrNew').removeAttr('style');");
                        sb.Append("  $('#msgErrNew').addClass('alert alert-danger text-center');");
                        sb.Append("  $('#msgErrNew').removeClass('alert alert-success text-center').addClass('alert alert-danger text-center');");
                        sb.Append("  $('#msgErrNew').show();");
                        //sb.Append("  setTimeout(function() { window.location.reload(1); }, 2000); ");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());                        
                    }
                }
                else
                {
                    buscar();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("  $('#msgErrNew').show(); $('#btnaddGestor').click();  };");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Page_Load";
                clsError.LogWrite();
            }
        }
        protected void btnaddGestor2_Click(object sender, EventArgs e)
        {
            txtNumempleado.Text = "";
            txtName.Text = "";
            txtIniciales.Text = "";
            txtAP.Text = "";
            txtAM.Text = "";
            msgErrNew.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewCentralModal');", true);
            buscar();
        }
    }
}