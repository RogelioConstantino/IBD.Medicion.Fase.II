using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Medicion.Class.Business
{
    public class clsContactCFE
    {
        /// <summary>
        /// Return all data from table ContactoCFE
        /// </summary>
        /// <returns></returns>
        public DataTable dtGetAllContactsCFE()
        {
            DataTable dtAllZo;
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            clsCatContactCFE.intActivo = 1;
            dtAllZo = clsCatContactCFE.GetAllContactCFE();

            return dtAllZo;
        }

        DataTable dtResponseFilters;
        /// <summary>
        /// Create a new CFE's Contact
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strDivision"></param>
        /// <param name="strZone"></param>
        /// <param name="strName"></param>
        /// <param name="strFirstName"></param>
        /// <param name="strLastName"></param>
        /// <param name="strCharge"></param>
        /// <param name="strWorkTel"></param>
        /// <param name="strExt"></param>
        /// <param name="strCel"></param>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public string NewContactCFE(string strTitle, string strDivision, string strZone, string strName, string strFirstName, string strLastName, string strCharge, string strWorkTel, string strExt, string strCel, string strEmail, string strPuesto)
        {
            Boolean bRespost = false;
            string sResp = "";

            if (!ExistContac(strName, strFirstName, strLastName, strEmail))
            {
                Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
                clsCatContactCFE.strTitle = strTitle;
                clsCatContactCFE.strDivision = strDivision;
                clsCatContactCFE.strZone = strZone;
                clsCatContactCFE.strName = strName;
                clsCatContactCFE.strFirstName = strFirstName;
                clsCatContactCFE.strLastName = strLastName;
                clsCatContactCFE.strCharge = strCharge;
                clsCatContactCFE.strWorkTel = strWorkTel;
                clsCatContactCFE.strPuesto = strPuesto;
                clsCatContactCFE.strExt = strExt;
                clsCatContactCFE.strCel = strCel;
                clsCatContactCFE.strEmail = strEmail;
                bRespost = clsCatContactCFE.NewContactCFE();

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar el contacto!";

            }
            else
            {
                sResp = "0-El nombre o correo electrónico ya existe para otro contacto!";
            }
            //return bRespost;
            return sResp;
        }

        /// <summary>
        /// Function to validate the filters and return the data according it
        /// </summary>
        /// <param name="strDivision"></param>
        /// <param name="strZone"></param>
        /// <returns></returns>
        public DataTable ValidateFilters(string strDivision, string strZone)
        {

            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();

            if (string.IsNullOrEmpty(strDivision) && string.IsNullOrEmpty(strZone) || strDivision == "-- TODOS --")
            {
                //Return all data
                dtResponseFilters = dtGetAllContactsCFE();
            }
            else if (!string.IsNullOrEmpty(strDivision) && string.IsNullOrEmpty(strZone) || strZone == "-- TODOS --")
            {
                dtResponseFilters = dtGetZoneByDivision(strDivision);
            }
            else if (!string.IsNullOrEmpty(strDivision) && !string.IsNullOrEmpty(strZone))
            {
                dtResponseFilters = dtGetZoneByDivisionAndZone(strDivision, strZone);
            }
            return dtResponseFilters;
        }

        /// <summary>
        /// Function to return all datat from table Zone filtering by division
        /// </summary>
        /// <param name="strDivision"></param>
        /// <returns></returns>
        public DataTable dtGetZoneByDivision(string strDivision)
        {
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            DataTable dtZBD;
            clsCatContactCFE.intActivo = 1;
            clsCatContactCFE.strDivision = strDivision;
            if (strDivision == "-- TODOS --")
            {
                dtZBD = clsCatContactCFE.GetAllContactCFE();
            }
            else
            {
                dtZBD = clsCatContactCFE.GetAllZoneByDivision();
            }
            return dtZBD;

        }

        private DataTable dtGetZoneByDivisionAndZone(string strDivision, string strZone)
        {
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            DataTable dtZBDZ;
            clsCatContactCFE.intActivo = 1;
            clsCatContactCFE.strDivision = strDivision;
            clsCatContactCFE.strZone = strZone;
            dtZBDZ = clsCatContactCFE.GetAllZoneByDivisionAndZone();
            return dtZBDZ;
        }

        public StringBuilder ReturnHTMLDivision(DataTable dtG)
        {
            StringBuilder strDiv = new StringBuilder();
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            clsCatContactCFE.dtContactCFE = dtG;
            strDiv = clsCatContactCFE.CreateTableHTML();
            return strDiv;
        }

        public string UpdateDivision(string strID, string strTitulo, string strDivision, string strZona, string strCorreo, string strNombre, string strApPaterno, string strApMaterno, string strTelTrabajo, string strExt , string strCelular, string strPuesto)
        {
            Boolean bRespost = false;
            string sResp = "";

            if (!ExistContacCveId(strDivision, strZona, strNombre, strApPaterno, strApMaterno, strCorreo, strID))
            {
                Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            clsCatContactCFE.strID = strID;
            clsCatContactCFE.strTitle= strTitulo;
            clsCatContactCFE.strDivision = strDivision;
            clsCatContactCFE.strZone = strZona;
            clsCatContactCFE.strEmail = strCorreo;
            clsCatContactCFE.strPuesto = strPuesto;
            clsCatContactCFE.strName = strNombre;
            clsCatContactCFE.strFirstName = strApPaterno;
            clsCatContactCFE.strLastName = strApMaterno;
            clsCatContactCFE.strWorkTel = strTelTrabajo;
            clsCatContactCFE.strExt = strExt;
            clsCatContactCFE.strCel = strCelular;
            clsCatContactCFE.intActivo = 1;
            bRespost = clsCatContactCFE.UpdateContactoCFE();

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar el contacto!";

            }
            else
            {
                sResp = "0-El nombre o correo electrónico ya existe para otro contacto!";
            }
            //return bRespost;
            return sResp;
        }

        public Boolean DeleteContactCFE(string strID, int intActivo)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            clsCatContactCFE.strID = strID;
            ///clsCatContactCFE.strDivision = strDivision;
            //clsCatContactCFE.strZone = strZone;
            //clsCatContactCFE.strEmail = strEmail;
            clsCatContactCFE.intActivo = 0;
            bRespost = clsCatContactCFE.DeleteContactoCFE();
            return bRespost;

        }

        public DataTable GetContactCFEByRPU(string strRPU) {

            DataTable dtCFE;
             Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            clsCatContactCFE.intActivo = 1;
            clsCatContactCFE.strRPU = strRPU;
            dtCFE = clsCatContactCFE.GetAllContactCFEbyRPU();
            return dtCFE;
        }

        public Boolean ExistContac(string strName, string strFirstName, string strLastName, string strEmail)
        {
            Boolean bmsg = false;
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
                        
            clsCatContactCFE.strName = strName;
            clsCatContactCFE.strFirstName = strFirstName;
            clsCatContactCFE.strLastName = strLastName;
            clsCatContactCFE.strEmail = strEmail;
            clsCatContactCFE.intActivo = 1;
            
            DataTable dtresp = clsCatContactCFE.GetAllZoneByDivisionAndCVe();
            int iExiste = int.Parse(dtresp.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bmsg = true;
            }
            return bmsg;
        }

        public Boolean ExistContacCveId(string strDivision, string strIdZone, string  strName, string  strFirstName, string strLastName, string strEmail, string strId)
        {// 
            Boolean bmsg = false;            
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();

            clsCatContactCFE.strDivision = strDivision;
            clsCatContactCFE.strZone = strIdZone;
            clsCatContactCFE.strName = strName;
            clsCatContactCFE.strFirstName = strFirstName;
            clsCatContactCFE.strLastName = strLastName;
            clsCatContactCFE.strEmail = strEmail;
            clsCatContactCFE.intActivo = 1;
            clsCatContactCFE.strID = strId;

            DataTable dtresp = clsCatContactCFE.GetAllZoneByDivisionAndCVeID();
            int iExiste = int.Parse(dtresp.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bmsg = true;
            }
            return bmsg;
        }

        public DataTable getByRUP(string strRUP)
        {

            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();

            dtResponseFilters = dtGetByRUP(strRUP);

            return dtResponseFilters;
        }
        public DataTable dtGetByRUP(string strRUP)
        {
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            DataTable dtZBD;
            clsCatContactCFE.intActivo = 1;


            dtZBD = clsCatContactCFE.GetByRUP(strRUP);

            return dtZBD;

        }

        public StringBuilder ReturnHTMLRup(DataTable dtG)
        {
            StringBuilder strDiv = new StringBuilder();
            Class.Catalogos.CatContactCFE clsCatContactCFE = new Class.Catalogos.CatContactCFE();
            clsCatContactCFE.dtContactCFE = dtG;
            strDiv = clsCatContactCFE.CreateTableHTMLSimple();
            return strDiv;
        }

    }
}