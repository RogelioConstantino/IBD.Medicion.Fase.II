using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Medicion.Class.Business;

using System.Data;
using System.Text;

namespace Medicion.WebService
{
    /// <summary>
    /// Summary description for wsContactCFE
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsContactCFE : System.Web.Services.WebService
    {

        [WebMethod]
        public String EditContactCFE(string strID, string strTitulo, string strDivision, string strZona, string strCorreo, string strNombre, string strApPaterno, string strApMaterno, string strTelTrabajo, string strExt, string strCelular, string strPuesto)
        {
            String msg = "";
            clsContactCFE oClsContactCFE = new clsContactCFE();


            //if (string.IsNullOrEmpty(strDivision)) { return "Falta agregar la división"; }
            //if (string.IsNullOrEmpty(strZona)) { return "Falta agregar la Zona"; }
            //if (string.IsNullOrEmpty(strCorreo)) { return "Falta agregar el Corro electrónico"; }

            msg = oClsContactCFE.UpdateDivision(strID, strTitulo,strDivision,strZona,strCorreo,strNombre,strApPaterno,strApMaterno,strTelTrabajo, strExt, strCelular, strPuesto);

            //if (msg) { return "La Division se ha actualizado en la base de datos"; }
            //else { return "Error al actualizar los datos"; }
            return msg;

        }
        [WebMethod]
        public String DeleteContactCFE(string strId )
        {
            Boolean msg = true;
            clsContactCFE oClsContactCFE = new clsContactCFE();
            //if (string.IsNullOrEmpty(strDivision)) { return "Falta agregar la division"; }
            //if (string.IsNullOrEmpty(strZone)) { return "Falta agregar la Zona"; }
            //if (string.IsNullOrEmpty(strEmail)) { return "Falta agregar el Corroe"; }
            msg = oClsContactCFE.DeleteContactCFE(strId, 0);
            if (msg) { return "El contacto se ha eliminado de la base de datos"; }
            else { return "Error al eliminar el contacto de la base de datos"; }
        }


        [WebMethod]
        public String getContactCFE_byRUP(string strRPU)
        {
            clsZone oClsZone = new clsZone();
            clsContactCFE oClsContactoCFE = new clsContactCFE();
            DataTable dtZone;
            StringBuilder strHTMLGroup = new StringBuilder();

            dtZone = oClsContactoCFE.getByRUP(strRPU);
            if (dtZone == null)
            {
                return "Error al recuperar los datos";
            }
            else
            {
                if (dtZone.Rows.Count > 0)
                {
                    strHTMLGroup = oClsContactoCFE.ReturnHTMLRup(dtZone);
                    return strHTMLGroup.ToString();
                    //return "<div class='table-responsive'><table class='table table-hover table-bordred table-striped table-bordered'><thead><tr class='filters'><th class='text-center'>CFE</th></tr></thead><tbody><tr><td>Juna Pérez <p>Av. 5 de Mayo No. 1390 Col. Napoles </p><p><a href='mailto:jperez@cfe.com'>jperez@cfe.com</a> </p></td></tr></tbody></table></div>: ";                    
                }
                else
                {
                    return "No hay datos para mostrar";
                }
            }

        }

    }
}
