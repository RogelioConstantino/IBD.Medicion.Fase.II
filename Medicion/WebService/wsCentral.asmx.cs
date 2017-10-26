using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Medicion.Class.Business;

using System.Data;
using System.Text;

using AjaxControlToolkit;

namespace Medicion.WebService
{
    /// <summary>
    /// Descripción breve de wsCentral
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsCentral : System.Web.Services.WebService
    {

        [WebMethod]
        public string UpdateCentral(string CodCentral, string Central, int IdCentral)
        {
            clsCentral oClsCentral = new clsCentral();
            String strIdCentral = Convert.ToString(IdCentral);
            Boolean msg = true;
            string sResp = "";

            if (string.IsNullOrEmpty(CodCentral)) { return "Falta agregar el Codigo de la Central"; }
            if (string.IsNullOrEmpty(Central)) { return "Falta agregar el Central"; }
            if (string.IsNullOrEmpty(strIdCentral)) { return "Falta agregar el Central"; }

            sResp = oClsCentral.UpdateCentral(IdCentral, CodCentral, Central);

            //Call function to update the Central
            //if (msg) { return "La Central se ha actualizado en la base de datos"; }
            //else { return "Error al actualizar los datos"; }

            return sResp;
        }
        [WebMethod]
        public string DeleteCentral(int IdCentral_delete)
        {
            clsCentral oClsCentral = new clsCentral();
            String strIdCentral = Convert.ToString(IdCentral_delete);
            Boolean msg = true;
            if (string.IsNullOrEmpty(strIdCentral)) { return "Falta agregar la Central"; }
            msg = oClsCentral.DeleteCentral(IdCentral_delete);
            //Call function to update the Central
            if (msg) { return "La Central se ha eliminado de la base de datos"; }
            else { return "Error al eliminar la Central de la base de datos"; }

        }
        [WebMethod]
        public String NewCentral(string CodeCentral, string Central)
        {
            clsCentral oClsCentral = new clsCentral();
            //Boolean msg = true;
            string sResp  = "" ;

            if (string.IsNullOrEmpty(CodeCentral)) { return "0-Falta agregar la clave de la Central"; }
            if (string.IsNullOrEmpty(Central)) { return "0-Falta agregar la descripción de la Central"; }

            //msg = oClsCentral.NewCentral(CodeCentral, Central);
            sResp = oClsCentral.NewCentral(CodeCentral, Central);

            //Call function to update the Central
            //if (msg) { return "La Central se creado en la base de datos"; }
            //else { return "Error al crear la Central de la base de datos"; }

            return sResp;
        }

        [WebMethod]
        public CascadingDropDownNameValue[] getCentrales(string knownCategoryValues)

        {
            clsCentral oCls = new clsCentral();
            DataTable dt;
            StringBuilder strHTML = new StringBuilder();

            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            dt = oCls.GetAllCentrales();
            if (dt == null)
            {
                return values.ToArray();
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        values.Add(new CascadingDropDownNameValue
                        {
                            name = row[2].ToString(),
                            value = row[0].ToString()
                        });
                    }
                    return values.ToArray();
                }
                else
                {
                    return values.ToArray();
                }
            }

        }
    }
}
