using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Medicion.Class.Catalogos
{
    public class PropertiesConvenios
    {
        public int idConvenio { get; set; }        
        public string Convenio { get; set; }
        public string Descripción { get; set; }
        public int idEstatus { get; set; }
        public string Estatus { get; set; }
        public int idCentral{ get; set; }
        public Int16 Activo { get; set; }
        public DataTable dtConvenio { get; set; }
    }
}