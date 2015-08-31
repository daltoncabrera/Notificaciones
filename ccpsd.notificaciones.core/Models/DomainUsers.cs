using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ccpsd.notificaciones.core
{
    public class DomainUsers
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; internal set; }
    }
}