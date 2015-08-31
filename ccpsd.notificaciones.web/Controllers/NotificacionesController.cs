
using ccpsd.notificaciones.core;
using ccpsd.notificaciones.web.Infrastructure;
using ccpsd.notificaciones.web.Models;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApiAungularWithPushNoti.Controllers;

namespace ccpsd.notificaciones.web.Controllers
{
    [RoutePrefix("api/Notificaciones")]
    public class NotificacionesController : ApiControllerWithHub
    {
     
        public NotificacionesController()
        {
            _repoNotificaciones = new NotificacionesRepository();
        }

        [Authorize]
        [Route("Get")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_repoNotificaciones.GetNotificaciones());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [Route("GetDomainUsers")]
        public IHttpActionResult GetDomainUsers()
        {
            try
            {
                return Ok(Utils.GetDomainUsers().Select(s => s.UserName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Authorize]
        [Route("Create")]
        public IHttpActionResult Create([FromBody] NotificacionModel notificacionModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }


            try
            {
                var createdNoti = _repoNotificaciones.CreateNotificacion(notificacionModel);

                DispachNotification(createdNoti.NotificacionId.Value);
                return Ok(createdNoti);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void updateAppField(NotificacionModel notificacionModel)
        {
            var _repoCliente = new ClienteRepository();
            var cliente = _repoCliente.FindClient(notificacionModel.Aplicacion);
            if (cliente != null)
                notificacionModel.AplicacionId = cliente.Id;
        }

        [Authorize]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] NotificacionModel notificacionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var updatedNoti = _repoNotificaciones.UpdateNotificacion(notificacionModel);
                
                DispachNotification(updatedNoti.NotificacionId.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                _repoNotificaciones.DeleteNotificacion(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Authorize]
        [Route("GetTiposNotificacionesVigenciasAplicaciones")]
        public IHttpActionResult GetTiposNotificacionesVigenciasAplicaciones()
        {
            try
            {
                return Ok(_repoNotificaciones.GetTiposNotificacionesVigenciasAplicaciones());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
