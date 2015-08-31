using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ccpsd.notificaciones.web
{
    public class UserConnection
    {  
        public UserConnection()
        {
            ConnectionIds = new List<string>();
        }
           public string Name { get; set; }
           public List<string> ConnectionIds { get; set; }
    }
}