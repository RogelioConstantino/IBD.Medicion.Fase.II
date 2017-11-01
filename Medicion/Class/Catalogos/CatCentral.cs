
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
    public class CatCentral : PropertiesCentral
    {
        private ConnectionDB con = new ConnectionDB();
        DataTable AllCentral;
        DataTable dtExistsCentral;

        public object CodeCentral { get; private set; }

        public DataTable GetAllCentralPrelacion()
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format("select IdCentral, CveCentral [Código], upper(Central) [Descripción] " +
                                             "     , isnull((select sum(carga) " +
                                             "                  from Convenios co join PuntoCargaPorConvenio pcpc on pcpc.IdConvenio = co.IdConvenio and pcpc.Activo= 1 " +
                                             "                 where co.Activo = 1  and  co.IdCentral = c.IdCentral),0) Carga  " +
                                             "     , FechaCreacion [Fecha de creación] " +
                                             "     , OrdenPre " +
                                             "  from Centrales c " +
                                             "  where Activo = @Activo " +
                                             "Order By OrdenPre ");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                con.dbConnection();
                AllCentral = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllCentral";
                clsError.LogWrite();
            }
            return AllCentral;
        }

        public DataTable GetAllCentral()
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format("select IdCentral, CveCentral [Código], Central [Descripción] " +
                                             "    ,CONVERT(varchar, CAST(   isnull((select sum(carga)   " +
                                             "                          from Convenios co join PuntoCargaPorConvenio pcpc on pcpc.IdConvenio = co.IdConvenio and pcpc.Activo = 1  and co.IdEstatus = 2 " +
                                             "                          where co.Activo = 1  and  co.IdCentral = c.IdCentral),0)AS money), 1)  Carga  " +
                                             "  from Centrales c " + 
                                             "  where Activo = @Activo " +
                                             "Order By Central " );

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                con.dbConnection();
                AllCentral = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllCentral";
                clsError.LogWrite();
            }
            return AllCentral;
        }

        public DataTable CentralByCentral(int idcentral)
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format("select IdCentral,CveCentral,Central from centrales where IdCentral = @IdCENtral");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@IdCENtral", SqlDbType.Int);
                sqlParameters[0].Value = idcentral;
                con.dbConnection();
                AllCentral = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllCentral";
                clsError.LogWrite();
            }
            return AllCentral;
        }

        public DataTable ConveniosByIdCEntral(int IdCentral)
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format("select c.IdConvenio, c.Convenio, count( b.IdPuntoCarga)[Num de Cargas], sum(b.Carga) [Carga total],ce.Descripcion [Estatus] from Convenios c join [PuntoCargaPorConvenio] b on c.IdConvenio = b.IdConvenio join conveniosEstatus ce on c.IdEstatus = ce.IdEstatus where c.IdCentral = @IdCentral and c.Activo = 1 group by c.IdConvenio, c.Convenio, ce.Descripcion");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@IdCentral", SqlDbType.Int);
                sqlParameters[0].Value = IdCentral;
                con.dbConnection();
                AllCentral = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllCentral";
                clsError.LogWrite();
            }
            return AllCentral;
        }

        public DataTable ConveniosByIdConvenio(int IdConvenio)
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format(" select pc.rpu [RPU],pc.PuntoCarga [Punto de Carga],pc.DemandaContratada [Demanda Contratada] from   [PuntoCargaPorConvenio]  pcpc join PuntosCarga pc on pcpc.idPuntoCarga = pc.idPuntoCarga where IdConvenio = @IdConvenio ");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@IdConvenio", SqlDbType.Int);
                sqlParameters[0].Value = IdConvenio;
                con.dbConnection();
                AllCentral = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllCentral";
                clsError.LogWrite();
            }
            return AllCentral;
        }
        public DataTable GetAllConvenio(string central)
        {
            //DataTable AllCentral;
            String FullName = string.Empty;
            try
            {
                string query = string.Format("select  cv.IdConvenio,cv.Convenio + ' ' + cve.Descripcion as Convenio from Convenios cv inner join Centrales ct on cv.IdCentral = ct.IdCentral join ConveniosEstatus cve on cv.IdEstatus = cve.IdEstatus where ct.IdCentral =@Central and cv.Activo = 1");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Central", SqlDbType.Int);
                sqlParameters[0].Value = Convert.ToInt32(central);
                con.dbConnection();
                AllCentral = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllCentral";
                clsError.LogWrite();
            }
            return AllCentral;
        }
        public DataTable ExistsCentral()
        {
            
            try
            {
                string query = string.Format("select count(1) existe   from Centrales where Activo = @Activo and ( CveCentral = @CodCentral or Central = @Central )");
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);

                sqlParameters[1] = new SqlParameter("@CodCentral", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(CodCentral);

                sqlParameters[2] = new SqlParameter("@Central", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(Central);
                
                con.dbConnection();
                dtExistsCentral = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistsCentral";
                clsError.LogWrite();
            }
            return dtExistsCentral;
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
                foreach (DataColumn column in dtCentral.Columns)
                {
                    if (id == 0)
                        html.Append("<th style='display:none'>");
                    else
                    {
                        if (column.ColumnName == "Fecha de creación")
                            html.Append("<th style='width: 220px !important;'>");
                        else if (column.ColumnName == "Fecha de Creación")
                            html.Append("<th style='width: 220px !important;'>");
                        else if (column.ColumnName == "Carga")
                            html.Append("<th style='width: 180px !important;'>");
                        else if (column.ColumnName == "Código")
                            html.Append("<th style='width: 150px !important;'>"); 
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
                //Building the Data rows
                                
                foreach (DataRow row in dtCentral.Rows)
                {
                    int id1 = 0;
                    html.Append("<tr>");
                    foreach (DataColumn column in dtCentral.Columns)
                    {                        
                        html.Append("<td " + ((id1 == 0) ? "style='display:none'" : ((id1 == 1) ? "style='width: 130px !important;'" : "")) + ">");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                        id1++;
                    }
                    html.Append("<td style='width: 80px !important;'><a href='#' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#edit' contenteditable='false'><span class='glyphicon glyphicon-pencil' TITLE='Editar Central'></span></a></td>");
                    html.Append("<td style='width: 80px !important;'><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'><span class='glyphicon glyphicon-trash' title='Eliminar Central' ></span></a></td>");
                    html.Append("</tr>");
                }
                html.Append("</tbody>");

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CreateTableHTMLCentral";
                clsError.LogWrite();
            }

            return html;
        }

        public Boolean UpdateCentral()
        {
            Boolean msg = true;
            try
            { 
            
                string query = string.Format("Update Centrales SET Central = upper(@Central), CveCentral = upper(@CodCentral) where Activo = @Activo and IdCentral= upper(@IdCentral) ");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@Central", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(Central);

                sqlParameters[1] = new SqlParameter("@CodCentral", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(this.CodCentral);

                sqlParameters[2] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[2].Value = Convert.ToString(Activo);

                sqlParameters[3] = new SqlParameter("@IdCentral", SqlDbType.Int);
                sqlParameters[3].Value = Convert.ToString(idCentral);

                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "UpdateCentral";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }
        public Boolean DeleteCentral()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("Update Centrales SET Activo = @Activo, FechaEliminacion= getdate() where IdCentral= @IdCentral ");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);
                sqlParameters[1] = new SqlParameter("@IdCentral", SqlDbType.Int);
                sqlParameters[1].Value = Convert.ToString(idCentral);
                con.dbConnection();
                msg = con.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "DeleteCentral";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }
        public Boolean NewCentral()
        {
            Boolean msg = true;
            try
            {
                string query = string.Format("INSERT INTO Centrales(CveCentral,Central,FechaCreacion)Values(upper(@CodeCentral), upper(@Central), GETDATE() )");
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@CodeCentral", SqlDbType.NChar);
                sqlParameters[0].Value = Convert.ToString(CodCentral);

                sqlParameters[1] = new SqlParameter("@Central", SqlDbType.NChar);
                sqlParameters[1].Value = Convert.ToString(Central);

                con.dbConnection();
                msg = con.executeInsertQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "NewCentral";
                clsError.LogWrite();
                msg = false;
            }

            return msg;
        }

        public DataTable ExistsCentralID()
        {

            try
            {
                string query = string.Format("select count(1) existe   from Centrales where Activo = @Activo and ( CveCentral = @CodCentral or Central = @Central ) and IdCentral <>  @ID");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);

                sqlParameters[1] = new SqlParameter("@CodCentral", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(CodCentral);

                sqlParameters[2] = new SqlParameter("@Central", SqlDbType.NVarChar);
                sqlParameters[2].Value = Convert.ToString(Central);

                sqlParameters[3] = new SqlParameter("@ID", SqlDbType.NVarChar);
                sqlParameters[3].Value = Convert.ToString(idCentral); 

                con.dbConnection();
                dtExistsCentral = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistsCentral";
                clsError.LogWrite();
            }
            return dtExistsCentral;
        }

    }
}

    