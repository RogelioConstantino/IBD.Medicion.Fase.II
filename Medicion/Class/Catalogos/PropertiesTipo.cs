using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesTipo
    {
        public int idTipo { get; set; }
        public string Tipo { get; set; }
        public Int16 Activo { get; set; }
        public DataTable dtTipo { get; set; }
    }
}