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
    public class CatZone : PropertiesZone
    {

        private ConnectionDB con = new ConnectionDB();
        DataTable Allzone;
        DataTable dtZoneByDivision;
        /// <summary>
        /// Get all Status data from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllZone()
        {
            
            try
            {
                string query = string.Format("select d.IdDivision, d.Division [División], z.IdZona Id, z.cveZona Clave, z.Zona, z.Observaciones, z.FechaCreacion [Fecha de Creación] from zonas z join divisiones d on d.IdDivision = z.IdDivision  where z.Activo = @Activo Order By z.Zona");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                con.dbConnection();
                Allzone = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllZone";
                clsError.LogWrite();
            }

            return Allzone;
        }
        public DataTable GetAllZoneByDivisionAndZone() {

            try 
            {
                string query = string.Format("select d.IdDivision, d.Division  [División], z.IdZona Id, z.cveZona Clave, z.Zona, z.Observaciones, z.FechaCreacion [Fecha de Creación] from zonas z join divisiones d on d.IdDivision = z.IdDivision  where z.Activo = @Activo and z.IdDivision=@strDivision and (  z.IdZona = @strZone )  Order By z.Zona");   //z.CveZona = @strCveZone  or 
                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                sqlParameters[1] = new SqlParameter("@strDivision", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strDivision);
                sqlParameters[2] = new SqlParameter("@strCveZone", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(strCveZone);
                sqlParameters[3] = new SqlParameter("@strZone", SqlDbType.NVarChar);
                sqlParameters[3].Value = Convert.ToString(strZone);
                con.dbConnection();
                Allzone = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllZoneByDivisionAndZone";
                clsError.LogWrite();
            }
            return Allzone;
        }
        public DataTable GetAllZoneByDivision(){
            try
            {
                string query = string.Format("select d.IdDivision, d.Division  [División], z.IdZona Id, z.cveZona Clave, z.Zona, z.Observaciones, z.FechaCreacion [Fecha de Creación] from zonas z join divisiones d on d.IdDivision = z.IdDivision  where z.Activo = @Activo and z.IdDivision=@strDivision Order By z.Zona");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                sqlParameters[1] = new SqlParameter("@strDivision", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strDivision);
                con.dbConnection();
                Allzone = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllZoneByDivision";
                clsError.LogWrite();
            }

            return Allzone;
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
                foreach (DataColumn column in dtZone.Columns)
                {
                    if ((column.ColumnName == "Id") || column.ColumnName == "IdDivision")
                        html.Append("<th style='display:none'>");
                    else if ((column.ColumnName == "Fecha de Creación"))
                        html.Append("<th style='width: 230px !important;'>");
                    else if ((column.ColumnName == "Cve")|| (column.ColumnName == "División"))
                        html.Append("<th style='width: 150px !important;'>");
                    else                    
                        html.Append("<th >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("<th ></th>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtZone.Rows)
                {
                    int id1 = 0;
                    html.Append("<tr>");
                    foreach (DataColumn column in dtZone.Columns)
                    {
                        if ((column.ColumnName =="Id") || (column.ColumnName == "IdDivision")  )
                            html.Append("<td style='display:none' >");
                        else if ( (column.ColumnName == "Cve") || (column.ColumnName == "Division") )
                            html.Append("<td style='width: 100px !important;' >");
                        else if (column.ColumnName == "Fecha de creación")
                            html.Append("<td style='width: 150px !important;' >");
                        else
                        {
                            html.Append("<td>");
                        }
                        //html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");

                    }
                    html.Append("<td><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false' title='Editar zona'><span class='glyphicon glyphicon-pencil'></span></a></td>");
                    html.Append("<td><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false' title='Eliminar zona'><span class='glyphicon glyphicon-trash'></span></a></td>");
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
        public Boolean NewZone()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("INSERT INTO Zonas (IdDivision, CveZona, Zona, Observaciones, FechaCreacion)Values(@strDivision, upper(@strCveZona), upper(@strZone), upper(@strObservation), GETDATE())");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@strDivision", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(strDivision);

                sqlParameters[1] = new SqlParameter("@strCveZona", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(strCveZone);

                sqlParameters[2] = new SqlParameter("@strZone", SqlDbType.NChar);
                sqlParameters[2].Value = Convert.ToString(strZone);

                sqlParameters[3] = new SqlParameter("@strObservation", SqlDbType.NChar);
                sqlParameters[3].Value = Convert.ToString(strObservation);

                con.dbConnection();
                msg = con.executeInsertQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "NewZone";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        /// <summary>
        /// Update Zone into the database
        /// </summary>
        /// <returns></returns>
        public Boolean UpdateZone()
        {
            Boolean msg = true;
            try
            {
                //string query = string.Format("Update Zona SET  CveZona = upper(@CveZona),  Observaciones = upper(@strObservation) where Activo = @Activo and Division= @Division and Zona=upper(@Zone) ");
                string query = string.Format("UPDATE Zonas " +
                                             "   SET CveZona        = upper(@CveZona)" +
                                             "     , Zona           = upper(@Zone)" +
                                             "     , Observaciones  = upper(@strObservation) " +
                                             " WHERE Activo = @Activo " +
                                             "   AND idzona = @IdZone  ");
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@CveZona", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(strCveZone);

                sqlParameters[1] = new SqlParameter("@Zone", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strZone);

                sqlParameters[2] = new SqlParameter("@strObservation", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(strObservation);

                sqlParameters[3] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[3].Value = Convert.ToString(intActivo);

                //sqlParameters[4] = new SqlParameter("@Division", SqlDbType.NVarChar);
                //sqlParameters[4].Value = Convert.ToString(strDivision);

                sqlParameters[4] = new SqlParameter("@IdZone", SqlDbType.NVarChar);
                sqlParameters[4].Value = Convert.ToString(intIdZone);

                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateZone";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        /// <summary>
        /// Logical delete data from database, just update Active =0
        /// </summary>
        /// <returns></returns>
        public Boolean DeleteZone()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update Zonas SET Activo = @Activo, FechaEliminacion= getdate() where IdZona = @strZone "); //Division= @strDivision and 
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                //sqlParameters[1] = new SqlParameter("@strDivision", SqlDbType.NVarChar);
                //sqlParameters[1].Value = Convert.ToString(strDivision);
                sqlParameters[1] = new SqlParameter("@strZone", SqlDbType.Int);
                sqlParameters[1].Value = Convert.ToString(strZone);

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

        public DataTable GetAllZoneByDivision_DDL()
        {
            try
            {
                string query = string.Format("select z.IdZona Id, z.Zona from zonas z where z.Activo = 1 and z.IdDivision=@strDivision Order By z.Zona");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                
                sqlParameters[0] = new SqlParameter("@strDivision", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(strDivision);
                con.dbConnection();
                Allzone = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllZoneByDivision_DDL";
                clsError.LogWrite();
            }

            return Allzone;
        }

        public DataTable GetAllZoneByDivisionAndCVe()
        {
            try
            {
                string query = string.Format("select 1 from zonas z where z.Activo = @Activo and   Zona = @CveZona  ");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                sqlParameters[1] = new SqlParameter("@CveZona", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strZone);
                con.dbConnection();
                Allzone = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllZoneByDivision";
                clsError.LogWrite();
            }

            return Allzone;
        }

        public DataTable GetAllZoneByDivisionAndCVeID()
        {
            try
            {
                string query = string.Format("select count(1) existe  " +
                                            "  from zonas z " +
                                            " where z.Activo = @Activo " +
                                            "   and ( CveZona = upper(@strCveZona)  or Zona = upper(@strZona) )   " +
                                            "   and idZona <> @IdZona  " );
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);

                sqlParameters[1] = new SqlParameter("@strCveZona", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strCveZone);

                sqlParameters[2] = new SqlParameter("@strZona", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(strZone);

                sqlParameters[3] = new SqlParameter("@IdZona", SqlDbType.NVarChar);
                sqlParameters[3].Value = Convert.ToString(intIdZone);

                con.dbConnection();
                Allzone = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllZoneByDivisionAndCVeID";
                clsError.LogWrite();
            }

            return Allzone;
        }

        public Boolean NewZoneByDiv()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("INSERT INTO Zonas (IdDivision, CveZona, Zona, Observaciones, FechaCreacion) " +
                                             "            Values( (SELECT top 1 IdDivision FROM Divisiones WHERE Activo = 1 and CveDivision = upper(@strDivision) ) " +
                                             "                   , upper(@strCveZona), upper(@strZone), upper(@strObservation), GETDATE() " +
                                             "                  ) ");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@strDivision", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(strDivision);

                sqlParameters[1] = new SqlParameter("@strCveZona", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(strCveZone);

                sqlParameters[2] = new SqlParameter("@strZone", SqlDbType.NChar);
                sqlParameters[2].Value = Convert.ToString(strZone);

                sqlParameters[3] = new SqlParameter("@strObservation", SqlDbType.NChar);
                sqlParameters[3].Value = Convert.ToString(strObservation);

                con.dbConnection();
                msg = con.executeInsertQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "NewZone";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

    }
}