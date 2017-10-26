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
    /// Descripción breve de wsGestor
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsGestor : System.Web.Services.WebService
    {
        [WebMethod]
        public String New(string tipo, string Nombre, string Paterno, string Materno, string strNumeroEmpleado, string strIniciales)
        {
            clsGestores oCls = new clsGestores();
            Boolean msg = true;
            string sResp = "";

            //if (string.IsNullOrEmpty(CodeCentral)) { return "0-Falta agregar la clave de la Central"; }
            //if (string.IsNullOrEmpty(Central)) { return "0-Falta agregar la descripción de la Central"; }

            //msg = oClsCentral.NewCentral(CodeCentral, Central);
            sResp = oCls.New(tipo, Nombre, Paterno, Materno, strNumeroEmpleado, strIniciales);

            //Call function to update the Central
            //if (msg) { return "La Central se creado en la base de datos"; }
            //else { return "Error al crear la Central de la base de datos"; }

            return sResp;
        }

        [WebMethod]
        public String Edit(string id, string tipo, string strNombre, string strApPaterno, string strApMaterno, string strNumeroEmpleado, string strIniciales)
        {
            Boolean msg = true;
            string sResp = "";
            clsGestores oClsGestor = new clsGestores();


            //if (string.IsNullOrEmpty(strDivision)) { return "Falta agregar la division"; }
            //if (string.IsNullOrEmpty(strZona)) { return "Falta agregar la Zona"; }
            //if (string.IsNullOrEmpty(strCorreo)) { return "Falta agregar el Corroe"; }

            sResp = oClsGestor.Update( id, tipo, strNumeroEmpleado, strNombre, strApPaterno, strApMaterno, strIniciales);

            //if (msg) { return "La Division se ha actualizado en la base de datos"; }
            //else { return "Error al actualizar los datos"; }

            return sResp;

        }

        [WebMethod]
        public String Delete(string strGestor)
        {
            Boolean msg = true;
            clsGestores oClsCon = new clsGestores();
            //if (string.IsNullOrEmpty(strDivision)) { return "Falta agregar la division"; }
            //if (string.IsNullOrEmpty(strZone)) { return "Falta agregar la Zona"; }
            //if (string.IsNullOrEmpty(strEmail)) { return "Falta agregar el Corroe"; }

            msg = oClsCon.Delete(strGestor);

            if (msg) { return "1-El contacto se ha eliminado de la base de datos"; }
            else { return "0-Error al eliminar el contacto de la base de datos"; }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] getTipo_New(string knownCategoryValues)
        {
            clsTipo oCls = new clsTipo();
            DataTable dt;
            StringBuilder strHTML = new StringBuilder();

            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            dt = oCls.GetAllTipo();
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



        [WebMethod]
        public CascadingDropDownNameValue[] getRol_New(string knownCategoryValues)
        {
            {
                clsRol oCls = new clsRol();
                DataTable dt;
                StringBuilder strHTML = new StringBuilder();

                List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

                string Id = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Id"];

                dt = oCls.GetAll(Id);
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
}
