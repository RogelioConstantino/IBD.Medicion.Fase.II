using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Medicion.Class.Business
{
    public class clsDivision
    {
        DataTable dtAllG;
        public DataTable GetAllDivision()
        {
            Class.Catalogos.CatDivision clsCatGroup = new Class.Catalogos.CatDivision();
            clsCatGroup.Activo = 1;
            dtAllG = clsCatGroup.GetAllGroup();

            return dtAllG;
        }

        public StringBuilder ReturnHTMLDivision(DataTable dtG)
        {
            StringBuilder strDiv = new StringBuilder();
            Class.Catalogos.CatDivision clsCatDivision = new Class.Catalogos.CatDivision();
           clsCatDivision.dtDivision = dtG;
            strDiv = clsCatDivision.CreateTableHTML();
            return strDiv;
        }

        public string NewDivision(string NewCveDivision, string NewDivision)
        {
            Boolean bRespost = false;
            string sResp = "";

            if (!ExistDivision(NewCveDivision, NewDivision))
            {
                Class.Catalogos.CatDivision clsCatDivision = new Class.Catalogos.CatDivision();
                clsCatDivision.CveDivision = NewCveDivision;
                clsCatDivision.Division = NewDivision;
                clsCatDivision.Activo = 0;
                bRespost = clsCatDivision.NewDivision();
                
                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar la División!";

            }
            else
            {
                sResp = "0-La clave o descripción ya existe para otra División!";
            }
            return sResp;
        }
        public string UpdateDivision(int IdDivision, string Division, string CVeDivision)
        {
            Boolean bRespost = false;
            string sResp = "";


            if (!ExistDivisionID(IdDivision, CVeDivision, Division))
            {
                Class.Catalogos.CatDivision clsCatDivision = new Class.Catalogos.CatDivision();
                clsCatDivision.idDivision = IdDivision;
                clsCatDivision.CveDivision = CVeDivision;
                clsCatDivision.Division = Division;
                clsCatDivision.Activo = 1;

                bRespost = clsCatDivision.UpdateDivision();

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar la información de la división!";
                
            }
            else
            {
                sResp = "0-La descripción o clave ya existen para otra división!";
            }
            return sResp;
        }
        public Boolean DeleteDivision(int IdDivision)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatDivision clsCatGroup = new Class.Catalogos.CatDivision();
            clsCatGroup.idDivision = IdDivision;
            clsCatGroup.Activo = 0;
            bRespost = clsCatGroup.DeleteDivision();
            return bRespost;

        }
        public Boolean ExistDivision(string strDivision)
        { 
            Boolean bRespost = false;
            Class.Catalogos.CatDivision oclsCatDivision = new Class.Catalogos.CatDivision();
            oclsCatDivision.CveDivision = strDivision;
            oclsCatDivision.Activo = 1;
            DataTable dtbExist;
            dtbExist = oclsCatDivision.ExistDivision();
            if (dtbExist.Rows.Count > 0) { bRespost = true; }
            return bRespost;
        }
        public Boolean ExistDivision(string strCveDivision, string strDivision)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatDivision oclsCatDivision = new Class.Catalogos.CatDivision();
            oclsCatDivision.CveDivision = strCveDivision;
            oclsCatDivision.Division = strDivision;
            oclsCatDivision.Activo = 1;
            DataTable dtbExist;
            dtbExist = oclsCatDivision.ExistDivisionCve();

            int iExiste = int.Parse(dtbExist.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;
        }
        public Boolean ExistDivisionID(int strIdDivision,string strCveDivision, string strDivision)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatDivision oclsCatDivision = new Class.Catalogos.CatDivision();
            oclsCatDivision.idDivision = strIdDivision;
            oclsCatDivision.CveDivision = strCveDivision;
            oclsCatDivision.Division = strDivision;
            oclsCatDivision.Activo = 1;
            DataTable dtbExist;
            dtbExist = oclsCatDivision.ExistDivisionCveID();

            int iExiste = int.Parse(dtbExist.Rows[0][0].ToString());

            if (iExiste > 0)
            {

                bRespost = true;
            }
            return bRespost;
        }

    }
}