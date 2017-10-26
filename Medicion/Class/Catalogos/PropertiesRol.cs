using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesRol
    {

        public int idTipo { get; set; }
        public int idRol { get; set; }
        public string Rol { get; set; }
        public Int16 Activo { get; set; }
        public DataTable dtTipo { get; set; }

    }


}