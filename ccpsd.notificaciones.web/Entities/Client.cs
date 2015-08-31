using ccpsd.notificaciones.core;
using ccpsd.notificaciones.web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ccpsd.notificaciones.web.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int Id { get; set; }
     
        [Required]
        public string Secret { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        public ApplicationTypes ApplicationType { get; set; }

        [Required]
        public bool Active { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        [MaxLength(256)]
        public string ImgName { get; set; }

        [MaxLength(256)]
        public string AllowedOrigin { get; set; }

        public static ClientModel GetFromEntitie(Client cliente)
        {
            return new ClientModel
            {
                Id = cliente.Id,
                Name = cliente.Name,
                Secret = cliente.Secret,
                Description = cliente.Description,
                Active = cliente.Active,
                ImgName = cliente.ImgName,
                AllowedOrigin = cliente.AllowedOrigin,
                ApplicationType = cliente.ApplicationType,
                RefreshTokenLifeTime = cliente.RefreshTokenLifeTime,
                RefreshKey = false
            };
        }

        public static Client FillClient(ClientModel clienteModel)
        {
            return new Client
            {
                Name = clienteModel.Name,
                //Secret = clienteModel.Secret,
                Description = clienteModel.Description,
                Active = clienteModel.Active,
                ImgName = clienteModel.ImgName,
                AllowedOrigin = clienteModel.AllowedOrigin,
                ApplicationType = clienteModel.ApplicationType,
                RefreshTokenLifeTime = clienteModel.RefreshTokenLifeTime
            };
        }

        internal static List<ClientModel> GetFromEntitie(List<Client> clientList)
        {
            var listClientModel = new List<ClientModel>();
            clientList.ForEach(s =>
            {
                listClientModel.Add(Client.GetFromEntitie(s));
            });

            return listClientModel;
        }

    }
}