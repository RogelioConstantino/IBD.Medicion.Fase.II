using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Medicion.Class.Business
{
    public class clsZone
    {
        DataTable dtResponseFilters;

        /// <summary>
        /// Return all data from table Zone
        /// </summary>
        /// <returns></returns>
        public DataTable dtGetAllZone()
        {
            DataTable dtAllZo;
            Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
            clsCatZone.intActivo = 1;
            dtAllZo = clsCatZone.GetAllZone();

            return dtAllZo;
        }
        /// <summary>
        /// Function to return all datat from table Zone filtering by division
        /// </summary>
        /// <param name="strDivision"></param>
        /// <returns></returns>
        public DataTable dtGetZoneByDivision(string strDivision) 
        {
            Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
            DataTable dtZBD;
            clsCatZone.intActivo = 1;
            clsCatZone.strDivision = strDivision;
            if (strDivision == "-- TODOS --")
            {
                dtZBD = clsCatZone.GetAllZone();
            }
            else {
                dtZBD = clsCatZone.GetAllZoneByDivision();
            }
            return dtZBD;

        }

        private DataTable dtGetZoneByDivisionAndZone(string strDivision, string strCveZone, string strZone) //
        {
            Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
            DataTable dtZBDZ;
            clsCatZone.intActivo = 1;
            clsCatZone.strDivision = strDivision;
            clsCatZone.strCveZone = strCveZone;
            clsCatZone.strZone = strZone;
            dtZBDZ = clsCatZone.GetAllZoneByDivisionAndZone();
            return dtZBDZ;
        }
        public Boolean ExistZona(string strDivision, string strCveZone, string strZone)
        {// 
            Boolean bmsg = false;
            DataTable dtresp = dtGetZoneByDivisionAndZone(strDivision, strCveZone, strZone); //
            if (dtresp.Rows.Count > 0) { bmsg = true; }
            return bmsg;
        }
        /// <summary>
        /// Function to validate the filters and return the data according it
        /// </summary>
        /// <param name="strDivision"></param>
        /// <param name="strZone"></param>
        /// <returns></returns>
        public DataTable ValidateFilters(string strDivision, string strZone)
        {
            
            Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();

            if (string.IsNullOrEmpty(strDivision) && string.IsNullOrEmpty(strZone) || strDivision == "-- TODOS --")
            {
                //Return all data
                dtResponseFilters= dtGetAllZone();
            }
            else if (!string.IsNullOrEmpty(strDivision) && string.IsNullOrEmpty(strZone) || strZone == "-- TODOS --") 
            {
                 dtResponseFilters = dtGetZoneByDivision(strDivision);
            }
            else if (!string.IsNullOrEmpty(strDivision) && !string.IsNullOrEmpty(strZone))
            {
                dtResponseFilters = dtGetZoneByDivisionAndZone(strDivision, "", strZone);
            }
            return dtResponseFilters;
        }
        public StringBuilder ReturnHTMLDivision(DataTable dtG)
        {
            StringBuilder strDiv = new StringBuilder();
            Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
            clsCatZone.dtZone = dtG;
            strDiv = clsCatZone.CreateTableHTML();
            return strDiv;
        }
        public string NewZone(string strdivision, string strNewCveZone, string strNewZone, string strNewObservations)
        {
            Boolean bRespost = false;
            string sResp = "";

            if (!ExistZonaCve(strdivision, strNewCveZone,  strNewZone))
            {
                Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
                clsCatZone.strDivision = strdivision;
                clsCatZone.strZone = strNewZone;
                clsCatZone.strCveZone = strNewCveZone;
                clsCatZone.strObservation = strNewObservations;
                bRespost = clsCatZone.NewZone();

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar la zona!";

            }
            else
            {
                sResp = "0-El código o descripción ya existe para otra zona!";
            }
            //return bRespost;
            return sResp;
        }
        public string UpdateZone(string strDivision, string strIdZone, string strCveZone, string strZone, string strObservations)
        {
            Boolean bRespost = false;
            string sResp = "";


            if (!ExistZonaCveID(strDivision, strIdZone, strCveZone, strZone))
            {
                Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
                clsCatZone.strDivision = strDivision;
                clsCatZone.intIdZone = int.Parse( strIdZone);
                clsCatZone.strCveZone = strCveZone;
                clsCatZone.strZone = strZone;
                clsCatZone.strObservation = strObservations;
                clsCatZone.intActivo = 1;
                bRespost = clsCatZone.UpdateZone();           

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar la información de la zona!";

            }
            else
            {
                sResp = "0-La descripción o clave ya existen para otra zona!";
            }
            return sResp;
        }
        public Boolean DeleteZone(string strDivision, string strZone)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
            clsCatZone.strDivision = strDivision;
            clsCatZone.strZone = strZone;
            clsCatZone.intActivo = 0;
            bRespost = clsCatZone.DeleteZone();
            return bRespost;

        }

        public DataTable dtGetZoneByDivision_Dll(string strDivision)
        {
            Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
            DataTable dtZBD;
            clsCatZone.intActivo = 1;
            clsCatZone.strDivision = strDivision;
            
                dtZBD = clsCatZone.GetAllZoneByDivision_DDL();
            
            return dtZBD;

        }

        //
        public Boolean ExistZonaCve(string strDivision, string strCveZone, string strZone)
        {// 
            Boolean bmsg = false;
            DataTable dtresp = dtGetZoneByDivisionAndZoneCve(strDivision, strCveZone, strZone); //
            if (dtresp.Rows.Count > 0) { bmsg = true; }
            return bmsg;
        }

        public Boolean ExistZonaCveID(string strDivision, string strIdZone, string strCveZone, string strZone)
        {// 
            Boolean bmsg = false;            
            Catalogos.CatZone clsCatZone = new Catalogos.CatZone();
            clsCatZone.strDivision = strDivision;
            clsCatZone.intIdZone = int.Parse(strIdZone);
            clsCatZone.strCveZone = strCveZone;
            clsCatZone.strZone = strZone;
            clsCatZone.intActivo= 1;

            DataTable dtresp = clsCatZone.GetAllZoneByDivisionAndCVeID();
            int iExiste = int.Parse(dtresp.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bmsg = true;
            }
            return bmsg;
        }

        private DataTable dtGetZoneByDivisionAndZoneCve(string strDivision, string strCveZone, string strZone) //
        {
            Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
            DataTable dtZBDZ;
            clsCatZone.intActivo = 1;
            clsCatZone.strDivision = strDivision;
            clsCatZone.strCveZone = strCveZone;
            clsCatZone.strZone = strZone;
            dtZBDZ = clsCatZone.GetAllZoneByDivisionAndCVe();
            return dtZBDZ;
        }
                
        public Boolean NewZoneByDiv(string strdivision, string strNewCveZone, string strNewZone, string strNewObservations)
        {
            Boolean bRespost = false;
            if (!string.IsNullOrEmpty(strNewZone))
            {
                Class.Catalogos.CatZone clsCatZone = new Class.Catalogos.CatZone();
                clsCatZone.strDivision = strdivision;
                clsCatZone.strZone = strNewZone;
                clsCatZone.strCveZone = strNewCveZone;
                clsCatZone.strObservation = strNewObservations;
                bRespost = clsCatZone.NewZoneByDiv();
            }
            return bRespost;
        }


    }
}