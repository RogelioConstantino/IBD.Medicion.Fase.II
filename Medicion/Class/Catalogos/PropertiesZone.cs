using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesZone
    {
        public int intIdZone { get; set; }
        public string strCveZone { get; set; }
        public string strZone { get; set; }
        public string strDivision { get; set; }
        public string strObservation { get; set; }
        public Int16 intActivo { get; set; }
        public DataTable dtZone { get; set; }
    }
}