using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Medicion.Class.ADO;
using System.Data.SqlClient;
using System.Text;

namespace Medicion.Class.Business
{
    public class clsGeneralReport
    {
        DataTable dtData;
        public DataTable GetGeneralReport(string strAct, string strRPU, string strOpcConv, string strOpcCom, string strOpcMed
            , string strGrupo
            , string strCentral
            , string strEstatus
            , string strGestor
        )
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[spRptEstatusGlobal]");
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@intActivo", SqlDbType.NVarChar);
                sqlParameters[0].Value = strAct;

                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = strRPU;

                sqlParameters[2] = new SqlParameter("@intOpcConv", SqlDbType.NVarChar);
                sqlParameters[2].Value = strOpcConv;

                sqlParameters[3] = new SqlParameter("@intOpcCom", SqlDbType.NVarChar);
                sqlParameters[3].Value = strOpcCom;

                sqlParameters[4] = new SqlParameter("@intOpcMed", SqlDbType.NVarChar);
                sqlParameters[4].Value = strOpcMed;
                
                sqlParameters[5] = new SqlParameter("@intGrupo", SqlDbType.NVarChar);
                sqlParameters[5].Value = strGrupo;

                sqlParameters[6] = new SqlParameter("@intCentral", SqlDbType.NVarChar);
                sqlParameters[6].Value = strCentral;

                sqlParameters[7] = new SqlParameter("@intEstatus", SqlDbType.NVarChar);
                sqlParameters[7].Value = strEstatus;

                sqlParameters[8] = new SqlParameter("@intGestor", SqlDbType.NVarChar);
                sqlParameters[8].Value = strGestor;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetGeneralReport";
                clsError.LogWrite();

            }
            return dtData;
        }

        /// <summary>
        /// Create table divison schema just columns
        /// </summary>
        /// <returns></returns>
        public StringBuilder CreateTableHTML(DataTable dtGeneralReport)
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {

                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    html.Append("<th class='text-uppercase'>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        //html.Append("<td>");
                        //html.Append(row[column.ColumnName]);
                        html.Append("<td>");
                        if (
                                column.ColumnName.ToUpper() == "REQUIERE TC Y TP"
                             || column.ColumnName.ToUpper() == "PRELACION"
                             || column.ColumnName.ToUpper() == "REQUIERE BASE 13 TERMINALES"

                             || column.ColumnName.ToUpper() == "REQUIERE REUBICACIÓN"
                             || column.ColumnName.ToUpper() == "REQUIERE GABINETE"
                             || column.ColumnName.ToUpper() == "REQUIERECONTACTO ELÉCTRICO"
                             || column.ColumnName.ToUpper() == "CONTACTO TERMINADO"
                             || column.ColumnName.ToUpper() == "REQUIERE NODO DE RED"
                             || column.ColumnName.ToUpper() == "NODO TERMINADO"
                             || column.ColumnName.ToUpper() == "MEDIDOR PRINCIPAL"
                             || column.ColumnName.ToUpper() == "ENTREGADO"
                             || column.ColumnName.ToUpper() == "MEDIDOR RESPALDO"
                             || column.ColumnName.ToUpper() == "ENTREGADO RESPALDO"
                             || column.ColumnName.ToUpper() == "CARTA DE SECION RECIBIDA"
                             || column.ColumnName.ToUpper() == "MEDIDOR INSTALADO"

                             || column.ColumnName.ToUpper() == "MEDIDOR CON PERFIL"
                             || column.ColumnName.ToUpper() == "REQUIERE LIBRANZA"
                             || column.ColumnName.ToUpper() == "CARTA COMPROMISO FIRMADA"

                             //|| column.ColumnName.ToUpper() == "MEDIDOR ACTUAL"
                             //|| column.ColumnName.ToUpper() == "TIPO DE MEDIDOR"
                             //|| column.ColumnName.ToUpper() == "MEDIDOR REQUERIDO"
                             || column.ColumnName.ToUpper() == "CLASE A / B"
                             //|| column.ColumnName.ToUpper() == "TIPO DE COMUNICACIÓN"
                             //|| column.ColumnName.ToUpper() == "PRUEBA COMUNICACIÓN LOCAL"
                             //|| column.ColumnName.ToUpper() == "PRUEBAS DE COMUNICACIÓN CFE"

                           )
                        {
                            if ((Convert.ToString(row[column.ColumnName]) == "2") || (Convert.ToString(row[column.ColumnName]) == "True" || (Convert.ToString(row[column.ColumnName]) == "1")))
                            {
                                html.Append("<span class='glyphicon glyphicon-ok text-success text-center' aria-hidden='true'></span>");
                            }
                            else if ((Convert.ToString(row[column.ColumnName]) == "0") || (Convert.ToString(row[column.ColumnName]) == "False"))
                            {
                                html.Append("<span class='glyphicon glyphicon-remove text-danger text-center' aria-hidden='true'></span>");
                            }
                            else
                            {
                                html.Append("<span class='glyphicon glyphicon-exclamation-sign text-warning text-center' aria-hidden='true'></span>");
                            }
                        }
                        else {
                            html.Append(row[column.ColumnName]);
                        }                      

                        html.Append("</td>");
                        //html.Append("</td>");

                    }
                    //html.Append("<td><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false'><span class='glyphicon glyphicon-pencil'></span></a></td>");
                    //html.Append("<td><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'><span class='glyphicon glyphicon-trash'></span></a></td>");
                    html.Append("</tr>");
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