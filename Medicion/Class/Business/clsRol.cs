using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Text;


namespace Medicion.Class.Business
{
    public class clsRol
    {
        DataTable dt;
        public DataTable GetAll(string strIdTipo)
        {
            Class.Catalogos.CatRol clsCat = new Class.Catalogos.CatRol();
            clsCat.Activo = 1;
            clsCat.idTipo =int.Parse(strIdTipo);
            dt = clsCat.GetAll();

            return dt;
        }

    }
}