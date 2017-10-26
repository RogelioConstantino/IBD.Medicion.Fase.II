using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicion.Class.Business
{
    public class clsPropertiesCommunications
    {
        public int intIDParametersCommunications { get; set; }
        public int intCheckActivo { get; set; }
        public int intRequiredTCTP { get; set; }
        public int intRequiredBaseTermianl { get; set; }
        public int intActualElectricMeter { get; set; }
        public string strTypesMeters { get; set; }
        public int intRequiredElectricMeter{ get; set;}
        public string strCommunicationClass { get; set; }
        public string strTypesCommunications { get; set; }
        public string strLocalCommunication { get; set; }
        public string strCFECommunication { get; set; }
        public string strRPU { get; set; }
        public string strObservation { get; set; }
        public string strDeliveryDate { get; set; }
        public string strInstallationDate { get; set; }
        public string strActualMeter { get; set; }
        public string strRequiredMeter { get; set; }
    }
}