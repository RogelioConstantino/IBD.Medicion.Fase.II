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

                    //DataTable dtC = clsBussCentral.GetAllCentrales();
                    //Session["dtC"] = dtC;
                    //strHTMLCentral = clsBussCentral.ReturnHTMLCentral(dtC);
                    //DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLCentral.ToString() });

                    DataTable Datos = clsBussCentral.GetAllCentrales();

                    GridView1.DataSource = Datos ;
                    GridView1.DataBind();
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

        public void LlenarGrid1() {
            clsCentral clsBussCentral2 = new clsCentral();

            GridView1.DataSource = clsBussCentral2.GetAllCentrales();
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              GridView  GridView2 = (GridView)(e.Row.FindControl("GridView2"));
                Panel pnlGrid = (Panel)(e.Row.FindControl("pnlGrid"));
                Label btnShowHide = (Label)(e.Row.FindControl("btnShowHide"));
                //  DataRowView CategoryId = (DataRowView)(e.Row.DataItem("IdCentral"));
                // var controlEditar = (Control)sender;
                // GridViewRow renglonGrid = (GridViewRow)controlEditar.NamingContainer;
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                String state = rowView["IdCentral"].ToString();
                int idCentral = Convert.ToInt32(state) ;

                string ShowHideScript = "ToggleVisiblity(this,'" + pnlGrid.ClientID + "');return false";

                btnShowHide.Attributes.Add("onclick", ShowHideScript);
                clsCentral clsBussCentral2 = new clsCentral();

                GridView2.DataSource = clsBussCentral2.ConveniosByCentral(idCentral);
                GridView2.DataBind();

            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView GridView2 = (GridView)(e.Row.FindControl("GridView3"));
                Panel pnlGrid = (Panel)(e.Row.FindControl("pnlGrid2"));
                Label btnShowHide = (Label)(e.Row.FindControl("btnShowHide2"));
                //  DataRowView CategoryId = (DataRowView)(e.Row.DataItem("IdCentral"));
                // var controlEditar = (Control)sender;
                // GridViewRow renglonGrid = (GridViewRow)controlEditar.NamingContainer;
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                String state = rowView["IdConvenio"].ToString();
                int idCovenio = Convert.ToInt32(state);

                string ShowHideScript = "ToggleVisiblity(this,'" + pnlGrid.ClientID + "');return false";

                btnShowHide.Attributes.Add("onclick", ShowHideScript);
                clsCentral clsBussCentral2 = new clsCentral();

                GridView2.DataSource = clsBussCentral2.ConveniosByConvenio(idCovenio);
                GridView2.DataBind();

            }
        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var controlEditar = (Control)sender;
                GridViewRow renglonGrid = (GridViewRow)controlEditar.NamingContainer;
                int idConvenio = Convert.ToInt32(GridView1.DataKeys[renglonGrid.RowIndex].Values["IdCentral"]);

                //var examen = new ExamenesBO().ObtenerExamen(idExamen);
                ViewState["IdConvenio"] = idConvenio;
                clsCentral oCls = new clsCentral();
                var Convenio = oCls.CentralBYIdCentral(idConvenio);

                foreach (DataRow dtRow in Convenio.Rows)
                {
                    // On all tables' columns
                    hdIdEdit.Value = dtRow[0].ToString();
                    txtConvenioEdit.Text = dtRow[1].ToString();
                    txtDescripcionEdit.Text = dtRow[2].ToString();
                  
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraModal", "mostrarModal('edit');", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var controlEditar = (Control)sender;
                GridViewRow renglonGrid = (GridViewRow)controlEditar.NamingContainer;
                int idConvenio = Convert.ToInt32(GridView1.DataKeys[renglonGrid.RowIndex].Values["IdCentral"]);

                //var examen = new ExamenesBO().ObtenerExamen(idExamen);
                ViewState["IdConvenio"] = idConvenio;
                clsCentral oCls = new clsCentral();
                var Convenio = oCls.CentralBYIdCentral(idConvenio);

                foreach (DataRow dtRow in Convenio.Rows)
                {
                    // On all tables' columns
                    hdIddelet.Value = dtRow[0].ToString();
                    txtConveniodelet.Text = dtRow[1].ToString();
                    txtdescripciondelet.Text = dtRow[2].ToString();

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraModal", "mostrarModal('delete');", true);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}