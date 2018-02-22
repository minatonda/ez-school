using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.UsuarioDomain {
    public class Usuario : IBaseModel {
        public Usuario () {
            this.ID = Guid.NewGuid ().ToString ();
        }

        [Key]
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UsuarioInfo UsuarioInfo { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}