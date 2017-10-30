using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Medicion.Class.ADO;
using System.Text;
using Medicion.Class;
namespace Medicion.Class.Business
{
    public class clsElectricMeters : clsPropertiesElectricMeters
    {
        DataTable dtAllGroup;
        DataTable dtGroup;
        DataTable dtSerarchRPU;
        DataTable dtHistoricRPU;
        DataTable dtAgreement4rpu;
        public DataTable GetLoadingCharge()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("          SELECT RPU " +
                                                "            , PuntoCarga, RPU + ' -- ' + PuntoCarga as RPUPuntoCarga " +
                                                "            , Direccion [Dirección] " +
                                                "            , pc.IdTarifa, t.Tarifa " +
                                                "            , PorteoMaximo " +
                                                "            , pc.idGrupo, g.Grupo " +
                                                "            , pc.FechaCreacion " +
                                                "            , pc.FechaModificacion " +
                                                "            , pc.Activo " +
                                                "         FROM PuntosCarga pc with(nolock) " +
                                                "         JOIN Grupos g ON pc.IdGrupo = g.idGrupo " +
                                                "         JOIN Tarifas t   ON pc.IdTarifa = t.IdTarifa " +
                                                "        WHERE pc.Activo = @intActive  and pc.idGrupo = @strGroup   order by PuntoCarga ");
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@intActive", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@strGroup", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strGroup);
                con.dbConnection();
                dtGroup = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetGroup";
                clsError.LogWrite();
                
            }
            return dtGroup;
        }
        public DataTable GetAllGroup()
        {

            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("          SELECT pc.IdGrupo, g.Grupo " +
                                                "            , RPU + ' -- ' + PuntoCarga as RPUPuntoCarga " +
                                                "            , PuntoCarga AS[Punto de Carga] " +
                                                "            , RPU " +
                                                "            , Direccion [Dirección]" +
                                                "            , pc.IdTarifa, t.Tarifa " +
                                                "            , PorteoMaximo AS[Capacidad MW] " +
                                                "            , z.IdDIVISION, d.Division  [División] " +
                                                "            , pc.IdZONA, z.Zona " +
                                                "            , pc.FechaCreacion AS[Fecha Alta] " +
                                                "            , e.Estatus , pc.IdEstatus, pc.IdGestorComercial, pc.IdGestorMedicion " +
                                                "         FROM PuntosCarga pc with(nolock) " +
                                                "         JOIN Grupos g ON pc.IdGrupo = g.idGrupo " +
                                                "         JOIN Tarifas t on pc.IdTarifa = t.IdTarifa " +
                                                "         JOIN Zonas z on pc.IdZona = z.IdZona " +
                                                "         JOIN Divisiones d on z.IdDivision = d.IdDivision " +
                                                "          JOIN PuntosCargaEstatus e   on pc.IdEstatus = e.IdEstatus " +
                                                "        WHERE pc.Activo=@intActive   order by  2, 3"  );

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@intActive", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                
                con.dbConnection();
                dtAllGroup = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllGroup";
                clsError.LogWrite();

            }
            return dtAllGroup;

        }
        public DataTable GetAllDistinctGroup()
        {

            try
            {
                ConnectionDB con = new ConnectionDB();
                //string query = string.Format("select IdGrupo, grupo  from  grupos g where IdGrupo in (Select distinct IdGrupo from PuntosCarga  where Activo=@intActive ) order by 2  ");
                string query = string.Format("select distinct IdGrupo,upper( grupo +' -- '+ +'('+gt.Cve+')') grupo  from  grupos gp join Gestores gt  on gp.IdGMedicion = gt.IdGestor where gp.Activo=1 and (gp.IdGMedicion = @IdGMedicion or 0 = @IdGMedicion)  order by 2 ");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@IdGMedicion", SqlDbType.Int);
                sqlParameters[0].Value = IdGMedicion;

                con.dbConnection();
                dtAllGroup = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllGroup";
                clsError.LogWrite();

            }
            return dtAllGroup;

        }
        public DataTable SearchRPUbyGroup(string strGroup)
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format(" Select pc.IdGrupo, g.Grupo " +
                                                "            , RPU + ' -- ' + PuntoCarga as RPUPuntoCarga " +
                                                "            , PuntoCarga AS[Punto de Carga] " +
                                                "            , RPU " +
                                                "            , Direccion [Dirección]" +
                                                "            , pc.IdTarifa, t.Tarifa " +
                                                "            , PorteoMaximo AS[Capacidad MW] " +
                                                "            , z.IdDIVISION, d.Division  [División] " +
                                                "            , pc.IdZONA, z.Zona " +
                                                "            , pc.FechaCreacion AS[Fecha Alta] " +
                                                " ,e.Estatus, pc.IdEstatus, pc.IdGestorComercial, pc.IdGestorMedicion  " +
                                                "from PuntosCarga PC with(nolock) " +
                                                "JOIN Grupos g ON pc.IdGrupo = g.idGrupo " +
                                                "JOIN Tarifas t on pc.IdTarifa = t.IdTarifa " +
                                                "JOIN Zonas z on pc.IdZona = z.IdZona " +
                                                "JOIN Divisiones d on z.IdDivision = d.IdDivision " +
                                                "          JOIN PuntosCargaEstatus e   on pc.IdEstatus = e.IdEstatus " +
                                                "where PC.Activo=@intActive and  PC.IdGrupo=@strGroup");
                //query = "spBuscarRPU";
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@intActive", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@strGroup", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strGroup);
                con.dbConnection();
                dtSerarchRPU = con.executeSelectQuery(query, sqlParameters);

            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "SearchRPU";
                clsError.LogWrite();

            }
            return dtSerarchRPU;
        }
        public DataTable SearchRPU(string strRPU)
        {
            
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("Select  pc.IdGrupo, g.Grupo " +
                                                "            , RPU + ' -- ' + PuntoCarga as RPUPuntoCarga " +
                                                "            , PuntoCarga AS[Punto de Carga] " +
                                                "            , RPU " +
                                                "            , Direccion [Dirección]" +
                                                "            , pc.IdTarifa, t.Tarifa " +
                                                "            , PorteoMaximo AS[Capacidad MW] " +
                                                "            , z.IdDIVISION, d.Division [División] " +
                                                "            , pc.IdZONA, z.Zona " +
                                                "            , pc.FechaCreacion AS[Fecha Alta] " +
                                                " ,e.Estatus , pc.IdEstatus,pc.IdGestorComercial, pc.IdGestorMedicion,pc.ConPrelacion [Con Prelación] " +
                                            //                                            ", pc.IdEstatus, pc.IdGestorComercial, pc.IdGestorMedicion "  +
                                            " from PuntosCarga PC with(nolock) " +
                                            " JOIN Grupos g ON pc.IdGrupo = g.idGrupo " +
                                            " JOIN Tarifas t on pc.IdTarifa = t.IdTarifa " +
                                            " JOIN Zonas z on pc.IdZona = z.IdZona " +
                                            " JOIN Divisiones d on z.IdDivision = d.IdDivision " +
                                            "          JOIN PuntosCargaEstatus e   on pc.IdEstatus = e.IdEstatus " +
                                            " where PC.Activo=@intActive and  PC.RPU=@strRPU ");
                //query = "spBuscarRPU";
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@intActive", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@strRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strRPU);
                con.dbConnection();
                dtSerarchRPU = con.executeSelectQuery(query, sqlParameters);
               
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "SearchRPU";
                clsError.LogWrite();

            }
            return dtSerarchRPU;
        }
        public DataTable GetData4ParameterMeterds(string strRPU, Int16 intActive, int idParameterMeter)
        {
            
            try
            {
                dtAgreement4rpu = null;
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarDatosParamMedicion");
                //query = "spBuscarRPU";
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@intActivo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strRPU);
                sqlParameters[2] = new SqlParameter("@intIdParametroMedicion", SqlDbType.Int);
                sqlParameters[2].Value = Convert.ToString(idParameterMeter);
                con.dbConnection();
                dtAgreement4rpu = con.executeStoreProcedure(query, sqlParameters);

                
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAgreement4RPU";
                clsError.LogWrite();

            }
            return dtAgreement4rpu;
        }
        public DataTable GetAgreement4RPU(string strRPU, Int16 intActive) 
        {
            SqlDataReader drAgreement;
            try 
            {
                dtAgreement4rpu= null;
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarConveniosPorRPU");
                //query = "spBuscarRPU";
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@intActivo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strRPU);
                con.dbConnection();
                dtAgreement4rpu = con.executeStoreProcedure(query, sqlParameters);

                //if (drAgreement.HasRows) 
                //{ 
                //    dtAgreement4rpu = new DataTable();
                //    dtAgreement4rpu.Load(drAgreement);
                
                //}
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAgreement4RPU";
                clsError.LogWrite();

            }
            return dtAgreement4rpu;
        }

        public DataTable GetAgreement4RPUHistorico(string strRPU, Int16 intActive)
        {
            SqlDataReader drAgreement;
            try
            {
                dtAgreement4rpu = null;
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("spBuscarConveniosPorRPUHistorico");
                //query = "spBuscarRPU";
                SqlParameter[] sqlParameters = new SqlParameter[1];
                
                sqlParameters[0] = new SqlParameter("@chrRPU", SqlDbType.NVarChar);
                sqlParameters[0].Value = Convert.ToString(strRPU);

                con.dbConnection();
                dtAgreement4rpu = con.executeStoreProcedure(query, sqlParameters);

                //if (drAgreement.HasRows) 
                //{ 
                //    dtAgreement4rpu = new DataTable();
                //    dtAgreement4rpu.Load(drAgreement);

                //}
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAgreement4RPUHistorico";
                clsError.LogWrite();

            }
            return dtAgreement4rpu;
        }
        public DataTable SearchStatusRPU(string strRPU)
        {
            
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("Select top 1  isnull(M.ESTATUS,'') as ESTATUS from	 Medidor M with(nolock)  where M.Activo=@intActive and  M.RPU=@strRPU AND ISNULL(M.ESTATUS,'')<>'' ORDER BY M.FechaCreacion DESC");
                //query = "spBuscarRPU";
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@intActive", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);
                sqlParameters[1] = new SqlParameter("@strRPU", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(strRPU);
                con.dbConnection();
                dtSerarchRPU = con.executeSelectQuery(query, sqlParameters);
                
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "SearchRPU";
                clsError.LogWrite();

            }
            return dtSerarchRPU;
        }
        public DataTable GetContactsCFE(string strRPU) {
            Class.Business.clsContactCFE oclsCFE = new Class.Business.clsContactCFE();
            try 
            {

                dtSerarchRPU = null;
                dtSerarchRPU = oclsCFE.GetContactCFEByRPU(strRPU);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "SearchRPU";
                clsError.LogWrite();

            }
            return dtSerarchRPU;
        }
        public DataTable SearchByFilter() {

            if (strGroup == "-- TODOS --" 
                && (  ( strLoadingCharges == "-- TODOS --") 
                      || 
                      (strLoadingCharges == "" )
                   ) 
               )
            {
                dtSerarchRPU = GetAllGroup();
            }
            else if ((strGroup != "-- TODOS --" && strLoadingCharges == "-- TODOS --") || (strGroup != "-- TODOS --" && string.IsNullOrEmpty(strLoadingCharges)))
            {
                dtSerarchRPU = SearchRPUbyGroup(strGroup);
            }
            else
            {
                dtSerarchRPU = SearchRPU(strRPU);
            }
            return dtSerarchRPU;
        }

        public DataTable SearchByFilter(string strGrupo,   string strGestor )
        {

            int iGrupo;
            int iCentral;
            int iGestor;

            if (strGrupo == "-- TODOS --") iGrupo = 0; else iGrupo =  int.Parse( strGrupo);
            //if (strCentral == "-- TODOS --") iCentral = 0; else iCentral = int.Parse(strCentral);
            if (strGestor == "-- TODOS --") iGestor = 0; else iGestor = int.Parse(strGestor);

            dtSerarchRPU = GetAllGroup(iGrupo,  iGestor);
            
            return dtSerarchRPU;

        }



        /// <summary>
        /// Create table divison schema just columns
        /// </summary>
        /// <returns></returns>
        public StringBuilder CreateTableHTML()
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
                foreach (DataColumn column in dtElectricMeters.Columns)
                {
                    if ( (column.ColumnName == "RPUPuntoCarga")
                        || (column.ColumnName == "IdDivivion")      || (column.ColumnName == "IdDIVISION")
                        || (column.ColumnName == "IdZona")          || (column.ColumnName == "IdZONA")
                        || (column.ColumnName == "IdGestorComercial") || (column.ColumnName == "IdGestorMedicion")  
                        || (column.ColumnName == "IdEstatus")
                        || (column.ColumnName == "IdTarifa")
                        || (column.ColumnName == "IdGrupo")
                        )
                    {
                        html.Append("<th style='display:none;'>");
                    }
                    else
                    {
                        html.Append("<th>");
                        
                    }
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtElectricMeters.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtElectricMeters.Columns)
                    {
                        if (column.ColumnName == "Con Prelación")
                        {
                            if (row[18].ToString() == ""|| row[18].ToString() == "0")
                            {
                                html.Append("<td>");
                                html.Append("No");
                                html.Append("</td>");
                            }
                            else if (row[18].ToString() == "1")
                            {
                                html.Append("<td>");
                                html.Append("Si");
                                html.Append("</td>");
                            }

                        }            
                        else if ( (column.ColumnName == "RPUPuntoCarga")
                               || (column.ColumnName == "IdDivivion") || (column.ColumnName == "IdDIVISION")
                               || (column.ColumnName == "IdZona") || (column.ColumnName == "IdZONA")
                               || (column.ColumnName == "IdEstatus")
                               || (column.ColumnName == "IdTarifa")
                               || (column.ColumnName == "IdGrupo")
                               || (column.ColumnName == "IdGestorMedicion")
                               || (column.ColumnName == "IdGestorComercial")
                        )
                        {
                            html.Append("<td style='display:none;'>");
                            html.Append(row[column.ColumnName]);
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
                            strRPU =Convert.ToString(row[column.ColumnName]);
                            //if (!string.IsNullOrEmpty(strRPU)) {
                            //    oclsEncrypt.strData = strRPU;
                            //    strRPU = oclsEncrypt.EncryptData(); 
                            //}
                        }
                    }
                    html.Append("<td>");
                    html.Append("<a href='#' class='btn btn-warning btn-sm' data-toggle='modal' data-target='.bs-example-modal-lg' aria-label='Left Align' title='Contactos' data-placement='top'  style='width:17px; height:17px;'><img src='img/account-card-details.png'  width='15px' height='15px' style='margin-top: -14px; margin-left: -8px; ' ></a>");
                    html.Append("<p></p>");
                    html.Append("<a href='comunicaciones.aspx?rpu=" + strRPU + "' class='btn btn-info btn-sm' title='Comunicacion'  data-target='.bs-example-modal-lg' data-toggle='tooltip' data-placement='top' style='width:17px; height:17px;'><img src='img/access-point-network.png'  width='15px' height='15px' style='margin-top: -14px; margin-left: -8px; ' ></a>");
                    html.Append("</td>");
                    html.Append("<td>");
                    html.Append("<a href='medidores.aspx?rpu=" + strRPU + "' class='btn btn-success btn-sm' aria-label='Left Align' title='Medidores' data-toggle='tooltip' data-placement='top'  style='width:17px; height:17px;' ><img src='img/smartmeter.png'  width='15px' height='15px' style='margin-top: -14px; margin-left: -8px; '  ></a><p></p>");
                    html.Append("</td>");
                    //html.Append("<td>");
                    //html.Append("<a href='#' class='btn btn-info btn-sm' aria-label='Left Align' title='Eliminar' data-toggle='tooltip' data-placement='top'  ><span class='glyphicon glyphicon-trash btn-sm'></span></a>");
                    //html.Append("</td>");
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
        /// Create table divison schema just columns
        /// </summary>
        /// <returns></returns>
        public StringBuilder CreateTableHTML4Contacts(DataTable dtContact)
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
                foreach (DataColumn column in dtContact.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtContact.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtContact.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
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
                clsError.logModule = "CreateTableHTML";
                clsError.LogWrite();
            }

            return html;
        }
        public StringBuilder CreateTableHTML4Agreement(DataTable dtAgreements)
        {
            Class.Encrypt oclsEncrypt = new Class.Encrypt();
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {
                string strAgregar = string.Empty;
                string strrpu;
                html.Append(" <thead>");
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtAgreements.Columns)
                {
                   
                    if (column.ColumnName == "RPU")
                    {
                        //html.Append("Activo");
                    }
                    else {
                        html.Append("<th>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                    
                    
                }
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtAgreements.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtAgreements.Columns)
                    {

                        if (column.ColumnName == "RPU")
                        {
                            strrpu = Convert.ToString(row["RPU"]);
                            strAgregar = Convert.ToString(row["Convenio"]);
                            oclsEncrypt.strData = strAgregar;
                            strAgregar = oclsEncrypt.EncryptData();
                            if (!string.IsNullOrEmpty(strrpu))
                            {
                                
                               // html.Append("<td>");
                                //html.Append("<span class='glyphicon glyphicon-ok text-success text-center' aria-hidden='true'></span>");
                                ////html.Append("<input type='checkbox' class='chk' runat='server' id='" + strAgregar + "' value='' checked='checked' disabled>");

                                //html.Append("</td>");
                            }
                            else {
                                
                               // html.Append("<td>");
                                //html.Append("<input type='checkbox' class='chk' runat='server' id='" + strAgregar + "' value='' >");

                                //html.Append("</td>");
                            }
                        }
                        else if (column.ColumnName == "CARGA")
                        {
                            html.Append("<td>");
                            //html.Append("<input type='number' id='str" + strAgregar + "' value = '" + row[column.ColumnName] + "' class='clsnmbr' data-bv-integer-message='Campo solo numérico' readonly >");

                            html.Append("<span>" + row[column.ColumnName] + "</span>");


                            html.Append("</td>");
                        }
                        else
                        {
                            html.Append("<td>");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");
                        }
                    }
                    
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

        public DataTable GetAllGroup(int grupo,  int gestor)
        {

            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("          SELECT pc.IdGrupo, g.Grupo " +
                                                "            , RPU + ' -- ' + PuntoCarga as RPUPuntoCarga " +
                                                "            , PuntoCarga AS[Punto de Carga] " +
                                                "            , RPU " +
                                                "            , Direccion [Dirección]" +
                                                "            , pc.IdTarifa, t.Tarifa " +
                                                "            , PorteoMaximo AS[Capacidad MW] " +
                                                "            , z.IdDIVISION, d.Division  [División] " +
                                                "            , pc.IdZONA, z.Zona " +
                                                "            , pc.FechaCreacion AS[Fecha Alta] " +
                                                "            , e.Estatus , pc.IdEstatus, pc.IdGestorComercial, pc.IdGestorMedicion " +
                                                "         FROM PuntosCarga pc with(nolock) " +
                                                "         JOIN Grupos g ON pc.IdGrupo = g.idGrupo " +
                                                "         JOIN Tarifas t on pc.IdTarifa = t.IdTarifa " +
                                                "         JOIN Zonas z on pc.IdZona = z.IdZona " +
                                                "         JOIN Divisiones d on z.IdDivision = d.IdDivision " +
                                                "          JOIN PuntosCargaEstatus e   on pc.IdEstatus = e.IdEstatus " +
                                                "        WHERE pc.Activo=@intActive "+
                                                "              and ( pc.IdGestorMedicion = @gestor  or 0 = @gestor  ) " +
                                                "              and ( pc.IdGrupo = @grupo  or 0 = @grupo  ) " +
                                                " order by  2, 3");

                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@intActive", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(intActive);

                sqlParameters[1] = new SqlParameter("@gestor", SqlDbType.SmallInt);
                sqlParameters[1].Value = Convert.ToString(gestor);

                sqlParameters[2] = new SqlParameter("@grupo", SqlDbType.SmallInt);
                sqlParameters[2].Value = Convert.ToString(grupo);


                con.dbConnection();
                dtAllGroup = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAllGroup";
                clsError.LogWrite();

            }
            return dtAllGroup;

        }

    }
}