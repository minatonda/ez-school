using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.UsuarioDomain {

    [Table("Usuario")]
    public class Usuario : IBaseModel {
        public Usuario () {
            this.ID = Guid.NewGuid ().ToString ();
        }

        [Key]
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UsuarioInfo UsuarioInfo { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}