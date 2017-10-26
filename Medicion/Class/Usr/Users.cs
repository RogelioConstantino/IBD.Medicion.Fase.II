using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Medicion.Class

{
    public class Users
      {
        public string UserName { get;set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public Boolean Active { get; set; }

     }

    
}