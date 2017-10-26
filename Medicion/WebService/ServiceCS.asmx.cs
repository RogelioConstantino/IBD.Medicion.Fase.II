using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;
using System.Collections.Generic;



namespace Medicion.WebService
{
    /// <summary>
    /// Descripción breve de ServiceCS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class ServiceCS : System.Web.Services.WebService
    {

        [WebMethod]
        public CascadingDropDownNameValue[] GetCountries(string knownCategoryValues)
        {
            string query = "SELECT CountryName, CountryId FROM Countries";
            List<CascadingDropDownNameValue> countries = GetData(query);
            return countries.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetStates(string knownCategoryValues)
        {
            string country = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["CountryId"];
            string query = string.Format("SELECT StateName, StateId FROM States WHERE CountryId = {0}", country);
            List<CascadingDropDownNameValue> states = GetData(query);
            return states.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetCities(string knownCategoryValues)
        {
            string state = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["StateId"];
            string query = string.Format("SELECT CityName, CityId FROM Cities WHERE StateId = {0}", state);
            List<CascadingDropDownNameValue> cities = GetData(query);
            return cities.ToArray();
        }

        private List<CascadingDropDownNameValue> GetData(string query)
        {
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices_loc"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);

            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd.Connection = con;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        values.Add(new CascadingDropDownNameValue
                        {
                            name = reader[0].ToString(),
                            value = reader[1].ToString()
                        });
                    }
                    reader.Close();
                    con.Close();
                    return values;
                }
            }


        }


    }
}
