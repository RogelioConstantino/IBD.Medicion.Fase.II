using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;

using Medicion.Class.LogError;
using Medicion.Class.Business;

using System.Text;

namespace Medicion
{
    public partial class catCentralesPrelacion : System.Web.UI.Page
    {

        LogErrorMedicion clsError = new LogErrorMedicion();
        StringBuilder strHTMLCentral = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            clsCentral clsBussCentral = new clsCentral();

            if (!IsPostBack)
            {

                DataTable dtC = clsBussCentral.GetAllCentralesPrelacion();

                gvLocations.DataSource = dtC;
                gvLocations.DataBind();

                // this.BindGrid();
            }

        }


        private void BindGrid()
        {
            string query = "SELECT Id, Location, Preference FROM HolidayLocations ORDER BY Preference";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvLocations.DataSource = dt;
                            gvLocations.DataBind();
                        }
                    }
                }
            }
        }



      

        private void UpdatePreference(string locationId, int preference)
        {
            string constr = ConfigurationManager.AppSettings["appConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Centrales SET OrdenPre = @Preference WHERE CveCentral = @Id"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", locationId);
                        cmd.Parameters.AddWithValue("@Preference", preference);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    lblMsg.Text = "La información fue guardada exitosamente.";
                    
                }
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            string[] locationIds = (from p in Request.Form["Código"].Split(',')
                                 select p).ToArray();
            int preference = 1;
            foreach (string locationId in locationIds)
            {
                this.UpdatePreference(locationId, preference);
                preference += 1;
            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}