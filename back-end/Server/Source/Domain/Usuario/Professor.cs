using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.UsuarioDomain {

    public class Professor : IBaseModel {
        
        public Professor () { }
        public Professor (UsuarioInfo usuarioInfo) {
            this.ID = usuarioInfo.ID;
            this.UsuarioInfo = usuarioInfo;
        }

        [Key]
        public string ID { get; set; }
        public UsuarioInfo UsuarioInfo { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}