using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesGestores
    {

        public string Id { get; set; }

        public string strNumeroEmpleado { get; set; }

        public string strIniciales { get; set; }

        public string strName { get; set; }
        public string strFirstName { get; set; }
        public string strLastName { get; set; }
        
        public string strPuesto { get; set; }

        public string strWorkTel { get; set; }
        public string strExt { get; set; }
        public string strCel { get; set; }        
        public string strEmail { get; set; }

        public Int16 intActivo { get; set; }
        
        public DataTable dt { get; set; }
    }
}