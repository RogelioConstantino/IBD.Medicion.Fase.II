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
    public class CatContactCFE :PropertiesContactCFE
    {
        private ConnectionDB con = new ConnectionDB();
        DataTable AllDivision;
        DataTable Allzone;
        /// <summary>
        /// Obtain all contacts from table ContactoCFE
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllContactCFE()
        {
            String FullName = string.Empty;

            try
            {
                //string query = string.Format("select Titulo, cfe.Division, cfe.Zona, Correo, Nombre, ApPaterno as [Apellido Paterno], ApMaterno as [Apellido Materno], TelTrabajo as [Tel. Trabajo], Extencion as Extension, Celular, cfe.FechaCreacion as Alta from ContactoCFE CFE inner join Zona Z on CFE.Zona = z.Zona where cfe.Activo = @Activo  and z.Activo= @Activo  Order By Nombre");
                string query = string.Format("select IdContactoCFE Id,  z.IDDivision IdDivision, d.Division [División],   c.IDZona IdZona,  z.Zona " +
                                             "     , Titulo + ' ' + Nombre + ' ' + ApPaterno + ' ' + ApMaterno as [Nombre Completo] " +
                                             ", Titulo, Nombre, ApPaterno, ApMaterno " +
                                             ", Puesto , Correo " +
                                             ", TelTrabajo[Teléfono de Oficina], Extencion[Ext] " +
                                             ", Celular, c.FechaCreacion [Fecha de Creación]  " +
                                             "  FROM ContactosCFE c" +
                                             "    inner join Zonas Z		on c.IDZona = z.IdZona    " +
                                             "    inner join divisiones d  on z.IDDivision = d.IdDivision   " +
                                             " WHERE c.Activo = @Activo   " +                                             
                                             " ORDER BY Nombre ");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                con.dbConnection();
                AllDivision = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllContactCFE";
                clsError.LogWrite();
            }

            return AllDivision;
        }

        public DataTable GetAllContactCFEbyRPU()
        {
            try
            {
                string query = string.Format("spBuscarContactoRPU");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@intActive", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = strRPU;
                con.dbConnection();
                AllDivision = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllContactCFE";
                clsError.LogWrite();
            }
            return AllDivision;
        }
        public Boolean NewContactCFE()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("INSERT INTO ContactosCFE(Titulo, IdZona, Correo, Nombre, ApPaterno, ApMaterno, TelTrabajo, Extencion, Celular, Puesto, FechaCreacion )" +
                                             "Values(upper(@strTitle),@strZone,upper(@strEmail),upper(@strName),upper(@strFirstName),upper(@strLastName),@strWorkTel,@strExt,@strCel,upper(@strPuesto),GETDATE())");
                SqlParameter[] sqlParameters = new SqlParameter[11];

                sqlParameters[0] = new SqlParameter("@strTitle", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(strTitle);
                sqlParameters[1] = new SqlParameter("@strDivision", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(strDivision);
                sqlParameters[2] = new SqlParameter("@strZone", SqlDbType.NChar);
                sqlParameters[2].Value = Convert.ToString(strZone);
                sqlParameters[3] = new SqlParameter("@strEmail", SqlDbType.NChar);
                sqlParameters[3].Value = Convert.ToString(strEmail);
                sqlParameters[4] = new SqlParameter("@strName", SqlDbType.NChar);
                sqlParameters[4].Value = Convert.ToString(strName);
                sqlParameters[5] = new SqlParameter("@strFirstName", SqlDbType.NChar);
                sqlParameters[5].Value = Convert.ToString(strFirstName);
                sqlParameters[6] = new SqlParameter("@strLastName", SqlDbType.NChar);
                sqlParameters[6].Value = Convert.ToString(strLastName);
                sqlParameters[7] = new SqlParameter("@strWorkTel", SqlDbType.NChar);
                sqlParameters[7].Value = Convert.ToString(strWorkTel);
                sqlParameters[8] = new SqlParameter("@strExt", SqlDbType.NChar);
                sqlParameters[8].Value = Convert.ToString(strExt);
                sqlParameters[9] = new SqlParameter("@strCel", SqlDbType.NChar);
                sqlParameters[9].Value = Convert.ToString(strCel);

                sqlParameters[10] = new SqlParameter("@strPuesto", SqlDbType.NChar);
                sqlParameters[10].Value = Convert.ToString(strPuesto);


                con.dbConnection();
                msg = con.executeInsertQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "NewContactCFE";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        public DataTable GetAllZoneByDivision()
        {
            
            try
            {
                string query = string.Format("select IdContactoCFE Id,  z.IDDivision IdDivision, d.Division [División],  z.Zona , c.Titulo, c.Nombre, c.ApPaterno as [Apellido Paterno], c.ApMaterno as [Apellido Materno] " +
" , Puesto, c.TelTrabajo as [Tel.Trabajo], c.Extencion as [Extensión], c.Celular, c.Correo, c.FechaCreacion[Fecha de Creación]   " +
                                            "from ContactosCFE c " +
                                            " inner  join Zonas Z     on c.IDZona = z.IdZona " +
                                            " inner  join divisiones d  on z.IDDivision = d.IdDivision " +
                                            "where c.Activo = @Activo and z.idDivision = @strDivision ");

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

        public DataTable GetAllZoneByDivisionAndZone()
        {

            try
            {
                string query = string.Format("select IdContactoCFE Id,  z.IDDivision IdDivision, d.Division [División],  z.Zona " +
                                             ", c.Titulo, c.Nombre, c.ApPaterno as [Apellido Paterno], c.ApMaterno as [Apellido Materno] , Puesto" +
                                             ", c.TelTrabajo as [Tel. Trabajo], c.Extencion as [Extensión], c.Celular, c.Correo, c.FechaCreacion [Fecha de Creación]  " +
                                             "from ContactosCFE c " +
                                             " inner join Zonas Z		on c.IDZona = z.IdZona  " +
                                             "    inner join divisiones d  on z.IDDivision = d.IdDivision   " +
                                             "where c.Activo = @Activo and c.IdZona = @strZone " + 
                                             "Order By z.Zona");  //and Division=@strDivision  //Division, 

                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                sqlParameters[1] = new SqlParameter("@strDivision", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strDivision);
                sqlParameters[2] = new SqlParameter("@strZone", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(strZone);
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
                foreach (DataColumn column in dtContactCFE.Columns)
                {
                        
                    if ((column.ColumnName == "Id")
                        || (column.ColumnName == "IdDivision")
                        || (column.ColumnName == "IdZona")
                        || (column.ColumnName == "Titulo")
                        || (column.ColumnName == "cve")
                          || (column.ColumnName == "Nombre")
                          || (column.ColumnName == "ApPaterno")
                          || (column.ColumnName == "ApMaterno")
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
                foreach (DataRow row in dtContactCFE.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtContactCFE.Columns)
                    {

                        if ((column.ColumnName == "Id")
                            || (column.ColumnName == "IdDivision")
                            || (column.ColumnName == "IdZona")
                            || (column.ColumnName == "Titulo")
                            || (column.ColumnName == "cve")
                            || (column.ColumnName == "Nombre")
                            || (column.ColumnName == "ApPaterno")
                            || (column.ColumnName == "ApMaterno")
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


        /// <summary>
        /// Update Contact CFE into the database
        /// </summary>
        /// <returns></returns>
        public Boolean UpdateContactoCFE()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update ContactosCFE "
                                            + "SET Titulo = upper(@strTitle), Correo=upper(@strEmail), Nombre=upper(@strName), ApPaterno=upper(@strFirstName), ApMaterno=upper(@strLastName), "
                                            + " TelTrabajo=@strWorkTel, Extencion=@strExt, Celular=@strCel ,  Puesto = upper(@strPuesto) "
                                            + "where Activo = @intActivo and IdContactoCFE=@strID ");  // and IdDivision= @IdDivision
                SqlParameter[] sqlParameters = new SqlParameter[11];

                // @strTitle ,@strEmail , @strName , @strFir  @strExt 

                sqlParameters[0] = new SqlParameter("@strTitle", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(strTitle);
                //sqlParameters[1] = new SqlParameter("@strDivision", SqlDbType.NChar);
                //sqlParameters[1].Value = Convert.ToString(strDivision);
                //sqlParameters[2] = new SqlParameter("@strZone", SqlDbType.NChar);
                //sqlParameters[2].Value = Convert.ToString(strZone);
                sqlParameters[1] = new SqlParameter("@strEmail", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(strEmail);
                sqlParameters[2] = new SqlParameter("@strName", SqlDbType.NChar);
                sqlParameters[2].Value = Convert.ToString(strName);
                sqlParameters[3] = new SqlParameter("@strFirstName", SqlDbType.NChar);
                sqlParameters[3].Value = Convert.ToString(strFirstName);
                sqlParameters[4] = new SqlParameter("@strLastName", SqlDbType.NChar);
                sqlParameters[4].Value = Convert.ToString(strLastName);
                sqlParameters[5] = new SqlParameter("@strWorkTel", SqlDbType.NChar);
                sqlParameters[5].Value = Convert.ToString(strWorkTel);
                sqlParameters[6] = new SqlParameter("@strExt", SqlDbType.NChar);
                sqlParameters[6].Value = Convert.ToString(strExt);
                sqlParameters[7] = new SqlParameter("@strCel", SqlDbType.NChar);
                sqlParameters[7].Value = Convert.ToString(strCel);
                sqlParameters[8] = new SqlParameter("@strPuesto", SqlDbType.NChar);
                sqlParameters[8].Value = Convert.ToString(strPuesto); 
                sqlParameters[9] = new SqlParameter("@intActivo", SqlDbType.Int);
                sqlParameters[9].Value = Convert.ToString(intActivo);
                sqlParameters[10] = new SqlParameter("@strID", SqlDbType.Int);
                sqlParameters[10].Value = Convert.ToString(strID);

                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateContactoCFE";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        public Boolean DeleteContactoCFE()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update ContactosCFE SET Activo = @intActivo, FechaEliminacion= getdate() where  IdContactoCFE=@strID ");//Division= @strDivision and Zona = @strZone and Correo = @strEmail
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@intActivo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);
                sqlParameters[1] = new SqlParameter("@strID", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strID);
                                
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


        public DataTable GetByRUP(string strRUP)
        {
            try
            {
                string query = string.Format("select Titulo + Nombre + ' ' + ApPaterno + ' ' + ApMaterno as Nombre, TelTrabajo [Teléfono], Extencion [Extensión], Celular, Correo  [Correo Electrónico]  from ContactosCFE c      join PuntosCarga p on p.IdZona = c.IdZona    where c.Activo = 1    and  p.RPU  like '%'+@RUP+'%'  Order By 2 ");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@RUP", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(strRUP);
                con.dbConnection();
                Allzone = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetByRUP";
                clsError.LogWrite();
            }

            return Allzone;
        }

        public DataTable GetAllZoneByDivisionAndCVe()
        {
            try
            {
                string query = string.Format("SELECT  count(1) existe   " +
                                            "FROM ContactosCFE c " +
                                            "WHERE " +
                                            "      c.Activo = @Activo " +
                                            "and(" +
                                            "        (" +
                                            "                rtrim(ltrim(Nombre)) = upper(@strName) " +
                                            "          and   rtrim(ltrim(ApPaterno)) = upper(@strFirstName) " +
                                            "          and   rtrim(ltrim(ApMaterno)) = upper(@strLastName) " +
                                            "        ) " +
                                            "    or   rtrim(ltrim(Correo)) = upper(@strEmail) " +
                                            "    ) " +
                                            "");
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);

                sqlParameters[1] = new SqlParameter("@strName", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(strName);

                sqlParameters[2] = new SqlParameter("@strFirstName", SqlDbType.NChar);
                sqlParameters[2].Value = Convert.ToString(strFirstName);

                sqlParameters[3] = new SqlParameter("@strLastName", SqlDbType.NChar);
                sqlParameters[3].Value = Convert.ToString(strLastName);

                sqlParameters[4] = new SqlParameter("@strEmail", SqlDbType.NChar);
                sqlParameters[4].Value = Convert.ToString(strEmail);
                
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

        public DataTable GetAllZoneByDivisionAndCVeID()
        {
            try
            {
                string query = string.Format("SELECT  count(1) existe   " +
                                            "FROM ContactosCFE c " +
                                            "WHERE " +
                                            "      c.Activo = @Activo " +
                                            "and(" +
                                            "        (" +
                                            "              Nombre = upper(@strName) " +
                                            "          and ApPaterno = upper(@strFirstName) " +
                                            "          and ApMaterno = upper(@strLastName) " +
                                            "        ) " +
                                            "    or Correo = upper(@strEmail) " +
                                            "    ) " +
                                            "and IdContactoCFE  <> @strIdContac");
                SqlParameter[] sqlParameters = new SqlParameter[6];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActivo);

                sqlParameters[1] = new SqlParameter("@strName", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(strName);

                sqlParameters[2] = new SqlParameter("@strFirstName", SqlDbType.NChar);
                sqlParameters[2].Value = Convert.ToString(strFirstName);

                sqlParameters[3] = new SqlParameter("@strLastName", SqlDbType.NChar);
                sqlParameters[3].Value = Convert.ToString(strLastName);

                sqlParameters[4] = new SqlParameter("@strEmail", SqlDbType.NChar);
                sqlParameters[4].Value = Convert.ToString(strEmail);

                sqlParameters[5] = new SqlParameter("@strIdContac", SqlDbType.NChar);
                sqlParameters[5].Value = Convert.ToString(strID);

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

        public StringBuilder CreateTableHTMLSimple()
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append("<div class='table-responsive'>");
                html.Append("<table class='table table-hover table-bordred table-striped table-bordered'>");
                html.Append("<thead>");

                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtContactCFE.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtContactCFE.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtContactCFE.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
                html.Append("</tbody>");
                html.Append("</table> ");
                html.Append("</div > ");
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