using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medicion.Class.ADO;
using Medicion.Class.LogError;
using Medicion.Class.Catalogos;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Medicion.Class.Catalogos
{
    public class CatConvenios:PropertiesConvenios
    {
        private ConnectionDB con = new ConnectionDB();
        //DataTable dtConvenio;
        public DataTable GeConveniosPorCentral()
        {
            
            try
            {
                string query = string.Format("SELECT IdConvenio Id , Convenio, c.Descripcion [Descripción], IdCentral,  ce.Descripcion [Estatus] , c.IdEstatus, c.FechaCreacion [Fecha de Creación],  c.Activo " +
                                            "   FROM Convenios c " +
                                            "   JOIN ConveniosEstatus ce " +
                                            "     ON c.IdEstatus = ce.IdEstatus " +
                                            "  WHERE c.activo = 1 and c.IdCentral = @IdCentral " +
                                            "  ORDER BY [Fecha de Creación] desc ");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@IdCentral", SqlDbType.SmallInt);
                sqlParameters[0].Value = idCentral;
                con.dbConnection();
                dtConvenio = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GeConveniosPorCentral";
                clsError.LogWrite();
            }
            return dtConvenio;
        }

        public DataTable GeConveniosPorConvenio()
        {

            try
            {
                string query = string.Format("SELECT IdConvenio Id , Convenio, c.Descripcion [Descripción], IdCentral,  ce.Descripcion [Estatus] , c.IdEstatus, c.FechaCreacion [Fecha de Creación],  c.Activo " +
                                            "   FROM Convenios c " +
                                            "   JOIN ConveniosEstatus ce " +
                                            "     ON c.IdEstatus = ce.IdEstatus " +
                                            "  WHERE c.activo = 1 and c.IdConvenio = @IdConvenio " +
                                            "  ORDER BY [Fecha de Creación] desc ");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@IdConvenio", SqlDbType.SmallInt);
                sqlParameters[0].Value = idConvenio;
                con.dbConnection();
                dtConvenio = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GeConveniosPorConvenio";
                clsError.LogWrite();
            }
            return dtConvenio;
        }

        public StringBuilder CreateTableHTML()
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {

                string SConvenios = "";

                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                int id = 0;
                foreach (DataColumn column in dtConvenio.Columns)
                {                    
                        if (column.ColumnName == "Fecha de creación")
                            html.Append("<th style='width: 220px !important;'>");                        
                        else if (column.ColumnName == "Carga")
                            html.Append("<th style='width: 180px !important;'>");
                        else if (column.ColumnName == "Código")
                            html.Append("<th style='width: 150px !important;'>");
                        else if (column.ColumnName == "IdCentral" || column.ColumnName == "IdEstatus" || column.ColumnName == "Id"  || column.ColumnName == "Activo" )
                            html.Append("<th style='display:none'>");
                        else
                            html.Append("<th>");
                    
                    id++;

                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("<th ></th>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows

                foreach (DataRow row in dtConvenio.Rows)
                {
                    int id1 = 0;
                    html.Append("<tr>");
                    foreach (DataColumn column in dtConvenio.Columns)
                    {
                        if (column.ColumnName == "Id")
                            SConvenios = row[column.ColumnName].ToString();

                        if (column.ColumnName == "IdCentral" || column.ColumnName == "IdEstatus" || column.ColumnName == "Id" || column.ColumnName == "Activo")
                            html.Append("<td style='display:none'>");
                        else
                            html.Append("<td style='width: 130px !important;'>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                        id1++;
                    }
                    html.Append("<td style='width: 80px !important;'  ><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false'><span class='glyphicon glyphicon glyphicon-edit' TITLE='Editar Convenios'></span></a></td>");                    
                    html.Append("</tr>");
                }
                html.Append("</tbody>");

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CreateTableHTMLConvenio";
                clsError.LogWrite();
            }

            return html;
        }


        public DataTable GeEstatus()
        {
            try
            {
                string query = string.Format("SELECT IdEstatus Id ,Descripcion  FROM ConveniosEstatus where Activo	=	@Activo  ");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = 1;
                con.dbConnection();
                dtConvenio = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GeEstatus";
                clsError.LogWrite();
            }
            return dtConvenio;
        }

        public Boolean Update()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("spUpdateConvenio");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@Descripcion", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(Descripción);

                sqlParameters[1] = new SqlParameter("@Estatus", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(Estatus);
                
                sqlParameters[2] = new SqlParameter("@IdConvenio", SqlDbType.Int);
                sqlParameters[2].Value = Convert.ToString(idConvenio);
                //Para barrer el estatus de los conveniones existentes a el esatus anterior
                if (Estatus == "Actual")
                {
                    string query2 = string.Format("[spUpdateEstatusConvenio]");
                    SqlParameter[] sqlParameters2 = new SqlParameter[1];

                    sqlParameters2[0] = new SqlParameter("@Convenio", SqlDbType.Int);
                    sqlParameters2[0].Value = Convert.ToInt32(Convenio);

                    con.dbConnection();

                    DataTable result = con.executeStoreProcedure(query2, sqlParameters2);
                }
                con.dbConnection();
                DataTable result2 = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateConvenio";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

    }

}