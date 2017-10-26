using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Medicion.Class.Business;
namespace Medicion.WebService
{
    /// <summary>
    /// Summary description for wsStatus
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsStatus : System.Web.Services.WebService
    {

        [WebMethod]
        public string NewStatus(string Cve , string Status)
        {
            clsStatus oclsStatus = new clsStatus();
            
            string sResp = "";

            if (string.IsNullOrEmpty(Cve)) { return "Falta agregar la clave del  Estatus"; }

            if (string.IsNullOrEmpty(Status)) { return "Falta agregar la descripcion del Estatus"; }

            sResp = oclsStatus.NewStatus(Cve,Status);

            return sResp;

        }

        [WebMethod]
        public String EditStatus(string Status, int IdStatus, string Cve)
        {
            //Boolean msg = true;
            string sResp = "";
            clsStatus oclsStatus = new clsStatus();

            String strIdStatus = Convert.ToString(IdStatus);
            if (string.IsNullOrEmpty(Status)) { return "Falta agregar el Estatus"; }
            if (string.IsNullOrEmpty(Cve)) { return "Falta agregar la clave"; }
            if (string.IsNullOrEmpty(strIdStatus)) { return "Falta agregar el Estatus"; }
            sResp = oclsStatus.UpdateStatus(IdStatus, Status, Cve);
            return sResp;
            //if (msg) { return "La Estatus se ha actualizado en la base de datos"; }
            //else { return "Error al actualizar los datos"; }
        }
        [WebMethod]
        public String DeleteStatus(int intIdStatus_delete)
        {
            clsStatus oClsStatus = new clsStatus();
            String strIdGrupo = Convert.ToString(intIdStatus_delete);
            Boolean msg = true;
            if (string.IsNullOrEmpty(strIdGrupo)) { return "Falta agregar el Estatus"; }
            msg = oClsStatus.DeleteStatus(intIdStatus_delete);

            if (msg) { return "El Estatus se ha eliminado de la base de datos"; }
            else { return "Error al eliminar el estatus de la base de datos"; }

        }
    }
}
