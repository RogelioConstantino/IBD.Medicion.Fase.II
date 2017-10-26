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
    public class catGestor:PropertiesGestores
    {
        private ConnectionDB con = new ConnectionDB();
        DataTable AllDT;
        DataTable dtExists;

        public DataTable GetAll()
        {
            String FullName = string.Empty;
            try
            {
                //string query = string.Format("select Titulo, cfe.Division, cfe.Zona, Correo, Nombre, ApPaterno as [Apellido Paterno], ApMaterno as [Apellido Materno], TelTrabajo as [Tel. Trabajo], Extencion as Extension, Celular, cfe.FechaCreacion as Alta from ContactoCFE CFE inner join Zona Z on CFE.Zona = z.Zona where cfe.Activo = @Activo  and z.Activo= @Activo  Order By Nombre");
                string query = string.Format("SELECT  IdGestor Id" +
                                            "       ,  r.IdGestorTipo, t.GestorTipo Tipo, g.IdGestorRol , r.GestorRol Rol" +
                                            "       , NumeroEmpleado[Núm de Empleado],  g.cve Iniciales, Nombre + ' ' + ApPaterno + ' ' + ApMaterno as [Nombre Completo], Nombre, ApPaterno, ApMaterno, NumeroEmpleado, g.FechaCreacion [Fecha de Creación] , g.FechaCreacion, g.cve  " +
                                             "  FROM Gestores g join [GestorRoles] r on g.IdGestorRol = r.IdGestorRol   join[GestorTipos] t on t.IdGestorTipo = r.IdGestorTipo " +
                                             " WHERE g.Activo = @Activo   " +
                                             " ORDER BY Nombre + ' ' + ApPaterno + ' ' + ApMaterno ");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                con.dbConnection();
                AllDT = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAll";
                clsError.LogWrite();
            }

            return AllDT;
        }

        public Boolean New()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("INSERT INTO gestores(NumeroEmpleado, Nombre, ApPaterno, ApMaterno, IdGestorRol, FechaCreacion , Cve)Values(@NumeroEmpleado, upper(@strName),upper(@strFirstName),upper(@strLastName), @strPuesto,GETDATE(), @Cve)");
                SqlParameter[] sqlParameters = new SqlParameter[6];
                                
                sqlParameters[0] = new SqlParameter("@NumeroEmpleado", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(strNumeroEmpleado);

                sqlParameters[1] = new SqlParameter("@strName", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(strName);

                sqlParameters[2] = new SqlParameter("@strFirstName", SqlDbType.NChar);
                sqlParameters[2].Value = Convert.ToString(strFirstName);

                sqlParameters[3] = new SqlParameter("@strLastName", SqlDbType.NChar);
                sqlParameters[3].Value = Convert.ToString(strLastName);
                                
                sqlParameters[4] = new SqlParameter("@strPuesto", SqlDbType.NChar);
                sqlParameters[4].Value = Convert.ToString(strPuesto);

                sqlParameters[5] = new SqlParameter("@Cve", SqlDbType.NChar);
                sqlParameters[5].Value = Convert.ToString(strIniciales); 

                con.dbConnection();
                msg = con.executeInsertQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "New";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }
        public DataTable Exists()
        {

            try
            {
                string query = string.Format("select count(1) existe   from gestores where Activo = @Activo and ( ( Nombre =  @Nombre and ApPaterno = @ApPaterno and ApMaterno = @ApMaterno )  or ( NumeroEmpleado = @NumeroEmpleado ) ) ");

                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);

                sqlParameters[1] = new SqlParameter("@Nombre", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strName);

                sqlParameters[2] = new SqlParameter("@ApPaterno", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(strFirstName);

                sqlParameters[3] = new SqlParameter("@ApMaterno", SqlDbType.NVarChar);
                sqlParameters[3].Value = Convert.ToString(strLastName);

                sqlParameters[4] = new SqlParameter("@NumeroEmpleado", SqlDbType.NVarChar);
                sqlParameters[4].Value = Convert.ToString(strNumeroEmpleado); 

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

        public DataTable ExistsGestor()
        {

            try
            {
                string query = string.Format("select count(1) existe   from gestores where Activo = @Activo and ( ( Nombre =  @Nombre and ApPaterno = @ApPaterno and ApMaterno = @ApMaterno )  or ( NumeroEmpleado = @NumeroEmpleado ) or ( Cve = @Iniciales )  ) ");

                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);

                sqlParameters[1] = new SqlParameter("@Nombre", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strName);

                sqlParameters[2] = new SqlParameter("@ApPaterno", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(strFirstName);

                sqlParameters[3] = new SqlParameter("@ApMaterno", SqlDbType.NVarChar);
                sqlParameters[3].Value = Convert.ToString(strLastName);

                sqlParameters[4] = new SqlParameter("@NumeroEmpleado", SqlDbType.NVarChar);
                sqlParameters[4].Value = Convert.ToString(strNumeroEmpleado);

                sqlParameters[5] = new SqlParameter("@Iniciales", SqlDbType.NVarChar);
                sqlParameters[5].Value = Convert.ToString(strIniciales);

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

        public DataTable ExistsId()
        {

            try
            {
                string query = string.Format("select count(1) existe   from gestores where Activo = @Activo and ( ( Nombre =  @Nombre and ApPaterno = @ApPaterno and ApMaterno = @ApMaterno and cve = @Cve)  AND ( NumeroEmpleado = @NumeroEmpleado ) ) and IdGestor <> @Id ");

                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);

                sqlParameters[1] = new SqlParameter("@Nombre", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strName);

                sqlParameters[2] = new SqlParameter("@ApPaterno", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(strFirstName);

                sqlParameters[3] = new SqlParameter("@ApMaterno", SqlDbType.NVarChar);
                sqlParameters[3].Value = Convert.ToString(strLastName);

                sqlParameters[4] = new SqlParameter("@Cve", SqlDbType.NVarChar);
                sqlParameters[4].Value = Convert.ToString(strIniciales); 

                sqlParameters[5] = new SqlParameter("@NumeroEmpleado", SqlDbType.NVarChar);
                sqlParameters[5].Value = Convert.ToString(strNumeroEmpleado);

                sqlParameters[6] = new SqlParameter("@Id", SqlDbType.NVarChar);
                sqlParameters[6].Value = Convert.ToString(Id);

                con.dbConnection();
                dtExists = con.executeSelectQuery(query, sqlParameters);

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistsCentralID";
                clsError.LogWrite();
            }
            return dtExists;

        }

        public StringBuilder CreateTableHTML()
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in  dt.Columns)
                {
                    if ((column.ColumnName == "Id")
                        || (column.ColumnName == "IdGestorTipo")
                        || (column.ColumnName == "IdGestorRol")
                          || (column.ColumnName == "cve")
                          || (column.ColumnName == "Nombre")
                          || (column.ColumnName == "ApPaterno")
                          || (column.ColumnName == "ApMaterno")
                          || (column.ColumnName == "NumeroEmpleado")
                            || (column.ColumnName == "FechaCreacion")
                          )
                        html.Append("<th style='display:none'>");
                    else
                    {
                        html.Append("<th >");
                    }
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        if ((column.ColumnName == "Id")
                        || (column.ColumnName == "IdGestorTipo")
                        || (column.ColumnName == "IdGestorRol")
                          || (column.ColumnName == "cve")
                          || (column.ColumnName == "Nombre")
                          || (column.ColumnName == "ApPaterno")
                          || (column.ColumnName == "ApMaterno")
                          || (column.ColumnName == "NumeroEmpleado")
                            || (column.ColumnName == "FechaCreacion")
                          )
                            html.Append("<td style='display:none'>");
                        else
                        {
                            html.Append("<td>");
                        }
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");

                    }
                    html.Append("<td style='width: 80px !important;'><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false'><span class='glyphicon glyphicon-pencil' TITLE='Editar Contacto'></span></a></td>");
                    html.Append("<td style='width: 80px !important;'><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'><span class='glyphicon glyphicon-trash' TITLE='Eliminar Contacto'></span></a></td>");
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

        public Boolean Update()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("UPDATE gestores " +
                                             "   SET " +
                                             "     NumeroEmpleado = upper(@NumeroEmpleado) " +
                                             "     , Puesto = upper(@Puesto) " +
                                             "     , Nombre = upper(@Nombre) " +
                                             "     , ApPaterno = upper(@ApPaterno) " +
                                             "     , ApMaterno = upper(@ApMaterno) " +
                                             "     , Cve = upper(@Cve) " +
                                             "  WHERE   IdGestor = @Id ");

                SqlParameter[] sqlParameters = new SqlParameter[7];
                
                sqlParameters[0] = new SqlParameter("@NumeroEmpleado", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(strNumeroEmpleado);

                sqlParameters[1] = new SqlParameter("@Puesto", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(strPuesto);

                sqlParameters[2] = new SqlParameter("@Nombre", SqlDbType.NChar);
                sqlParameters[2].Value = Convert.ToString(strName);
                sqlParameters[3] = new SqlParameter("@ApPaterno", SqlDbType.NChar);
                sqlParameters[3].Value = Convert.ToString(strFirstName);
                sqlParameters[4] = new SqlParameter("@ApMaterno", SqlDbType.NChar);
                sqlParameters[4].Value = Convert.ToString(strLastName);

                sqlParameters[5] = new SqlParameter("@Cve", SqlDbType.NChar);
                sqlParameters[5].Value = Convert.ToString(strIniciales);

                sqlParameters[6] = new SqlParameter("@Id", SqlDbType.NChar);
                sqlParameters[6].Value = Convert.ToString(Id); 

                
                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Update";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        public Boolean Delete()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update gestores SET Activo = @intActivo, FechaEliminacion= getdate() where IdGestor= @strIdGestor ");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@intActivo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);                
                sqlParameters[1] = new SqlParameter("@strIdGestor", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(Id);
                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "Delete";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        public DataTable ExistsIniciales()
        {
            try
            {
                string query = string.Format("Select  count(1) existe  from gestores g join GestorRoles gr on g.IdGestorRol = gr.IdGestorRol and gr.IdGestorTipo = @tipo  where g.Activo = @Activo and   cve =  @Iniciales    ");
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@tipo", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(strPuesto);
                sqlParameters[1] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[1].Value = Convert.ToString(intActivo);
                sqlParameters[2] = new SqlParameter("@Iniciales", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(strIniciales);                
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