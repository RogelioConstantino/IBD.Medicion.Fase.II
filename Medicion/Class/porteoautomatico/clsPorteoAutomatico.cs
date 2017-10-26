using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Medicion.Class.ADO;
using Medicion.Class.LogError;
using Medicion.Class.Business;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Medicion.Class.porteoautomatico
{
    public class clsPorteoAutomatico :clsPropiertiesPorteoAutomatico
    {
        private ConnectionDB con = new ConnectionDB();

        public StringBuilder RPU()
        {
            ADO.ConnectionDB cn = new ConnectionDB();
            String RPU = System.String.Empty;
            Boolean bRespostExistsRPU = false;

            strRPUrepeated = new StringBuilder();
                        
            clsDivision oclsDivision = new clsDivision();
            clsZone oclsZone = new clsZone();

            clsGrupo oclsGroup = new clsGrupo();

            clsTarifa oclsTarifa = new clsTarifa();
            clsGestores oclsGestor = new clsGestores() ;

            Boolean bexistgroup = false;
            Boolean bExistZone = false;
            Boolean bExistDivision = false;

            Boolean bExistTarifa = false;
            Boolean bExistGestorComercial = false;
            Boolean bExistGestorMedicion = false;

            int iNewConvenio = 0;

            int newRup = 0;

            DataTable dtConvenio = new DataTable("Convenio");
            dtConvenio = InsertConvenio();
            if (dtConvenio.Rows.Count > 0)
            {
                iNewConvenio = int.Parse(dtConvenio.Rows[0][0].ToString());
                intConvenio = iNewConvenio;

                foreach (DataRow drChargeShiping in dtResult.Rows)
                {
                    try
                    {
                        //Select Query
                        String strRPU = String.Empty;
                        strRPU = Convert.ToString(drChargeShiping[2]);
                        strRPU = strRPU.Replace(" ", "");                        
                        strServiceCFE = strRPU;
                        RPU = GetRPU();

                        bExistTarifa = oclsTarifa.ExistTarifa(  Convert.ToString(drChargeShiping[4])   );

                        bexistgroup = oclsGroup.ExistGroup(Convert.ToString(drChargeShiping[6]));
                        bExistDivision = oclsDivision.ExistDivision(   Convert.ToString(drChargeShiping[8])  );
                        bExistZone = oclsZone.ExistZonaCve ( Convert.ToString(drChargeShiping[8]), Convert.ToString(drChargeShiping[7]), Convert.ToString(drChargeShiping[9]));

                        bExistGestorMedicion = oclsGestor.ExistIniciales(Convert.ToString(drChargeShiping[11]),"2");
                        bExistGestorComercial = oclsGestor.ExistIniciales(Convert.ToString(drChargeShiping[12]), "1");

                        if (bExistTarifa){
                            if (bexistgroup){
                                        if (bExistDivision){
                                                //if (bExistZone){
                                                        if (bExistGestorMedicion){
                                                                if (bExistGestorComercial){
                                                                            if (!string.IsNullOrEmpty(strServiceCFE)){
                                                                                strDivision = Convert.ToString(drChargeShiping[8]);
                                                                                strZona = Convert.ToString(drChargeShiping[9]);
                                                                                if (!bExistZone)
                                                                                {
                                                                              //      oclsZone.NewZoneByDiv(Convert.ToString(drChargeShiping[7]).Substring(0, 2), strZona, strDivision, strDivision);
                                                                                }
                                                                                strLoadPoint = Convert.ToString(drChargeShiping[1]);

                                                                                strAddressPoint = Convert.ToString(drChargeShiping[3]);
                                                                                strRate = Convert.ToString(drChargeShiping[4]);
                                                                                dblMaxShipping = Convert.ToDouble(drChargeShiping[5]);
                                                                                strGroup = Convert.ToString(drChargeShiping[6]);
                                                                                strCta = Convert.ToString(drChargeShiping[7]);
                                                                               
                                                                                dblDemanda = Convert.ToDouble("0"+drChargeShiping[10]);
                                                                                strGestorComercial = Convert.ToString(drChargeShiping[11]);
                                                                                strGestorMedicion = Convert.ToString(drChargeShiping[12]);
                                                //se asignan los campos nuevos
                                                                            if (Convert.ToString(drChargeShiping[13]) != "")
                                                                            {strIdEstatusOferta = Convert.ToString(drChargeShiping[13]);
                                                                            }else { strRPUrepeated.Append("El Estatus Oferta del RPU: " + strServiceCFE + " Contiene datos incorrectos!" + "<br>"); }
                                                                            if (Convert.ToString(drChargeShiping[14]) != "" && Convert.ToUInt32(drChargeShiping[14]) >= 0 && Convert.ToUInt32(drChargeShiping[14]) <= 1)
                                                                            {strConPrelacion = Convert.ToInt32(drChargeShiping[14] );
                                                                            }else{strRPUrepeated.Append("El campo Esta en prelacion del RPU: " + strServiceCFE + " Contiene datos incorrectos!" + "<br>"); }
                                                                            if (Convert.ToString(drChargeShiping[15]) != "" && Convert.ToUInt32(drChargeShiping[15]) >= 0 && Convert.ToUInt32(drChargeShiping[15]) <=1)
                                                                            {strFirmado = Convert.ToInt32(drChargeShiping[15]);
                                                                            }else { strRPUrepeated.Append("El campo Firmado o prospecto del RPU: " + strServiceCFE + " Contiene datos incorrectos!" + "<br>"); }  
                                                                            DataTable DtRup = new DataTable("Convenio");
                                                                                DtRup = InsertRPU();
                                                                                if (dtConvenio.Rows.Count > 0)
                                                                                {newRup = int.Parse(dtConvenio.Rows[0][0].ToString());}
                                                                            }
                                                                    }else{strRPUrepeated.Append("No existe el Gestor Comercial: " + Convert.ToString(drChargeShiping[12]).ToUpper() + " del RPU: " + strServiceCFE + "<br>");}
                                                        }else{strRPUrepeated.Append("No existe el Gestor Medición: " + Convert.ToString(drChargeShiping[11]).ToUpper() + " del RPU: " + strServiceCFE + "<br>");}
                                                //}else{strRPUrepeated.Append("No existe la Zona " + Convert.ToString(drChargeShiping[7]).ToUpper() + " del RPU: " + strServiceCFE + "<br>");}
                                        }else{strRPUrepeated.Append("No existe la División: " + Convert.ToString(drChargeShiping[8]).ToUpper() + " del RPU: " + strServiceCFE + "<br>");}
                                }else{strRPUrepeated.Append("No existe el Grupo: " + Convert.ToString(drChargeShiping[6]).ToUpper() + " del RPU: " + strServiceCFE + "<br>");}
                        }else{strRPUrepeated.Append("No existe la Tarifa: " + Convert.ToString(drChargeShiping[4]).ToUpper() + " del RPU: " + strServiceCFE + "<br>");}

                    }
                    catch (Exception ex)
                    {
                        LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                        clsError.logMessage = ex.ToString();
                        clsError.logModule = "RPU";
                        clsError.LogWrite();
                  
                    }

                }
            }
            if (strRPUrepeated.Length > 0)
            {

                DataTable dtR = new DataTable("dtres");
                dtR = EliminaCarga();
                int iRes = 0;

                if (dtR.Rows.Count > 0)
                {
                    iRes = int.Parse(dtR.Rows[0][0].ToString());
                }

            }

            return strRPUrepeated;
        }

        public String GetRPU()
        {
            String FullName = string.Empty;
            DataTable RPU;
            try
            {


                string query = string.Format("select RPU from PuntosCarga where (RPU = @RPU )");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@RPU", SqlDbType.VarChar);
                sqlParameters[0].Value = Convert.ToString(strServiceCFE);
                con.dbConnection();
                RPU = con.executeSelectQuery(query, sqlParameters);
                if (RPU.Rows.Count > 0)
                {
                    foreach (DataRow row in RPU.Rows)
                    {
                        FullName = Convert.ToString(row["RPU"]);
                    }

                }
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetRPU";
                clsError.LogWrite();
            }

            return FullName;
        }

        public DataTable InsertRPU()
        {

            DataTable dtData;

            //Boolean ResponseInerst = false;
            //try {

            //string query = string.Format("INSERT INTO Puntocarga (RPU, PuntoCarga,Division,Zona, Direccion, Tarifa, PorteoMaximo, Grupo, FechaCreacion) VALUES(@intServiceCFE,@strLoadPoint,@strDivision,@strZone,@strAddressPoint,@strRate,@dblMaxShipping,@strGroup,@FechaCreacion)");
            string query = string.Format("spInsertar_ConveniosTransmmision_PuntosCarga");
            SqlParameter[] sqlParameters = new SqlParameter[19];
            
            sqlParameters[0] = new SqlParameter("@RUP", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(strServiceCFE);
            sqlParameters[1] = new SqlParameter("@PuntoCarga", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(strLoadPoint);
            sqlParameters[2] = new SqlParameter("@IdEstatusOferta", SqlDbType.NVarChar);
            sqlParameters[2].Value = Convert.ToString(strIdEstatusOferta);

            sqlParameters[3] = new SqlParameter("@Direccion", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(strAddressPoint);

            sqlParameters[4] = new SqlParameter("@NumCta", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(strCta);

            sqlParameters[5] = new SqlParameter("@PorteoMaximo", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(dblMaxShipping);

            sqlParameters[6] = new SqlParameter("@DemandaContratada", SqlDbType.VarChar);
            sqlParameters[6].Value = Convert.ToString(dblDemanda);

            sqlParameters[7] = new SqlParameter("@IdEstatus", SqlDbType.NVarChar);
            sqlParameters[7].Value = Convert.ToString(1);

            sqlParameters[8] = new SqlParameter("@IdGrupo", SqlDbType.NVarChar);
            sqlParameters[8].Value = Convert.ToString(strGroup);
            sqlParameters[9] = new SqlParameter("@IdDivision", SqlDbType.NVarChar);
            sqlParameters[9].Value = Convert.ToString(strDivision);

            sqlParameters[10] = new SqlParameter("@IdZona", SqlDbType.NVarChar);
            sqlParameters[10].Value = Convert.ToString(strZona);
            
            sqlParameters[11] = new SqlParameter("@IdTarifa", SqlDbType.VarChar);
            sqlParameters[11].Value = Convert.ToString(strRate);

            sqlParameters[12] = new SqlParameter("@IdGestorMedicion", SqlDbType.NVarChar);
            sqlParameters[12].Value = Convert.ToString(strGestorMedicion);

            sqlParameters[13] = new SqlParameter("@IdGestorComercial", SqlDbType.NVarChar);
            sqlParameters[13].Value = Convert.ToString(strGestorComercial);
            
            sqlParameters[14] = new SqlParameter("@IdConvenio", SqlDbType.Int);
            sqlParameters[14].Value = Convert.ToString(intConvenio);

            sqlParameters[15] = new SqlParameter("@IdUsuario", SqlDbType.Int);
            sqlParameters[15].Value = Convert.ToString(strIdUsuario);

            sqlParameters[16] = new SqlParameter("@ConPrelacion", SqlDbType.Int);
            sqlParameters[16].Value = Convert.ToInt32(strConPrelacion);
            sqlParameters[17] = new SqlParameter("@Firmado", SqlDbType.Int);
            sqlParameters[17].Value = Convert.ToInt32(strFirmado);
            sqlParameters[18] = new SqlParameter("@strConvenio", SqlDbType.VarChar);
            sqlParameters[18].Value = Convert.ToString(strConvenio);
            con.dbConnection();

            dtData = con.executeStoreProcedure(query, sqlParameters);

            return dtData;
            //    ResponseInerst = con.executeInsertQuery(query, sqlParameters);
            //}
            //catch (Exception ex)
            //{
            //    LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
            //    clsError.logMessage = ex.ToString();
            //    clsError.logModule = "InsertRPU";
            //    clsError.LogWrite();
            //}

            //return ResponseInerst;
        }


        public DataTable InsertConvenio()
        {
            DataTable dtData;

            string query = string.Format("[spInsertar_ConvenioTransmmision_Central]");
            SqlParameter[] sqlParameters = new SqlParameter[3];

            sqlParameters[0] = new SqlParameter("@IdCentral", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(strCentral);

            sqlParameters[1] = new SqlParameter("@IdUsuario", SqlDbType.NVarChar);
            sqlParameters[1].Value = Convert.ToString(strIdUsuario);
            sqlParameters[2] = new SqlParameter("@strConvenio", SqlDbType.NVarChar);
            sqlParameters[2].Value = Convert.ToString(strConvenio);
            con.dbConnection();

            dtData = con.executeStoreProcedure(query, sqlParameters);

            return dtData;
        }

        public DataTable EliminaCarga()
        {
            DataTable dtData;

            string query = string.Format("[spElimina_CargaConvenioTransmmision]");
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@IdCarga", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(intConvenio);

            con.dbConnection();

            dtData = con.executeStoreProcedure(query, sqlParameters);

            return dtData;
        }


    }
}