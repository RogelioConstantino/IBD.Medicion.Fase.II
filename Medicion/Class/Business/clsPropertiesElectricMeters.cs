using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Business
{
    public class clsPropertiesElectricMeters
    {
        public string strGroup { get; set; }
        public string strCentral { get; set; }
        public string strGestorMedicion { get; set; }
        public string strLoadingCharges { get; set; }
        public string strRPU { get; set; }
        public Int16 intActive { get; set; }
        public int IdGMedicion { get; set; }
        public DataTable dtElectricMeters { get; set; }
    }
}