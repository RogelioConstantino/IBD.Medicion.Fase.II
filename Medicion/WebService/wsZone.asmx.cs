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
    /// Summary description for wsZone
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsZone : System.Web.Services.WebService
    {

        [WebMethod]
        public String EditZone(string strDivision, string strIdZone, string strCveZone, string strZone, string strObservations)
        {
            Boolean msg = true;
            string sResp = "";
            clsZone oclsZone = new clsZone();

            
            //if (string.IsNullOrEmpty(strDivision)) { return "Falta agregar la Division"; }
            //if (string.IsNullOrEmpty(strIdZone)) { return "Falta agregar el Id de la zona"; }
            //if (string.IsNullOrEmpty(strCveZone)) { return "Falta agregar la clave de la zona"; }
            //if (string.IsNullOrEmpty(strZone)) { return "Falta agregar la descripción de la zona"; }

            sResp = oclsZone.UpdateZone(strDivision, strIdZone, strCveZone, strZone, strObservations);

            //if (msg) { return "La Zona se ha actualizado en la base de datos"; }
            //else { return "Error al actualizar los datos"; }
            return sResp;

        }

        [WebMethod]
        public String DeleteZone(string strDivision, string strZone)
        {
            clsZone oclsZone = new clsZone();
            Boolean msg = true;
            if (string.IsNullOrEmpty(strDivision) && string.IsNullOrEmpty(strZone)) { return "Falta agregar la división y la zona"; }
            msg = oclsZone.DeleteZone(strDivision,strZone);

            if (msg) { return "La Zona se ha eliminado de la base de datos"; }
            else { return "Error al eliminar la Zona de la base de datos"; }

        }


        [WebMethod]
        public CascadingDropDownNameValue[] getZonas(string knownCategoryValues)
        {
            clsZone oCls = new clsZone();
            DataTable dt;
            StringBuilder strHTML = new StringBuilder();

            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            string Id = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Id"];

            dt = oCls.dtGetZoneByDivision_Dll(Id);

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
                            name = row[1].ToString(),
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
