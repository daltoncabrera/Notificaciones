using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ccpsd.notificaciones.core;
using WebApiAungularWithPushNoti;
using ccpsd.notificaciones.web;

namespace WebApiAungularWithPushNoti.Controllers
{
    public class SignalRPruebaController : ApiControllerWithHub // ApiController
    {   

        protected override void Dispose(bool disposing)
        {
      
            base.Dispose(disposing);
        }

    
    }
}