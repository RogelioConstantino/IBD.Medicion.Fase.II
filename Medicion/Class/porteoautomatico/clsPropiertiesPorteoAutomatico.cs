using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Medicion.Class.porteoautomatico
{

    public static class ExcelHasHeader
    {
        public static String Yes { get { return "Yes"; } }
        public static String No { get { return "No"; } }
    }


    public class clsPropiertiesPorteoAutomatico
    {
       
        public string strLoadPoint { get; set; }
        public double dblServiceCFE { get; set; }
        public string strIdEstatusOferta { get; set; }
        public string strServiceCFE { get; set; }
        public string strAddressPoint { get; set; }
        public string strRate { get; set; }
        public double dblMaxShipping { get; set; }
        public string strGroup { get; set; }
        public string strDivision { get; set; }
        public string strZona { get; set; }

        public string strCta{ get; set; }
        
        public string strGestorComercial { get; set; }
        public string strGestorMedicion { get; set; }
        //campos agregados nuevos jose manuel
        public int strConPrelacion { get; set; }
        public int strFirmado { get; set; }


        public DataTable dtShiping { get; set; }
        public string strFileName { get; set; }
        public string strExtension { get; set; }
        public string strFilePath { get; set; }
        public string strIsHDR { get; set; }


        public StringBuilder strRPUrepeated { get; set; }


        public DataTable dtResult { get; set; }

        public string strCentral { get; set; }

        public int intConvenio { get; set; }

        public double dblDemanda { get; set; }

        public int strIdUsuario { get; set; }

        public string strConvenio { get; set; }
        


    }

}