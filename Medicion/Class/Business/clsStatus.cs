using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Medicion.Class.Business
{
    public class clsStatus
    {
        DataTable dtAllG;
        public DataTable GetAllStatus()
        {
            Class.Catalogos.CatStatus clsCatStatus = new Class.Catalogos.CatStatus();
            clsCatStatus.Activo = 1;
            dtAllG = clsCatStatus.GetAllStatus();

            return dtAllG;
        }
        public StringBuilder ReturnHTMLDivision(DataTable dtG)
        {
            StringBuilder strStat = new StringBuilder();
            Class.Catalogos.CatStatus clsCatDivision = new Class.Catalogos.CatStatus();
            clsCatDivision.dtStatus = dtG;
            strStat = clsCatDivision.CreateTableHTML();
            return strStat;
        }
        public string NewStatus( string NewCveStatus, string NewStatus)
        {
            Boolean bRespost = false;
            string sResp = "";

            if (!ExistCve( NewCveStatus, NewStatus))
            {
                Class.Catalogos.CatStatus clsCatStatus = new Class.Catalogos.CatStatus();
                clsCatStatus.Status = NewStatus;
                clsCatStatus.Cve= NewCveStatus;
                clsCatStatus.Activo = 0;

                bRespost = clsCatStatus.NewStatus();

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar el estatus!";
            }
            else
            {
                sResp = "0-La clave o descripción ya existe para otro estatus!";
            }
            //return bRespost;
            return sResp; 
        }
        public String UpdateStatus(int IdStatus, string Status, string Cve)
        {
            Boolean bRespost = false;
            string sResp = "";


            if (!ExistCveUpdate(IdStatus.ToString(), Cve, Status))
            {
                Class.Catalogos.CatStatus clsCatStatus = new Class.Catalogos.CatStatus();
            clsCatStatus.idStatus = IdStatus;
            clsCatStatus.Status = Status;
            clsCatStatus.Activo = 1;
            bRespost = clsCatStatus.UpdateStatus();

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar el gestor!";

            }
            else
            {
                sResp = "0-El nombre o número de empleado ya existe para otro gestor!";
            }
            return sResp;
        }
        public Boolean DeleteStatus(int IdStatus)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatStatus clsCatGroup = new Class.Catalogos.CatStatus();
            clsCatGroup.idStatus = IdStatus;
            clsCatGroup.Activo = 0;
            bRespost = clsCatGroup.DeleteStatus();
            return bRespost;

        }

        public Boolean ExistCve( string strCve, string strEstatus)
        {
            DataTable dtExist;
            Boolean bRespost = false;
            Class.Catalogos.CatStatus clsCat = new Class.Catalogos.CatStatus();

            clsCat.Cve = strCve;
            clsCat.Status = strEstatus;            
            clsCat.Activo = 1;

            dtExist = clsCat.ExistsCve();

            int iExiste = int.Parse(dtExist.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;
        }
        public Boolean ExistCveUpdate(string strIdEstatus, string strCve, string strEstatus)
        {
            DataTable dtExist;
            Boolean bRespost = false;
            Class.Catalogos.CatStatus clsCat = new Class.Catalogos.CatStatus();

            clsCat.Cve = strCve;
            clsCat.Status = strEstatus;
            clsCat.idStatus = int.Parse(strIdEstatus);
            clsCat.Activo = 1;

            dtExist = clsCat.ExistsCve_Update();

            int iExiste = int.Parse(dtExist.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;

        }

    }
}