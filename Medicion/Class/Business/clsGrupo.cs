using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Medicion.Class.ADO;
using Medicion.Class.LogError;
using Medicion.Class.Business;
using System.Text;
namespace Medicion.Class.Business

{
    public class clsGrupo
    {
        DataTable dtAllG;
        public DataTable GetAllGroups(int GMedicion, int GComercial)
        {
            Class.Catalogos.CatGroup clsCatGroup = new Class.Catalogos.CatGroup();
            clsCatGroup.Activo = 1;
            dtAllG = clsCatGroup.GetAllGroup(GMedicion,GComercial);

            return dtAllG;
        }
 
        public StringBuilder ReturnHTMLGroup( DataTable dtG) 
        {
            StringBuilder strG = new StringBuilder();
            Class.Catalogos.CatGroup clsCatGroup = new Class.Catalogos.CatGroup();
            clsCatGroup.dtGroup = dtG;
            strG =clsCatGroup.CreateTableHTML();
            return strG;
        }

        public String UpdateGroup(int IdGroup, string Group, string InicioOperacioens,int IdMed, int IdComer)
        { 
            Boolean bRespost = false;
            string sResp = "";
            if (!ExistGroupID(IdGroup.ToString(), Group))
            {
                    Class.Catalogos.CatGroup clsCatGroup = new Class.Catalogos.CatGroup();
                clsCatGroup.idGrupo = IdGroup;
                clsCatGroup.Grupo = Group;
                clsCatGroup .InicioOperaciones = InicioOperacioens;
                clsCatGroup.Activo = 1;
                clsCatGroup.IdMed = IdMed;
                clsCatGroup.IdComer = IdComer;
                bRespost =clsCatGroup.UpdateGroup();

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar la información de la división!";
            }
            else
            {
                sResp = "0-La descripción ya existe para otro Grupo!";
            }
            return sResp;
        }

        public Boolean DeleteGroup(int IdGroup)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatGroup clsCatGroup = new Class.Catalogos.CatGroup();
            clsCatGroup.idGrupo = IdGroup;
            clsCatGroup.Activo = 0;
            bRespost = clsCatGroup.DeleteGroup();
            return bRespost;

        }
        public string NewGroup(string NewGroup, string NewInicioOperaciones ,int IdMed,int IdComer)
        {
            Boolean bRespost = false;
            string sResp = "";

            if (!ExistGroup(NewGroup))
            {
                Class.Catalogos.CatGroup clsCatGroup = new Class.Catalogos.CatGroup();
                clsCatGroup.Grupo = NewGroup;
                clsCatGroup.InicioOperaciones = NewInicioOperaciones;
                clsCatGroup.Activo = 0;
                clsCatGroup.IdMed = IdMed;
                clsCatGroup.IdComer = IdComer;
                bRespost = clsCatGroup.NewGroup();
            
                if (bRespost)
                    sResp = "1-La información se guardo exitosamente!";
                else
                    sResp = "0-Ocurrio un error al intentar guardar el grupo!";

            }
            else
            {
                sResp = "0-La clave o descripción ya existe para otro grupo!";
            }
            
            return sResp; 
        }

        public Boolean ExistGroup(string strGroup)
        {

            DataTable dtExistGroup;
            Boolean bRespost = false;
            Class.Catalogos.CatGroup clsCatGroup = new Class.Catalogos.CatGroup();
            clsCatGroup.Grupo = strGroup;
            clsCatGroup.Activo = 1;

            dtExistGroup = clsCatGroup.ExistsGroup();

            int iExiste = int.Parse(dtExistGroup.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;

        }

        public Boolean ExistGroupID(string strIdGroup, string strGroup)
        {

            DataTable dtExistGroup;
            Boolean bRespost = false;
            Class.Catalogos.CatGroup clsCatGroup = new Class.Catalogos.CatGroup();
            clsCatGroup.Grupo = strGroup;
            clsCatGroup.idGrupo = int.Parse(strIdGroup);
            clsCatGroup.Activo = 1;

            dtExistGroup = clsCatGroup.ExistsGroupID();

            int iExiste = int.Parse(dtExistGroup.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;

        }


    }
}