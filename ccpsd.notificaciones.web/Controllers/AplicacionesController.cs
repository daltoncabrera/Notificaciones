using ccpsd.notificaciones.core;
using ccpsd.notificaciones.web.Entities;
using ccpsd.notificaciones.web.Infrastructure;
using ccpsd.notificaciones.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace ccpsd.notificaciones.web.Controllers
{
    
    [RoutePrefix("api/Aplicaciones")]
    public class AplicacionesController : ApiController
    {
        ClienteRepository _repoCLiente;

        public AplicacionesController()
        {
            _repoCLiente = new ClienteRepository();
        }

        [Authorize]
        [Route("Get")]
        public IHttpActionResult Get()
        {   
            return Ok(_repoCLiente.GetClientes());
        }


        [Authorize]
        [Route("Create")]
        public async Task<IHttpActionResult> Create([FromBody]ClientModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                var createdCli = await _repoCLiente.CreateCliente(clienteModel);
                return Ok(createdCli);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [Authorize]
        [Route("Update")]
        public async Task<IHttpActionResult> Update([FromBody] ClientModel clienteModel)
        {          
            if (ModelState.IsValid)
            {
                var updatedCli = await _repoCLiente.UpdateCliente(clienteModel);
                return Ok(updatedCli);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [Authorize]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete(int id)
        { 
             var result = await _repoCLiente.DeleteCliente(id);
            return Ok(result);
        }

        [Authorize]
        [Route("GetTiposAplicaciones")]
        public IHttpActionResult GetTiposAplicaciones()
        {
            try
            {
                return Ok(_repoCLiente.GetTiposAplicaciones());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
