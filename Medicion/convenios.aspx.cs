using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using Medicion.Class.Business;
using Medicion.Class.LogError;
using System.Text;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace Medicion
{
    public partial class convenios : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        protected void Page_Load(object sender, EventArgs e)
        {
            clsCentral clsBussinesCentral = new clsCentral();
            if (!IsPostBack)
            {
                //cmbDivision.Items.Clear();
                msgExito.Visible = false;
                msgErrNew.Visible = false;
                DataTable dtG;

                dtG = clsBussinesCentral.GetAllCentrales();

                //Session["dtG"] = dtG;
                //dtG.Rows.Add(0, "0", "-- TODOS --");
                DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                ds.Tables[0].DefaultView.Sort = "Descripción";
                cmbCentral.DataSource = ds;
                cmbCentral.DataTextField = "Descripción";
                cmbCentral.DataValueField = "IdCentral";
                cmbCentral.DataBind();
                //cmbCentral.Items.Add("");
               // cmbCentral.SelectedValue = "";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()

        {
            msgErrNew.Visible = false;
            clsConvenios oCls = new clsConvenios();
            StringBuilder strHTML = new StringBuilder();
            string strLoadingCharge = string.Empty;
            DataTable dtConvenios;
            try
            {
                //msgErrNew.InnerText = "";
                //msgErrNew.Style.Add("display", "none");
                string strCentral = cmbCentral.Items[cmbCentral.SelectedIndex].Value;

                if (strCentral != "" && strCentral != "0")
                {
                    dtConvenios = oCls.GetPorCentrales(strCentral);
                    if (dtConvenios.Rows.Count > 0)
                    {
                        //strHTML = oCls.ReturnHTMLConvenios(dtConvenios);
                        //DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTML.ToString() });
                        //dtConvenios = null;
                        //agregar tabla89
                        gvConvenios.Visible = true;
                        gvConvenios.DataSource = oCls.GetPorCentrales(strCentral);
                        gvConvenios.DataBind();

                    }
                    else
                    {
                        //msgErrNew.Visible = true;
                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        //msgErrNew.InnerText = "No hay datos para mostrar";
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('No hay datos para mostrar','error');", true);

                        gvConvenios.Visible = false;
                    }
                }
                else
                {
                    //msgErrNew.Visible = true;
                    //msgErrNew.InnerText = "";
                    //msgErrNew.Style.Add("display", "inline");
                    //msgErrNew.InnerText = "Debes selecionar una Central";
                    gvConvenios.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Debes selecionar una Central','error');", true);

                }

            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSearch_Click";
                clsError.LogWrite();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string mensaje = serializer.Serialize(ex.Message);
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error al cargar los Grupos Detalle: " + mensaje + "');", true);
            }

        }

        
        public void  Actualizar()
        {
            buscar();
           
        }
        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            msgExito.Visible = false;
            msgErrNew.Visible = false;

            try
            {
                var controlEditar = (Control)sender;
                GridViewRow renglonGrid = (GridViewRow)controlEditar.NamingContainer;
                int idConvenio = Convert.ToInt32(gvConvenios.DataKeys[renglonGrid.RowIndex].Values["Id"]);

                //var examen = new ExamenesBO().ObtenerExamen(idExamen);
                ViewState["IdConvenio"] = idConvenio;
                clsConvenios oCls = new clsConvenios();
                var Convenio = oCls.GetPorIdConvenio(idConvenio);

                foreach (DataRow dtRow in Convenio.Rows)
                {
                    // On all tables' columns
                    hdId.Value = dtRow[0].ToString();
                    txtConvenio.Text = dtRow[1].ToString();
                    txtDescripcion.Text = dtRow[2].ToString();
                    var Estatus = dtRow[4].ToString();
                    if (Estatus == "Anterior")
                    {
                        ddl_Estatus.Items.Clear();
                        ddl_Estatus.Items.Add("Anterior");
                        ddl_Estatus.Items.Add("Inactivo");
                    }
                    else if (Estatus == "Actual")
                    {
                        ddl_Estatus.Items.Clear();
                        ddl_Estatus.Items.Add("Actual");
                        ddl_Estatus.Items.Add("Inactivo");
                        ddl_Estatus.Items.Add("Anterior");
                    }
                    else if (Estatus == "Por Entrar")
                    {
                        ddl_Estatus.Items.Clear();
                        ddl_Estatus.Items.Add("Por Entrar");
                        ddl_Estatus.Items.Add("Inactivo");
                        ddl_Estatus.Items.Add("Actual");
                    }
                    else if (Estatus == "Inactivo")
                    {
                        ddl_Estatus.Items.Clear();
                        ddl_Estatus.Items.Add("Inactivo");
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraModal", "mostrarModal('edit');", true);
            }
            catch (Exception)
            {

                throw;
            }
            //}

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            msgExito.Visible = false;
            msgErrNew.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarError", "CerrarError();", true);

            try
            {
                if (txtDescripcion.Text =="")
                {
                    //msgExito.Visible = false;
                    //msgExito.Visible = false;
                    //msgErrNew.Visible = true;
                    //JavaScriptSerializer serializer = new JavaScriptSerializer();
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','El campo descripción de convenio no debe estar vacío','error');", true);
                }
                else if (ddl_Estatus.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','El campo estatus no debe estar vacío','error');", true);
                }
                else
                {
                    clsConvenios oCls = new clsConvenios();
                    oCls.update(int.Parse(hdId.Value),cmbCentral.Text, txtDescripcion.Text, ddl_Estatus.Text);
                    
                    buscar();
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Información actualizada correctamente','success');", true);

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void gvConvenios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string strCentral = cmbCentral.Items[cmbCentral.SelectedIndex].Value;
            clsConvenios oCls = new clsConvenios();
            gvConvenios.Visible = true;
            gvConvenios.PageIndex = e.NewPageIndex;
            buscar();
            //gvConvenios.DataSource = oCls.GetPorCentrales(strCentral);
            //gvConvenios.DataBind();
        }
        //protected void UpdatePanel1_Load(object sender, EventArgs e)
        //{
        //    gvConvenios.DataBind();
        //}
    }
}

