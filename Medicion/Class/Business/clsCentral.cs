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
    public class clsCentral
    {
        DataTable dtAllG;
        public DataTable GetAllCentrales()
        {
            Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();
            clsCatCentral.Activo = 1;
            dtAllG = clsCatCentral.GetAllCentral();

            return dtAllG;
        }
        public DataTable GetAllConvenioByCentral(string central)
        {
            DataTable dtAllG2;
            Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();
            clsCatCentral.Activo = 1;
            dtAllG2 = clsCatCentral.GetAllConvenio(central);

            return dtAllG2;
        }
        public DataTable GetAllCentralesPrelacion()
        {
            Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();
            clsCatCentral.Activo = 1;
            dtAllG = clsCatCentral.GetAllCentralPrelacion();

            return dtAllG;
        }
        public StringBuilder ReturnHTMLCentral(DataTable dtG)
        {
            StringBuilder strG = new StringBuilder();
            Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();
            clsCatCentral.dtCentral = dtG;
            strG = clsCatCentral.CreateTableHTML();
            return strG;
        }

        public string UpdateCentral(int IdCentral, string CodeCentral, string Central)
        {
            Boolean bRespost = false;
            string sResp = "";


            if (!ExistCentralID( IdCentral.ToString(), CodeCentral, Central))
            {

                Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();
                clsCatCentral.idCentral = IdCentral;
                clsCatCentral.CodCentral = CodeCentral;
                clsCatCentral.Central = Central;
                clsCatCentral.Activo = 1;

                bRespost = clsCatCentral.UpdateCentral();            

                if (bRespost)
                    sResp = "1-La información se guardo exitosamente";
                else
                    sResp = "0-Ocurrio un error al intentar guardar el la central";

            }
            else
            {
                sResp = "0-El código o descripción ya existe para otra central";
            }
            return sResp;
        }

        public Boolean DeleteCentral(int IdCentral)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();
            clsCatCentral.idCentral = IdCentral;
            clsCatCentral.Activo = 0;
            bRespost = clsCatCentral.DeleteCentral();
            return bRespost;

        }
        public string  NewCentral(string NewCodeCentral, string NewCentral)
        {
            Boolean bRespost = false;
            string sResp = "";

            if (!ExistCentral(NewCodeCentral, NewCentral))
            {
                Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();

                clsCatCentral.Central = NewCentral;
                clsCatCentral.CodCentral = NewCodeCentral;
                clsCatCentral.Activo = 0;

                bRespost = clsCatCentral.NewCentral();

                if (bRespost )
                    sResp = "1-La información se guardo exitosamente";
                else
                    sResp = "0-Ocurrio un error al intentar guardar la central";

            }
            else
            {
                sResp = "0-El código o descripción ya existe para otra central";
            }
            //return bRespost;
            return sResp;            
        }

        public Boolean ExistCentral(string strCodCentral, string strCentral)
        {
            DataTable dtExistCentral;
            Boolean bRespost = false;
            Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();

            clsCatCentral.CodCentral = strCodCentral;
            clsCatCentral.Central = strCentral;
            clsCatCentral.Activo = 1;

             dtExistCentral = clsCatCentral.ExistsCentral();

            int iExiste = int.Parse(dtExistCentral.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;
        }

        public Boolean ExistCentralID(string strId, string strCodCentral, string strCentral)
        {
            DataTable dtExistCentral;
            Boolean bRespost = false;
            Class.Catalogos.CatCentral clsCatCentral = new Class.Catalogos.CatCentral();

            clsCatCentral.idCentral = int.Parse(strId);

            clsCatCentral.CodCentral = strCodCentral;
            clsCatCentral.Central = strCentral;
            clsCatCentral.Activo = 1;

            dtExistCentral = clsCatCentral.ExistsCentralID();

            int iExiste = int.Parse(dtExistCentral.Rows[0][0].ToString());

            if (iExiste > 0)
            {
                bRespost = true;
            }
            return bRespost;
        }

    }
}
