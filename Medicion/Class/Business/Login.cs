using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medicion.Class.ADO;
using System.Data;
using System.Data.SqlClient;
namespace Medicion.Class
{
    public class Login : Users 
    {
        public ConnectionDB con= new ConnectionDB();
        DataTable Usr;
       
        public  DataTable GetUser() 
        {
            String FullName = string.Empty;
            try
            {


                string query = string.Format("select IdUsuario, Nombre FirstName,ApellidoPaterno LastName , Email, IdRol,IdGestor from Usuarios where (usuario = @Email )  AND Password =@Password "); // or Email like @Email) "); //
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@Email", SqlDbType.VarChar);
                sqlParameters[0].Value = Convert.ToString(UserName);
                sqlParameters[1] = new SqlParameter("@Password", SqlDbType.VarChar);
                sqlParameters[1].Value = Convert.ToString(Password);

                con.dbConnection();
                Usr = con.executeSelectQuery(query, sqlParameters);
                //if (Usr.Rows.Count > 0)
                //{
                //    foreach (DataRow row in Usr.Rows)
                //    {
                //        FullName = Convert.ToString(row["FirstName"]) + Convert.ToString(row["LastName"]);
                //    }

                //}
 
            }
            catch (Exception ex)
            {
                LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                clsError.logMessage = ex.ToString();
                clsError.logModule = "CreateTableHTML";
                clsError.LogWrite();
            }

            return Usr;
        }
    }
}