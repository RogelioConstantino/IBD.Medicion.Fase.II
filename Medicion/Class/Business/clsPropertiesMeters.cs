using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicion.Class.Business
{
    public class clsPropertiesMeters
    {
        public int intIdMeter { get; set; }
        public int intIDParametersMeters { get; set; }
        public string strRPU { get; set; }
        public string strStatus { get; set; }
        public string strEmailusr { get; set; }
        public string strFileName { get; set; }
        public string strFileExtension { get; set; }
        public String strDeliveryDate { get; set; }
        public String strInstallationDate { get; set; }
        public string strObservation { get; set; }
        public Int16 intCheckActivo { get; set; }
        public string strArchivo { get; set; }
        public string strContentType { get; set; }
        public List<clsPropertiesMeters> LPM { get; set; }

        //public const Int32 REQUIRED_ELECTRICAL_CONTACT =1;
        //public const Int32 ELECTRIC_CONTACT_COMPLETED = 2;
        //public const Int32 REQUIRED_NETWORK_NODE = 3;
        //public const Int32 NETWORK_NODE_COMPLETED = 4;
        //public const Int32 PRINCIPAL_ELECTRIC_METER = 5;
        //public const Int32 DELIVERED_ELECTRIC_METER = 6;
        //public const Int32 HAS_ELECTRIM_METER_BACKUP = 7;
        //public const Int32 SESSION_LETTER_RECEIVED = 8;
        //public const Int32 ELECTRIC_METER_INSTALLED = 9;
        //public const Int32 METER_WITH_PROFILE = 10;
        //public const Int32 REQUIRED_REWARD = 11;
        //public const Int32 LETTER_COMMITMEND_SIGNED = 12;
        //public const Int32 REQUIRED_RELOCATION = 13;
        //public const Int32 REQUIRED_CABINET = 14;

        enum ParameterMeter
        { 
         REQUIRED_ELECTRICAL_CONTACT =1,
         ELECTRIC_CONTACT_COMPLETED = 2,
         REQUIRED_NETWORK_NODE = 3,
         NETWORK_NODE_COMPLETED = 4,
         PRINCIPAL_ELECTRIC_METER = 5,
         DELIVERED_ELECTRIC_METER = 6,
         HAS_ELECTRIM_METER_BACKUP = 7,
         SESSION_LETTER_RECEIVED = 8,
         ELECTRIC_METER_INSTALLED = 9,
         METER_WITH_PROFILE = 10,
         REQUIRED_REWARD = 11,
         LETTER_COMMITMEND_SIGNED = 12,
         REQUIRED_RELOCATION = 13,
         REQUIRED_CABINET = 14
        };

    }

}