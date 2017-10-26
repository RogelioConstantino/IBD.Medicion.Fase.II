using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medicion.Class.LogError;
using System.Data;
using Medicion.Class.Business;
using System.Text;
using System.Drawing;


namespace Medicion
{
    public partial class comunicaciones : System.Web.UI.Page
    {
        DataTable dtGetRPUData;
        LogErrorMedicion clsError = new LogErrorMedicion();
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Form.Attributes.Add("enctype", "multipart/form-data");


            LogErrorMedicion clsError = new LogErrorMedicion();
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            Class.Business.clsElectricMeters oClsElectricMeters = new Class.Business.clsElectricMeters();


            try
            {
                if (!IsPostBack)
                {

                    msgExito.Visible = false;
                    msgError.Visible = false;
                    

                    //div_CommunicationType_IP_1.Visible = false;
                    //div_CommunicationType_IP_2.Visible = false;
                    //div_CommunicationType_IP_3.Visible = false;
                    //div_CommunicationType_IP_4.Visible = false;

                    //div_CommunicationType_4G_1.Visible = false;
                    //div_CommunicationType_4G_SubirArchivo.Visible = false;
                    //div_CommunicationType_4G_3.Visible = false;

                    string strEmail = Convert.ToString(Session["email"]);
                    if (string.IsNullOrEmpty(strEmail))
                    {
                        Response.Redirect("Default.aspx");
                        clsError.logMessage = "Usuario no logueado:" + Convert.ToString(Session["email"]);
                        clsError.logModule = "Page_Load";
                        clsError.LogWrite();
                    }
                    string strRpu = Request["rpu"];
                    if (string.IsNullOrEmpty(strRpu))
                    {
                        Response.Redirect("consultascargas.aspx");
                        clsError.logMessage = "No se encontró el RPU:" + Convert.ToString(Session["rpu"]);
                        clsError.logModule = "Page_Load";
                        clsError.LogWrite();
                    }
                    if (!string.IsNullOrEmpty(strRpu))
                    {
                        //oclsEncrypt.strData = strRpu;
                        //strRpu = oclsEncrypt.DecryptData();
                        oClsElectricMeters.strRPU = strRpu;
                        oClsElectricMeters.intActive = 1;
                        dtGetRPUData = oClsElectricMeters.SearchRPU(strRpu);
                    }

                    string strOpc = Request["opc"];
                    if (!string.IsNullOrEmpty(strOpc))
                    { 
                        if (strOpc=="ok")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','¡La actualización se realizó con éxito! ','success');", true);

                            //msgExito.Visible = true;
                            //msgError.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','¡Ocurrio un error al guardar los datos! ','error');", true);
                            //msgError.Visible = true;
                            //msgExito.Visible = false;
                        }
                    }



                    FillEstatus();
                    FillGestorMedicion();
                    FillGestorComercial();


                    foreach (DataRow row in dtGetRPUData.Rows)
                    {
                        txtGroup.Text = Convert.ToString(row["Grupo"]);
                        txtLoadingCharge.Text = Convert.ToString(row["PUNTO DE CARGA"]);
                        txtRPU.Text = strRpu;

                        ddlEstatusRup.SelectedValue = Convert.ToString(row["IdEstatus"]);
                        ddlGestorComercial.SelectedValue = Convert.ToString(row["IdGestorComercial"]);
                        ddlGestorMedicion.SelectedValue = Convert.ToString(row["IdGestorMedicion"]);

                        //lblStatus.InnerText = Convert.ToString(row["Estatus"]); 
                    }
                    if (dtGetRPUData.Rows.Count > 0)
                    {
                        //ddlEstatusRup.SelectedValue = dtGetRPUData.Rows[0][14].ToString();
                        //ddlGestorComercial.SelectedValue = dtGetRPUData.Rows[0][15].ToString();
                        //ddlGestorMedicion.SelectedValue = dtGetRPUData.Rows[0][16].ToString();
                        
                        //Colocar valor al chekecbox
                        if (dtGetRPUData.Rows[0][18].ToString() == "1" )
                        {
                            ChkPrelacion.Checked = true;
                        }
                        else if (dtGetRPUData.Rows[0][18].ToString() == null || dtGetRPUData.Rows[0][18].ToString() == "0")
                        {
                            ChkPrelacion.Checked = false;
                        }

                    }

                    
                    //fill up all comboboxes
                    FillCFECommunications();
                    FillCommunicationClass();
                    FillCommunicationType();
                    FillLocalCommunication();
                    FillMeterType();
                    FillActualMeter();
                    FillRequiredMeter();


                    DataTable dtPregunta;
                    clsCommunication oClsComPregunta = new clsCommunication();
                    
                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "1");
                    if (dtPregunta.Rows.Count > 0)
                    {
                        chkPM1.Checked =  (dtPregunta.Rows[0][2].ToString()=="2"?true:false );
                        txtFecPrev1.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                        txtFecInst1.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs1.Text = dtPregunta.Rows[0][5].ToString();
                    }
                    else {
                        chkPM1.Checked = false;
                        txtFecPrev1.Value = "";
                        txtFecInst1.Value = "";
                        txtObs1.Text = "";
                    }

                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "2");
                    if (dtPregunta.Rows.Count > 0)
                    {
                        chkPM2.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                        txtFecPrev2.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                        txtFecInst2.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs2.Text = dtPregunta.Rows[0][5].ToString();
                    }
                    else
                    {
                        chkPM2.Checked = false;
                        txtFecPrev2.Value = "";
                        txtFecInst2.Value = "";
                        txtObs2.Text = "";
                    }

                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "3");
                    if (dtPregunta.Rows.Count > 0)
                    {                        
                        //cmbActualMeter.Items.FindByValue(dtPregunta.Rows[0][2].ToString()).Selected = true;
                        cmbActualMeter.SelectedValue = dtPregunta.Rows[0][2].ToString();
                        txtFecPrev3.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                        txtFecIns3.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs3.Text = dtPregunta.Rows[0][5].ToString();
                    }
                    else
                    {                     
                        cmbActualMeter.Text = "";
                        txtFecPrev3.Value = "";
                        txtFecIns3.Value = "";
                        txtObs3.Text = "";
                    }

                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "4");
                    if (dtPregunta.Rows.Count > 0)
                    {                        
                        cmbMeterType.SelectedValue = dtPregunta.Rows[0][2].ToString();
                        txtFecPrev4.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                        txtFecIns4.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs4.Text = dtPregunta.Rows[0][5].ToString();
                    }
                    else
                    {
                        cmbMeterType.Text = "";
                        txtFecPrev4.Value = "";
                        txtFecIns4.Value = "";
                        txtObs4.Text = "";
                    }

                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "5");
                    if (dtPregunta.Rows.Count > 0)
                    {                        
                        cmbRequiredMeter.SelectedValue = dtPregunta.Rows[0][2].ToString();
                        txtFecPrev5.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                        txtFecIns5.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs5.Text = dtPregunta.Rows[0][5].ToString();
                    }
                    else
                    {                        
                        cmbRequiredMeter.Text = "";
                        txtFecPrev5.Value = "";
                        txtFecIns5.Value = "";
                        txtObs5.Text = "";
                    }

                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "6");
                    if (dtPregunta.Rows.Count > 0)
                    {
                        cmbCommunicationClass.SelectedValue = dtPregunta.Rows[0][2].ToString();
                        txtFecPrev6.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                        txtFecIns6.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs6.Text = dtPregunta.Rows[0][5].ToString();
                    }
                    else
                    {
                        cmbCommunicationClass.Text = "";
                        txtFecPrev6.Value = "";
                        txtFecIns6.Value = "";
                        txtObs6.Text = "";
                    }

                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "7");
                    if (dtPregunta.Rows.Count > 0)
                    {
                        cmbCommunicationType.SelectedValue = dtPregunta.Rows[0][2].ToString();
                        txtFecPrev7.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                        txtFecIns7.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs7.Text = dtPregunta.Rows[0][5].ToString();

                        if (cmbCommunicationType.SelectedValue == "1")
                        {

                            DataTable dtPreguntaTpoCom = oClsComPregunta.getPreguntaTpoCom(strRpu);
                            
                            txtIP.Text = dtPregunta.Rows[0][1].ToString();
                            txtMascara.Text = dtPregunta.Rows[0][2].ToString();
                            txtPuertaEnlace.Text = dtPregunta.Rows[0][3].ToString();
                        }
                        else
                        {
                            txtIP.Text = "";
                            txtMascara.Text = "";
                            txtPuertaEnlace.Text = "";
                        }


                        string strPath = oClsComPregunta.GetPathUploadCommunication();
                        string strFullPath = Server.MapPath(strPath);


                        DataTable  dtGetRPUDataArchivos = null;
                        dtGetRPUDataArchivos = oClsComPregunta.GetArchivosComunicacion(strRpu);
                        if (dtGetRPUData.Rows.Count > 0)
                        {
                            StringBuilder strHTMLCommunication = new StringBuilder();
                            strHTMLCommunication = oClsComPregunta.CreateTableHTMLArchivos(dtGetRPUDataArchivos, strFullPath);
                            DBDataPlaceHolderArchivos.Controls.Add(new Literal { Text = strHTMLCommunication.ToString() });
                        }



                        //if (cmbCommunicationType.SelectedValue == "4")
                        //{
                        //    div_CommunicationType_4G_1.Visible = true;
                        //    div_CommunicationType_4G_SubirArchivo.Visible = true;
                        //    div_CommunicationType_4G_3.Visible = true;
                        //}
                        //else if (cmbCommunicationType.SelectedValue == "1")
                        //{
                        //    div_CommunicationType_IP_1.Visible = true;
                        //    div_CommunicationType_IP_2.Visible = true;
                        //    div_CommunicationType_IP_3.Visible = true;
                        //    div_CommunicationType_IP_4.Visible = true;
                        //}


                    }
                    else
                    {
                        cmbCommunicationType.Text = "";
                        txtFecPrev7.Value = "";
                        txtFecIns7.Value = "";
                        txtObs7.Text = "";
                    }

                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "8");
                    if (dtPregunta.Rows.Count > 0)
                    {
                        cmbLocalCommunication.SelectedValue = dtPregunta.Rows[0][2].ToString();
                        txtFecPrev8.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                        txtFecIns8.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs8.Text = dtPregunta.Rows[0][5].ToString();
                    }
                    else
                    {
                        cmbLocalCommunication.Text = "";
                        txtFecPrev8.Value = "";
                        txtFecIns8.Value = "";
                        txtObs8.Text = "";
                    }

                    dtPregunta = oClsComPregunta.getPregunta(strRpu, "9");
                    if (dtPregunta.Rows.Count > 0)
                    {
                        cmbCFECommunication.SelectedValue = dtPregunta.Rows[0][2].ToString();
                        txtFecPrev9.Value = (dtPregunta.Rows[0][3].ToString()=="01-01-1900" ? "": dtPregunta.Rows[0][3].ToString());
                        txtFecIns9.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                        txtObs9.Text = dtPregunta.Rows[0][5].ToString();
                    }
                    else
                    {
                        cmbCFECommunication.Text = "";
                        txtFecPrev9.Value = "";
                        txtFecIns9.Value = "";
                        txtObs9.Text = "";
                    }

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("  changeCommunicationType()  };");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                }

                string strIdUsuario = (string)Session["IdUsuario"];
                string strGestrorMedicion = ddlGestorMedicion.Items[ddlGestorMedicion.SelectedIndex].Value;

                btnAddCommunnication.Visible = (strIdUsuario == strGestrorMedicion);
                //Button1.Visible = (strIdUsuario == strGestrorMedicion);
                ddlGestorMedicion.Enabled = ((string)Session["Rol"] == "1");

            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Page_Load";
                clsError.LogWrite();
            }
        }
        protected void historicorpuclic(object sender, EventArgs e)
        {
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            try
            {
                if (string.IsNullOrEmpty(txtRPU.Text) && string.IsNullOrEmpty(txtGroup.Text) && string.IsNullOrEmpty(txtLoadingCharge.Text))
                {
                    Response.Redirect("historicorpu.aspx");
                }
                else
                {
                    string strlink = txtRPU.Text;
                    oclsEncrypt.strData = strlink;
                    strlink = oclsEncrypt.EncryptData();
                    string strGroup = txtGroup.Text;
                    oclsEncrypt.strData = strGroup;
                    strGroup = oclsEncrypt.EncryptData();
                    string strLoadingCharge = txtLoadingCharge.Text;
                    oclsEncrypt.strData = strLoadingCharge;
                    strLoadingCharge = oclsEncrypt.EncryptData();
                    string strUrl = "historicorpu.aspx?rpu=" + strlink + "&gp=" + strGroup + "&lch=" + strLoadingCharge;
                    Response.Redirect(strUrl);
                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Page_Load";
                clsError.LogWrite();
            }


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            string strIdUsuario = (string)Session["IdUsuario"];

            try 
            {
                string strEmail = (string)Session["email"];
                string strRPU = (string)Request["rpu"];
                oclsEncrypt.strData = strRPU;
                //strRPU = oclsEncrypt.DecryptData();
                if (!string.IsNullOrEmpty(strRPU)) 
                {
                    if (string.IsNullOrEmpty(strEmail)) { Response.Redirect("Default.aspx"); }

                    string cm = cmbMeterType.Items[cmbMeterType.SelectedIndex].Text;
                    cm = cmbMeterType.SelectedValue.ToString();
                                        
                    string strEstatus = ddlEstatusRup.Items[ddlEstatusRup.SelectedIndex].Value;
                    string strGestrorComercial = ddlGestorComercial.Items[ddlGestorComercial.SelectedIndex].Value;
                    string strGestrorMedicion = ddlGestorMedicion.Items[ddlGestorMedicion.SelectedIndex].Value;
                    int strchprelacion = (ChkPrelacion.Checked ? 1 : 0);
                    if (strEstatus == "" || strGestrorComercial == "" || strGestrorMedicion == "")
                    {

                        if (strEstatus == "") LblError.Text = "Debe seleccionar un estatus para el punto de carga.";
                        if (strGestrorComercial == "") LblError.Text = "Debe seleccionar un Gestor Comercial para el punto de carga.";
                        if (strGestrorMedicion == "") LblError.Text = "Debe seleccionar un Gestor de Medición  para el punto de carga.";

                        //msgError.Visible = true;
                        //msgExito.Visible = false;
                    }
                    else
                    {
                        Boolean bRes = oclsCommunication.UpdateRUP(strRPU, strEstatus, strGestrorComercial, strGestrorMedicion,strchprelacion, strIdUsuario);
                    
                        string strchkPM1 = (chkPM1.Checked ? "2" : "0");
                        string strchkPM2 = (chkPM2.Checked ? "2" : "0");
                       
                        string strcmbActualMeter = cmbActualMeter.Items[cmbActualMeter.SelectedIndex].Value;
                        string strcmbMeterType = cmbMeterType.Items[cmbMeterType.SelectedIndex].Value;
                        string strcmbRequiredMeter = cmbRequiredMeter.Items[cmbRequiredMeter.SelectedIndex].Value;
                        string strcmbCommunicationClass = cmbCommunicationClass.Items[cmbCommunicationClass.SelectedIndex].Value;
                        string strcmbCommunicationType = cmbCommunicationType.Items[cmbCommunicationType.SelectedIndex].Value;
                        string strcmbLocalCommunication = cmbLocalCommunication.Items[cmbLocalCommunication.SelectedIndex].Value;
                        string strcmbCFECommunication = cmbCFECommunication.Items[cmbCFECommunication.SelectedIndex].Value;
                        DataTable dt;
                        dt = oclsCommunication.InsertaActualizaComunicaciones(strRPU,
                                                                                strchkPM1,
                                                                                strchkPM2,
                                                                                strcmbActualMeter,
                                                                                strcmbMeterType,
                                                                                strcmbRequiredMeter,
                                                                                strcmbCommunicationClass,
                                                                                strcmbCommunicationType,
                                                                                strcmbLocalCommunication,
                                                                                strcmbCFECommunication, strIdUsuario);

                        string IdLog = dt.Rows[0][0].ToString();

                        string strIP = txtIP.Text;
                        string strMascara = txtMascara.Text;
                        string strPuertaEnlace = txtPuertaEnlace.Text;

                        Boolean bRes2 = oclsCommunication.InsertaActualizaComunicacionesTpoCom(strRPU, strIP, strMascara, strPuertaEnlace, strIdUsuario, IdLog);
                   
                        if (guardaPreguntas(strRPU, strIdUsuario, IdLog))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','¡La actualización se realizó con éxito! ','success');", true);

                            //msgExito.Visible = true;
                            //msgError.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','¡¡" + LblError.Text + "!! ','error');", true);
                            //msgError.Visible = true;
                            //msgExito.Visible = false;
                        }
                    }
                    //List<clsPropertiesCommunications> oclsPropComm = new List<clsPropertiesCommunications>();
                    //oclsPropComm = GetData();
                    //string strMsg = oclsCommunication.ValidateCommunication(oclsPropComm, strRPU, strEmail);
                }

                //Response.Redirect(Request.RawUrl);                                
                if (!string.IsNullOrEmpty(strRPU))
                {
                    oclsEncrypt.strData = strRPU;
                    //strRPU = oclsEncrypt.EncryptData();
                    Response.Redirect("comunicaciones.aspx?rpu=" + strRPU + "&opc=ok");
                }                    


            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSave_Click";
                clsError.LogWrite();
            }

        }

        protected List<clsPropertiesCommunications> GetData() {
            List<clsPropertiesCommunications> oclsPC = new List<clsPropertiesCommunications>();
            short intPM1 = 0;
            string strObser = string.Empty;
            string strFechaPrev = string.Empty;
            string strFechaIns = string.Empty;
            string strTypesMeters = string.Empty;
            string strCommunicationClass = string.Empty;
            string strCommunicationType = string.Empty;
            string strLocalCommunication = string.Empty;
            string strCFECommunication = string.Empty;
            try 
            {
                if (chkPM1.Checked)
                {
                    intPM1 = 1;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev1.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev1.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecInst1.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecInst1.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs1.Text))
                {
                    strObser = txtObs1.Text;
                }
                oclsPC = AddData2List(1, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns,"","","","","","","");

                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM2
                if (chkPM2.Checked)
                {
                    intPM1 = 1;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev2.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev2.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecInst2.UniqueID]))
                {
                    strFechaIns = txtFecInst2.Value;
                }
                if (!string.IsNullOrEmpty(txtObs2.Text))
                {
                    strObser = txtObs2.Text;
                }
                oclsPC = AddData2List(2, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns, "", "", "", "", "","","");

                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM3
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev3.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev3.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns3.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns3.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs3.Text))
                {
                    strObser = txtObs3.Text;
                }
                oclsPC = AddData2List(3, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns, "", "", "", "", "", cmbActualMeter.Text,"");
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM4                
                intPM1 = 0;
                if (cmbMeterType.SelectedIndex != -1)
                {
                    strTypesMeters = cmbMeterType.Items[cmbMeterType.SelectedIndex].Text;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev4.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev4.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns4.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns4.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs4.Text))
                {
                    strObser = txtObs4.Text;
                }
                oclsPC = AddData2List(0, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns,strTypesMeters, "", "", "", "","","");
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);
                strTypesMeters = CleanStringData(strTypesMeters);

                //PM5
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev5.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev5.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns5.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns5.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs5.Text))
                {
                    strObser = txtObs5.Text;
                }
                oclsPC = AddData2List(4, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns, "", "", "", "", "","", cmbRequiredMeter.Text);
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM6                
                intPM1 = 0;
                if (cmbCommunicationClass.SelectedIndex != -1)
                {
                    strCommunicationClass = cmbCommunicationClass.Items[cmbCommunicationClass.SelectedIndex].Text;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev6.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev6.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns6.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns6.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs6.Text))
                {
                    strObser = txtObs6.Text;
                }
                oclsPC = AddData2List(0, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns, "", strCommunicationClass, "", "", "","","");
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);
                strCommunicationClass = CleanStringData(strCommunicationClass);

                //PM7                
                intPM1 = 0;
                if (cmbCommunicationType.SelectedIndex != -1)
                {
                    strCommunicationType = cmbCommunicationType.Items[cmbCommunicationType.SelectedIndex].Text;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev7.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev7.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns7.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns7.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs7.Text))
                {
                    strObser = txtObs7.Text;
                }
                oclsPC = AddData2List(0, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns, "", "", strCommunicationType, "", "","","");
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);
                strCommunicationType = CleanStringData(strCommunicationType);

                //PM8                
                intPM1 = 0;
                if (cmbLocalCommunication.SelectedIndex != -1)
                {
                    strLocalCommunication = cmbLocalCommunication.Items[cmbLocalCommunication.SelectedIndex].Text;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev8.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev8.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns8.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns8.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs8.Text))
                {
                    strObser = txtObs8.Text;
                }
                oclsPC = AddData2List(0, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns, "", "", "", strLocalCommunication, "","","");
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);
                strLocalCommunication = CleanStringData(strLocalCommunication);

                //PM9                
                intPM1 = 0;
                if (cmbCFECommunication.SelectedIndex != -1)
                {
                    strCFECommunication = cmbCFECommunication.Items[cmbCFECommunication.SelectedIndex].Text;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev9.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev9.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns9.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns9.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs9.Text))
                {
                    strObser = txtObs9.Text;
                }
                oclsPC = AddData2List(0, oclsPC, txtRPU.Text, intPM1, strObser, strFechaPrev, strFechaIns, "", "", "", "", strCFECommunication,"","");
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);
                strCFECommunication = CleanStringData(strCFECommunication);
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSave_Click";
                clsError.LogWrite();
            }

            return oclsPC;
        }
        private String CleanStringData(string strData) { return string.Empty; }
        private Int16 CleanIntData(Int16 intData) { return 0; }

        private List<clsPropertiesCommunications> AddData2List(int intIDParametersCommunications, List<clsPropertiesCommunications> LCM, 
            string strRPU, int intPM, string strObservation, string strDeliveryDate, string strInstallationDate, 
            string strTypesMeters, string strCommunicationClass, string strTypesCommunications, 
            string strLocalCommunication, string strCFECommunication, string strActualMeters,string strRequiredMeters)
        {
            try
            {

                LCM.Add(new clsPropertiesCommunications { intIDParametersCommunications = intIDParametersCommunications, 
                    strRPU = strRPU, intCheckActivo = intPM, strObservation = strObservation, strDeliveryDate = strDeliveryDate, 
                    strInstallationDate = strInstallationDate, strTypesMeters = strTypesMeters, 
                    strCommunicationClass = strCommunicationClass, strTypesCommunications = strTypesCommunications, 
                    strLocalCommunication = strLocalCommunication, strCFECommunication = strCFECommunication, strActualMeter = strActualMeters, strRequiredMeter = strRequiredMeters });
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CheckData()";
                clsError.LogWrite();
            }
            return LCM;
        }

        protected void Upload_Files(object sender, EventArgs e) 
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try 
            { 
                string strEmail = Convert.ToString(Session["email"]);
                if (string.IsNullOrEmpty(strEmail))
                {
                    Response.Redirect("Default.aspx");
                    clsError.logMessage = "Usuario no logueado:" + Convert.ToString(Session["email"]);
                    clsError.logModule = "Page_Load";
                    clsError.LogWrite();
                }
                string strRpu = Request["rpu"];
                if (string.IsNullOrEmpty(strRpu))
                {
                    Response.Redirect("consultascargas.aspx");
                    clsError.logMessage = "No se encontró el RPU:" + Convert.ToString(Session["rpu"]);
                    clsError.logModule = "Page_Load";
                    clsError.LogWrite();
                }
                string strPath= oclsCommunication.GetPathUploadCommunication();
                string strFullPath = Server.MapPath(strPath);

                Class.Encrypt oclsEncrypt = new Class.Encrypt();
                string strRPUDes = (string)Request["rpu"];
                oclsEncrypt.strData = strRPUDes;
               // strRPUDes = oclsEncrypt.DecryptData();


                string str = oclsCommunication.UploadFiles(fileUpload, strFullPath, Request.Files, strEmail, strRPUDes);

               // DataTable dtPregunta;
                clsCommunication oClsComPregunta = new clsCommunication();

                //string strPath = oClsComPregunta.GetPathUploadCommunication();
                //string strFullPath = Server.MapPath(strPath);


                DataTable dtGetRPUDataArchivos = null;
                dtGetRPUDataArchivos = oClsComPregunta.GetArchivosComunicacion(strRPUDes);
                if (dtGetRPUDataArchivos.Rows.Count > 0)
                {
                    StringBuilder strHTMLCommunication = new StringBuilder();
                    strHTMLCommunication = oClsComPregunta.CreateTableHTMLArchivos(dtGetRPUDataArchivos, strFullPath);
                    DBDataPlaceHolderArchivos.Controls.Add(new Literal { Text = strHTMLCommunication.ToString() });
                }




            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillUpComboboxes";
                clsError.LogWrite();
            }
        }

        private void FillCFECommunications()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetCFECommunication();
         
                cmbCFECommunication.DataSource = dtCommunication;
                cmbCFECommunication.DataTextField = "EstatusComunicacionCFE";
                cmbCFECommunication.DataValueField = "IdEstatusComunicacionCFE";
                cmbCFECommunication.DataBind();
                cmbCFECommunication.Items.Add("");
                cmbCFECommunication.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillUpComboboxes";
                clsError.LogWrite();
            }

        }


        private void FillMeterType()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetElectricMeterType();
                cmbMeterType.DataSource = dtCommunication;
                cmbMeterType.DataTextField = "TipoMedidor";
                cmbMeterType.DataValueField = "IdTipoMedidor";
                cmbMeterType.DataBind();
                cmbMeterType.Items.Add("");
                cmbMeterType.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillUpComboboxes";
                clsError.LogWrite();
            }
        
        }
        private void FillCommunicationClass()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetCommunicationClass();
                cmbCommunicationClass.DataSource = dtCommunication;
                cmbCommunicationClass.DataTextField = "ComunicacionClase";
                cmbCommunicationClass.DataValueField = "IdComunicacionClase";
                cmbCommunicationClass.DataBind();
                cmbCommunicationClass.Items.Add("");
                cmbCommunicationClass.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillCommunicationClass";
                clsError.LogWrite();
            }
        }
        private void FillCommunicationType() 
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetCommunicationType();
                cmbCommunicationType.DataSource = dtCommunication;
                cmbCommunicationType.DataTextField = "TipoComunicacion";
                cmbCommunicationType.DataValueField = "IdTipoComunicacion";
                cmbCommunicationType.DataBind();
                cmbCommunicationType.Items.Add("");
                cmbCommunicationType.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillUpComboboxes";
                clsError.LogWrite();
            }
        }

        private void FillLocalCommunication()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetLocalCommunication();
                cmbLocalCommunication.DataSource = dtCommunication;
                cmbLocalCommunication.DataTextField = "EstatusComunicacionLocal";
                cmbLocalCommunication.DataValueField = "IdEstatusComunicacionLocal";
                cmbLocalCommunication.DataBind();
                cmbLocalCommunication.Items.Add("");
                cmbLocalCommunication.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillUpComboboxes";
                clsError.LogWrite();
            }
        }


        private void FillActualMeter()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetMedidorActual();
                cmbActualMeter.DataSource = dtCommunication;
                cmbActualMeter.DataTextField = "MedidorActual";
                cmbActualMeter.DataValueField = "IdMedidorActual";
                cmbActualMeter.DataBind();
                cmbActualMeter.Items.Add("");
                cmbActualMeter.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillActualMeter";
                clsError.LogWrite();
            }
        }
        private void FillRequiredMeter()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetMedidorRequerido();
                cmbRequiredMeter.DataSource = dtCommunication;
                cmbRequiredMeter.DataTextField = "MedidorRequerido";
                cmbRequiredMeter.DataValueField = "IdMedidorRequerido";
                cmbRequiredMeter.DataBind();
                cmbRequiredMeter.Items.Add("");
                cmbRequiredMeter.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillRequiredMeter";
                clsError.LogWrite();
            }
        }

        private void FillEstatus()
        {
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            LogErrorMedicion clsError = new LogErrorMedicion();
            try
            {
                DataTable dtCommunication;
                dtCommunication = oclsCommunication.GetEstatusRPU();
                ddlEstatusRup.DataSource = dtCommunication;
                ddlEstatusRup.DataTextField = "Estatus";
                ddlEstatusRup.DataValueField = "IdEstatus";
                ddlEstatusRup.DataBind();
                //ddlEstatusRup.Items.Add("");
                //ddlEstatusRup.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillEstatus";
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
                ddlGestorComercial.DataSource = dtCommunication;
                ddlGestorComercial.DataTextField = "GestorComercial";
                ddlGestorComercial.DataValueField = "IdGestor";
                ddlGestorComercial.DataBind();
                //ddlGestorComercial.Items.Add("");
                //ddlGestorComercial.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillGestorComercial";
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
                ddlGestorMedicion.DataSource = dtCommunication;
                ddlGestorMedicion.DataTextField = "GestorMedicion";
                ddlGestorMedicion.DataValueField = "IdGestor";
                ddlGestorMedicion.DataBind();
                //ddlGestorMedicion.Items.Add("");
                //ddlGestorMedicion.SelectedValue = "";
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillGestorMedicion";
                clsError.LogWrite();
            }
        }


        private Boolean guardaPreguntas( string strRPU, string strIdUsuario, string IdLog)
        {
            string strPregunta = string.Empty;
            string strObser = string.Empty;
            string strFechaPrev = string.Empty;
            string strFechaIns = string.Empty;

            Boolean bRes ;

            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();

            strPregunta = "1";
            if (!string.IsNullOrEmpty(txtFecPrev1.Value))
                strFechaPrev = txtFecPrev1.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecInst1.Value))
                strFechaIns = txtFecInst1.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs1.Text))
                strObser = txtObs1.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "2";
            if (!string.IsNullOrEmpty(txtFecPrev2.Value))
                strFechaPrev = txtFecPrev2.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecInst2.Value))
                strFechaIns = txtFecInst2.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs2.Text))
                strObser = txtObs2.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "3";
            if (!string.IsNullOrEmpty(txtFecPrev3.Value))
                strFechaPrev = txtFecPrev3.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns3.Value))
                strFechaIns = txtFecIns3.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs3.Text))
                strObser = txtObs3.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser,  strIdUsuario, IdLog);

            strPregunta = "4";
            if (!string.IsNullOrEmpty(txtFecPrev4.Value))
                strFechaPrev = txtFecPrev4.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns4.Value))
                strFechaIns = txtFecIns4.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs4.Text))
                strObser = txtObs4.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "5";
            if (!string.IsNullOrEmpty(txtFecPrev5.Value))
                strFechaPrev = txtFecPrev5.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns5.Value))
                strFechaIns = txtFecIns5.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs5.Text))
                strObser = txtObs5.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "6";
            if (!string.IsNullOrEmpty(txtFecPrev6.Value))
                strFechaPrev = txtFecPrev6.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns6.Value))
                strFechaIns = txtFecIns6.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs6.Text))
                strObser = txtObs6.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "7";
            if (!string.IsNullOrEmpty(txtFecPrev7.Value))
                strFechaPrev = txtFecPrev7.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns7.Value))
                strFechaIns = txtFecIns7.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs7.Text))
                strObser = txtObs7.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "8";
            if (!string.IsNullOrEmpty(txtFecPrev8.Value))
                strFechaPrev = txtFecPrev8.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns8.Value))
                strFechaIns = txtFecIns8.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs8.Text))
                strObser = txtObs8.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "9";
            if (!string.IsNullOrEmpty(txtFecPrev9.Value))
                strFechaPrev = txtFecPrev9.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns9.Value))
                strFechaIns = txtFecIns9.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs9.Text))
                strObser = txtObs9.Text;
            else
                strObser = "";
            bRes = oclsCommunication.InsertaActualizaComunicacionesDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            return true;

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("consultascargas.aspx");
        }

        protected void cmbCommunicationType_SelectedIndexChanged(object sender, EventArgs e)
        {

            div_CommunicationType_4G_1.Visible = false;
            div_CommunicationType_4G_SubirArchivo.Visible = false;
            div_CommunicationType_4G_3.Visible = false;

            div_CommunicationType_IP_1.Visible = false;
            div_CommunicationType_IP_2.Visible = false;
            div_CommunicationType_IP_3.Visible = false;
            div_CommunicationType_IP_4.Visible = false;

            if (cmbCommunicationType.SelectedValue == "4")
            {
                div_CommunicationType_4G_1.Visible = true;
                div_CommunicationType_4G_SubirArchivo.Visible = true;
                div_CommunicationType_4G_3.Visible = true;
            }
            else if (cmbCommunicationType.SelectedValue == "1")
            {
                div_CommunicationType_IP_1.Visible = true;
                div_CommunicationType_IP_2.Visible = true;
                div_CommunicationType_IP_3.Visible = true;
                div_CommunicationType_IP_4.Visible = true;
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("consultascargas.aspx");
        }
    }
}