using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.UsuarioDomain;

namespace Domain.InstituicaoDomain {

    [Table("ICPerfil")]
    public class InstituicaoColaboradorPerfil : IBaseModel {

        public InstituicaoColaboradorPerfil() {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Nome { get; set; }
        public string Roles { get; set; }
        public Instituicao Instituicao { get; set; }
        public DateTime? Ativo { get; set; } = null;
    }
}