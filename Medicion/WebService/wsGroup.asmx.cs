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
    /// Summary description for wsGroup
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsGroup : System.Web.Services.WebService
    {
                
        [WebMethod]
        public string UpdateGroup(string InicioOperaciones, string Grupo, int IdGrupo, int IdMed, int IdComer)
        {
            clsGrupo oClsGroup = new clsGrupo();
            String strIdGrupo = Convert.ToString(IdGrupo);
            Boolean msg = true;
            string sResp = "";

            if (string.IsNullOrEmpty(Grupo)){ return "Falta agregar la descripción del Grupo"; }
            if(string.IsNullOrEmpty(strIdGrupo)) { return "Falta agregar el grupo"; }
            if (string.IsNullOrEmpty(InicioOperaciones)) { return "Falta agregar la fecha de incio de operacioens del grupo."; }

            sResp = oClsGroup.UpdateGroup(IdGrupo, Grupo, InicioOperaciones,IdMed,IdComer);
            //Call function to update the group
            //if (msg) { return "El Grupo se ha actualizado en la base de datos"; } 
            //else {
            //    return "Error al actualizar los datos!"  ;
            //}
            return sResp;
        }
        [WebMethod]
        public string DeleteGroup(int IdGrupo_delete) 
        {
            clsGrupo oClsGroup = new clsGrupo();
            String strIdGrupo = Convert.ToString(IdGrupo_delete);
            Boolean msg = true;
            if (string.IsNullOrEmpty(strIdGrupo)) { return "Falta agregar el Grupo"; }
            msg = oClsGroup.DeleteGroup(IdGrupo_delete);
            //Call function to update the group
            if (msg) { return "El Grupo se ha eliminado de la base de datos"; } 
            else { return "Error al eliminar el grupo de la base de datos"; }
            
        }
        [WebMethod]
        public String NewGroup(string Grupo, string InicioOperaciones,int IdMed,int IdComer)
        {
            clsGrupo oClsGroup = new clsGrupo();
            string sResp = "";
            if (string.IsNullOrEmpty(Grupo)) { return "0-Falta agregar la descripción del Grupo"; }
            if (string.IsNullOrEmpty(InicioOperaciones)) { return "0-Falta agregar la fecha de inicio de operaciones del Grupo"; }
            sResp = oClsGroup.NewGroup(Grupo, InicioOperaciones,IdMed,IdComer);

            return sResp;
        }



        //[WebMethod]
        //public CascadingDropDownNameValue[] getGrupos(string knownCategoryValues)
        //{
        //     clsGrupo  oCls = new clsGrupo();
        //    DataTable dt;
        //    StringBuilder strHTML = new StringBuilder();

        //    List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        //    dt = oCls.GetAllGroups();
        //    if (dt == null)
        //    {
        //        return values.ToArray();
        //    }
        //    else
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                values.Add(new CascadingDropDownNameValue
        //                {
        //                    name = row[1].ToString(),
        //                    value = row[0].ToString()
        //                });
        //            }
        //            return values.ToArray();
        //        }
        //        else
        //        {
        //            return values.ToArray();
        //        }
        //    }

        //}



    }
}
