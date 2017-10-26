using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Text;
using Medicion.Class.porteoautomatico;
using Medicion.Class.Business;
using Medicion.Class.LogError;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace Medicion
{
    public partial class porteoautomatico : System.Web.UI.Page
    {
        LogErrorMedicion clsError = new LogErrorMedicion();
        DataTable dtTemp;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Button1.Visible = false;
     
            if (Request["__EVENTTARGET"] == "MiFuncion" &&
             Request["__EVENTARGUMENT"] == "")
            {
                btnSave();
                pnlBrowseFile.Visible = true;
                pnlFileLoad.Visible = false;
                pnlMuestraDatos.Visible = false;
                btnCancel.Enabled = false;
                Button1.Disabled = true;
            }
            if (!IsPostBack)
            {
                //btnUpload.Enabled = false;
                //Button1.Visible = false;
                Button1.Disabled = true;
                lblFileLoad.Visible = false;
                pnlFileLoad.Visible = false;
                pnlMuestraDatos.Visible = false;

                pnlErrorLoad.Visible = false;
                pnlErrorLoadDiv.Visible = false;

                pnlValidaciones.Visible = false;

                pnlSuccessLoad.Visible = false;

                //cmbDivision.Items.Clear();

                CargarDDL();
                CargarDDLConvenio();
            }
            else
            {

            }
            if ((String)Session["Rol"] != "1")
            {
                pnlSuccessLoad.Visible = true;
                pnlBrowseFile.Visible = false;
                pnlPermiso.Visible = false;
                btnCancel.Visible = false;
                Button1.Visible = false;
            }
           
                
        }
        public void CargarDDL()
        {
            clsCentral clsBussinesCentral = new clsCentral();
            DataTable dtG;
            dtG = clsBussinesCentral.GetAllCentrales();
            //Session["dtG"] = dtG;
            // dtG.Rows.Add(0, "0","-- TODOS --");
            DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
            ds.Tables[0].DefaultView.Sort = "Descripción";
            cmbCentral.DataSource = ds;
            cmbCentral.DataTextField = "Descripción";
            cmbCentral.DataValueField = "IdCentral";
            cmbCentral.DataBind();
            //cmbCentral.Items.Add("");
            //cmbCentral.SelectedValue = "";

            dtTemp = new DataTable();
        }
        public void CargarDDLConvenio()
        {
            clsCentral clsBussinesCentral = new clsCentral();
            DataTable dtG;
            dtG = clsBussinesCentral.GetAllConvenioByCentral(cmbCentral.Text);
            //Session["dtG"] = dtG;
             //dtG.Rows.Add(0, "0","Nuevo Convenio");
            DataSet ds = new DataSet(); ds.Tables.Add(dtG.Copy());
            ds.Tables[0].DefaultView.Sort = "Convenio";
            cmbConvenio.DataSource = ds;
            cmbConvenio.DataTextField = "Convenio";
            cmbConvenio.DataValueField = "IdConvenio";
            cmbConvenio.DataBind();
            cmbConvenio.Items.Add("Nuevo Convenio");
            cmbConvenio.SelectedValue = "Nuevo Convenio";

            dtTemp = new DataTable();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            pnlErrorLoad.Visible = false;
            clsError.logMessage = "btnUpload_Click";
            clsError.logModule = "porteoatomatico";
            clsError.LogWrite();

            DataTable dtRsponseImport2Grid = new DataTable();
            clsPorteo clsBusinessPorteo = new clsPorteo();
            StringBuilder strHtmlTablePorteo;
            if (IsPostBack)
            {
                try
                {
                    //CargarDDL();
                    if (cmbCentral.SelectedValue == "" || cmbCentral.SelectedValue == "--TODOS--")
                    {
                        //pnlValidaciones.InnerText = "Se debe seleccionar una Central";
                        //pnlValidaciones.Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','Debe seleccionar una central','error');", true);

                        clsError.logMessage = "Se debe seleccionar una Central";
                        clsError.logModule = "porteoatomatico-btnUpload_Click";
                        clsError.LogWrite();
                    }
                    else {
                        if (FileUpload1.HasFile)
                        {
                            clsError.logMessage = "FileUpload1.HasFile";
                            clsError.logModule = "porteoatomatico-btnUpload_Click";
                            clsError.LogWrite();

                            clsBusinessPorteo.strFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                            if (!string.IsNullOrEmpty(clsBusinessPorteo.strFileName))
                            {

                                clsError.logMessage = "!string.IsNullOrEmpty(clsBusinessPorteo.strFileName";
                                clsError.logModule = "porteoatomatico-btnUpload_Click";
                                clsError.LogWrite();

                                clsBusinessPorteo.strExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                                //string nombreArchivo = FileUpload1.FileName;
                                clsBusinessPorteo.strFilePath = Server.MapPath(clsBusinessPorteo.strFileName);
                                //GridView1.Caption = FilePath;
                                FileUpload1.SaveAs(clsBusinessPorteo.strFilePath);
                                clsBusinessPorteo.strIsHDR = ExcelHasHeader.Yes;
                                dtRsponseImport2Grid = clsBusinessPorteo.Import_To_Grid();
                                //Import_To_Grid(clsBusinessPorteo.strFilePath, clsBusinessPorteo.strFileName, "Yes");
                                if (dtRsponseImport2Grid.Rows.Count > 0 && dtRsponseImport2Grid.Columns.Count == 16)
                                {


                                    clsError.logMessage = "dtRsponseImport2Grid.Rows.Count";
                                    clsError.logModule = "porteoatomatico-btnUpload_Click";
                                    clsError.LogWrite();


                                    Session["dt_Sess_Porteo"] = dtRsponseImport2Grid;
                                    clsBusinessPorteo.dtShiping = dtRsponseImport2Grid;
                                    strHtmlTablePorteo = clsBusinessPorteo.CreateTableHTML();
                                    //CreateTableHTML(dtRsponseImport2Grid);
                                    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHtmlTablePorteo.ToString() });
                                    pnlMuestraDatos.Visible = true;
                                    pnlBrowseFile.Visible = false;
                                    btnCancel.Enabled = true;
                                    Button1.Disabled = false;
                                }
                                else
                                {
                                    
                                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','Debe seleccionar un archivo valido a cargar.','error');", true);

                                }
                                
                            }
                        }
                        else
                        {

                            clsError.logMessage = "Debes seccionar un archivo a cargar.";
                            clsError.logModule = "porteoatomatico-btnUpload_Click";
                            clsError.LogWrite();


                            Button1.Disabled = true;
                            btnCancel.Enabled = false;

                            lblFileLoad.Visible = false;
                            pnlFileLoad.Visible = false;
                            pnlMuestraDatos.Visible = false;

                            //pnlBrowseFile.Visible = true;
                            //pnlValidaciones.Visible = true;
                            //pnlValidaciones.InnerText = "Debes seccionar un archivo a cargar.";
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','Debe seleccionar un archivo a cargar.','error');", true);

                        }
                    }
                }
                catch (Exception ex)
                {
                    clsError.logMessage = ex.ToString();
                    clsError.logModule = "btnUpload_Click";
                    clsError.LogWrite();
                }
                finally
                {
                    dtRsponseImport2Grid = null;
                    clsBusinessPorteo = null;
                    strHtmlTablePorteo = null;
                }

            }

        }

        protected void btnSave()
        {

            if (IsPostBack)
            {
                ClientScriptManager CSM = Page.ClientScript;
                if (!ReturnValue())
                {



                    clsPorteoAutomatico clsShiping = new clsPorteoAutomatico();
                    string strEmail = (string)Session["email"];
                    string strIdUsuario = (string)Session["IdUsuario"];

                    try
                    {
                        DataTable dtResultado = new DataTable();
                        dtResultado = (DataTable)Session["dt_Sess_Porteo"];
                        StringBuilder strBullResultado = new StringBuilder();
                        clsShiping.dtResult = (DataTable)Session["dt_Sess_Porteo"];


                        string strCentral = cmbCentral.Items[cmbCentral.SelectedIndex].Value;
                        string strConvenio = cmbConvenio.Items[cmbConvenio.SelectedIndex].Value;
                        clsShiping.strCentral = strCentral;
                        clsShiping.strConvenio = strConvenio;
                        clsShiping.strIdUsuario = int.Parse(strIdUsuario);
                        strBullResultado = clsShiping.RPU();

                        //clsShiping.dtShiping = (DataTable)Session["dt_Sess_Porteo"];
                        //clsShiping.strRPUrepeated = clsShiping.RPU();
                        //if (clsShiping.strRPUrepeated.Length > 0)
                        if (strBullResultado.Length > 0)
                        {
                            //ErrorMsg.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Los siguientes RPU's ya están en la base de datos y no se importaron: <br></strong> " + clsShiping.strRPUrepeated + "</div>";

                            pnlErrorLoad.Visible = true;
                            pnlErrorLoadDiv.Visible = true;
                            //pnlErrorLoadDiv.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Los siguientes RPU's ya están en la base de datos y no se importaron: <br></strong> " + clsShiping.strRPUrepeated + "</div>";
                            //pnlErrorLoadDiv.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Los siguientes RPU's ya están en la base de datos y no se importaron: <br></strong> " + strBullResultado + "</div>";
                            pnlErrorLoadDiv.InnerHtml = "<strong>Los siguientes registros contienen fallas  y no se importaron: <br></strong> " + strBullResultado + "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error','Se econtraron registros con fallas','error');", true);
                            clsPorteo clsBusinessPorteo = new clsPorteo();
                            clsBusinessPorteo.dtShiping = (DataTable)Session["dt_Sess_Porteo"];
                            StringBuilder strHtmlTablePorteo = new StringBuilder();
                            strHtmlTablePorteo = clsBusinessPorteo.CreateTableHTML();
                            //CreateTableHTML(dtRsponseImport2Grid);
                            DBDataPlaceHolder.Controls.Add(new Literal { Text = strHtmlTablePorteo.ToString() });

                            Button1.Disabled = true;
                            btnCancel.Enabled = true;
                        }
                        else
                        {
                           // pnlMuestraDatos.Visible = true;
                           //// pnlSuccessLoad.Visible = true;

                           // //Label1.Text = "convenio generado: " + strBullResultado;

                           // Button1.Visible = true;
                           // btnCancel.Visible = false;

                           // btnRegresar.Enabled = true;
                           // btnRegresar.Visible = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Cargado', 'Su archivo se ha cargado correctamente.', 'success');", true);
                           
                           //cmbCentral.SelectedValue = "";

                            //ErrorMsg.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>La carga dfue exitosa! <br></strong> </div>";
                        }
                    }
                    catch (Exception ex)
                    {
                        clsError.logMessage = ex.ToString();
                        clsError.logModule = "btnUpload_Click";
                        clsError.LogWrite();
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string mensaje = serializer.Serialize(ex.Message);
                        ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error  Detalle: " + mensaje + "');", true);
                    }
                    finally
                    {
                        Session["dt_Sess_Porteo"] = null;
                        clsShiping = null;
                       
                    }
                }
            }

        }

        bool ReturnValue()
        {
            return false;
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Button1.Disabled = true;
            btnCancel.Enabled = false;

            lblFileLoad.Visible = false;
            pnlFileLoad.Visible = false;

            pnlMuestraDatos.Visible = false;

            pnlBrowseFile.Visible = true;


            pnlErrorLoad.Visible = false;
            pnlErrorLoadDiv.Visible = false;

            pnlSuccessLoad.Visible = false;

            Session["dt_Sess_Porteo"] = null;            

        }

        protected void cmbCentral_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlBrowseFile.Visible = true;
            pnlFileLoad.Visible = false;
            pnlMuestraDatos.Visible = false;
            btnCancel.Enabled = false;
            Button1.Disabled = true;
            CargarDDLConvenio();
        }
        //guardar archivo en bd
        protected  void save()
        {
            clsPorteoAutomatico clsShiping = new clsPorteoAutomatico();
            string strEmail = (string)Session["email"];
            string strIdUsuario = (string)Session["IdUsuario"];

            try
            {
                DataTable dtResultado = new DataTable();
                dtResultado = (DataTable)Session["dt_Sess_Porteo"];
                StringBuilder strBullResultado = new StringBuilder();
                clsShiping.dtResult = (DataTable)Session["dt_Sess_Porteo"];

            
                string strCentral = this.cmbCentral.Items[cmbCentral.SelectedIndex].Value;
                clsShiping.strCentral = strCentral;
                clsShiping.strIdUsuario = int.Parse(strIdUsuario);
                strBullResultado = clsShiping.RPU();

                //clsShiping.dtShiping = (DataTable)Session["dt_Sess_Porteo"];
                //clsShiping.strRPUrepeated = clsShiping.RPU();
                //if (clsShiping.strRPUrepeated.Length > 0)
                if (strBullResultado.Length > 0)
                {
                    //ErrorMsg.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Los siguientes RPU's ya están en la base de datos y no se importaron: <br></strong> " + clsShiping.strRPUrepeated + "</div>";

                    pnlErrorLoad.Visible = true;
                    pnlErrorLoadDiv.Visible = true;
                    //pnlErrorLoadDiv.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Los siguientes RPU's ya están en la base de datos y no se importaron: <br></strong> " + clsShiping.strRPUrepeated + "</div>";
                    //pnlErrorLoadDiv.InnerHtml = "<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Los siguientes RPU's ya están en la base de datos y no se importaron: <br></strong> " + strBullResultado + "</div>";
                    pnlErrorLoadDiv.InnerHtml = "<strong>Los siguientes registros contienen fallas  y no se importaron: <br></strong> " + strBullResultado + "";


                    clsPorteo clsBusinessPorteo = new clsPorteo();
                    clsBusinessPorteo.dtShiping = (DataTable)Session["dt_Sess_Porteo"];
                    StringBuilder strHtmlTablePorteo = new StringBuilder();
                    strHtmlTablePorteo = clsBusinessPorteo.CreateTableHTML();
                    //CreateTableHTML(dtRsponseImport2Grid);
                    DBDataPlaceHolder.Controls.Add(new Literal { Text = strHtmlTablePorteo.ToString() });

                    //Button1.Enabled = false;
                    btnCancel.Enabled = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Cargado!', 'Su archivo se ha cargado correctamente.', 'success');", true);
                }
                else
                {
                    pnlMuestraDatos.Visible = false;
                    pnlSuccessLoad.Visible = true;

                    //Label1.Text = "convenio generado: " + strBullResultado;

                    //Button1.Visible = false;
                    btnCancel.Visible = false;

                    btnRegresar.Enabled = true;
                    btnRegresar.Visible = true;

                    cmbCentral.SelectedValue = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Cargado!', 'Su archivo se ha cargado correctamente.', 'success');", true); }
            }
            catch (Exception ex)
            {
                clsError.logMessage = ex.ToString();
                clsError.logModule = "btnUpload_Click";
                clsError.LogWrite();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string mensaje = serializer.Serialize(ex.Message);
                ScriptManager.RegisterStartupScript(this, GetType(), "muestraError", "swal('Error  Detalle: " + mensaje + "');", true);

            }
            finally
            {
                Session["dt_Sess_Porteo"] = null;
                clsShiping = null;
            }
        }

        protected void cmbConvenio_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlBrowseFile.Visible = true;
            pnlFileLoad.Visible = false;
            pnlMuestraDatos.Visible = false;
            btnCancel.Enabled = false;
            Button1.Disabled = true;
        }
    }
}