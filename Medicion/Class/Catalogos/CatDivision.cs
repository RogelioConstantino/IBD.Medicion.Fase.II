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
    public class CatDivision : PropertiesDivision
    {
        private ConnectionDB con = new ConnectionDB();
        DataTable AllDivision;

        /// <summary>
        /// Get all division data from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllGroup()
        {
            String FullName = string.Empty;

            try
            {
                string query = string.Format("SELECT IdDivision Id, convert(varchar(2),CveDivision) Clave, Division [Descripción], FechaCreacion [Fecha de Creación] FROM Divisiones WHERE Activo = @Activo ORDER BY Division");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                con.dbConnection();
                AllDivision = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllGroup";
                clsError.LogWrite();
            }

            return AllDivision;
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
                foreach (DataColumn column in dtDivision.Columns)
                {

                    if (id == 0)
                        html.Append("<th style='display:none'>");
                    else
                    {
                        if ((column.ColumnName == "FechaCreacion") || (column.ColumnName == "Fecha de Creación"))
                            html.Append("<th style='width: 220px !important;'>");
                        else
                            html.Append("<th>");
                    }
                    id++;

                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("<th ></th>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.

                foreach (DataRow row in dtDivision.Rows)
                {
                    int id1 = 0;
                    html.Append("<tr>");
                    foreach (DataColumn column in dtDivision.Columns)
                    {
                        if (id1 == 0)
                            html.Append("<td style='display:none' >");
                        else if (id1 == 1)
                            html.Append("<td style='width: 130px !important;' >");
                        else {
                            html.Append("<td>");
                        }
                        // html.Append("<td " + ((id1 == 0) ? "style='display:none'" : ((id1 == 1) ? "style='width: 130px !important;'" : "")) + ">");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                        id1++;
                    }
                    html.Append("<td><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false' title='Editar División'><span class='glyphicon glyphicon-pencil'></span></a></td>");
                    html.Append("<td><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'  title='Eliminar División'><span class='glyphicon glyphicon-trash'></span></a></td>");
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
        public Boolean NewDivision()
        {
            Boolean msg = true;
            DataTable dtData = null;
            try
            {
                //string query = string.Format("INSERT INTO Division(CveDivision, Division, FechaCreacion) Values(@CveDivision, upper(@Division), GETDATE())");
                string query = string.Format("spInsertar_catDivision ");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@CveDivision", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(CveDivision);

                sqlParameters[1] = new SqlParameter("@Division", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(Division);

                con.dbConnection();
                //msg = con.executeStoreProcedure(query, sqlParameters);

                dtData = con.executeStoreProcedure(query, sqlParameters);

                msg = true;

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "NewDivision";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }
        /// <summary>
        /// Update Division into the database
        /// </summary>
        /// <returns></returns>
        public Boolean UpdateDivision()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update Divisiones SET  CveDivision = upper(@CveDivision),   Division = upper(@Division) where Activo = @Activo and IdDivision= @IdDivision");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@CveDivision", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(CveDivision);

                sqlParameters[1] = new SqlParameter("@Division", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(Division);

                sqlParameters[2] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[2].Value = Convert.ToString(Activo);

                sqlParameters[3] = new SqlParameter("@IdDivision", SqlDbType.Int);
                sqlParameters[3].Value = Convert.ToString(idDivision);

                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateDivision";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }
        /// <summary>
        /// Logical Delete data from table Division
        /// </summary>
        /// <returns></returns>
        public Boolean DeleteDivision()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update Divisiones SET Activo = @Activo, FechaEliminacion= getdate() where IdDivision= @IdDivision ");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                sqlParameters[1] = new SqlParameter("@IdDivision", SqlDbType.Int);
                sqlParameters[1].Value = Convert.ToString(idDivision);
                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "DeleteDivision";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        public DataTable ExistDivision()
        {
            String FullName = string.Empty;

            try
            {
                string query = string.Format("SELECT IdDivision, CveDivision, Division, FechaCreacion FROM Divisiones WHERE Activo = @Activo and Division = @strCveDivision  ");//and  Division = upper(@strDivision)
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);

                sqlParameters[1] = new SqlParameter("@strCveDivision", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(CveDivision);

                //sqlParameters[2] = new SqlParameter("@strDivision", SqlDbType.NVarChar);
                //sqlParameters[2].Value = Convert.ToString(Division);

                con.dbConnection();
                AllDivision = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistDivision";
                clsError.LogWrite();
            }

            return AllDivision;
        }

        public DataTable ExistDivisionCve()
        {
            String FullName = string.Empty;

            try
            {
                string query = string.Format("SELECT  count(1) existe   FROM Divisiones WHERE Activo = @Activo and ( CveDivision = upper(@strCveDivision)  or  Division = upper(@strDivision) )");//and  Division = upper(@strDivision)
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);

                sqlParameters[1] = new SqlParameter("@strCveDivision", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(CveDivision);

                sqlParameters[2] = new SqlParameter("@strDivision", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(Division);

                con.dbConnection();
                AllDivision = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistDivision";
                clsError.LogWrite();
            }

            return AllDivision;
        }

        public DataTable ExistDivisionCveID()
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format("SELECT  count(1) existe   " +
                                              "FROM Divisiones " + 
                                              "WHERE Activo = @Activo and ( CveDivision = upper(@strCveDivision)  or  Division = upper(@strDivision) ) "+
                                              "  and idDivision <> @IdDivision  ");//and  Division = upper(@strDivision)
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);

                sqlParameters[1] = new SqlParameter("@strCveDivision", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(CveDivision);

                sqlParameters[2] = new SqlParameter("@strDivision", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(Division);

                sqlParameters[3] = new SqlParameter("@IdDivision", SqlDbType.NVarChar);
                sqlParameters[3].Value = Convert.ToString(idDivision);

                con.dbConnection();
                AllDivision = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistDivision";
                clsError.LogWrite();
            }

            return AllDivision;
        }

    }
}