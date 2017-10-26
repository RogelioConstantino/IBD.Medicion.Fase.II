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
    /// Summary description for wsDivision
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsDivision : System.Web.Services.WebService
    {

        [WebMethod]
        public string NewDivision(string CveDivision, string Division)
        {
            clsDivision oclsDivision = new clsDivision();
            string sResp = "";

            if (string.IsNullOrEmpty(CveDivision)) { return "0-Falta agregar la clave de la División"; }
            if (string.IsNullOrEmpty(Division)) { return "0-Falta agregar la descripción de la División"; }

            sResp = oclsDivision.NewDivision(CveDivision, Division);

            return sResp;
        }
        [WebMethod]
        public String EditDivision(string  CveDivision, string Division, int IdDivision) {
            //Boolean msg = true;
            string sResp = "";


            clsDivision oclsDivision = new clsDivision();

            String strIdDivision = Convert.ToString(IdDivision);

            //if (string.IsNullOrEmpty(CveDivision)) { return "Falta agregar la clave del la División"; }
            //if (string.IsNullOrEmpty(Division)) { return "Falta agregar la descripción"; }
            //if (string.IsNullOrEmpty(strIdDivision)) { return "Falta agregar el Id de la división"; }

            sResp = oclsDivision.UpdateDivision(IdDivision, Division, CveDivision);

            //if (msg) { return "La Division se ha actualizado en la base de datos"; }
            //else { return "Error al actualizar los datos"; }
            return sResp;

        }
        [WebMethod]
        public String DeleteDivision(int intIdDivision_delete) {
            clsDivision oClsDivision = new clsDivision();
            String strIdGrupo = Convert.ToString(intIdDivision_delete);
            Boolean msg = true;
            if (string.IsNullOrEmpty(strIdGrupo)) { return "Falta agregar la División"; }
            msg = oClsDivision.DeleteDivision(intIdDivision_delete);
            
            if (msg) { return "La división se ha eliminado de la base de datos"; }
            else { return "Error al eliminar la división de la base de datos"; }
        
        }

        [WebMethod]
        public CascadingDropDownNameValue[] getDivisiones(string knownCategoryValues)
        {
            clsDivision oCls = new clsDivision();
            DataTable dt;
            StringBuilder strHTML = new StringBuilder();

            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            dt = oCls.GetAllDivision();
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

        [WebMethod]
        public CascadingDropDownNameValue[] getDivisiones_New(string knownCategoryValues)
        {
            clsDivision oCls = new clsDivision();
            DataTable dt;
            StringBuilder strHTML = new StringBuilder();

            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            dt = oCls.GetAllDivision();
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
