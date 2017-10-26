using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace Medicion.Class.Business
{
    public class clsConvenios
    {
        DataTable dtPorCentral;
        public DataTable GetPorCentrales(string IdCentral)
        {
            Class.Catalogos.CatConvenios clsCat = new Class.Catalogos.CatConvenios();
            clsCat.Activo = 1;
            clsCat.idCentral = int.Parse( IdCentral);
            dtPorCentral = clsCat.GeConveniosPorCentral();

            return dtPorCentral;
        }
        public DataTable GetPorIdConvenio(int IdConvenio)
        {
            Class.Catalogos.CatConvenios clsCat = new Class.Catalogos.CatConvenios();
            clsCat.Activo = 1;
            clsCat.idConvenio = IdConvenio;
            dtPorCentral = clsCat.GeConveniosPorConvenio();

            return dtPorCentral;
        }

        public Boolean update(int IdConvenio ,string convenio, string descripcion, string Estatus ) {

            Class.Catalogos.CatConvenios clsCat = new Class.Catalogos.CatConvenios();

            clsCat.idConvenio = IdConvenio;
            clsCat.Descripción = descripcion;
            clsCat.Estatus = Estatus;
            clsCat.Convenio = convenio;
            
            return clsCat.Update();

        }


        public StringBuilder ReturnHTMLConvenios(DataTable dtG)
        {
            StringBuilder strG = new StringBuilder();
            Class.Catalogos.CatConvenios clsCat = new Class.Catalogos.CatConvenios();
            clsCat.dtConvenio = dtG;
            strG = clsCat.CreateTableHTML();
            return strG;
        }


        public DataTable GetEstatus()
        {
            Class.Catalogos.CatConvenios clsCat = new Class.Catalogos.CatConvenios();
            clsCat.Activo = 1;            
            dtPorCentral = clsCat.GeEstatus();

            return dtPorCentral;
        }


    }

}