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

    public class NotificacionesRepository : IDisposable
    {
        private NotificacionesContext _ctx;
        private ClienteRepository clienteRepo;

        public Notificacion NotifcacionesModel { get; private set; }

        public NotificacionesRepository()
        {
            _ctx = new NotificacionesContext();
            clienteRepo = new ClienteRepository();
        }



        public List<NotificacionModel> GetNotificaciones()
        {
            var notificacionList = _ctx.Notificacions.OrderByDescending(s => s.NotificacionId).ToList();
            return Notificacion.GetFromEntitie(notificacionList);
        }

        public void Dispose()
        {
            if (_ctx != null)
                _ctx.Dispose();
        }

        public object GetTiposNotificacionesVigenciasAplicaciones()
        {
            var listaDeTipos = new
            {
                TipoNotificacion = new List<KeyValuePar>(),
                TipoVigencia = new List<KeyValuePar>(),
                Aplicaciones = new List<KeyValuePar>()
            };

            listaDeTipos.TipoNotificacion.AddRange(KeyValuePar.ListFrom<Enumeraciones.TiposNotificaciones>());
            listaDeTipos.TipoVigencia.AddRange(KeyValuePar.ListFrom<Enumeraciones.TipoVigencia>());
            listaDeTipos.Aplicaciones.AddRange(clienteRepo.GetClientes().Select(s => new KeyValuePar{ Key = s.Id, Value = s.Name }).ToList());
            return listaDeTipos;
        }

        internal NotificacionModel UpdateNotificacion(NotificacionModel notificacionModel)
        {
            var app = notificacionModel.AplicacionId > 0
                        ? _ctx.Clients.FirstOrDefault(s => s.Id == notificacionModel.AplicacionId)
                        : _ctx.Clients.FirstOrDefault(s => s.Name == notificacionModel.Aplicacion);

            if (app != null)
            {
                notificacionModel.Aplicacion = app.Name;
                notificacionModel.AplicacionId = app.Id;
            }

            notificacionModel.Tipo = KeyValuePar.GetDescriptionFromEnumValue((Enumeraciones.TiposNotificaciones)notificacionModel.IdTipo);
            notificacionModel.TipoVigencia = KeyValuePar.GetDescriptionFromEnumValue((Enumeraciones.TipoVigencia)notificacionModel.IdTipo);


            var oldNotificacion = _ctx.Notificacions.FirstOrDefault(s => s.NotificacionId == notificacionModel.NotificacionId);
            var result = Notificacion.UpdateFromModel(oldNotificacion, notificacionModel);
            _ctx.SaveChanges();
            notificacionModel.NotificacionId = result.NotificacionId;
            saveNotificationLog(result, notificacionModel.Usuarios);
            return notificacionModel;
        }

        internal void DeleteNotificacion(int id)
        {
            var oldNotificacion = _ctx.Notificacions.FirstOrDefault(s => s.NotificacionId == id);
            _ctx.Notificacions.Remove(oldNotificacion);
            _ctx.SaveChanges();
        }

        internal NotificacionModel CreateNotificacion(NotificacionModel notificacionModel)
        {

            notificacionModel.FechaCreacion = DateTime.Now;

            var app = notificacionModel.AplicacionId > 0
                          ? _ctx.Clients.FirstOrDefault(s => s.Id == notificacionModel.AplicacionId)
                          : _ctx.Clients.FirstOrDefault(s => s.Name == notificacionModel.Aplicacion);

            if (app != null)
            {
                notificacionModel.Aplicacion = app.Name;
                notificacionModel.AplicacionId = app.Id;
            }

            notificacionModel.Tipo = KeyValuePar.GetDescriptionFromEnumValue((Enumeraciones.TiposNotificaciones)notificacionModel.IdTipo);
            notificacionModel.TipoVigencia = KeyValuePar.GetDescriptionFromEnumValue((Enumeraciones.TipoVigencia)notificacionModel.IdTipo);

            var result = _ctx.Notificacions.Add(Notificacion.FillEntitie(notificacionModel));
            _ctx.SaveChanges();
            saveNotificationLog(result,notificacionModel.Usuarios);
            notificacionModel.NotificacionId = result.NotificacionId;  
            return notificacionModel;
        }

        private void saveNotificationLog(Notificacion notificacion, List<string> listaUsuarios)
        {
            var oldLogs = _ctx.NotificacionesLogs.Where(s => s.IdNotificacion == notificacion.NotificacionId).ToList();

            var toDeleteList = oldLogs.Where(s => !listaUsuarios.Contains(s.Usuario));

            foreach (var notiToDel in toDeleteList)
            {
                _ctx.NotificacionesLogs.Remove(notiToDel);
            }

            
            
            foreach (var user in listaUsuarios)
            {
                var curUserLog = oldLogs.FirstOrDefault(s => s.Usuario.Equals(user));

                if (curUserLog == null)
                {
                    curUserLog = new NotificacionesLog();
                    curUserLog.IdNotificacion = notificacion.NotificacionId;
                    curUserLog.FechaCreacion = DateTime.Now;
                    _ctx.NotificacionesLogs.Add(curUserLog);
                }

                
                curUserLog.Usuario = user;
                curUserLog.IdTipo = notificacion.IdTipo;
                curUserLog.Tipo = notificacion.Tipo;
                curUserLog.IdTipoVigencia = notificacion.IdTipoVigencia;
                curUserLog.TipoVigencia = notificacion.TipoVigencia;
                curUserLog.Vigencia = notificacion.Vigencia;
                curUserLog.Intervalo = notificacion.Intervalo;
                curUserLog.Titulo = notificacion.Titulo;
                curUserLog.Nota = notificacion.Nota;
                curUserLog.Link = notificacion.Link;
                curUserLog.Activo = notificacion.Activo;
                curUserLog.AplicacionId = notificacion.AplicacionId;
                curUserLog.Aplicacion = notificacion.Aplicacion;
            }

            _ctx.SaveChanges();

        }

        public List<NotificacionesLogModel> GetNotificacionesLogs(int idNotificacion)
        {
            var notiLogsList =  _ctx.NotificacionesLogs.Where(s => s.IdNotificacion == idNotificacion).ToList();
            return NotificacionesLog.GetModelFromEntity(notiLogsList);
        }

        internal List<NotificacionesLogModel> GetNotificacionesLogs(string userName, bool soloActivas = true)
        {
            var notiLog =
                _ctx.NotificacionesLogs
                    .Where(s => s.Usuario.Equals(userName)
                                && (!soloActivas || s.Activo)).ToList();

            return NotificacionesLog.GetModelFromEntity(notiLog);

        }

        internal void CerrarNotificacion(int idNotificacionLog)
        {
            var notiLog = _ctx.NotificacionesLogs.FirstOrDefault(s => s.IdNotificacionLog == idNotificacionLog);
            if (notiLog != null && notiLog.Activo)
            {
                notiLog.Activo = false;
                notiLog.FechaCierre = DateTime.Now;
                notiLog.MostradoUltimaVez = DateTime.Now;
                _ctx.SaveChanges();
            }
        }


        internal void RegistrarLecturaNotificacion(int idNotificacionLog, bool cerrar = false)
        {
            var notiLog = _ctx.NotificacionesLogs.FirstOrDefault(s => s.IdNotificacionLog == idNotificacionLog);
            notiLog.Contador += 1;

            if(!notiLog.MostradoPrimeraVez.HasValue)
            {
                notiLog.MostradoPrimeraVez = DateTime.Now;
                notiLog.MostradoUltimaVez = DateTime.Now;
            }
            
            if(cerrar)
            {
                notiLog.Activo = false;
                notiLog.FechaCierre = DateTime.Now;
                notiLog.MostradoUltimaVez = DateTime.Now;
            }

            _ctx.SaveChanges();
        }
    }

}