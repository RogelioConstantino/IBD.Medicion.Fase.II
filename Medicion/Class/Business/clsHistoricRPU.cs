using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Medicion.Class.ADO;
using System.Text;

namespace Medicion.Class.Business
{
    public class clsHistoricRPU
    {
        private ConnectionDB con = new ConnectionDB();
        SqlDataReader drInsert;
        DataTable dtrpu;
        public DataTable GetHistoricRPU(string strRPU, Int16 intActive)
        {
           try
            {

                string query = string.Format("SELECT M.Email, M.FechaCreacion,M.Estatus,  PM.ParametrosMedidor, M.CheckActivo,  M.FechaPrevista, M.FechaInstalacion, M.Observaciones  FROM	PuntoCarga PC WITH(NOLOCK) INNER JOIN Medidor M WITH(NOLOCK) ON PC.RPU = M.RPU LEFT JOIN ParametrosMedidor PM WITH(NOLOCK) ON M.IdParametrosMedidor = PM.IdParametrosMedidor WHERE	PC.RPU = @chrRPU and pc.Activo= @intActivo order by M.FechaCreacion desc");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@intActivo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = strRPU;
                con.dbConnection();
                dtrpu = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetHistoricRPU";
                clsError.LogWrite();
            }

            return dtrpu;
        }
        public DataTable GetSPCommunicationHistoricPorRPU(string strRPU)
        {
            try
            {
                dtrpu = null;
                string query = string.Format("spBuscarHistoricoPorRPU");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                
                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;

                con.dbConnection();
                dtrpu = con.executeStoreProcedure(query, sqlParameters);

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetSPCommunicationHistoricPorRPU";
                clsError.LogWrite();
            }
            return dtrpu;
        }
        
        public DataTable GetSPCommunicationHistoricPorRPU(string strRPU , string intPregunta)
        {
            try
            {
                dtrpu = null;
                string query = string.Format("spBuscarHistoricoPorRPUPorPregunta");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;

                sqlParameters[1] = new SqlParameter("@intPregunta", SqlDbType.NVarChar);
                sqlParameters[1].Value = intPregunta;

                con.dbConnection();
                dtrpu = con.executeStoreProcedure(query, sqlParameters);

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetSPCommunicationHistoricPorRPU";
                clsError.LogWrite();
            }
            return dtrpu;
        }

        public DataTable GetSPCommunicationHistoricPorRPUMed(string strRPU, string intPregunta)
        {
            try
            {
                dtrpu = null;
                string query = string.Format("spBuscarHistoricoPorRPUPorPreguntaMed");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = strRPU;

                sqlParameters[1] = new SqlParameter("@intPregunta", SqlDbType.NVarChar);
                sqlParameters[1].Value = intPregunta;

                con.dbConnection();
                dtrpu = con.executeStoreProcedure(query, sqlParameters);

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "spBuscarHistoricoPorRPUPorPreguntaMed";
                clsError.LogWrite();
            }
            return dtrpu;
        }

        public DataTable GetSPCommunicationHistoricRPU(string strRPU, Int16 intActive)
        {
            try 
            {
                dtrpu = null;
                string query = string.Format("spBuscarComunicacionHistoricoRPU");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@intActivo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = strRPU;
                con.dbConnection();
                dtrpu = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetHistoricRPU";
                clsError.LogWrite();
            }
            return dtrpu;
        }
        public DataTable GetSPHistoricRPU(string strRPU, Int16 intActive) 
        {
            
            try
            {
                dtrpu = null;
                string query = string.Format("spBuscarHistoricoRPU");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@intActivo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = strRPU;
                con.dbConnection();
                dtrpu = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetHistoricRPU";
                clsError.LogWrite();
            }
            return dtrpu;
        }
        /// <summary>
        /// Create table divison schema just columns
        /// </summary>
        /// <returns></returns>
        public StringBuilder CreateCommunicationTableHTML(DataTable dtCommunication)
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            string strFileName;
            string strRPU = string.Empty;
            try
            {

                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtCommunication.Columns)
                {
                    if (column.ColumnName == "NombreArchivo")
                    {
                        html.Append("<th id='hidefile'>");
                        html.Append("ARCHIVO ADJUNTO");
                        html.Append("</th>");
                    }
                    else if (column.ColumnName == "Archivo")
                    {

                    }
                    else
                    {
                        html.Append("<th>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTableCom'> ");
                //Building the Data rows.
                foreach (DataRow row in dtCommunication.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtCommunication.Columns)
                    {
                        if (column.ColumnName == "NombreArchivo")
                        {
                            html.Append("<td id='hidefile'>");
                            strFileName = Convert.ToString(row[column.ColumnName]);
                            //if (!string.IsNullOrEmpty(strFileName))
                            //{
                            //    oclsEncrypt.strData = strFileName;
                            //    strFileName = oclsEncrypt.EncryptData();
                            //}
                            html.Append("<a href='bajararchivo.aspx?doc=" + Convert.ToString(row["Archivo"]) + "'>" + Convert.ToString(row[column.ColumnName]) + "</a>");
                            html.Append("</td>");
                        }
                        else if (column.ColumnName == "Archivo")
                        {

                        }
                        else
                        {
                           
                            if (column.ColumnName == "HABILITADO")
                            {
                                html.Append("<td class='text-center'>");
                                if (Convert.ToString(row[column.ColumnName]) == "1")
                                {
                                    html.Append("<span class='glyphicon glyphicon-ok text-success text-center' aria-hidden='true'></span>");
                                }
                                else if (Convert.ToString(row[column.ColumnName]) == "0")
                                {
                                    html.Append("<span class='glyphicon glyphicon-remove text-danger text-center' aria-hidden='true'></span>");
                                }
                                html.Append("</td>");
                            }
                            else
                            {
                                html.Append("<td>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }

                            
                            if (column.ColumnName == "RPU")
                            {
                                strRPU = Convert.ToString(row[column.ColumnName]);
                                if (!string.IsNullOrEmpty(strRPU))
                                {
                                    oclsEncrypt.strData = strRPU;
                                    strRPU = oclsEncrypt.EncryptData();
                                }
                            }
                        }

                    }
                    //html.Append("<td><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false'><span class='glyphicon glyphicon-pencil'></span></a></td>");
                    //html.Append("<td><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'><span class='glyphicon glyphicon-trash'></span></a></td>");
                    //html.Append("</tr>");
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
        /// <summary>
        /// Create table divison schema just columns
        /// </summary>
        /// <returns></returns>
        public StringBuilder CreateTableHTML(DataTable dtHistoricRPU)
        {
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            string strFileName;
            try
            {
                string strRPU = string.Empty;

                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtHistoricRPU.Columns)
                {
                    if (column.ColumnName == "NombreArchivo")
                    {
                        html.Append("<th id='hidefile'>");
                        html.Append("ARCHIVO ADJUNTO");
                        html.Append("</th>");
                    }
                    else if (column.ColumnName == "Archivo")
                    {

                    }
                    else
                    {
                        html.Append("<th>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtHistoricRPU.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtHistoricRPU.Columns)
                    {
                        if (column.ColumnName == "NombreArchivo")
                        {
                            html.Append("<td id='hidefile'>");
                            strFileName = Convert.ToString(row[column.ColumnName]);
                            //if (!string.IsNullOrEmpty(strFileName))
                            //{
                            //    oclsEncrypt.strData = strFileName;
                            //    strFileName = oclsEncrypt.EncryptData();
                            //}
                            html.Append("<a href='bajararchivo.aspx?doc=" + Convert.ToString(row["Archivo"]) + "'>" + Convert.ToString(row[column.ColumnName]) + "</a>");
                            html.Append("</td>");
                        }
                        else if (column.ColumnName == "Archivo") { 
                        
                        }
                        else
                        {
                            
                            if (column.ColumnName == "HABILITADO")
                            {
                                html.Append("<td class='text-center'>");
                                if (Convert.ToString(row[column.ColumnName]) == "1")
                                {
                                    html.Append("<span class='glyphicon glyphicon-ok text-success text-center' aria-hidden='true'></span>");
                                }
                                else if (Convert.ToString(row[column.ColumnName]) == "0")
                                {
                                    html.Append("<span class='glyphicon glyphicon-remove text-danger text-center' aria-hidden='true'></span>");
                                }
                                html.Append("</td>");
                            }
                            else
                            {
                                html.Append("<td>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }

                           
                            if (column.ColumnName == "RPU")
                            {
                                strRPU = Convert.ToString(row[column.ColumnName]);
                                if (!string.IsNullOrEmpty(strRPU))
                                {
                                    oclsEncrypt.strData = strRPU;
                                    strRPU = oclsEncrypt.EncryptData();
                                }
                            }
                        }
                        
                        
                    }
                    //html.Append("<td>");
                    //html.Append("<a href='#' class='btn btn-warning btn-sm' data-toggle='modal' data-target='.bs-example-modal-lg' aria-label='Left Align' title='Contactos' data-placement='top' ><img src='img/account-card-details.png'></a>");
                    //html.Append("<p></p>");
                    //html.Append("<a href='comunicaciones.aspx?rpu=" + strRPU + "' class='btn btn-info btn-sm' title='Comunicacion'  data-target='.bs-example-modal-lg' data-toggle='tooltip' data-placement='top'><img src='img/access-point-network.png'></a>");
                    //html.Append("</td>");
                    //html.Append("<td>");
                    //html.Append("<a href='medidores.aspx?rpu=" + strRPU + "' class='btn btn-success btn-sm' aria-label='Left Align' title='Medidores' data-toggle='tooltip' data-placement='top'  ><img src='img/smartmeter.png'></a><p></p>");
                    //html.Append("</td>");
                    //html.Append("<td>");
                    //html.Append("<a href='#' class='btn btn-info btn-sm' aria-label='Left Align' title='Eliminar' data-toggle='tooltip' data-placement='top'  ><span class='glyphicon glyphicon-trash btn-sm'></span></a>");
                    //html.Append("</td>");
                    //html.Append("</tr>");
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

        public StringBuilder CreateTableHTMLHistorico(DataTable dtHistoricRPU)
        {
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {
                string strRPU = string.Empty;

                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtHistoricRPU.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtHistoricRPU.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtHistoricRPU.Columns)
                    {
                        html.Append("<td>");
                        if (row[column.ColumnName].ToString() != "01-01-1900")
                            html.Append(row[column.ColumnName]);
                        else
                            html.Append("&nbsp;");
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
                html.Append("</tbody>");
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CreateTableHTMLHistorico";
                clsError.LogWrite();
            }

            return html;
        }

    }
}