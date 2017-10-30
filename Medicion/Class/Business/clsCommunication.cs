using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Medicion.Class.ADO;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

using System.Text;

namespace Medicion.Class.Business
{
    public class clsCommunication 
    {
        DataTable dtData;

        public DataTable GetMedidorActual()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarMedidorActual");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetMedidorActual";
                clsError.LogWrite();

            }
            return dtData;
        }
        public DataTable GetMedidorRequerido()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarMedidorRequerido");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetMedidorRequerido";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable GetElectricMeterType()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarTipoMedidor");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetGroup";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable GetCommunicationClass()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarComunicacionClase");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetCommunicationClass";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable GetCommunicationType()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarTipoComunicacion");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetGroup";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable GetLocalCommunication()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarEstatusComunicacionLocal");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetGroup";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable GetCFECommunication()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarEstatusComunicacionCFE");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetGroup";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable GetEstatusRPU()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarEstatusRUP");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetEstatusRPU";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable GetGestorComercial()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarGestorComercial");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetGestorComercial";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable GetGestorMedicion()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarGestorMedicion");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetGestorMedicion";
                clsError.LogWrite();

            }
            return dtData;
        }


        public DataTable getPregunta(string RUP, string Pregunta)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("buscarPregunta");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                                
                sqlParameters[0] = new SqlParameter("@IdPuntoCarga", SqlDbType.VarChar);
                sqlParameters[0].Value = RUP;

                sqlParameters[1] = new SqlParameter("@pregunta", SqlDbType.Int);
                sqlParameters[1].Value = Pregunta;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "getPregunta";
                clsError.LogWrite();

            }
            return dtData;
        }


        public DataTable getPreguntaTpoCom(string RUP)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("buscarPreguntaTpoCom");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@IdPuntoCarga", SqlDbType.VarChar);
                sqlParameters[0].Value = RUP;
                 

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "buscarPreguntaTpoCom";
                clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable getPreguntaMedidor(string RUP, string Pregunta)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("buscarPreguntaMedicion");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@IdPuntoCarga", SqlDbType.VarChar);
                sqlParameters[0].Value = RUP;

                sqlParameters[1] = new SqlParameter("@pregunta", SqlDbType.Int);
                sqlParameters[1].Value = Pregunta;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "getPreguntaMedidor";
                clsError.LogWrite();

            }
            return dtData;
        }


        public String UploadFiles(System.Web.UI.WebControls.FileUpload UploadFiles, string strPathServer, HttpFileCollection hfc, string strEmail, string strRPU)
        {
            String strMsg = string.Empty;
            Class.Encrypt oclsEncrypt = new Class.Encrypt();

            try 
            {
                Int32 intNumMaxUploadFiles = GetMaxNumber2UploadFiles();
                string strArchivo = DateTime.Now.ToString("ddMMyyyyhhmmssffff");
                oclsEncrypt.strData = strArchivo;
                strArchivo = oclsEncrypt.EncryptData();
                if (!Directory.Exists(strPathServer))
                {
                    Directory.CreateDirectory(strPathServer);
                }
                if (UploadFiles.HasFile)
                {
                    int iUploadedCnt = 0;
                    int iFailedCnt = 0;
                    if (hfc.Count <= intNumMaxUploadFiles)    // 10 FILES RESTRICTION.
                    {
                        for (int i = 0; i <= hfc.Count - 1; i++)
                        {
                            HttpPostedFile hpf = hfc[i];
                            if (hpf.ContentLength > 0) {
                                if (!File.Exists(strPathServer + Path.GetFileName(strArchivo)))
                                {
                                    DirectoryInfo objDir = new DirectoryInfo(strPathServer);
                                    string sFileName = Path.GetFileName(hpf.FileName);
                                    string sFileExt = Path.GetExtension(hpf.FileName);
                                    FileInfo[] objFI = objDir.GetFiles(sFileName.Replace(sFileExt, "") + ".*");
                                    if (objFI.Length > 0)
                                    {
                                        // CHECK IF FILE WITH THE SAME NAME EXISTS 
                                        //(IGNORING THE EXTENTIONS).
                                        foreach (FileInfo file in objFI)
                                        {
                                            string sFileName1 = objFI[0].Name;
                                            string sFileExt1 = Path.GetExtension(objFI[0].Name);

                                            if (sFileName1.Replace(sFileExt1, "") == sFileName.Replace(sFileExt, ""))
                                            {
                                                iFailedCnt += 1;        // NOT ALLOWING DUPLICATE.
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string strContentType = hpf.ContentType.ToString();
                                        // SAVE THE FILE IN A FOLDER.
                                        hpf.SaveAs(strPathServer + Path.GetFileName(sFileName ));
                                        //save to the database like a log
                                        Boolean strSavedFile = SaveFileCommunication2DB(sFileName, sFileExt, strEmail, strRPU, strArchivo, strContentType);
                                        iUploadedCnt += 1;
                                    }
                                }
                                else
                                {
                                    string strContentType = hpf.ContentType.ToString();
                                    if (File.Exists(strPathServer + Path.GetFileName(strArchivo)))
                                    {
                                        string sFileName = Path.GetFileName(hpf.FileName);
                                        string sFileExt = Path.GetExtension(hpf.FileName);
                                        File.Delete(strPathServer + Path.GetFileName(strArchivo + '.' + sFileExt));
                                        hpf.SaveAs(strPathServer + Path.GetFileName(strArchivo + '.' + sFileExt));
                                        Boolean strSavedFile = SaveFileCommunication2DB(sFileName, sFileExt, strEmail, strRPU, strArchivo, strContentType);

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UploadFiles";
                clsError.LogWrite();

            }
            return strMsg;
        }

        public String GetPathUploadCommunication()
        {
            return ConfigurationManager.AppSettings["SubirArchivosComunicacion"].ToString();
        }
        private Int32 GetMaxNumber2UploadFiles()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["NoMaximoArchivosSubir"].ToString());
        }
        private Boolean SaveFileCommunication2DB(string strFilename, string strExtension, string strEmail, string strRPU, string strArchivo, string strContentTypeFile)
        {
            ConnectionDB con = new ConnectionDB();
            Boolean msg = true;
            DataTable dtData;
            try
            {
                string query = string.Format("spInsertar_ArchivosComunicacion");
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;
                sqlParameters[1] = new SqlParameter("@chrEmail", SqlDbType.NVarChar);
                sqlParameters[1].Value = strEmail;
                sqlParameters[2] = new SqlParameter("@chrNombreArchivo", SqlDbType.NVarChar);
                sqlParameters[2].Value = strFilename;
                sqlParameters[3] = new SqlParameter("@chrExtension", SqlDbType.NVarChar);
                sqlParameters[3].Value = strExtension;
                sqlParameters[4] = new SqlParameter("@chrArchivo", SqlDbType.NVarChar);
                sqlParameters[4].Value = strArchivo;
                sqlParameters[5] = new SqlParameter("@chrTipoArchivo", SqlDbType.NVarChar);
                sqlParameters[5].Value = strContentTypeFile;
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateGroup";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        public String ValidateCommunication(List<clsPropertiesCommunications> clsPC, string strRpu, string strEmail)
        {
            List<clsPropertiesCommunications> oClsPC = new List<clsPropertiesCommunications>();
            string strMsg = string.Empty;
            Boolean bMsg = true;
            try 
            {
                foreach (clsPropertiesCommunications pm in clsPC) 
                {
                    bMsg = InsertCommunication(pm.intIDParametersCommunications, strEmail, strRpu, pm.strDeliveryDate, pm.strInstallationDate, pm.strObservation, pm.intCheckActivo, pm.strTypesMeters, pm.strCommunicationClass, pm.strTypesCommunications, pm.strLocalCommunication, pm.strCFECommunication,pm.strActualMeter, pm.strRequiredMeter);
                
                }
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateGroup";
                clsError.LogWrite();
                strMsg = string.Empty;
            }
            return strMsg;
        }
        private Boolean InsertCommunication(int intIDParametersCommunications, string strEmailusr, string strRPU, 
            string strDeliveryDate, string strInstallationDate, string strObservation, int intCheckActivo, 
            string strTypesMeters, string strCommunicationClass, string strCommunicationType, string strLocalCommunication, 
            string strCFECommunication, string strActualMeter, string strRequiredMeter)
        {
            ConnectionDB con = new ConnectionDB();
            Boolean bMsg = true;
            DataTable dtData;
            try 
            {
                string query = string.Format("spInsertar_Comunicacion");
                SqlParameter[] sqlParameters = new SqlParameter[14];
                sqlParameters[0] = new SqlParameter("@intIdParametrosComunicacion", SqlDbType.Int);
                sqlParameters[0].Value = intIDParametersCommunications;
                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = strRPU;
                sqlParameters[2] = new SqlParameter("@chrEmail", SqlDbType.NVarChar);
                sqlParameters[2].Value = strEmailusr;
                sqlParameters[3] = new SqlParameter("@chrFechaPrevista", SqlDbType.NVarChar);
                sqlParameters[3].Value = strDeliveryDate;
                sqlParameters[4] = new SqlParameter("@chrFechaInstalacion", SqlDbType.NVarChar);
                sqlParameters[4].Value = strInstallationDate;
                sqlParameters[5] = new SqlParameter("@chrObservaciones", SqlDbType.NVarChar);
                sqlParameters[5].Value = strObservation;
                sqlParameters[6] = new SqlParameter("@smiCheckActivo", SqlDbType.Int);
                sqlParameters[6].Value = intCheckActivo;
                sqlParameters[7] = new SqlParameter("@TipoMedidor", SqlDbType.NVarChar);
                sqlParameters[7].Value = strTypesMeters;
                sqlParameters[8] = new SqlParameter("@ComunicacionClase", SqlDbType.NVarChar);
                sqlParameters[8].Value = strCommunicationClass;
                sqlParameters[9] = new SqlParameter("@TipoComunicacion", SqlDbType.NVarChar);
                sqlParameters[9].Value = strCommunicationType;
                sqlParameters[10] = new SqlParameter("@EstatusComunicacionLocal", SqlDbType.NVarChar);
                sqlParameters[10].Value = strLocalCommunication;
                sqlParameters[11] = new SqlParameter("@EstatusComunicacionCFE", SqlDbType.NVarChar);
                sqlParameters[11].Value = strCFECommunication;
                sqlParameters[12] = new SqlParameter("@chrMedidorActual", SqlDbType.NVarChar);
                sqlParameters[12].Value = strActualMeter;
                sqlParameters[13] = new SqlParameter("@chrMedidorRequerido", SqlDbType.NVarChar);
                sqlParameters[13].Value = strRequiredMeter;
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "InsertCommunication";
                clsError.LogWrite();
                bMsg = false;
            }
            return bMsg;
        }
        
        public Boolean UpdateRUP(String strRPU, String intEStatus, String intGestorMedicion, String intGestorComunicaciones,int conPrelacion, string strIdUsuario,string strComenTMedidor,int TipoComentario)
        {
            ConnectionDB con = new ConnectionDB();
            Boolean bMsg = true;
            DataTable dtData;
            try
            {
                string query = string.Format("spUpdate_Rup");
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@chrIdEstatus", SqlDbType.Int);
                sqlParameters[0].Value = intEStatus;

                sqlParameters[1] = new SqlParameter("@chrIdGestorComercial", SqlDbType.NVarChar);
                sqlParameters[1].Value = intGestorComunicaciones;

                sqlParameters[2] = new SqlParameter("@chrIdGestorMedicion", SqlDbType.NVarChar);
                sqlParameters[2].Value = intGestorMedicion ;                

                sqlParameters[3] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[3].Value = strRPU;
                sqlParameters[4] = new SqlParameter("@chrIdPrelacion", SqlDbType.Int);
                sqlParameters[4].Value = conPrelacion;
                sqlParameters[5] = new SqlParameter("@IdUsuario", SqlDbType.NVarChar);
                sqlParameters[5].Value = strIdUsuario;
                sqlParameters[6] = new SqlParameter("@Comentario", SqlDbType.VarChar);
                sqlParameters[6].Value = strComenTMedidor;
                sqlParameters[7] = new SqlParameter("@TipoComentario", SqlDbType.Int);
                sqlParameters[7].Value = TipoComentario;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateRUP";
                clsError.LogWrite();
                bMsg = false;
            }
            return bMsg;
        }
        
        public DataTable InsertaActualizaComunicaciones
            (string strRPU,
             string strchkPM1,             string strchkPM2,
             string strcmbActualMeter,     string strcmbMeterType ,   string strcmbRequiredMeter ,
             string strcmbCommunicationClass ,
             string strcmbCommunicationType,
             string strcmbLocalCommunication,             
             string strcmbCFECommunication
            , string strIdUsuario
            )
        {
            ConnectionDB con = new ConnectionDB();
            Boolean bMsg = true;
            DataTable dtData;
            try
            {
                string query = string.Format("spInsertaActualizaComunicaciones");
                SqlParameter[] sqlParameters = new SqlParameter[11];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;

                sqlParameters[1] = new SqlParameter("@bTCyTP", SqlDbType.NVarChar);
                sqlParameters[1].Value = strchkPM1;
                sqlParameters[2] = new SqlParameter("@bBase13", SqlDbType.NVarChar);
                sqlParameters[2].Value = strchkPM2;

                sqlParameters[3] = new SqlParameter("@IdMedidorActual", SqlDbType.NVarChar);
                sqlParameters[3].Value = strcmbActualMeter;
                sqlParameters[4] = new SqlParameter("@IdTipoMedidor", SqlDbType.NVarChar);
                sqlParameters[4].Value = strcmbMeterType;
                sqlParameters[5] = new SqlParameter("@IdMedidorRequerido", SqlDbType.NVarChar);
                sqlParameters[5].Value = strcmbRequiredMeter;

                sqlParameters[6] = new SqlParameter("@IdComunicacionClase", SqlDbType.NVarChar);
                sqlParameters[6].Value = strcmbCommunicationClass;
                sqlParameters[7] = new SqlParameter("@IdTipoComunicacion", SqlDbType.NVarChar);
                sqlParameters[7].Value = strcmbCommunicationType;

                sqlParameters[8] = new SqlParameter("@IdEstatusLocal", SqlDbType.NVarChar);
                sqlParameters[8].Value = strcmbLocalCommunication;
                sqlParameters[9] = new SqlParameter("@IdEstatusCFE", SqlDbType.NVarChar);
                sqlParameters[9].Value = strcmbCFECommunication;
                
                sqlParameters[10] = new SqlParameter("@IdUsuario", SqlDbType.NVarChar);
                sqlParameters[10].Value = strIdUsuario;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
                return dtData;
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "spInsertaActualizaComunicaciones";
                clsError.LogWrite();
                bMsg = false;
                return null;
            }
            //return bMsg;
            
        }

        public Boolean InsertaActualizaComunicacionesTpoCom
                   (string strRPU,                   
                    string strIP,
                    string strMascara,
                    string strPuertaEnlace
            , string strIdUsuario
            , string idLog
                   )
        {
            ConnectionDB con = new ConnectionDB();
            Boolean bMsg = true;
            DataTable dtData;
            try
            {
                string query = string.Format("spInsertaActualizaComunicacionesDetalleTpoCom");
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;

                sqlParameters[1] = new SqlParameter("@IP", SqlDbType.NVarChar);
                sqlParameters[1].Value = strIP;
                sqlParameters[2] = new SqlParameter("@Mascara", SqlDbType.NVarChar);
                sqlParameters[2].Value = strMascara;
                sqlParameters[3] = new SqlParameter("@PuertaEnlace", SqlDbType.NVarChar);
                sqlParameters[3].Value = strPuertaEnlace;

                sqlParameters[4] = new SqlParameter("@IdUsuario", SqlDbType.NVarChar);
                sqlParameters[4].Value = strIdUsuario;

                sqlParameters[5] = new SqlParameter("@IdLog", SqlDbType.NVarChar);
                sqlParameters[5].Value = idLog;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "InsertaActualizaComunicacionesTpoCom";
                clsError.LogWrite();
                bMsg = false;
            }
            return bMsg;
        }




        public Boolean InsertaActualizaComunicacionesDetalle
            (string strRPU,
             string strComunicacionPregunta,
             string FechaPrevista,
             string FechaInstalacion,
             string Observaciones
            , string strIdUsuario
            ,string idLog
            )
        {
            ConnectionDB con = new ConnectionDB();
            Boolean bMsg = true;
            DataTable dtData;
            try
            {
                string query = string.Format("spInsertaActualizaComunicacionesDetalle");
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;
                
                sqlParameters[1] = new SqlParameter("@IdComunicacionPregunta", SqlDbType.NVarChar);
                sqlParameters[1].Value = strComunicacionPregunta;

                sqlParameters[2] = new SqlParameter("@FechaPrevista", SqlDbType.NVarChar);
                sqlParameters[2].Value = FechaPrevista;
                sqlParameters[3] = new SqlParameter("@FechaInstalacion", SqlDbType.NVarChar);
                sqlParameters[3].Value = FechaInstalacion;
                sqlParameters[4] = new SqlParameter("@Observaciones", SqlDbType.NVarChar);
                sqlParameters[4].Value = Observaciones;

                sqlParameters[5] = new SqlParameter("@IdUsuario", SqlDbType.NVarChar);
                sqlParameters[5].Value = strIdUsuario;

                sqlParameters[6] = new SqlParameter("@IdLog", SqlDbType.NVarChar);
                sqlParameters[6].Value = idLog;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "spInsertaActualizaComunicaciones";
                clsError.LogWrite();
                bMsg = false;
            }
            return bMsg;
        }


        public DataTable GetArchivosComunicacion(string strRPU)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarArchivosComunicacion");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetArchivosComunicacion";
                clsError.LogWrite();

            }
            return dtData;
        }


        public StringBuilder CreateTableHTMLArchivos(DataTable dtAgreements, string strFullPath)
        {
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {
                string strAgregar = string.Empty; 
                html.Append(" <thead>");

                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtAgreements.Columns)
                {                    
                    if ( (column.ColumnName == "IdArchivosComunicacion") || (column.ColumnName == "Archivo") || (column.ColumnName == "TipoArchivo") || (column.ColumnName == "ExtensionArchivo") || (column.ColumnName == "FechaCreacion"))
                    {
                        html.Append("<th style='display:none;'> ");
                    }
                    else
                    {
                        html.Append("<th>");
                    }
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");

                //Building the Data rows.
                foreach (DataRow row in dtAgreements.Rows)
                {
                    html.Append("<tr>");

                    //string sFilename =  String.Format("<a href='\\servername\\folder1\\folder2\\folder3\\{0}'  target = '_blank' > " + row[1] + "</ a > ", row[2] + row[4]);

                    html.Append("<td >");//strFullPath
                    html.Append("<a href='" + "\\uploads\\Comunicacion\\" + row[1] + "' target='_Blank'>" + row[1]+"</a>");
                    html.Append("</td>");


                    //foreach (DataColumn column in dtAgreements.Columns)
                    //{
                    //    if ((column.ColumnName == "IdArchivosComunicacion") || (column.ColumnName == "Archivo") || (column.ColumnName == "TipoArchivo") || (column.ColumnName == "ExtensionArchivo") || (column.ColumnName == "FechaCreacion"))
                    //    {
                    //        html.Append("<td style='display:none;'>");
                    //        html.Append(row[column.ColumnName]);
                    //        html.Append("</td>");
                    //    }
                    //    else
                    //    {
                    //        html.Append("<td>");
                    //        html.Append(row[column.ColumnName]);
                    //        html.Append("</td>");
                    //    }
                    //}
                }
                html.Append("</tbody>");
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CreateTableHTML";
                clsError.LogWrite();
            }

            return html;
        }

    }
}