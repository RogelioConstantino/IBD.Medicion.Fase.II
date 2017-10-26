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
    public class CatTipo: PropertiesTipo
    {

        private ConnectionDB con = new ConnectionDB();
        DataTable dt;
        public DataTable GetAll()
        {
            String FullName = string.Empty;

            try
            {
                string query = string.Format("SELECT IdGestorTipo Id, GestorTipo FROM GestorTipos WHERE Activo = @Activo  ORDER BY GestorTipo");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@Activo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(1);
                con.dbConnection();
                dt = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAll";
                clsError.LogWrite();
            }

            return dt;
        }
    }
}