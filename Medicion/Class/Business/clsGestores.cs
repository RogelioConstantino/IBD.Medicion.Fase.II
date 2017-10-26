using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Medicion.Class.Business
{
    public class clsGestores
    {
        DataTable dtAllG;
        public DataTable dtGetAll()
        {
            Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();
            clsCat.intActivo = 1;

            dtAllG = clsCat.GetAll();

            return dtAllG;
        }

        DataTable dtResponseFilters;

        public string New(string strRol, string strName, string strFirstName, string strLastName, string strNumeroEmpleado, string strIniciales)
        {

            Boolean bRespost = false;
            string sResp = "";

            if (!ExistGestor(strName, strFirstName, strLastName, strNumeroEmpleado, strIniciales))
            {
                Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();

                clsCat.strPuesto = strRol;
                clsCat.strNumeroEmpleado = strNumeroEmpleado;
                clsCat.strName = strName;
                clsCat.strFirstName = strFirstName;
                clsCat.strLastName = strLastName;

                clsCat.strIniciales = strIniciales;

                bRespost = clsCat.New();

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar el gestor!";

            }
            else
            {
                sResp = "0-El nombre, número de empleado o iniciales ya existen para otro gestor!";
            }            
            return sResp;
        }

        public DataTable ValidateFilters(string strDivision, string strZone)
        {
            Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();
            if (string.IsNullOrEmpty(strDivision) && string.IsNullOrEmpty(strZone) || strDivision == "-- TODOS --")
            {   //Return all data
                dtResponseFilters = dtGetAll();
            }                        
            return dtResponseFilters;
        }

        public StringBuilder ReturnHTML(DataTable dtG)
        {
            StringBuilder strDiv = new StringBuilder();
            Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();
            clsCat.dt = dtG;
            strDiv = clsCat.CreateTableHTML();
            return strDiv;
        }
        
        public string Update(string id, string tipo, string strNumeroEmpleado, string strNombre, string strApPaterno, string strApMaterno, string strIniciales)
        {
            Boolean bRespost = false;
            string sResp = "";


            if (!ExistId(id, strNombre, strApPaterno, strApMaterno, strNumeroEmpleado, strIniciales))
            {
                           
                Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();
                
                clsCat.Id = id;
                clsCat.strPuesto = tipo;
                clsCat.strNumeroEmpleado = strNumeroEmpleado;

                clsCat.strName = strNombre;
                clsCat.strFirstName = strApPaterno;
                clsCat.strLastName = strApMaterno;

                clsCat.strIniciales = strIniciales;

                clsCat.intActivo = 1;
                bRespost = clsCat.Update();
                
                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar el gestor!";
                
            }
            else
            {
                sResp = "0-El nombre, número de empleado o iniciales ya existen para otro gestor!";
            } 
            return sResp;
        }

        public Boolean Delete(string strDivision)
        {
            Boolean bRespost = false;
            Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();

            clsCat.Id = strDivision;
            clsCat.intActivo = 0;
            bRespost = clsCat.Delete();
            return bRespost;

        }

        public Boolean Exist(string strName, string strFirstName, string strLastName, string strNumeroEmpleado)
        {
            DataTable dtExist;
            Boolean bRespost = false;
            Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();

            clsCat.strNumeroEmpleado = strNumeroEmpleado;
            clsCat.strName = strName;
            clsCat.strFirstName = strFirstName;
            clsCat.strLastName = strLastName;

            clsCat.intActivo= 1;

            dtExist = clsCat.Exists();

            int iExiste = int.Parse(dtExist.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;
        }
        public Boolean ExistGestor(string strName, string strFirstName, string strLastName, string strNumeroEmpleado, string strIniciales)
        {
            DataTable dtExist;
            Boolean bRespost = false;
            Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();

            clsCat.strNumeroEmpleado = strNumeroEmpleado;
            clsCat.strIniciales = strIniciales;
            clsCat.strName = strName;
            clsCat.strFirstName = strFirstName;
            clsCat.strLastName = strLastName;

            clsCat.intActivo = 1;

            dtExist = clsCat.ExistsGestor();

            int iExiste = int.Parse(dtExist.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;
        }
        public Boolean ExistIniciales(string strIniciales, string strtipo)
        {
            DataTable dtExist;
            Boolean bRespost = false;
            Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();

            clsCat.strIniciales = strIniciales;
            clsCat.strPuesto = strtipo;
            clsCat.intActivo = 1;

            dtExist = clsCat.ExistsIniciales();

            int iExiste = int.Parse(dtExist.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;
        }


        public Boolean ExistId(string strId, string strName, string strFirstName, string strLastName, string strNumeroEmpleado, string strIniciales)
        {
            DataTable dtExist;
            Boolean bRespost = false;
            Class.Catalogos.catGestor clsCat = new Class.Catalogos.catGestor();

            clsCat.Id = strId;
            clsCat.strNumeroEmpleado = strNumeroEmpleado;
            clsCat.strName = strName;
            clsCat.strFirstName = strFirstName;
            clsCat.strLastName = strLastName;

            clsCat.strIniciales = strIniciales;

            clsCat.intActivo = 1;

            dtExist = clsCat.ExistsId();

            int iExiste = int.Parse(dtExist.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;
        }


    }
}