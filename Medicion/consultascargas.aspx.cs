using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medicion.Class.Business;
using Medicion.Class.LogError;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace Medicion
{
    public partial class consultascargas : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        protected void Page_Load(object sender, EventArgs e)
        {
            clsElectricMeters oClsElectricMeters = new clsElectricMeters();
            try {
                if (!IsPostBack) {
                    FillGestorMedicion();

                    DataTable dtAllGroups;
                    oClsElectricMeters.intActive = 1;
                    oClsElectricMeters.IdGMedicion = 0;
                    dtAllGroups = oClsElectricMeters.GetAllDistinctGroup();
                    cmbGroup.DataSource = dtAllGroups;
                    cmbGroup.DataTextField = "Grupo";
                    cmbGroup.DataValueField = "IdGrupo";                   
                    cmbGroup.DataBind();
                   // CargaDDL();
                    //CargarCentral();
                  
                    cmbGroup.Items.Add("-- TODOS --");
                    cmbGroup.SelectedValue = "-- TODOS --";

                }
                //CargaDDL();
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Page_Load";
                clsError.LogWrite();
            }
        }

        //public void CargaDDL()
        //{
        //    clsElectricMeters oClsElectricMeters = new clsElectricMeters();
        //    DataTable dtGetLadingCharge;
        //    try
        //    {
        //            string strGroup = cmbGroup.Items[cmbGroup.SelectedIndex].Value;
        //            if (!string.IsNullOrEmpty(strGroup))
        //            {
        //                if (strGroup == "-- TODOS --")
        //                {
        //                    //cmbLoadingCharge.Items.Clear();
        //                    ddl.Items.Clear();
        //                    oClsElectricMeters.intActive = 1;
        //                    dtGetLadingCharge = oClsElectricMeters.GetAllGroup();
        //                    if (dtGetLadingCharge == null)
        //                    {
        //                        //msgErrNew.InnerHtml = "";
        //                        //msgErrNew.Style.Add("display", "none");
        //                        //msgErrNew.InnerHtml = "Error al recuperar los datos";
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','Error al recuperar los datos.','error');", true);

        //                    }
        //                    else
        //                    {
        //                        if (dtGetLadingCharge.Rows.Count > 0)
        //                        {
        //                            DataSet dsAll = new DataSet(); dsAll.Tables.Add(dtGetLadingCharge.Copy());
        //                            ddl.DataSource = dsAll;
        //                            ddl.DataTextField = "RPUPuntoCarga";
        //                            ddl.DataValueField = "RPU";
        //                            ddl.DataBind();
        //                            //ddl.Items.Add("-- TODOS --");
        //                            //ddl.SelectedValue = "-- TODOS --";

        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    oClsElectricMeters.strGroup = strGroup;
        //                    oClsElectricMeters.intActive = 1;
        //                    dtGetLadingCharge = oClsElectricMeters.GetLoadingCharge();
        //                    if (dtGetLadingCharge == null)
        //                    {
        //                        //msgErrNew.InnerHtml = "";
        //                        //msgErrNew.Style.Add("display", "none");
        //                        //msgErrNew.InnerHtml = "Error al recuperar los datos";
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','Error al recuperar los datos.','error');", true);

        //                    }
        //                    else
        //                    {
        //                        if (dtGetLadingCharge.Rows.Count > 0)
        //                        {
        //                            DataSet ds = new DataSet(); ds.Tables.Add(dtGetLadingCharge.Copy());
        //                            ddl.DataSource = ds;
        //                            ddl.DataTextField = "RPUPuntoCarga";
        //                            ddl.DataValueField = "RPU";
        //                            ddl.DataBind();
        //                            // ddl.Items.Add("-- TODOS --");
        //                            //ddl.SelectedValue = "-- TODOS --";

        //                        }
        //                    }
        //                }
        //            }           
        //    }
        //    catch (Exception ex)
        //    {
        //        clsError.logMessage = ex.ToString();
        //        clsError.logModule = "itemSelected";
        //        clsError.LogWrite();
        //        //msgErrNew.InnerHtml = "";
        //        //msgErrNew.Style.Add("display", "none");
        //        //msgErrNew.InnerHtml = ex.ToString();
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        string mensaje = serializer.Serialize(ex.Message);
        //        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error  Detalle: " + mensaje + "');", true);

        //    }
        //}


        //protected void itemSelected(object sender, EventArgs e)
        //{
        //    clsElectricMeters oClsElectricMeters = new clsElectricMeters();
        //    DataTable dtGetLadingCharge;
        //    try
        //    {
        //        if (IsPostBack)
        //        {
        //            string strGroup = cmbGroup.Items[cmbGroup.SelectedIndex].Value;
        //            if (!string.IsNullOrEmpty(strGroup))
        //            {
        //                if (strGroup == "-- TODOS --")
        //                {
        //                    //cmbLoadingCharge.Items.Clear();
        //                    ddl.Items.Clear();
        //                    oClsElectricMeters.intActive = 1;
        //                    dtGetLadingCharge = oClsElectricMeters.GetAllGroup();
        //                    if (dtGetLadingCharge == null)
        //                    {
        //                        //msgErrNew.InnerHtml = "";
        //                        //msgErrNew.Style.Add("display", "none");
        //                        //msgErrNew.InnerHtml = "Error al recuperar los datos";
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','Error al recuperar los datos.','error');", true);

        //                    }
        //                    else
        //                    {
        //                        if (dtGetLadingCharge.Rows.Count > 0)
        //                        {
        //                            DataSet dsAll = new DataSet(); dsAll.Tables.Add(dtGetLadingCharge.Copy());
        //                            ddl.DataSource = dsAll;
        //                            ddl.DataTextField = "RPUPuntoCarga";
        //                            ddl.DataValueField = "RPU";
        //                            ddl.DataBind();
        //                            //ddl.Items.Add("-- TODOS --");
        //                            //ddl.SelectedValue = "-- TODOS --";

        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    oClsElectricMeters.strGroup = strGroup;
        //                    oClsElectricMeters.intActive = 1;
        //                    dtGetLadingCharge = oClsElectricMeters.GetLoadingCharge();
        //                    if (dtGetLadingCharge == null)
        //                    {
        //                        //msgErrNew.InnerHtml = "";
        //                        //msgErrNew.Style.Add("display", "none");
        //                        //msgErrNew.InnerHtml = "Error al recuperar los datos";
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','Error al recuperar los datos.','error');", true);

        //                    }
        //                    else
        //                    {
        //                        if (dtGetLadingCharge.Rows.Count > 0)
        //                        {
        //                            DataSet ds = new DataSet(); ds.Tables.Add(dtGetLadingCharge.Copy());
        //                            ddl.DataSource = ds;
        //                            ddl.DataTextField = "RPUPuntoCarga";
        //                            ddl.DataValueField = "RPU";
        //                            ddl.DataBind();
        //                            // ddl.Items.Add("-- TODOS --");
        //                            //ddl.SelectedValue = "-- TODOS --";

        //                        }
        //                        else if (dtGetLadingCharge.Rows.Count == 0)
        //                        {
        //                            ddl.Items.Clear();
        //                            //ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Grupo sin puntos de carga','warning');", true);

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsError.logMessage = ex.ToString();
        //        clsError.logModule = "itemSelected";
        //        clsError.LogWrite();
        //        //msgErrNew.InnerHtml = "";
        //        //msgErrNew.Style.Add("display", "none");
        //        //msgErrNew.InnerHtml = ex.ToString();
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        string mensaje = serializer.Serialize(ex.Message);
        //        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error  Detalle: " + mensaje + "');", true);

        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e) {
            clsElectricMeters oClsElectricMeters = new clsElectricMeters();
            StringBuilder strHTMLElectric = new StringBuilder();
            string strLoadingCharge = "";// string.Empty;
            DataTable tdReqElectric;
            try {
                msgErrNew.InnerText = "";
                msgErrNew.Style.Add("display", "none");

                string strGrupo = cmbGroup.Items[cmbGroup.SelectedIndex].Value;
                //string strCentral = cmbCentral.Items[cmbCentral.SelectedIndex].Value;
                string strGestorComercial = cboGestorMedicion.Items[cboGestorMedicion.SelectedIndex].Value;


                //if (ddl.SelectedIndex != -1)
                //{
                //    strLoadingCharge = ddl.Items[ddl.SelectedIndex].Value;
                //}
                //if (ddl.Text =="")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal({title: 'Error',html: $('<div>').addClass('some-class').text('Debe seleccionar un punto de carga.'),animation: true,customClass:'animated tada'});", true);
                //}
                //else
                //{
                oClsElectricMeters.strGroup = strGrupo;
                    oClsElectricMeters.strLoadingCharges = strLoadingCharge;
                    oClsElectricMeters.strRPU = strLoadingCharge;
                    oClsElectricMeters.strGestorMedicion = strGestorComercial;
                    oClsElectricMeters.intActive = 1;


                    tdReqElectric = oClsElectricMeters.SearchByFilter(strGrupo, strGestorComercial);

                    oClsElectricMeters.dtElectricMeters = tdReqElectric;             
                    if (tdReqElectric.Rows.Count > 0)
                    {
                        strHTMLElectric = oClsElectricMeters.CreateTableHTML();
                        DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                        //Get Contacts from division and zone
                        tdReqElectric = null;
                        tdReqElectric = oClsElectricMeters.GetContactsCFE(strLoadingCharge);

                    
                    }
                    else
                    {
                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        //msgErrNew.InnerText = "No hay datos para mostrar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal({title: 'Error',html: $('<div>').addClass('some-class').text('No hay datos para mostrar.'),animation: false,customClass: 'animated tada'});", true);
                   
                    }
                //}
                tdReqElectric = null;
                tdReqElectric = oClsElectricMeters.GetContactsCFE(strLoadingCharge);
                if (tdReqElectric.Rows.Count > 0) {
                    strHTMLElectric = oClsElectricMeters.CreateTableHTML4Contacts(tdReqElectric);
                    //PlaceHolder1.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSearch_Click";
                clsError.LogWrite();
                //msgErrNew.InnerText = "";
                //msgErrNew.Style.Add("display", "inline");
                //msgErrNew.InnerHtml = ex.ToString();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string mensaje = serializer.Serialize(ex.Message);
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error  Detalle: " + mensaje + "');", true);

            }
        }


        //public void CargarCentral()
        //{
        //    clsCentral clsBussinesCentral = new clsCentral();
        //    DataTable dtG;
        //    dtG = clsBussinesCentral.GetAllCentrales();
        //    //Session["dtG"] = dtG;
        //    // dtG.Rows.Add(0, "0","-- TODOS --");
        //    DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
        //    ds.Tables[0].DefaultView.Sort = "Descripción";
        //    cmbCentral.DataSource = ds;
        //    cmbCentral.DataTextField = "Descripción";
        //    cmbCentral.DataValueField = "IdCentral";
        //    cmbCentral.DataBind();            

        //    cmbCentral.Items.Add("-- TODOS --");
        //    cmbCentral.SelectedValue = "-- TODOS --";  
        //}

        private void FillGestorMedicion()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetGestorMedicion();
                cboGestorMedicion.DataSource = dtCommunication;
                cboGestorMedicion.DataTextField = "GestorMedicion";
                cboGestorMedicion.DataValueField = "IdGestor";
                cboGestorMedicion.DataBind();
                cboGestorMedicion.Items.Add("-- TODOS --");
                cboGestorMedicion.SelectedValue = "-- TODOS --";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillGestorMedicion";
                clsError.LogWrite();
            }
        }

        public void CargarGrupo()
        {
            clsElectricMeters oClsElectricMeters = new clsElectricMeters();
            string strIdGMedicion = cboGestorMedicion.Items[cboGestorMedicion.SelectedIndex].Value;
            DataTable dtAllGroups;
            if (strIdGMedicion == "-- TODOS --")
            {oClsElectricMeters.IdGMedicion = 0;
            }
            else
            {
                oClsElectricMeters.IdGMedicion = Convert.ToInt32(strIdGMedicion);
            }
            oClsElectricMeters.intActive = 1;

            dtAllGroups = oClsElectricMeters.GetAllDistinctGroup();
            cmbGroup.DataSource = dtAllGroups;
            cmbGroup.DataTextField = "Grupo";
            cmbGroup.DataValueField = "IdGrupo";
            cmbGroup.DataBind();
            // CargaDDL();
            //CargarCentral();

            cmbGroup.Items.Add("-- TODOS --");
            cmbGroup.SelectedValue = "-- TODOS --";
        }

        protected void cboGestorMedicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrupo();
        }
    }
}