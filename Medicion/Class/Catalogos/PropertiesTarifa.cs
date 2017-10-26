using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesTarifa
    {
        public int idTarifa { get; set; }
        public string CveTarifa { get; set; }
        public string Tarifa { get; set; }
        public Int16 Activo { get; set; }
        public DataTable dtTarifa { get; set; }
    }
}