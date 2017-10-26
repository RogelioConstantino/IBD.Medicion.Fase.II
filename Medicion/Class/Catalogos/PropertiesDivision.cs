using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesDivision
    {       
        public int idDivision { get; set; }
        public string CveDivision { get; set; }
        public string Division { get; set; }
        public Int16 Activo { get; set; }
        public DataTable dtDivision { get; set; }
        
    }
}