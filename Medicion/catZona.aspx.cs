using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Medicion.Class.LogError;
using Medicion.Class.Business;
using System.Data;
using System.Text;

namespace Medicion
{
    public partial class zona : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            clsDivision clsBussinesDivision = new clsDivision();
            try {
                if (Request["__EVENTTARGET"] == "MiFuncion" &&
                 Request["__EVENTARGUMENT"] == "")
                {
                    //cmbZone.Items.Clear();
                    Select();
                }
                if (!IsPostBack)
                {
                    //cmbDivision.Items.Clear();
                    DataTable dtG;
                    
                    dtG= clsBussinesDivision.GetAllDivision();

                    //Session["dtG"] = dtG;
                    dtG.Rows.Add(0, "0", "--TODOS--");
                    DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                    ds.Tables[0].DefaultView.Sort = " Descripción";
                    cmbDivision.DataSource = ds;
                    cmbDivision.DataTextField = "Descripción";
                    cmbDivision.DataValueField = "Id";
                    cmbDivision.DataBind();
                    cmbDivision.SelectedValue = "0";

                    ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                    cmbNewDivision.DataSource = ds;
                    cmbNewDivision.DataTextField = "Descripción";
                    cmbNewDivision.DataValueField = "Id";
                    cmbNewDivision.DataBind();
                    //cmbNewDivision.SelectedValue = "";

                    //cmbDivision.Items.Add("");                    
                    //cmbNewDivision.Items.Add("");

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
        public void Buscar()
        {
            clsZone oClsZone = new clsZone();
            DataTable dtZone;
            StringBuilder strHTMLGroup = new StringBuilder();

            try
            {
                msgErrNew.InnerText = "";
                msgErrNew.Style.Add("display", "none");
                string strDivision = cmbDivision.Items[cmbDivision.SelectedIndex].Value;
                string strZone = string.Empty;
                if (cmbZone.SelectedIndex != -1)
                {
                    strZone = cmbZone.Items[cmbZone.SelectedIndex].Value;
                }
                if (strDivision == "0") strDivision = "";

                dtZone = oClsZone.ValidateFilters(strDivision, strZone);
                if (dtZone == null)
                {
                    //msgErrNew.InnerText = "";
                    //msgErrNew.Style.Add("display", "inline");
                    //msgErrNew.InnerText = "Error al recuperar los datos";
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','warning');", true);

                }
                else
                {
                    if (dtZone.Rows.Count > 0)
                    {
                        strHTMLGroup = oClsZone.ReturnHTMLDivision(dtZone);
                        DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLGroup.ToString() });
                    }
                    else
                    {
                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        //msgErrNew.InnerText = "No hay datos para mostrar";
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','No hay datos para mostrar','warning');", true);

                    }

                }

            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSearch_Click";
                clsError.LogWrite();
                msgErrNew.InnerText = "";
                msgErrNew.Style.Add("display", "inline");
                msgErrNew.InnerHtml = ex.ToString();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e) 
        {
            Buscar();
        }

        public void Select()
        {
            clsZone clsBussinesZone = new clsZone();
            try
            {

                if (IsPostBack)
                {
                    string s = cmbDivision.Items[cmbDivision.SelectedIndex].Value;
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (s == "-- TODOS --")
                        {
                            cmbZone.Items.Clear();
                            //btnSearch_Click(sender, e);
                        }
                        else
                        {
                            DataTable dtZBD;
                            cmbZone.Items.Clear();
                            dtZBD = clsBussinesZone.dtGetZoneByDivision(s);
                            if (dtZBD == null)
                            {
                                //msgErrNew.InnerHtml = "";
                                //msgErrNew.Style.Add("display", "none");
                                //msgErrNew.InnerHtml = "Error al recuperar los datos";
                                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','warning');", true);

                            }
                            else
                            {
                                if (dtZBD.Rows.Count > 0)
                                {
                                    dtZBD.Rows.Add(-1, "-- TODOS --", 0);
                                    DataSet ds = new DataSet(); ds.Tables.Add(dtZBD.Copy());
                                    cmbZone.DataSource = ds;
                                    cmbZone.DataTextField = "Zona";
                                    cmbZone.DataValueField = "Id";
                                    cmbZone.DataBind();
                                    //cmbZone.Items.Add("");
                                    //cmbZone.SelectedValue = "";
                                    msgErrNew.InnerHtml = "";
                                }


                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "itemSelected";
                clsError.LogWrite();
                msgErrNew.InnerHtml = "";
                msgErrNew.Style.Add("display", "none");
                msgErrNew.InnerHtml = ex.ToString();
            }

        }
        protected void itemSelected(object sender, EventArgs e)
        {
            Select();
            
        }
        
        protected void btnAddZone_Click(object sender, EventArgs e)
        {
            clsZone clsBussinesZone = new clsZone();
            Boolean bNewZone = true;
            try
            {
                string strDivision = cmbNewDivision.Items[cmbNewDivision.SelectedIndex].Value;
                string strCveZone = txtNewCveZone.Text;
                string strZone = txtNewZone.Text;
                string strObservations = txtNewObservations.Text;
                
                if (txtNewObservations.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Se debe capturar las observaciones de la zona','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);

                    //msgErrNewDivision.InnerText = "Se debe capturar las observaciones de la zona!";
                    ////msgErrNewDivision.Style.Add("display", "inline");
                    bNewZone = false;
                }                
                if (txtNewZone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Se debe capturar la descripción de la zona','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);

                    //msgErrNewDivision.InnerText = "Se debe capturar la descripción de la zona!";
                    ////msgErrNewDivision.Style.Add("display", "inline");
                    bNewZone = false;
                }
                if (txtNewCveZone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Se debe capturar la clave de la zona','warning');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);

                    //msgErrNewDivision.InnerText = "Se debe capturar la clave de la zona!";
                    //msgErrNewDivision.Style.Add("display", "inline");
                    bNewZone = false;
                }


                if (bNewZone)
                {
                    string sResp = "";

                    sResp = clsBussinesZone.NewZone(strDivision, strCveZone, strZone, strObservations);

                    string[] aResp = sResp.Split('-');

                    if (aResp[0] == "1")
                    {

                        //msgErrNewDivision.InnerHtml = "<strong>" + aResp[1] + "</strong> .";
                        ScriptManager.RegisterStartupScript(this, GetType(), "OcultaModal", "myFunc();", true);

                       ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','" + aResp[1] + "','success');", true);
                        Select();
                        Buscar();
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("  $('#msgErrNewDivision').show(); $('#btnAddZona').click();  };");
                        sb.Append("  $('#msgErrNewDivision').removeAttr('style');");
                        sb.Append("  $('#msgErrNewDivision').addClass('alert alert-success text-center');");
                        sb.Append("  $('#msgErrNewDivision').removeClass('alert alert-danger text-center').addClass('alert alert-success text-center');");
                        sb.Append("  $('#msgErrNewDivision').show();");
                        sb.Append("   $('#btnCloseNewx').click(); ");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());


                        clsZone oClsZone = new clsZone();
                        DataTable dtZone;
                        StringBuilder strHTMLGroup = new StringBuilder();

                        try
                        {
                            msgErrNew.InnerText = "";
                            msgErrNew.Style.Add("display", "none");
                            string strDivision1 = cmbDivision.Items[cmbDivision.SelectedIndex].Value;
                            string strZone1 = string.Empty;
                            if (cmbZone.SelectedIndex != -1)
                            {
                                strZone1 = cmbZone.Items[cmbZone.SelectedIndex].Value;
                            }

                            dtZone = oClsZone.ValidateFilters(strDivision1, strZone1);
                            if (dtZone == null)
                            {
                                //msgErrNew.InnerText = "";
                                //msgErrNew.Style.Add("display", "inline");
                                //msgErrNew.InnerText = "Error al recuperar los datos";
                                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','error');", true);
                              //  ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);

                            }
                            else
                            {
                                if (dtZone.Rows.Count > 0)
                                {
                                    strHTMLGroup = oClsZone.ReturnHTMLDivision(dtZone);
                                    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLGroup.ToString() });
                                }
                                else
                                {
                                    //msgErrNew.InnerText = "";
                                    //msgErrNew.Style.Add("display", "inline");
                                    //msgErrNew.InnerText = "No hay datos para mostrar";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','No hay datos para mostrar','error');", true);
                                //    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);

                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            clsError.logMessage = ex.ToString();
                            clsError.logModule = "btnSearch_Click";
                            clsError.LogWrite();
                            msgErrNew.InnerText = "";
                            msgErrNew.Style.Add("display", "inline");
                            msgErrNew.InnerHtml = ex.ToString();
                        }


                    }
                    else
                    {

                        //msgErrNewDivision.InnerHtml = "<strong>" + aResp[1] + "</strong> .";
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','" + aResp[1] + "','error');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("  $('#msgErrNewDivision').show(); $('#btnAddZona').click();  };");
                        sb.Append("  $('#msgErrNewDivision').removeAttr('style');");
                        sb.Append("  $('#msgErrNewDivision').addClass('alert alert-danger text-center');");
                        sb.Append("  $('#msgErrNewDivision').removeClass('alert alert-success text-center').addClass('alert alert-danger text-center');");
                        sb.Append("  $('#msgErrNewDivision').show();");
                        //sb.Append("  setTimeout(function() { window.location.reload(1); }, 2000); ");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());                        

                    }

                }
                else
                {
                    
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");                                   
                    sb.Append("  $('#msgErrNewDivision').show(); $('#btnAddZona').click();  };");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                }

            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "itemSelected";
                clsError.LogWrite();
                msgErrNew.InnerHtml = "";
                msgErrNew.Style.Add("display", "none");
                msgErrNew.InnerHtml = ex.ToString();
            }
           

        
        }

        protected void btnAddZona_Click(object sender, EventArgs e)
        {
            msgErrNewDivision.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModal('NewDivisionModal');", true);
            txtNewCveZone.Text = "";
            txtNewZone.Text = "";
            txtNewObservations.Text = "";
            Buscar();
        }
    }
}