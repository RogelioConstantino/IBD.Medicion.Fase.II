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
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsConvenio : System.Web.Services.WebService
    {

        [WebMethod]
        public String Edit(string strID, string strIdEstatus, string strDescripcion)
        {
            String msg = "";
            clsConvenios oCls = new clsConvenios();
            Boolean resp = false;
            //resp = oCls.update(int.Parse(strID), strDescripcion, (strIdEstatus));
            msg = "1-La información se guardo exitosamente!";
            return msg;
        }

        [WebMethod]
        public CascadingDropDownNameValue[] getConvenioEstatus(string knownCategoryValues)
        {
             clsConvenios  oCls = new clsConvenios();
            DataTable dt;
            StringBuilder strHTML = new StringBuilder();

            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            dt = oCls.GetEstatus();
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
