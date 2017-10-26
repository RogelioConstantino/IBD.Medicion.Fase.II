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
    public partial class contactocfe : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        protected void Page_Load(object sender, EventArgs e)
        {
            TabName.Value = Request.Form[TabName.UniqueID];
            clsDivision clsBussinesDivision = new clsDivision();
            try
            {
                if (!IsPostBack)
                {
                    //cmbDivision.Items.Clear();
                    DataTable dtG;
                    dtG = clsBussinesDivision.GetAllDivision();
                    
                    DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
                    ds.Tables[0].DefaultView.Sort = " Descripción";
                    cmbNewDivision.DataSource = ds;
                    cmbNewDivision.DataTextField = "Descripción";
                    cmbNewDivision.DataValueField = "Id";
                    cmbNewDivision.DataBind();                    
                    dtG.Rows.Add(0, "0", "-- TODOS --");
                    DataSet dss = new DataSet();
                    dss.Tables.Add(dtG.Copy());
                    cmbSearchDivision.DataSource = dss;
                    cmbSearchDivision.DataTextField = "Descripción";
                    cmbSearchDivision.DataValueField = "Id";
                    cmbSearchDivision.DataBind();

                    cmbNewDivision.Items.Add("");
                    cmbNewDivision.SelectedValue = "";
                    cmbSearchDivision.Items.Add("");
                    cmbSearchDivision.SelectedValue = "";

                    Buscar();

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
        /// <summary>
        /// Save new CFE contact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            clsContactCFE oClsContactCFE = new clsContactCFE();
            Boolean bmsg = false;
            try
            {
                msgErrNew.InnerText = "";
                msgErrNew.Style.Add("display", "none");
                string strTitle = cmbTitle.Items[cmbTitle.SelectedIndex].Value;
                string strDivision = cmbNewDivision.Items[cmbNewDivision.SelectedIndex].Value;
                string strZone = cmbNewZone.Items[cmbNewZone.SelectedIndex].Value;
                string strName = txtName.Text;
                string strFirstName = txtFN.Text;
                string strLastName = txtLN.Text;
                string strCharge = txtCharge.Text;
                string strWorkTel = txtWorkTel.Text;
                string strExt = txtExt.Text;
                string strCel = txtCel.Text;
                string strPuesto = txtCharge.Text;
                string strEmail = txtEmail.Text;

                if (string.IsNullOrEmpty(strDivision))
                {
                    msgErrNew.InnerText = "";
                    msgErrNew.Style.Add("display", "inline");
                    msgErrNew.InnerText = "Falta seleccionar La división y la zona";

                }
                if (string.IsNullOrEmpty(strZone))
                {
                    msgErrNew.InnerText = "";
                    msgErrNew.Style.Add("display", "inline");
                    msgErrNew.InnerText = "Falta seleccionar La división y la zona";
                }

                if (!string.IsNullOrEmpty(strDivision) && !string.IsNullOrEmpty(strZone) && !string.IsNullOrEmpty(strName))
                {
                    bmsg = oClsContactCFE.NewContactCFE(strTitle, strDivision, strZone, strName, strFirstName, strLastName, strCharge, strWorkTel, strExt, strCel, strEmail, strPuesto);
                    if (!bmsg)
                    {
                        msgErrNew.InnerText = "";
                        msgErrNew.Style.Add("display", "inline");
                        msgErrNew.InnerText = "Falta seleccionar La división y la zona";
                    }
                    else 
                    {
                        Response.Redirect("contactocfe.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Page_Load";
                clsError.LogWrite();
            }

        }
        protected void itemSelectedSearch(object sender, EventArgs e)
        {

            clsZone clsBussinesZone = new clsZone();
            try
            {

                if (IsPostBack)
                {
                    string s = cmbSearchDivision.Items[cmbSearchDivision.SelectedIndex].Value;
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (s == "-- TODOS --" || string.IsNullOrEmpty(s))
                        {
                            cmbSearchZone.Items.Clear();
                            btnSearch_Click(sender, e);
                        }
                        else
                        {
                            DataTable dtZBD;
                            cmbSearchZone.Items.Clear();
                            dtZBD = clsBussinesZone.dtGetZoneByDivision(s);
                            if (dtZBD == null)
                            {
                                msgErrorSearch.InnerHtml = "";
                                msgErrorSearch.Style.Add("display", "none");
                                msgErrorSearch.InnerHtml = "Error al recuperar los datos";
                            }
                            else
                            {
                                if (dtZBD.Rows.Count > 0)
                                {

                                    dtZBD.Rows.Add(0, "-- TODOS --",0);
                                    DataSet ds = new DataSet(); ds.Tables.Add(dtZBD.Copy());
                                    cmbSearchZone.DataSource = ds;
                                    cmbSearchZone.DataTextField = "Zona";
                                    cmbSearchZone.DataValueField = "Id";
                                    cmbSearchZone.DataBind();
                                    cmbSearchZone.Items.Add("");
                                    cmbSearchZone.SelectedValue = "";
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
                msgErrorSearch.InnerHtml = "";
                msgErrorSearch.Style.Add("display", "none");
                msgErrorSearch.InnerHtml = ex.ToString();
            }

        }
        /// <summary>
        /// Get Zone value from Division Selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void itemSelected(object sender, EventArgs e)
        {
            clsZone clsBussinesZone = new clsZone();
            try
            {
                    string s = cmbNewDivision.Items[cmbNewDivision.SelectedIndex].Value;
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (s == "-- TODOS --")
                        {
                            cmbNewZone.Items.Clear();
                            //btnSearch_Click(sender, e);
                        }
                        else
                        {
                            DataTable dtZBD;
                            cmbNewZone.Items.Clear();
                            dtZBD = clsBussinesZone.dtGetZoneByDivision(s);
                            if (dtZBD == null)
                            {
                                msgErrNew.InnerHtml = "";
                                msgErrNew.Style.Add("display", "none");
                                msgErrNew.InnerHtml = "Error al recuperar los datos";
                            }
                            else
                            {
                                if (dtZBD.Rows.Count > 0)
                                {                                   
                                    DataSet ds = new DataSet(); ds.Tables.Add(dtZBD.Copy());
                                    cmbNewZone.DataSource = ds;
                                    cmbNewZone.DataTextField = "Zona";
                                    cmbNewZone.DataValueField = "Id";
                                    cmbNewZone.DataBind();
                                    cmbNewZone.Items.Add("");
                                    cmbNewZone.SelectedValue = "";
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar() {

            clsZone oClsZone = new clsZone();
            clsContactCFE oClsContactoCFE = new clsContactCFE();
            DataTable dtZone;
            StringBuilder strHTMLGroup = new StringBuilder();

            try
            {
                msgErrorSearch.InnerText = "";
                msgErrorSearch.Style.Add("display", "none");
                string strDivision = cmbSearchDivision.Items[cmbSearchDivision.SelectedIndex].Value;
                string strZone = string.Empty;
                if (cmbSearchZone.SelectedIndex != -1)
                {
                    strZone = cmbSearchZone.Items[cmbSearchZone.SelectedIndex].Value;
                }

                dtZone = oClsContactoCFE.ValidateFilters(strDivision, strZone);
                if (dtZone == null)
                {
                    msgErrorSearch.InnerText = "";
                    msgErrorSearch.Style.Add("display", "inline");
                    msgErrorSearch.InnerText = "Error al recuperar los datos";
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
                        msgErrorSearch.InnerText = "";
                        msgErrorSearch.Style.Add("display", "inline");
                        msgErrorSearch.InnerText = "No hay datos para mostrar";
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
    }
}