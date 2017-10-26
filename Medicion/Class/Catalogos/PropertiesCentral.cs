using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesCentral
    {
        public int idCentral { get; set; }
        public string CodCentral { get; set; }
        public string Central { get; set; }
        public Int16 Activo { get; set; }
        public DataTable dtCentral { get; set; }
    }
}