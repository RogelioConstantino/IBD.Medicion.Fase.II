using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesStatus
    {
        public int idStatus { get; set; }
        public string Cve { get; set; }
        public string Status { get; set; }
        public Int16 Activo { get; set; }
        public DataTable dtStatus { get; set; }
    }
}