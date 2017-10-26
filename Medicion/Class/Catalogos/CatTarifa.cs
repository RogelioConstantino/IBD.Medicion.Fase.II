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
    public class CatTarifa:PropertiesTarifa
    {
        private ConnectionDB con = new ConnectionDB();
        DataTable AllTarifas;

        public DataTable Exist()
        {
            String FullName = string.Empty;
            try
            {
                string query = string.Format("SELECT 1 FROM Tarifas WHERE Activo = @Activo and CveTarifa = upper(@strCveTarifa)  ");//and  Division = upper(@strDivision)
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(Activo);

                sqlParameters[1] = new SqlParameter("@strCveTarifa", SqlDbType.NVarChar);
                sqlParameters[1].Value = Convert.ToString(CveTarifa);
                
                con.dbConnection();
                AllTarifas = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "ExistTarifas";
                clsError.LogWrite();
            }
            return AllTarifas;
        }

    }
}