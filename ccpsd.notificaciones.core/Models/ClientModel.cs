using ccpsd.notificaciones.core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ccpsd.notificaciones.core
{
    public class ClientModel
    {
            
        public int? Id { get; set; }

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

        public bool RefreshKey { get; set; }

        [MaxLength(256)]
        public string AllowedOrigin { get; set; }

      
    }
}