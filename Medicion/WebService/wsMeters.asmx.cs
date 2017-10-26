using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace Medicion.WebService
{
    /// <summary>
    /// Summary description for wsMeters
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsMeters : System.Web.Services.WebService
    {

        [WebMethod]
        public string NewAgreemet(string strAgreementselected, string strRPU, string strChecked, string strEmail, string strCharge)
        {
            Decimal decCharge = 0;
            Class.Business.clsMeters oclsMeters = new Class.Business.clsMeters();
            if (string.IsNullOrEmpty(strAgreementselected))
            {
                return "Falata seleccionar el convenio";
            }
            else 
            {
                if (!string.IsNullOrEmpty(strCharge) && (strCharge != "undefined"))
                {
                    decCharge = Convert.ToDecimal(strCharge);
                }
                Boolean bMsg = oclsMeters.InsertAgreement(strAgreementselected, strRPU, strChecked, strEmail, decCharge);
              if (bMsg) { strAgreementselected= "El convenio se insertó con éxito."; }
              else { strAgreementselected= "Error al tratar de insertar en la base de datos"; }
            }
            return strAgreementselected;
        }
        
    }
}
