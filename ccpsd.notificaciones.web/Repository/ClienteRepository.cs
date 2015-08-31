using ccpsd.notificaciones.core;
using ccpsd.notificaciones.web.Entities;
using ccpsd.notificaciones.web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;


namespace ccpsd.notificaciones.web.Infrastructure
{

    public class ClienteRepository : IDisposable
    {
        private NotificacionesContext _ctx;


        public ClienteRepository()
        {
            _ctx = new NotificacionesContext();
        }

        public async Task<ClientModel> CreateCliente(ClientModel clienteModel)
        {

            var oCliente = new Client
            {
                Name = clienteModel.Name,
                Description = clienteModel.Description,
                Secret = Helper.GetHash(Guid.NewGuid().ToString()),
                AllowedOrigin = clienteModel.AllowedOrigin,
                Active = clienteModel.Active,
                ApplicationType = clienteModel.ApplicationType,
                ImgName = clienteModel.ImgName
            };

            var result = _ctx.Clients.Add(oCliente);
            var save = await _ctx.SaveChangesAsync();
            return Client.GetFromEntitie(result);
        }

        public async Task<ClientModel> UpdateCliente(ClientModel clienteModel)
        {
            Client oCliente = _ctx.Clients.FirstOrDefault(s => s.Id == clienteModel.Id);
            oCliente.Name = clienteModel.Name;
            oCliente.Description = clienteModel.Description;
            oCliente.AllowedOrigin = clienteModel.AllowedOrigin;
            oCliente.Active = clienteModel.Active;
            oCliente.ApplicationType = clienteModel.ApplicationType;
            oCliente.ImgName = clienteModel.ImgName;
            if(clienteModel.RefreshKey)
            {
                oCliente.Secret = Helper.GetHash(Guid.NewGuid().ToString());
            }

            var save = await _ctx.SaveChangesAsync();
            return Client.GetFromEntitie(oCliente);
        }



        public async Task<int> DeleteCliente(int id)
        {
            Client oCliente = _ctx.Clients.FirstOrDefault(s => s.Id == id);
            _ctx.Clients.Remove(oCliente);
            return  await _ctx.SaveChangesAsync();
        }



        public List<ClientModel> GetClientes()
        {
            var clientList = _ctx.Clients.OrderByDescending(s => s.Id).ToList();
            return Client.GetFromEntitie(clientList);
        }

        public Client FindClient(string nombre)
        {
            var client = _ctx.Clients.FirstOrDefault(s => s.Name == nombre);
            return client;
        }

        public void Dispose()
        {
            if (_ctx != null)
                _ctx.Dispose();
        }

        internal bool ValidateClientByKey(string key)
        {
            return _ctx.Clients.Any(s => s.Secret.Equals(key) && s.Active);
        }

        internal List<KeyValuePar> GetTiposAplicaciones()
        {
            return KeyValuePar.ListFrom<ApplicationTypes>();
        }

  
    }
}