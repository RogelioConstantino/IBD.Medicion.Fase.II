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
    public class CatRol:PropertiesRol
    {

        private ConnectionDB con = new ConnectionDB();
        DataTable AllDivision;
        public DataTable GetAll()
        {
            String FullName = string.Empty;

            try
            {
                string query = string.Format("SELECT IdGestorRol Id ,GestorRol FROM GestorRoles WHERE  Activo = 1 and IdGestorTipo = @IdGestorTipo ORDER BY GestorRol");
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@IdGestorTipo", SqlDbType.SmallInt);
                sqlParameters[0].Value = Convert.ToString(idTipo);
                con.dbConnection();
                AllDivision = con.executeSelectQuery(query, sqlParameters);
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "GetAll";
                clsError.LogWrite();
            }

            return AllDivision;
        }
    }
}