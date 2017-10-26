using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Medicion.Class.ADO;
using Medicion.Class.LogError;
using Medicion.Class.Catalogos;
using System.Text;

namespace Medicion.Class.Catalogos
{
    public class CatStatus : PropertiesStatus
    {
        private ConnectionDB con = new ConnectionDB();
        DataTable AllStatus;
        DataTable dtExists;

        /// <summary>
        /// Get all Status data from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllStatus()
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format("select IdEstatus, Cve, Estatus, FechaCreacion, FechaCreacion [Fecha de Creación] from [PuntosCargaEstatus]  where Activo = @Activo Order By Estatus");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                con.dbConnection();
                AllStatus = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllStatus";
                clsError.LogWrite();
            }

            return AllStatus;
        }

        /// <summary>
        /// Create table divison schema just columns
        /// </summary>
        /// <returns></returns>
        public StringBuilder CreateTableHTML()
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {

                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                int id = 0;
                foreach (DataColumn column in dtStatus.Columns)
                {

                    if (
                        (column.ColumnName == "FechaCreacion")
                        ||
                        (column.ColumnName == "IdEstatus")
                     ) 
                        html.Append("<th  style='display:none' '>");
                    else  if ((column.ColumnName == "Fecha de creación")                                                                 )
                        html.Append("<th style='width: 220px !important;'>");
                    else
                        html.Append("<th style='width: auto !important;'>");
                                        
                    if (    (column.ColumnName == "Cve")   )
                        html.Append("Clave");
                    else
                        html.Append(column.ColumnName);

                    html.Append("</th>");
                }
                html.Append("<th ><th>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtStatus.Rows)
                {                    
                    html.Append("<tr>");
                    foreach (DataColumn column in dtStatus.Columns)
                    {
                        if ( 
                            (column.ColumnName == "FechaCreacion")                         
                            ||
                            (column.ColumnName == "IdEstatus")
                            )
                            html.Append("<td style='display:none' >");                        
                        else
                            html.Append("<td >");

                        id++;
                        
                            html.Append(row[column.ColumnName]);

                        html.Append("</td>");
                    }
                    html.Append("<td style='width: 80px !important;'><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false'><span class='glyphicon glyphicon-pencil'></span></a></td>");
                    html.Append("<td style='width: 80px !important;'><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'><span class='glyphicon glyphicon-trash'></span></a></td>");
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

        /// <summary>
        /// Insert New Division into datat base
        /// </summary>
        /// <returns></returns>
        public Boolean NewStatus()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("INSERT INTO [PuntosCargaEstatus](Estatus,FechaCreacion, Cve )Values(@Status,GETDATE() , @Cve )");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Status", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(Status);

                sqlParameters[1] = new SqlParameter("@Cve", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(Cve);


                con.dbConnection();
                msg = con.executeInsertQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "NewStatus";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        /// <summary>
        /// Update Status into the database
        /// </summary>
        /// <returns></returns>
        public Boolean UpdateStatus()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update PuntosCargaEstatus SET Estatus = @Status where Activo = @Activo and IdEstatus= @IdStatus");
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@Status", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(Status);
                sqlParameters[1] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[1].Value = Convert.ToString(Activo);
                sqlParameters[2] = new SqlParameter("@IdStatus", SqlDbType.Int);
                sqlParameters[2].Value = Convert.ToString(idStatus);
                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateStatus";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }
        /// <summary>
        /// Logical delete data from database, just update Active =0
        /// </summary>
        /// <returns></returns>
        public Boolean DeleteStatus()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update [PuntosCargaEstatus] SET Activo = @Activo, FechaEliminacion= getdate() where IdEstatus= @IdStatus ");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                sqlParameters[1] = new SqlParameter("@IdStatus", SqlDbType.Int);
                sqlParameters[1].Value = Convert.ToString(idStatus);
                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "DeleteStatus";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        public DataTable ExistsCve()
        {

            try
            {
                string query = string.Format("select count(1) existe   from PuntosCargaEstatus where Activo = @Activo and ( Cve = @Cve or  Estatus = @Status )  "); // and IdEstatus <> @idStatus 
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);

                sqlParameters[1] = new SqlParameter("@Cve", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(Cve);

                sqlParameters[2] = new SqlParameter("@Status", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(Status);

                con.dbConnection();
                dtExists = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistsCentral";
                clsError.LogWrite();
            }
            return dtExists;
        }

        public DataTable ExistsCve_Update()
        {

            try { 
           
                string query = string.Format("select count(1) existe   from PuntosCargaEstatus where Activo = @Activo and ( Cve = @Cve or  Estatus = @Status ) and IdEstatus <> @idStatus  ");  
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);

                sqlParameters[1] = new SqlParameter("@Cve", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(Cve);

                sqlParameters[2] = new SqlParameter("@Status", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(Status);

                sqlParameters[3] = new SqlParameter("@idStatus", SqlDbType.NVarChar);
                sqlParameters[3].Value = Convert.ToString(idStatus);


                con.dbConnection();
                dtExists = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistsCentral";
                clsError.LogWrite();
            }
            return dtExists;
        }


    }
}