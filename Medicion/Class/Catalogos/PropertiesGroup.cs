using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace Medicion.Class.Catalogos
{
    public class PropertiesGroup
    {
        public int idGrupo { get; set; }
        public string Grupo { get; set; }
        public string InicioOperaciones { get; set; }
        public Int16 Activo { get; set; }
        public DataTable dtGroup { get; set; }
        public int IdMed { get; set; }
        public int IdComer { get; set; }
    }
}