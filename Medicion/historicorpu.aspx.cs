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
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace Medicion
{
    public partial class historicorpu : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        SqlDataReader drEx;
            private string _strRpu;
        private string _strgp;
        private string _strlch;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsElectricMeters oClsElectricMeters = new clsElectricMeters();
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            DataTable dtGetLadingCharge;
            try
            {
                string strRpu = Request["rpu"];
                string strgp = Request["gp"];
                string strlch = Request["lch"];

                
                if (!IsPostBack)
                {

                    DataTable dtAllGroups;
                    oClsElectricMeters.intActive = 1;
                    dtAllGroups = oClsElectricMeters.GetAllDistinctGroup();
                    cmbGroup.DataSource = dtAllGroups;
                    cmbGroup.DataTextField = "Grupo";
                    cmbGroup.DataValueField = "IdGrupo";
                    cmbGroup.DataBind();
                    CargaDDl();
                    //cmbGroup.Items.Add("-- TODOS --");
                    //cmbGroup.SelectedValue = "-- TODOS --";

                    //if variable are not empty fill combo with input variable
                    if (!string.IsNullOrEmpty(strRpu) && !string.IsNullOrEmpty(strgp) && !string.IsNullOrEmpty(strlch))
                    {
                        
                        oclsEncrypt.strData = strRpu;
                        strRpu = oclsEncrypt.DecryptData();
                        //oclsEncrypt.strData = strgp;
                        //strgp = oclsEncrypt.DecryptData();
                        //oclsEncrypt.strData = strlch;
                        //strlch = oclsEncrypt.DecryptData(); 

                        
                        _strgp = strgp;
                        _strlch = strlch;
                        _strRpu = strRpu;
                        oClsElectricMeters.strGroup = strgp;
                        oClsElectricMeters.intActive = 1;
                        dtGetLadingCharge = oClsElectricMeters.SearchRPU(strRpu);
                        if (dtGetLadingCharge == null)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','error');", true);

                            //msgErrNew.InnerHtml = "";
                            //msgErrNew.Style.Add("display", "none");
                            //msgErrNew.InnerHtml = "";
                        }
                        else
                        {
                            if (dtGetLadingCharge.Rows.Count > 0)
                            {
                                dtGetLadingCharge.Rows.Add("-- TODOS --");
                                DataSet ds = new DataSet(); ds.Tables.Add(dtGetLadingCharge.Copy());
                                //cmbLoadingCharge.DataSource = ds;
                                //cmbLoadingCharge.DataTextField = "RPUPuntoCarga";
                                //cmbLoadingCharge.DataValueField = "RPU";
                                //cmbLoadingCharge.SelectedIndex =0;
                                ddl.DataSource = ds;
                                ddl.DataTextField = "PuntoCarga";
                                ddl.DataValueField = "RPU";

                                ddl.DataBind();
                                //cmbLoadingCharge.DataBind();
                                ddl.Items.Add("");
                                 foreach (DataRow renglon in ds.Tables[0].Rows)
                                   {
                                              strgp = renglon["Grupo"].ToString();
                                              strlch = renglon["PuntoCarga"].ToString();
                                              break;
                                      }
                                 ddl.SelectedValue = strRpu;
                                 cmbGroup.SelectedValue = strgp;
                                
                            }
                        }
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
        public void CargaDDl()
        {
            clsElectricMeters oClsElectricMeters = new clsElectricMeters();
            DataTable dtGetLadingCharge;
            try
            {
                    string strGroup = cmbGroup.Items[cmbGroup.SelectedIndex].Value;
                    if (!string.IsNullOrEmpty(strGroup))
                    {
                        if (strGroup == "-- TODOS --")
                        {
                            //cmbLoadingCharge.Items.Clear();
                            ddl.Items.Clear();
                            oClsElectricMeters.intActive = 1;
                            dtGetLadingCharge = oClsElectricMeters.GetAllGroup();
                            if (dtGetLadingCharge == null)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','error');", true);

                                //    msgErrNew.InnerHtml = "";
                                //    msgErrNew.Style.Add("display", "none");
                                //    msgErrNew.InnerHtml = "Error al recuperar los datos";
                            }
                            else
                            {
                                if (dtGetLadingCharge.Rows.Count > 0)
                                {
                                    //dtGetLadingCharge.Rows.Add("-- TODOS --");
                                    DataSet dsAll = new DataSet(); dsAll.Tables.Add(dtGetLadingCharge.Copy());
                                    ddl.DataSource = dsAll;
                                    ddl.DataTextField = "RPUPuntoCarga";
                                    ddl.DataValueField = "RPU";
                                    ddl.DataBind();
                                    //ddl.Items.Add("-- TODOS --");
                                    //ddl.SelectedValue = "-- TODOS --";

                                }
                            }
                        }
                        else
                        {
                            oClsElectricMeters.strGroup = strGroup;
                            oClsElectricMeters.intActive = 1;
                            dtGetLadingCharge = oClsElectricMeters.GetLoadingCharge();
                            if (dtGetLadingCharge == null)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','error');", true);

                                //    msgErrNew.InnerHtml = "";
                                //    msgErrNew.Style.Add("display", "none");
                                //    msgErrNew.InnerHtml = "Error al recuperar los datos";
                            }
                            else
                            {
                                if (dtGetLadingCharge.Rows.Count > 0)
                                {
                                    //dtGetLadingCharge.Rows.Add("-- TODOS --");
                                    DataSet ds = new DataSet(); ds.Tables.Add(dtGetLadingCharge.Copy());
                                    ddl.DataSource = ds;
                                    ddl.DataTextField = "RPUPuntoCarga";
                                    ddl.DataValueField = "RPU";
                                    ddl.DataBind();
                                    //ddl.Items.Add("-- TODOS --");
                                    //ddl.SelectedValue = "-- TODOS --";

                                }
                                else if (dtGetLadingCharge.Rows.Count == 0)
                                {
                                    ddl.Items.Clear();
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
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string mensaje = serializer.Serialize(ex.Message);
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error  Detalle: " + mensaje + "');", true);

                //msgErrNew.InnerHtml = "";
                //msgErrNew.Style.Add("display", "none");
                //msgErrNew.InnerHtml = ex.ToString();
            }

        }
        protected void itemSelected(object sender, EventArgs e)
        {
            clsElectricMeters oClsElectricMeters = new clsElectricMeters();
            DataTable dtGetLadingCharge;
            try
            {
                if (IsPostBack)
                {
                    string strGroup = cmbGroup.Items[cmbGroup.SelectedIndex].Value;
                    if (!string.IsNullOrEmpty(strGroup))
                    {
                        if (strGroup == "-- TODOS --")
                        {
                            //cmbLoadingCharge.Items.Clear();
                            ddl.Items.Clear();
                            oClsElectricMeters.intActive = 1;
                            dtGetLadingCharge = oClsElectricMeters.GetAllGroup();
                            if (dtGetLadingCharge == null)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','error');", true);

                            //    msgErrNew.InnerHtml = "";
                            //    msgErrNew.Style.Add("display", "none");
                            //    msgErrNew.InnerHtml = "Error al recuperar los datos";
                            }
                            else
                            {
                                if (dtGetLadingCharge.Rows.Count > 0)
                                {
                                    //dtGetLadingCharge.Rows.Add("-- TODOS --");
                                    DataSet dsAll = new DataSet(); dsAll.Tables.Add(dtGetLadingCharge.Copy());
                                    ddl.DataSource = dsAll;
                                    ddl.DataTextField = "RPUPuntoCarga";
                                    ddl.DataValueField = "RPU";
                                    ddl.DataBind();
                                    //ddl.Items.Add("-- TODOS --");
                                    //ddl.SelectedValue = "-- TODOS --";

                                }
                            }
                        }
                        else
                        {                         
                            oClsElectricMeters.strGroup = strGroup;
                            oClsElectricMeters.intActive = 1;
                            dtGetLadingCharge = oClsElectricMeters.GetLoadingCharge();
                            if (dtGetLadingCharge == null)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Error al recuperar los datos','error');", true);

                            //    msgErrNew.InnerHtml = "";
                            //    msgErrNew.Style.Add("display", "none");
                            //    msgErrNew.InnerHtml = "Error al recuperar los datos";
                            }
                            else
                            {
                                if (dtGetLadingCharge.Rows.Count > 0)
                                {
                                    //dtGetLadingCharge.Rows.Add("-- TODOS --");
                                    DataSet ds = new DataSet(); ds.Tables.Add(dtGetLadingCharge.Copy());
                                    ddl.DataSource = ds;
                                    ddl.DataTextField = "RPUPuntoCarga";
                                    ddl.DataValueField = "RPU";
                                    ddl.DataBind();
                                    //ddl.Items.Add("-- TODOS --");
                                    //ddl.SelectedValue = "-- TODOS --";

                                }
                                else if (dtGetLadingCharge.Rows.Count == 0)
                                {
                                    ddl.Items.Clear();
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
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string mensaje = serializer.Serialize(ex.Message);
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error  Detalle: " + mensaje + "');", true);

                //msgErrNew.InnerHtml = "";
                //msgErrNew.Style.Add("display", "none");
                //msgErrNew.InnerHtml = ex.ToString();
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            clsHistoricRPU oclsHistoricRPU = new clsHistoricRPU();
            StringBuilder strHTMLElectric = new StringBuilder();
            StringBuilder strHTMLCommunication = new StringBuilder();
            string strLoadingCharge = string.Empty;
            try
            {
                msgErrNew.InnerText = "";
                msgErrNew.Style.Add("display", "none");
                string strGrupo = cmbGroup.Items[cmbGroup.SelectedIndex].Value;
                if (ddl.SelectedIndex != -1)
                {
                    strLoadingCharge = ddl.Items[ddl.SelectedIndex].Value;
                }
                if (ddl.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal({title: 'Error',html: $('<div>').addClass('some-class').text('Debe seleccionar un punto de carga.'),animation: true,customClass:'animated tada'});", true);
                }
                else
                {
                    DataTable dtHistoricRPU = new DataTable("HistoricRPU");

                    dtHistoricRPU = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge);

                    if (dtHistoricRPU.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricRPU);
                        DBDataPlaceHolderPuntosdeCarga.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','No hay datos para mostrar','error');", true);

                        //msgErrNew.InnerText = "";
                        //msgErrNew.Style.Add("display", "inline");
                        //msgErrNew.InnerText = "";
                    }

                    DataTable dtHistoricPreguntas = new DataTable("HistoricPreguntas");

                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "1");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder1.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "2");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder2.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "3");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder3.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "4");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder4.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "5");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder5.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "6");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder6.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "7");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder7.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "8");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder8.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntas = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge, "9");
                    if (dtHistoricPreguntas.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntas);
                        PlaceHolder9.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }


                    DataTable dtPregunta;
                    clsCommunication oClsComPregunta = new clsCommunication();
                    string strPath = oClsComPregunta.GetPathUploadCommunication();
                    string strFullPath = Server.MapPath(strPath);


                    DataTable dtGetRPUDataArchivos = null;
                    dtGetRPUDataArchivos = oClsComPregunta.GetArchivosComunicacion(strLoadingCharge);
                    if (dtGetRPUDataArchivos.Rows.Count > 0)
                    {
                        StringBuilder strHTMLCommunication1 = new StringBuilder();
                        strHTMLCommunication1 = oClsComPregunta.CreateTableHTMLArchivos(dtGetRPUDataArchivos, strFullPath);
                        DBDataPlaceHolderArchivos.Controls.Add(new Literal { Text = strHTMLCommunication1.ToString() });
                    }


                    DataTable dtHistoricPreguntasMed = new DataTable("HistoricPreguntasMed");

                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "1");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {


                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores10.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "2");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores11.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "3");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores12.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "4");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores13.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "5");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores14.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "6");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores15.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "7");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores16.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "8");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores17.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "9");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores18.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "10");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores19.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "11");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores20.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "12");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores21.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "13");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores22.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "14");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores23.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }
                    dtHistoricPreguntasMed = oclsHistoricRPU.GetSPCommunicationHistoricPorRPUMed(strLoadingCharge, "15");
                    if (dtHistoricPreguntasMed.Rows.Count > 0)
                    {
                        strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricPreguntasMed);
                        PlaceHolderrMedidores24.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    }


                    DataTable dtHistoricRPUConvenios = new DataTable("HistoricConvenios");
                    Class.Business.clsElectricMeters oClsElectricMeters = new Class.Business.clsElectricMeters();
                    dtHistoricRPUConvenios = null;
                    dtHistoricRPUConvenios = oClsElectricMeters.GetAgreement4RPUHistorico(strLoadingCharge, 1);
                    if (dtHistoricRPUConvenios.Rows.Count > 0)
                    {
                        strHTMLCommunication = oClsElectricMeters.CreateTableHTML4Agreement(dtHistoricRPUConvenios);
                        DBDataPlaceHolderConvenios.Controls.Add(new Literal { Text = strHTMLCommunication.ToString() });
                    }

                    //dtHistoricRPU = oclsHistoricRPU.GetSPCommunicationHistoricPorRPU(strLoadingCharge);

                    //if (dtHistoricRPU.Rows.Count > 0)
                    //{
                    //    strHTMLElectric = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricRPU);
                    //    DBDataPlaceHolderPuntosdeCarga.Controls.Add(new Literal { Text = strHTMLElectric.ToString() });
                    //}
                    //else
                    //{
                    //    msgErrNew.InnerText = "";
                    //    msgErrNew.Style.Add("display", "inline");
                    //    msgErrNew.InnerText = "No hay datos para mostrar";
                    //}



                    //Now we need to fill up the communication table
                    //spBuscarComunicacionHistoricoRPU
                    dtHistoricRPU = null;
                    //dtHistoricRPU = oclsHistoricRPU.GetSPCommunicationHistoricRPU(strLoadingCharge, 1);
                    //if (dtHistoricRPU.Rows.Count > 0)
                    //{
                    //    strHTMLCommunication = oclsHistoricRPU.CreateTableHTMLHistorico(dtHistoricRPU);
                    //    DBDataPlaceHolderCommunication.Controls.Add(new Literal { Text = strHTMLCommunication.ToString() });
                    //}


                    //msgErrNew.InnerText = "";
                    //msgErrNew.Style.Add("display", "inline");
                    //msgErrNew.InnerText = "Seleccion a un grupo";

                    //if  ((ddl.Items[ddl.SelectedIndex].Value != "--TODOS--" && ddl.Items[ddl.SelectedIndex].Value !=""))
                    //     { 

                    //        //dtHistoricRPU = oclsHistoricRPU.GetHistoricRPU(strLoadingCharge, 1);


                    //    }
                    //    else if ((ddl.Items[ddl.SelectedIndex].Value == ""))
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('','Seleccione  un punto de carga','error');", true);

                    //        //msgErrNew.InnerText = "";
                    //        //msgErrNew.Style.Add("display", "inline");
                    //        //msgErrNew.InnerText = "Seleccion a un punto de carga";
                    //      }

                }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnSearch_Click";
                clsError.LogWrite();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string mensaje = serializer.Serialize(ex.Message);
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error  Detalle: " + mensaje + "');", true);
                //msgErrNew.InnerText = "";
                //msgErrNew.Style.Add("display", "inline");
                //msgErrNew.InnerHtml = ex.ToString();
            }
        }
    }
}