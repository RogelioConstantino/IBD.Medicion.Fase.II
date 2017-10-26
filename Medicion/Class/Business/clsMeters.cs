using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Medicion.Class.ADO;
using System.Data;
using System.Configuration;

namespace Medicion.Class.Business
{
    public class clsMeters :clsPropertiesMeters
    {
       
        private ConnectionDB con = new ConnectionDB();

        public Boolean InsertFiles() 
        {
            Boolean msg = true;
            SqlDataReader drInsert;
            DataTable dtData;
            try 
            {
                string query = string.Format("spInsertar_ArchivosMedicion");
                SqlParameter[] sqlParameters = new SqlParameter[6];
                
                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;
                sqlParameters[1] = new SqlParameter("@chrEmail", SqlDbType.NVarChar);
                sqlParameters[1].Value = strEmailusr;
                sqlParameters[2] = new SqlParameter("@chrNombreArchivo", SqlDbType.NVarChar);
                sqlParameters[2].Value = strFileName;
                sqlParameters[3] = new SqlParameter("@chrExtension", SqlDbType.NVarChar);
                sqlParameters[3].Value = strFileExtension;
                sqlParameters[4] = new SqlParameter("@chrArchivo", SqlDbType.NVarChar);
                sqlParameters[4].Value = strArchivo;
                sqlParameters[5] = new SqlParameter("@chrTipoArchivo", SqlDbType.NVarChar);
                sqlParameters[5].Value = strContentType;
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
        public Boolean InsertMeters()
        {
            Boolean msg = true;
            SqlDataReader drInsert;
            DataTable dtData;
            try
            {

                string query = string.Format("spInsertar_Medicion");
                SqlParameter[] sqlParameters = new SqlParameter[8];

                sqlParameters[0] = new SqlParameter("@intIdParametrosMedidor", SqlDbType.Int);
                sqlParameters[0].Value = Convert.ToString(intIDParametersMeters);
                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = strRPU;
                sqlParameters[2] = new SqlParameter("@chrEstatus", SqlDbType.NVarChar);
                sqlParameters[2].Value = strStatus;
                sqlParameters[3] = new SqlParameter("@chrEmail", SqlDbType.NVarChar);
                sqlParameters[3].Value = strEmailusr;
                sqlParameters[4] = new SqlParameter("@chrFechaPrevista", SqlDbType.NVarChar);
                sqlParameters[4].Value = strDeliveryDate;
                sqlParameters[5] = new SqlParameter("@chrFechaInstalacion", SqlDbType.NVarChar);
                sqlParameters[5].Value = strInstallationDate;
                sqlParameters[6] = new SqlParameter("@smiCheckActivo", SqlDbType.SmallInt);
                sqlParameters[6].Value = intCheckActivo;
                sqlParameters[7] = new SqlParameter("@chrObservaciones", SqlDbType.NVarChar);
                sqlParameters[7].Value = strObservation;
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

        public Boolean ValidateParameters() 
        {
            Boolean bMsg = false;
            
            foreach (clsPropertiesMeters pm in LPM) {
                intIDParametersMeters = pm.intIDParametersMeters;
                strEmailusr = pm.strEmailusr;
                
                strDeliveryDate = pm.strDeliveryDate;
                strInstallationDate = pm.strInstallationDate;
                strObservation = pm.strObservation;
                intCheckActivo = pm.intCheckActivo;
                try 
                {
                    InsertMeters();
                }
                catch (Exception ex)
                {
                    LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                    clsError.logMessage = ex.ToString();
                    clsError.logModule = "CreateNewMeters";
                    clsError.LogWrite();
                    
                }
            }
            return bMsg;
        }

        public String GetPathUploadMeters()
        {
            return ConfigurationManager.AppSettings["SubirArchivosMedidores"].ToString();
        }
        public Int32 GetMaxNumber2UploadFiles()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["NoMaximoArchivosSubir"].ToString());
        }

        public Boolean SaveFileMeter2DB(string strFilename, string strExtension, string strEmail, string strRPUf, string strArchivoEnc, string strContentTypeFile) 
        {
            strRPU = strRPUf;
            strEmailusr = strEmail;
            strFileExtension = strExtension;
            strFileName = strFilename;
            strArchivo = strArchivoEnc;
            strContentType = strContentTypeFile;
            Boolean bmsg = InsertFiles();
            return bmsg;
        }
        public PropertiesDocuments DownloadFile(string strEncryptedFile) {
            PropertiesDocuments oclPropDoc = new PropertiesDocuments();
            LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
            DataTable dtData;
            try 
            {
                string query = string.Format("spBuscarArchivoMedicion");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@chrArchivo", SqlDbType.NVarChar);
                sqlParameters[0].Value = strEncryptedFile;
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        oclPropDoc.strFileExtension = row["ExtensionArchivo"].ToString();
                        oclPropDoc.strFileName = row["NombreArchivo"].ToString();
                        oclPropDoc.strMimeType = row["TipoArchivo"].ToString();
                        oclPropDoc.strRPU = row["RPU"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetFile";
                clsError.LogWrite();
                oclPropDoc = null;
            }
            
            return oclPropDoc;
        }
        public Boolean InsertDownloadLogFile(string strFile, string strEmail) {
            Boolean bMsg = true;
            DataTable dtData;
            LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
            try 
            {
                string query = string.Format("spInsertar_LogArchivosMedicion");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@chrEmail", SqlDbType.NVarChar);
                sqlParameters[0].Value = strEmail;
                sqlParameters[1] = new SqlParameter("@chrArchivo", SqlDbType.NVarChar);
                sqlParameters[1].Value = strFile;
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {

                clsError.logMessage = ex.ToString();
                clsError.logModule = "InsertAgreement";
                clsError.LogWrite();
                bMsg = false;
            }
            return bMsg;
        }
        public Boolean InsertAgreement(string strAgreement, string strRPU, string strChecked, string strEmail,decimal decCharge) 
        {
            //SqlDataReader drInsert;
            DataTable dtData;
            Boolean bMsg = true;
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            string strAgreementDecrypted = string.Empty;
            LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
            try 
            {
                oclsEncrypt.strData = strAgreement;
                strAgreementDecrypted = oclsEncrypt.DecryptData();
                Int16 intSeleccionado = Convert.ToInt16(strChecked);
                string query = string.Format("spInsertar_ConvenioPuntoCarga");
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;
                sqlParameters[1] = new SqlParameter("@chrConvenio", SqlDbType.NVarChar);
                sqlParameters[1].Value = strAgreementDecrypted;
                sqlParameters[2] = new SqlParameter("@intSeleccionado", SqlDbType.SmallInt);
                sqlParameters[2].Value = intSeleccionado;
                sqlParameters[3] = new SqlParameter("@chrEmail", SqlDbType.NVarChar);
                sqlParameters[3].Value = strEmail;
                sqlParameters[4] = new SqlParameter("@dblTotalCarga", SqlDbType.Float);
                sqlParameters[4].Value = decCharge;
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
                
            }
            catch (Exception ex)
            {
                
                clsError.logMessage = ex.ToString();
                clsError.logModule = "InsertAgreement";
                clsError.LogWrite();
                bMsg = false;
            }
            return bMsg;
        }

        public DataTable InsertaActualizaMedidores
            (string strRPU,
             string strchkPM1, string strchkPM2,
             string strchkPM3, string strchkPM4,
             string strchkPM5, string strchkPM6,
             string strchkPM7, string strchkPM8,
             string strchkPM9, string strchkPM10,
             string strchkPM11, string strchkPM12,
             string strchkPM13, string strchkPM14,
             string strchkPM15
            , string strIdUsuario
            )
        {
            ConnectionDB con = new ConnectionDB();
            Boolean bMsg = true;
            DataTable dtData;
            try
            {
                string query = string.Format("spInsertaActualizaMedidores");
                SqlParameter[] sqlParameters = new SqlParameter[17];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;

                sqlParameters[1] = new SqlParameter("@ReqContactoElec", SqlDbType.NVarChar);
                sqlParameters[1].Value = strchkPM1;

                sqlParameters[2] = new SqlParameter("@ContactoTerminado", SqlDbType.NVarChar);
                sqlParameters[2].Value = strchkPM2;

                sqlParameters[3] = new SqlParameter("@ReqNodoRed", SqlDbType.NVarChar);
                sqlParameters[3].Value = strchkPM3;

                sqlParameters[4] = new SqlParameter("@NodoTerminado", SqlDbType.NVarChar);
                sqlParameters[4].Value = strchkPM4;

                sqlParameters[5] = new SqlParameter("@esMedidorPrincipal", SqlDbType.NVarChar);
                sqlParameters[5].Value = strchkPM5;

                sqlParameters[6] = new SqlParameter("@Entregado", SqlDbType.NVarChar);
                sqlParameters[6].Value = strchkPM6;

                sqlParameters[7] = new SqlParameter("@TieneMedidorRespaldo", SqlDbType.NVarChar);
                sqlParameters[7].Value = strchkPM7;

                sqlParameters[8] = new SqlParameter("@EntregadoRespaldo", SqlDbType.NVarChar);
                sqlParameters[8].Value = strchkPM8;

                sqlParameters[9] = new SqlParameter("@CArtaSesionRecibida", SqlDbType.NVarChar);
                sqlParameters[9].Value = strchkPM9;

                sqlParameters[10] = new SqlParameter("@MedidorInstalado", SqlDbType.NVarChar);
                sqlParameters[10].Value = strchkPM10;

                sqlParameters[11] = new SqlParameter("@MedidorConPerfil", SqlDbType.NVarChar);
                sqlParameters[11].Value = strchkPM11;

                sqlParameters[12] = new SqlParameter("@ReqLibranza", SqlDbType.NVarChar);
                sqlParameters[12].Value = strchkPM12;

                sqlParameters[13] = new SqlParameter("@CartaCompromisoFirmada", SqlDbType.NVarChar);
                sqlParameters[13].Value = strchkPM13;

                sqlParameters[14] = new SqlParameter("@ReqUbicacion", SqlDbType.NVarChar);
                sqlParameters[14].Value = strchkPM14;

                sqlParameters[15] = new SqlParameter("@ReqGabinete", SqlDbType.NVarChar);
                sqlParameters[15].Value = strchkPM15;

                sqlParameters[16] = new SqlParameter("@IdUsuario", SqlDbType.NVarChar);
                sqlParameters[16].Value = strIdUsuario;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
                return dtData;
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "InsertaActualizaMedidores";
                clsError.LogWrite();
                bMsg = false;
                return null;
            }
            //return bMsg;
        }

        public Boolean InsertaActualizaMedidoresDetalle
            (string strRPU,
             string strComunicacionPregunta,
             string FechaPrevista,
             string FechaInstalacion,
             string Observaciones
            , string strIdUsuario
             , string idLog
            )
        {
            ConnectionDB con = new ConnectionDB();
            Boolean bMsg = true;
            DataTable dtData;
            try
            {
                string query = string.Format("spInsertaActualizaMedidoresDetalle");
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;

                sqlParameters[1] = new SqlParameter("@IdMedidorPregunta", SqlDbType.NVarChar);
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
                clsError.logModule = "spInsertaActualizaMedidoresDetalle";
                clsError.LogWrite();
                bMsg = false;
            }
            return bMsg;
        }
    }
}