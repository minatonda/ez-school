using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.UsuarioDomain {

    [Table("Aluno")]
    public class Aluno : IBaseModel {

        public Aluno () { }
        public Aluno (UsuarioInfo usuarioInfo) {
            this.ID = usuarioInfo.ID;
            this.UsuarioInfo = usuarioInfo;
        }

        [Key]
        public string ID { get; set; }
        public UsuarioInfo UsuarioInfo { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}