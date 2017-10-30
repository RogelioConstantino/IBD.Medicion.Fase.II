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
    public partial class Weconsultagrupo : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        StringBuilder strHTMLGroup = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["__EVENTTARGET"] == "NewGrupo" &&
            Request["__EVENTARGUMENT"] == "")
            {  }

            try 
            {
                if (!IsPostBack) 
                {

                    FillGestorComercial();
                    FillGestorMedicion();
                    
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
        private void FillGestorMedicion()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetGestorMedicion();
                cmbGMedicion.DataSource = dtCommunication;
                cmbGMedicion.DataTextField = "GestorMedicion";
                cmbGMedicion.DataValueField = "IdGestor";
                cmbGMedicion.DataBind();
                cmbGMed.DataSource = dtCommunication;
                cmbGMed.DataTextField = "GestorMedicion";
                cmbGMed.DataValueField = "IdGestor";
                cmbGMed.DataBind();
                cmbGMed.Items.Add("--Seleccione un gestor--");
                cmbGMed.SelectedValue = "--Seleccione un gestor--";
                cmbGMed2.DataSource = dtCommunication;
                cmbGMed2.DataTextField = "GestorMedicion";
                cmbGMed2.DataValueField = "IdGestor";
                cmbGMed2.DataBind();
                cmbGMedicion.Items.Add("-- TODOS --");
                cmbGMedicion.SelectedValue = "-- TODOS --";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillGestorMedicion";
                clsError.LogWrite();
            }
        }
        private void FillGestorComercial()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetGestorComercial();
                cmbGComercial.DataSource = dtCommunication;
                cmbGComercial.DataTextField = "GestorComercial";
                cmbGComercial.DataValueField = "IdGestor";
                cmbGComercial.DataBind();
                cmbGComercial2.DataSource = dtCommunication;
                cmbGComercial2.DataTextField = "GestorComercial";
                cmbGComercial2.DataValueField = "IdGestor";
                cmbGComercial2.DataBind();
                cmbGComercial2.Items.Add("--Seleccione un gestor--");
                cmbGComercial2.SelectedValue = "--Seleccione un gestor--";
                cmbGComer2.DataSource = dtCommunication;
                cmbGComer2.DataTextField = "GestorComercial";
                cmbGComer2.DataValueField = "IdGestor";
                cmbGComer2.DataBind();
                cmbGComercial.Items.Add("-- TODOS --");
                cmbGComercial.SelectedValue = "-- TODOS --";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillGestorComercial";
                clsError.LogWrite();
            }
        }
        private void FillGestorComercial2()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication2;
                dtCommunication2 = oclsCommunication.GetGestorComercial();
                cmbGComercial2.DataSource = dtCommunication2;
                cmbGComercial2.DataTextField = "GestorComercial";
                cmbGComercial2.DataValueField = "IdGestor";
                cmbGComercial2.DataBind();
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillGestorComercial";
                clsError.LogWrite();
            }
        }
        private void FillCMBGestoresM()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication2;
                dtCommunication2 = oclsCommunication.GetGestorMedicion();
                cmbGMed.DataSource = dtCommunication2;
                cmbGMed.DataTextField = "GestorMedicion";
                cmbGMed.DataValueField = "IdGestor";
                cmbGMed.DataBind();
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillGestorComercial";
                clsError.LogWrite();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            clsGrupo clsBussGroup = new clsGrupo();
            string strGestorMedicion = cmbGMedicion.Items[cmbGMedicion.SelectedIndex].Value;
            //string strCentral = cmbCentral.Items[cmbCentral.SelectedIndex].Value;
            string strGestorComercial = cmbGComercial.Items[cmbGComercial.SelectedIndex].Value;
            int GMedicion; int GComercial;
            if (strGestorMedicion == "-- TODOS --")
            { GMedicion = 0; }else { GMedicion = Convert.ToInt32(strGestorMedicion); }
            if (strGestorComercial == "-- TODOS --")
            { GComercial = 0; }else { GComercial = Convert.ToInt32(strGestorComercial); }
            DataTable dtG = clsBussGroup.GetAllGroups(GMedicion,GComercial);
            Session["dtG"] = dtG;
            strHTMLGroup = clsBussGroup.ReturnHTMLGroup(dtG);
            DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLGroup.ToString() });
        }
    }
}