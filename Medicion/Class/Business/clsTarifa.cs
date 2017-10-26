using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Medicion.Class.Business
{
    public class clsTarifa
    {
        DataTable dtAllG;
        public Boolean ExistTarifa(string strCveTarifa)
        {
            Boolean bRespost = false;
            Class.Catalogos.CatTarifa oclsCat = new Class.Catalogos.CatTarifa();
            oclsCat.CveTarifa = strCveTarifa;
            oclsCat.Activo = 1;
            DataTable dtbExist;
            dtbExist = oclsCat.Exist();
            if (dtbExist.Rows.Count > 0) { bRespost = true; }
            return bRespost;
        }
    }
}