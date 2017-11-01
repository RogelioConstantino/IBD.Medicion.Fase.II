using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medicion.Class;
using Medicion.Class.LogError;
using System.Data;
using Medicion.Class.Business;
using System.IO;
using System.Text;

namespace Medicion
{
    public partial class medidores : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        DataTable dtGetRPUData;
        DataTable dtGetComentarios;
        string strRpu = null;
        Class.Encrypt oclsEncrypt = new Class.Encrypt();
        Class.Business.clsElectricMeters oClsElectricMeters = new Class.Business.clsElectricMeters();
        Class.Business.clsStatus oClsStatus = new Class.Business.clsStatus();
        StringBuilder strHTMLGroup = new StringBuilder();
        StringBuilder strHTMLCommunication = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
            {

            
            try 
            {
                
                if (!IsPostBack)
                {

                    msgExito.Visible = false;
                    msgError.Visible = false;


                    chkPM1.Checked = true;
                    string strEmail = Convert.ToString(Session["email"]);
                    if (string.IsNullOrEmpty(strEmail))
                    {
                        Response.Redirect("Default.aspx");
                        clsError.logMessage = "Usuario no logueado:" + Convert.ToString(Session["email"]);
                        clsError.logModule = "Page_Load";
                        clsError.LogWrite();
                    }
                     strRpu = Request["rpu"];
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
                        dtGetComentarios = oClsElectricMeters.BuscarComentario(strRpu, 1);

                        string strOpc = Request["opc"];
                        if (!string.IsNullOrEmpty(strOpc))
                        {
                            if (strOpc == "ok")
                            {
                                msgExito.Visible = true;
                                msgError.Visible = false;
                            }
                            else
                            {
                                msgError.Visible = true;
                                msgExito.Visible = false;
                            }
                        }

                        FillEstatus();
                        FillGestorMedicion();
                        FillGestorComercial();
                        foreach (DataRow row in dtGetComentarios.Rows)
                        {
                            txtComentMedicdor.Text = Convert.ToString(row["Comentario"]);
                        }
                        foreach (DataRow row in dtGetRPUData.Rows)
                        {
                            txtGroup.Text = Convert.ToString(row["Grupo"]);
                            txtLoadingCharge.Text = Convert.ToString(row[1]);
                            txtRPU.Text = strRpu;
                            ////lblStatus.InnerText = "";
                            //lblStatus.InnerText = Convert.ToString(row["Estatus"]); 

                            ddlEstatusRup.SelectedValue = Convert.ToString(row["IdEstatus"]);
                            ddlGestorComercial.SelectedValue = Convert.ToString(row["IdGestorComercial"]);
                            ddlGestorMedicion.SelectedValue = Convert.ToString(row["IdGestorMedicion"]);
                            
                        }
                        if (dtGetRPUData.Rows.Count > 0)
                        {
                            //ddlEstatusRup.SelectedValue = dtGetRPUData.Rows[0][14].ToString();
                            //ddlGestorComercial.SelectedValue = dtGetRPUData.Rows[0][15].ToString();
                            //ddlGestorMedicion.SelectedValue = dtGetRPUData.Rows[0][16].ToString();
                            ////Colocar valor al chekecbox
                            if (dtGetRPUData.Rows[0][18].ToString() == "1")
                            {
                                ChkPrelacion.Checked = true;
                            }
                            else if (dtGetRPUData.Rows[0][18].ToString() == null || dtGetRPUData.Rows[0][18].ToString() == "0")
                            {
                                ChkPrelacion.Checked = false;
                            }
                        }

                        DataTable dtPregunta;
                        clsCommunication oClsComPregunta = new clsCommunication();

                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "1");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM1.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev1.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecInst1.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs1.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM1.Checked = false;
                            txtFecPrev1.Value = "";
                            txtFecInst1.Value = "";
                            txtObs1.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "2");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM2.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrevs2.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecInsts2.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs2.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM2.Checked = false;
                            txtFecPrevs2.Value = "";
                            txtFecInsts2.Value = "";
                            txtObs2.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "3");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM3.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev3.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns3.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs3.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM3.Checked = false;
                            txtFecPrev3.Value = "";
                            txtFecIns3.Value = "";
                            txtObs3.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "4");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM4.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev4.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns4.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs4.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM4.Checked = false;
                            txtFecPrev4.Value = "";
                            txtFecIns4.Value = "";
                            txtObs4.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "5");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM5.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev5.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns5.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs5.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM5.Checked = false;
                            txtFecPrev5.Value = "";
                            txtFecIns5.Value = "";
                            txtObs5.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "6");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM6.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev6.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns6.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs6.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM6.Checked = false;
                            txtFecPrev6.Value = "";
                            txtFecIns6.Value = "";
                            txtObs6.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "7");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM7.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev7.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns7.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs7.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM7.Checked = false;
                            txtFecPrev7.Value = "";
                            txtFecIns7.Value = "";
                            txtObs7.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "8");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM8.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev8.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns8.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs8.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM8.Checked = false;
                            txtFecPrev8.Value = "";
                            txtFecIns8.Value = "";
                            txtObs8.Text = "";
                        }

                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "9");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM9.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev9.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns9.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs9.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM9.Checked = false;
                            txtFecPrev9.Value = "";
                            txtFecIns9.Value = "";
                            txtObs9.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "10");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM10.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev10.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns10.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs10.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM10.Checked = false;
                            txtFecPrev10.Value = "";
                            txtFecIns10.Value = "";
                            txtObs10.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "11");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM11.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev11.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns11.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs11.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM11.Checked = false;
                            txtFecPrev11.Value = "";
                            txtFecIns11.Value = "";
                            txtObs11.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "12");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM12.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev12.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns12.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs12.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM12.Checked = false;
                            txtFecPrev12.Value = "";
                            txtFecIns12.Value = "";
                            txtObs12.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "13");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM13.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev13.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns13.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs13.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM13.Checked = false;
                            txtFecPrev13.Value = "";
                            txtFecIns13.Value = "";
                            txtObs13.Text = "";
                        }


                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "14");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM14.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev14.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns14.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs14.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM14.Checked = false;
                            txtFecPrev14.Value = "";
                            txtFecIns14.Value = "";
                            txtObs14.Text = "";
                        }



                        dtPregunta = oClsComPregunta.getPreguntaMedidor(strRpu, "15");
                        if (dtPregunta.Rows.Count > 0)
                        {
                            chkPM15.Checked = (dtPregunta.Rows[0][2].ToString() == "2" ? true : false);
                            txtFecPrev15.Value = (dtPregunta.Rows[0][3].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][3].ToString());
                            txtFecIns15.Value = (dtPregunta.Rows[0][4].ToString() == "01-01-1900" ? "" : dtPregunta.Rows[0][4].ToString());
                            txtObs15.Text = dtPregunta.Rows[0][5].ToString();
                        }
                        else
                        {
                            chkPM15.Checked = false;
                            txtFecPrev15.Value = "";
                            txtFecIns15.Value = "";
                            txtObs15.Text = "";
                        }

                        ////dtGetRPUData = oClsElectricMeters.SearchStatusRPU(strRpu);
                        ////foreach (DataRow row in dtGetRPUData.Rows)
                        ////{

                        ////    lblStatus.InnerText = "";
                        ////    lblStatus.InnerText = Convert.ToString(row["Estatus"]);
                        ////}
                        //GetDataItem all DataBind from meters
                        //dtGetRPUData = null;
                        //dtGetRPUData = oClsElectricMeters.GetAgreement4RPU(strRpu, 1);
                        //if (dtGetRPUData.Rows.Count > 0)
                        //{
                        //    strHTMLCommunication = oClsElectricMeters.CreateTableHTML4Agreement(dtGetRPUData);
                        //    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLCommunication.ToString() });
                        //}
                        pintarTabla(strRpu);
                        //foreach (DataRow row in dtGetRPUData.Rows)
                        //{
                        //    //string strresp = Convert.ToString(row["CheckActivo"]);
                        //    //if (strresp == "1")
                        //    //{
                        //    //    chkPM1.Checked = true;
                        //    //}

                        //    //txtObs1.Text = Convert.ToString(row["obs"]);
                        //    //txtFecInst1.Value = Convert.ToString(row["fecInst"]);
                        //    //txtFecPrev1.Value = Convert.ToString(row["fecInst"]);

                        //}

                        //Get all agreement
                        ///-*-*-*FillDataParameter(strRpu);
                    }
                    else
                    {

                        //Redirect to constultascargas.aspx
                        Response.Redirect("consultascargas.aspx");

                    }

                   // pintarTabla(strRpu);
                    //dtGetRPUData = null;
                    //dtGetRPUData = oClsElectricMeters.GetAgreement4RPU(strRpu, 1);
                    //if (dtGetRPUData.Rows.Count > 0)
                    //{
                    //    strHTMLCommunication = oClsElectricMeters.CreateTableHTML4Agreement(dtGetRPUData);
                    //    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLCommunication.ToString() });
                    //}
                    //////Fill combo Status
                    ////DataTable dtStatus = oClsStatus.GetAllStatus();
                    ////DataSet ds = new DataSet(); ds.Tables.Add(dtStatus.Copy());
                    ////ds.Tables[0].DefaultView.Sort = "Estatus";
                    ////cmbStatus.Items.Add("");
                    ////cmbStatus.DataSource = ds;
                    ////cmbStatus.DataTextField = "Estatus";
                    ////cmbStatus.DataValueField = "IdEstatus";

                    //////cmbStatus.Text = "";
                    ////cmbStatus.DataBind();
                    ////cmbStatus.Items.Add("");
                    ////cmbStatus.SelectedValue = "";
                }
                //dtGetRPUData = null;
                //dtGetRPUData = oClsElectricMeters.GetAgreement4RPU(strRpu, 1);
                //if (dtGetRPUData.Rows.Count > 0)
                //{
                //    strHTMLCommunication = oClsElectricMeters.CreateTableHTML4Agreement(dtGetRPUData);
                //    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLCommunication.ToString() });
                //}


                string strIdUsuario = (string)Session["IdUsuario"];
                string strGestrorMedicion = ddlGestorMedicion.Items[ddlGestorMedicion.SelectedIndex].Value;

                btnAddZone.Visible = (strIdUsuario== strGestrorMedicion);
                Button1.Visible = (strIdUsuario == strGestrorMedicion);

                ddlGestorMedicion.Enabled = ((string)Session["Rol"] == "1");


            }

            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Page_Load";
                clsError.LogWrite();
            }
            
        }

        public void pintarTabla(string rpu)
        {
            DataTable dtGetRPUData2 = null;
            //dtGetRPUData = null;
            Class.Business.clsElectricMeters oClsElectricMeters2 = new Class.Business.clsElectricMeters();
            StringBuilder strHTMLCommunication2 = new StringBuilder();
            dtGetRPUData2 = oClsElectricMeters2.GetAgreement4RPU(rpu, 1);
            if (dtGetRPUData2.Rows.Count > 0)
            {
                strHTMLCommunication2 = oClsElectricMeters2.CreateTableHTML4Agreement(dtGetRPUData2);
                DBDataPlaceHolder.Controls.Add(new Literal { Text = strHTMLCommunication2.ToString() });
            }
        }

        protected void FillDataParameter(string strRPU)
        {
            Class.Business.clsElectricMeters oClsElectricMeters = new Class.Business.clsElectricMeters();
            try 
            {
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 1);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM1.Checked = true;
                    }

                    txtObs1.Text = Convert.ToString(row["obs"]);
                    txtFecInst1.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev1.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 2);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM2.Checked = true;
                    }

                    txtObs2.Text = Convert.ToString(row["obs"]);
                    txtFecInsts2.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrevs2.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 3);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM3.Checked = true;
                    }

                    txtObs3.Text = Convert.ToString(row["obs"]);
                    txtFecIns3.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev3.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 4);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM4.Checked = true;
                    }

                    txtObs4.Text = Convert.ToString(row["obs"]);
                    txtFecIns4.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev4.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 5);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM5.Checked = true;
                    }

                    txtObs5.Text = Convert.ToString(row["obs"]);
                    txtFecIns5.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev5.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 6);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM6.Checked = true;
                    }

                    txtObs6.Text = Convert.ToString(row["obs"]);
                    txtFecIns6.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev6.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 7);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM7.Checked = true;
                    }

                    txtObs7.Text = Convert.ToString(row["obs"]);
                    txtFecIns7.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev7.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 8);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM8.Checked = true;
                    }

                    txtObs8.Text = Convert.ToString(row["obs"]);
                    txtFecIns8.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev8.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 9);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM9.Checked = true;
                    }

                    txtObs9.Text = Convert.ToString(row["obs"]);
                    txtFecIns9.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev9.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 10);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM10.Checked = true;
                    }

                    txtObs10.Text = Convert.ToString(row["obs"]);
                    txtFecIns10.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev10.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 11);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM11.Checked = true;
                    }

                    txtObs11.Text = Convert.ToString(row["obs"]);
                    txtFecIns11.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev11.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 12);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM12.Checked = true;
                    }

                    txtObs12.Text = Convert.ToString(row["obs"]);
                    txtFecIns12.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev12.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 13);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM13.Checked = true;
                    }

                    txtObs13.Text = Convert.ToString(row["obs"]);
                    txtFecIns13.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev13.Value = Convert.ToString(row["fecPrev"]);

                }
                dtGetRPUData = oClsElectricMeters.GetData4ParameterMeterds(strRPU, 1, 14);
                foreach (DataRow row in dtGetRPUData.Rows)
                {
                    string strresp = Convert.ToString(row["CheckActivo"]);
                    if (strresp == "1")
                    {
                        chkPM14.Checked = true;
                    }

                    txtObs14.Text = Convert.ToString(row["obs"]);
                    txtFecIns14.Value = Convert.ToString(row["fecInst"]);
                    txtFecPrev14.Value = Convert.ToString(row["fecPrev"]);

                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "FillDataParameter";
                clsError.LogWrite();
            }
        }

        /// <summary>
        /// Save data from tab "Medidores"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e) 
        {
            clsMeters oClsMeters = new clsMeters();
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            string strIdUsuario = (string)Session["IdUsuario"];
            
            try 
            {
                string strStatus = "";// cmbStatus.Items[cmbStatus.SelectedIndex].Text;                

                string strEmail = (string)Session["email"];
                string strRPU = Request["rpu"];
                oclsEncrypt.strData = strRPU;
                //strRPU = oclsEncrypt.DecryptData();
                //oclsEncrypt.strData = strEmail;
                //strEmail = oclsEncrypt.DecryptData();
                if (!string.IsNullOrEmpty(strRPU))
                {
                    if (string.IsNullOrEmpty(strEmail)) { Response.Redirect("Default.aspx"); }


                    string strEstatus = ddlEstatusRup.Items[ddlEstatusRup.SelectedIndex].Value;
                    string strGestrorComercial = ddlGestorComercial.Items[ddlGestorComercial.SelectedIndex].Value;
                    string strGestrorMedicion = ddlGestorMedicion.Items[ddlGestorMedicion.SelectedIndex].Value;
                    int strchprelacion = (ChkPrelacion.Checked ? 1 : 0);
                    if (strEstatus == "" || strGestrorComercial == "" || strGestrorMedicion == "")
                    {
                        if (strEstatus == "") LblError.Text = "Debe seleccionar un estatus para el punto de carga.";
                        if (strGestrorComercial == "") LblError.Text = "Debe seleccionar un Gestor Comercial para el punto de carga.";
                        if (strGestrorMedicion == "") LblError.Text = "Debe seleccionar un Gestor de Medición  para el punto de carga.";
                        msgError.Visible = true;
                        msgExito.Visible = false;
                    }
                    else
                    {
                        Boolean bRes = oclsCommunication.UpdateRUP(strRPU, strEstatus, strGestrorComercial, strGestrorMedicion,strchprelacion,  strIdUsuario,txtComentMedicdor.Text,1);

                        string strchkPM1 = (chkPM1.Checked ? "2" : "0");
                        string strchkPM2 = (chkPM2.Checked ? "2" : "0");
                        string strchkPM3 = (chkPM3.Checked ? "2" : "0");
                        string strchkPM4 = (chkPM4.Checked ? "2" : "0");
                        string strchkPM5 = (chkPM5.Checked ? "2" : "0");
                        string strchkPM6 = (chkPM6.Checked ? "2" : "0");
                        string strchkPM7 = (chkPM7.Checked ? "2" : "0");
                        string strchkPM8 = (chkPM8.Checked ? "2" : "0");
                        string strchkPM9 = (chkPM9.Checked ? "2" : "0");
                        string strchkPM10 = (chkPM10.Checked ? "2" : "0");
                        string strchkPM11 = (chkPM11.Checked ? "2" : "0");
                        string strchkPM12 = (chkPM12.Checked ? "2" : "0");
                        string strchkPM13 = (chkPM13.Checked ? "2" : "0");
                        string strchkPM14 = (chkPM14.Checked ? "2" : "0");
                        string strchkPM15 = (chkPM14.Checked ? "2" : "0");

                        DataTable dt;
                        dt = oClsMeters.InsertaActualizaMedidores(strRPU,
                                                                    strchkPM1,
                                                                    strchkPM2,
                                                                    strchkPM3,
                                                                    strchkPM4,
                                                                    strchkPM5,
                                                                    strchkPM6,
                                                                    strchkPM7,
                                                                    strchkPM8,
                                                                    strchkPM9,
                                                                    strchkPM10,
                                                                    strchkPM11,
                                                                    strchkPM12,
                                                                    strchkPM13,
                                                                    strchkPM14,
                                                                    strchkPM15
                                                                    , strIdUsuario
                                                                  );

                        string IdLog = dt.Rows[0][0].ToString();
                        Div1.Visible = true;
                        
                        if (guardaPreguntas(strRPU, strIdUsuario, IdLog))
                        {
                            pintarTabla(strRPU);
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','¡La actualización se realizó con éxito! ','success');", true);
                            //Div1.Visible = true;
                            //DBDataPlaceHolder.Visible = true;
                           
                            //msgExito.Visible = true;
                            //msgError.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','¡"+LblError.Text+"! ','error');", true);

                            //msgError.Visible = true;
                            //msgExito.Visible = false;
                        }
                    }

                    //List<clsPropertiesMeters> lstPM = new List<clsPropertiesMeters>();
                    //lstPM = CheckData();
                    //oClsMeters.strStatus = strStatus;
                    //oClsMeters.LPM = lstPM;
                    //oClsMeters.strRPU = strRPU;
                    //oClsMeters.strEmailusr = strEmail;
                    //oClsMeters.ValidateParameters();

                }
                else
                {
                    Response.Redirect("consultascargas.aspx");
                }
                //Response.Redirect(Request.RawUrl);
         
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSave_Click";
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

        ///// <summary>
        ///// Upload files 
        ///// </summary>
        ///// <param name="context"></param>
        public void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        hpf.SaveAs(Server.MapPath("~/uploads/") + System.IO.Path.GetFileName(hpf.FileName));
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

        /// <summary>
        /// Function from update electric meters files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    //    protected void Upload_Files(object sender, EventArgs e)
    //    {
    //        Class.Business.clsMeters oclsMeters = new clsMeters();
    //        Class.Encrypt oclsEncrypt = new Class.Encrypt();

    //        string strPathMeter = oclsMeters.GetPathUploadMeters();
    //        Int32 intNumMaxUploadFiles = oclsMeters.GetMaxNumber2UploadFiles();
    //        string strEmail = (string)Session["email"];
    //        string strRPU = txtRPU.Text;
    //        string strArchivo = DateTime.Now.ToString("ddMMyyyyhhmmssffff");
    //        try 
    //        {
    //            oclsEncrypt.strData = strArchivo;
    //            strArchivo = oclsEncrypt.EncryptData();
    //            strPathMeter = strPathMeter + '/' + strRPU + '/';
    //            if (!Directory.Exists(Server.MapPath(strPathMeter)))
    //            {
    //                Directory.CreateDirectory(Server.MapPath(strPathMeter));
    //            }
    //            if (string.IsNullOrEmpty(strEmail)) { Response.Redirect("Default.aspx"); }
    //            if (string.IsNullOrEmpty(strRPU)) { Response.Redirect("consultascargas.aspx"); }
    //            if (IsPostBack)
    //            {
    //                if (fileUpload.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
    //                {
    //                    int iUploadedCnt = 0;
    //                    int iFailedCnt = 0;
    //                    HttpFileCollection hfc = Request.Files;
    //                    lblFileList.Text = "Select <b>" + hfc.Count + "</b> file(s)";
                        
    //                    if (hfc.Count <= intNumMaxUploadFiles)    // 10 FILES RESTRICTION.
    //                    {
    //                        for (int i = 0; i <= hfc.Count - 1; i++)
    //                        {
    //                            HttpPostedFile hpf = hfc[i];
    //                            if (hpf.ContentLength > 0)
    //                            {
    //                                if (!File.Exists(Server.MapPath(strPathMeter) + Path.GetFileName(strArchivo) ))
    //                                {
    //                                    DirectoryInfo objDir =   new DirectoryInfo(Server.MapPath(strPathMeter));

    //                                    string sFileName = Path.GetFileName(hpf.FileName);
    //                                    string sFileExt = Path.GetExtension(hpf.FileName);

    //                                    // CHECK FOR DUPLICATE FILES.
    //                                    FileInfo[] objFI = objDir.GetFiles(sFileName.Replace(sFileExt, "") + ".*");

    //                                    if (objFI.Length > 0)
    //                                    {
    //                                        // CHECK IF FILE WITH THE SAME NAME EXISTS 
    //                                        //(IGNORING THE EXTENTIONS).
    //                                        foreach (FileInfo file in objFI)
    //                                        {
    //                                            string sFileName1 = objFI[0].Name;
    //                                            string sFileExt1 = Path.GetExtension(objFI[0].Name);

    //                                            if (sFileName1.Replace(sFileExt1, "") == sFileName.Replace(sFileExt, ""))
    //                                            {
    //                                                iFailedCnt += 1;        // NOT ALLOWING DUPLICATE.
    //                                                break;
    //                                            }
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        string strContentType = hpf.ContentType.ToString();
    //                                        // SAVE THE FILE IN A FOLDER.
    //                                        hpf.SaveAs(Server.MapPath(strPathMeter) + Path.GetFileName(strArchivo + sFileExt));
    //                                        //Save to data base
    //                                        Boolean strSavedFile = oclsMeters.SaveFileMeter2DB(sFileName, sFileExt, strEmail, strRPU, strArchivo, strContentType);
    //                                        iUploadedCnt += 1;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    string strContentType = hpf.ContentType.ToString();
    //                                    if (File.Exists(Server.MapPath(strPathMeter) + Path.GetFileName(strArchivo)))
    //                                    {
    //                                        string sFileName = Path.GetFileName(hpf.FileName);
    //                                        string sFileExt = Path.GetExtension(hpf.FileName);
    //                                        File.Delete(Server.MapPath(strPathMeter) + Path.GetFileName(strArchivo + '.' + sFileExt));
    //                                        hpf.SaveAs(Server.MapPath(strPathMeter) + Path.GetFileName(strArchivo + '.' + sFileExt));
    //                                        Boolean strSavedFile = oclsMeters.SaveFileMeter2DB(sFileName, sFileExt, strEmail, strRPU, strArchivo, strContentType);
                                            
    //                                    }
                                        
    //                                }
    //                            }
    //                        }
    //                        lblUploadStatus.Text = "<b>" + iUploadedCnt + "</b> Archivos Subidos.";
    //                        lblFailedStatus.Text = "<b>" + iFailedCnt +
    //                            "</b> Los archivos están duplicados y no se pudieron subir";
    //                    }
    //                    else lblUploadStatus.Text = string.Format("Número total de archivos permitidos: {0}", intNumMaxUploadFiles);
    //                }
    //                else lblUploadStatus.Text = "Ningún archivo seleccionado";

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            clsError.logMessage = ex.ToString();
    //            clsError.logModule = "Upload_Files";
    //            clsError.LogWrite();
    //        }
            
            
           
    //}

        /// <summary>
        /// Check all data selected from web page
        /// </summary>
        /// <returns></returns>
        private List<clsPropertiesMeters>  CheckData()
        {
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            List<clsPropertiesMeters> lstP = new List<clsPropertiesMeters>();
            short intPM1 = 0;
            string strObser = string.Empty;
            string strEmail = (string)Session["email"];
            String strFechaPrev = string.Empty;
            String strFechaIns = string.Empty;
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
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                   lstP = AddData2List(1, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);
                //PM2
                if (chkPM2.Checked)
                {
                    intPM1 = 1;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrevs2.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrevs2.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecInsts2.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecInsts2.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs2.Text))
                {
                    strObser = txtObs2.Text;
                }
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(2, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM3
                if (chkPM3.Checked)
                {
                    intPM1 = 1;
                }
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
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(3, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM4
                if (chkPM4.Checked)
                {
                    intPM1 = 1;
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
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(4, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM5
                if (chkPM5.Checked)
                {
                    intPM1 = 1;
                }
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
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(5, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM6
                if (chkPM6.Checked)
                {
                    intPM1 = 1;
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
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(6, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM7
                if (chkPM7.Checked)
                {
                    intPM1 = 1;
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
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(7, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM8
                if (chkPM8.Checked)
                {
                    intPM1 = 1;
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
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(8, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM9
                if (chkPM9.Checked)
                {
                    intPM1 = 1;
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
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(9, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM10
                if (chkPM10.Checked)
                {
                    intPM1 = 1;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev10.UniqueID]))
                {
                    strFechaPrev = txtFecPrev10.Value;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns10.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns10.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs10.Text))
                {
                    strObser = txtObs10.Text;
                }
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(10, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM11
                if (chkPM11.Checked)
                {
                    intPM1 = 1;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev11.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev11.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns11.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns11.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs11.Text))
                {
                    strObser = txtObs11.Text;
                }
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(11, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM12
                if (chkPM12.Checked)
                {
                    intPM1 = 1;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev12.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev12.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns12.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns12.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs12.Text))
                {
                    strObser = txtObs12.Text;
                }
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(12, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM13
                if (chkPM13.Checked)
                {
                    intPM1 = 1;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev13.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev13.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns13.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns13.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs13.Text))
                {
                    strObser = txtObs13.Text;
                }
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(13, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);

                //PM14
                if (chkPM14.Checked)
                {
                    intPM1 = 1;
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecPrev14.UniqueID]))
                {
                    strFechaPrev = Request.Form[txtFecPrev14.UniqueID];
                }
                if (!string.IsNullOrEmpty(Request.Form[txtFecIns14.UniqueID]))
                {
                    strFechaIns = Request.Form[txtFecIns14.UniqueID];
                }
                if (!string.IsNullOrEmpty(txtObs14.Text))
                {
                    strObser = txtObs14.Text;
                }
                if (intPM1 == 0 || intPM1 == 1 || !string.IsNullOrEmpty(strFechaPrev) || !string.IsNullOrEmpty(strFechaIns) || !string.IsNullOrEmpty(strObser))
                {
                    lstP = AddData2List(14, lstP, txtRPU.Text, intPM1, strEmail, strObser, strFechaPrev, strFechaIns);

                }
                strObser = CleanStringData(strObser);
                intPM1 = CleanIntData(intPM1);
                strFechaIns = CleanStringData(strFechaIns);
                strFechaPrev = CleanStringData(strFechaPrev);
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CheckData()";
                clsError.LogWrite();
            }

            

            return lstP;
        }

        public void BacK()
        {
            Response.Redirect("consultascargas.aspx");
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


        private String CleanStringData(string strData) {  return string.Empty;  }
        private Int16 CleanIntData(Int16 intData) { return 0; }
        private List<clsPropertiesMeters> AddData2List(int intIDParametersMeters, List<clsPropertiesMeters> LPM, string strRPU, Int16 intPM, string strEmail, string strObservation, string strDeliveryDate, string strInstallationDate)
        {
            try {
                LPM.Add(new clsPropertiesMeters { intIDParametersMeters = intIDParametersMeters, strRPU = txtRPU.Text, intCheckActivo = intPM, strEmailusr = strEmail, strObservation = strObservation, strDeliveryDate = strDeliveryDate, strInstallationDate = strInstallationDate });
            
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CheckData()";
                clsError.LogWrite();
            }
            return LPM;
        }
        
        private Boolean guardaPreguntas(string strRPU, string strIdUsuario, string IdLog)
        {
            string strPregunta = string.Empty;
            string strObser = string.Empty;
            string strFechaPrev = string.Empty;
            string strFechaIns = string.Empty;

            Boolean bRes;

            //Class.Business.clsCommunication oclsCommunication = new Class.Business.clsCommunication();
            Class.Business.clsMeters oClsMeters = new Class.Business.clsMeters();

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

            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "2";
            if (!string.IsNullOrEmpty(txtFecPrevs2.Value))
                strFechaPrev = txtFecPrevs2.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecInsts2.Value))
                strFechaIns = txtFecInsts2.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs2.Text))
                strObser = txtObs2.Text;
            else
                strObser = "";
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

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
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

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
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

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
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

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
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog); ;
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
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

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
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

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
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "10";
            if (!string.IsNullOrEmpty(txtFecPrev10.Value))
                strFechaPrev = txtFecPrev10.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns10.Value))
                strFechaIns = txtFecIns10.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs10.Text))
                strObser = txtObs10.Text;
            else
                strObser = "";
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);


            strPregunta = "11";
            if (!string.IsNullOrEmpty(txtFecPrev11.Value))
                strFechaPrev = txtFecPrev11.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns11.Value))
                strFechaIns = txtFecIns11.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs11.Text))
                strObser = txtObs11.Text;
            else
                strObser = "";
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "12";
            if (!string.IsNullOrEmpty(txtFecPrev12.Value))
                strFechaPrev = txtFecPrev12.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns12.Value))
                strFechaIns = txtFecIns12.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs12.Text))
                strObser = txtObs12.Text;
            else
                strObser = "";
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "13";
            if (!string.IsNullOrEmpty(txtFecPrev13.Value))
                strFechaPrev = txtFecPrev13.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns13.Value))
                strFechaIns = txtFecIns13.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs13.Text))
                strObser = txtObs13.Text;
            else
                strObser = "";
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);

            strPregunta = "14";
            if (!string.IsNullOrEmpty(txtFecPrev14.Value))
                strFechaPrev = txtFecPrev14.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns14.Value))
                strFechaIns = txtFecIns14.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs14.Text))
                strObser = txtObs14.Text;
            else
                strObser = "";
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);


            strPregunta = "15";
            if (!string.IsNullOrEmpty(txtFecPrev15.Value))
                strFechaPrev = txtFecPrev15.Value;
            else
                strFechaPrev = "";
            if (!string.IsNullOrEmpty(txtFecIns15.Value))
                strFechaIns = txtFecIns15.Value;
            else
                strFechaIns = "";
            if (!string.IsNullOrEmpty(txtObs15.Text))
                strObser = txtObs15.Text;
            else
                strObser = "";
            bRes = oClsMeters.InsertaActualizaMedidoresDetalle(strRPU, strPregunta, strFechaPrev, strFechaIns, strObser, strIdUsuario, IdLog);


            return true;

        }

        protected void btnregresar1_Click(object sender, EventArgs e)
        {
            BacK();
        }

        protected void btnregresar2_Click(object sender, EventArgs e)
        {
            BacK();
        }
    }
}