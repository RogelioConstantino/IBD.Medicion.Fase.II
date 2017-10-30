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
    public class CatGroup: PropertiesGroup
    {
        private ConnectionDB con = new ConnectionDB();
        DataTable AllGroups;
        DataTable dtExistsGroup;

        public DataTable GetAllGroup(int GMedicion,int GComercial)
        {
            String FullName = string.Empty;
            
            try
            {
                string query = string.Format("spBuscarGruposByGestores");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@IdGMedicion", SqlDbType.Int);
                sqlParameters[0].Value = GMedicion;
                sqlParameters[1] = new SqlParameter("@IdGComercial", SqlDbType.Int);
                sqlParameters[1].Value = GComercial;
                con.dbConnection();
                AllGroups = con.executeStoreProcedure(query, sqlParameters);                
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetRPU";
                clsError.LogWrite();
            }

            return AllGroups;
        }
        public DataTable GetAllGroup()
        {
            String FullName = string.Empty;

            try
            {
                string query = string.Format("select  IdGrupo Id , Grupo [Descripción], convert(varchar(10), FechaInicioOperaciones,126 ) as  [Inicio de Operaciones], FechaCreacion  [Fecha de Creación]  from Grupos where Activo = @Activo Order By Grupo");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                con.dbConnection();
                AllGroups = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetRPU";
                clsError.LogWrite();
            }

            return AllGroups;
        }
        public DataTable ExistsGroup() {
            
            
            try
            {
                string query = string.Format("select  count(1) existe   from Grupos where Activo = @Activo and Grupo=@Group");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                sqlParameters[1] = new SqlParameter("@Group", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(Grupo);
                con.dbConnection();
                dtExistsGroup = con.executeSelectQuery(query, sqlParameters);
                
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetRPU";
                clsError.LogWrite();
            }
            return dtExistsGroup;
        }
        public DataTable ExistsGroupID()
        {
            try
            {
                string query = string.Format("select  count(1) existe   from Grupos where Activo = @Activo and Grupo=upper(@Group) and IdGrupo <> @IdGrupo ");
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                sqlParameters[1] = new SqlParameter("@Group", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(Grupo);
                sqlParameters[2] = new SqlParameter("@IdGrupo", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(idGrupo);
                con.dbConnection();
                dtExistsGroup = con.executeSelectQuery(query, sqlParameters);

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetRPU";
                clsError.LogWrite();
            }
            return dtExistsGroup;
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
                int id = 0;
                foreach (DataColumn column in dtGroup.Columns)
                {
                    if (id == 0)
                        html.Append("<th style='display:none'>");
                    else
                    {
                        if ( ( column.ColumnName.ToUpper() == "FECHA DE CREACIÓN")                           
                          || (column.ColumnName.ToUpper() == "INICIO DE OPERACIONES")                          
                          )
                            html.Append("<th style='width: 220px !important;'>");
                        else
                            html.Append("<th style='width: auto !important;'>");
                    }
                    id++;

                    // html.Append("<th class='text-uppercase'>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                   
                }
                html.Append("<th ><th>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.


                foreach (DataRow row in dtGroup.Rows)
                {
                    int id1 = 0;
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGroup.Columns)
                    {
                        html.Append("<td " + ((id1 == 0) ? "style='display:none'" : ((id1 == 1) ? "" : "")) + ">");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                        id1++;
                    }
                    html.Append("<td style='width: 80px !important;'><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false'><span class='glyphicon glyphicon-pencil' TITLE='Editar grupo'></span></a></td>");
                    html.Append("<td style='width: 80px !important;'><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'><span class='glyphicon glyphicon-trash' TITLE='Eliminar grupo'></span></a></td>");
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

        public Boolean UpdateGroup() 
        {
            Boolean msg = true;
            try 
            {
                //string query = string.Format("Update Grupos SET Grupo = upper(@Grupo), FechaInicioOperaciones = @InicioOperaciones, IdGMedicion = @IdGMedicion, IdGComercial = @IdGComercial where Activo = @Activo and IdGrupo= @IdGrupo");
                string query = string.Format("spUpdateGrupo");

                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@Grupo", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(Grupo);

                sqlParameters[1] = new SqlParameter("@InicioOperaciones", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(InicioOperaciones);


                sqlParameters[2] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[2].Value = Convert.ToInt32(Activo);

                sqlParameters[3] = new SqlParameter("@IdGrupo", SqlDbType.Int);
                sqlParameters[3].Value = Convert.ToInt32(idGrupo);

                sqlParameters[4] = new SqlParameter("@IdGMedicion", SqlDbType.Int);
                sqlParameters[4].Value = IdMed;
                sqlParameters[5] = new SqlParameter("@IdGComercial", SqlDbType.Int);
                sqlParameters[5].Value = IdComer;

                con.dbConnection();
                 con.executeStoreProcedure(query, sqlParameters);  
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
        public Boolean DeleteGroup()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update Grupos SET Activo = @Activo, FechaEliminacion= getdate() where IdGrupo= @IdGrupo ");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                sqlParameters[1] = new SqlParameter("@IdGrupo", SqlDbType.Int);
                sqlParameters[1].Value = Convert.ToString(idGrupo);
                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
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
        public Boolean NewGroup()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("INSERT INTO GRUPOs(Grupo,FechaInicioOperaciones, FechaCreacion,FechaEliminacion,Activo,IdGMedicion,IdGComercial) Values(upper(@Grupo), @FechaInicioOperaciones,  GETDATE(),null,1,@IdGMedicion,@IdGComercial)");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@Grupo", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(Grupo);

                sqlParameters[1] = new SqlParameter("@FechaInicioOperaciones", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(InicioOperaciones);
                sqlParameters[2] = new SqlParameter("@IdGMedicion", SqlDbType.Int);
                sqlParameters[2].Value = IdMed;
                sqlParameters[3] = new SqlParameter("@IdGComercial", SqlDbType.Int);
                sqlParameters[3].Value = IdComer;

                con.dbConnection();
                msg = con.executeInsertQuery(query, sqlParameters);
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
    }
}