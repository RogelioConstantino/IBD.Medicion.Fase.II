using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Text;


namespace Medicion.Class.Business
{
    public class clsTipo
    {
        DataTable dt;
        public DataTable GetAllTipo()
        {
            Class.Catalogos.CatTipo clsCatGroup = new Class.Catalogos.CatTipo();
            clsCatGroup.Activo = 1;
            dt = clsCatGroup.GetAll();

            return dt;
        }

    }
}