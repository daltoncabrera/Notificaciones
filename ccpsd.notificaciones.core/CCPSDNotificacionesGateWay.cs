using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccpsd.notificaciones.core
{
    public class CCPSDNotificacionesGateWay
    {

        const string app = "someuser@someemail.com";
        const string secret = "Password1!";
        const string apiBaseUri = "http://localhost:18342";
        const string apiNotiPath = "/api/people";

        public string AppKey { get; set; }
        public string AppName { get; set; }
        public string ServerUri { get; set; }
        
        public CCPSDNotificacionesGateWay()
        {

        }

        public CCPSDNotificacionesGateWay(string appName, string appKey, string  serverUri)
        {
            this.AppName = appName;
            this.AppKey = appKey;
            this.ServerUri = serverUri;
        }

        


    }
}
