using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.UsuarioDomain;

namespace Domain.InstituicaoDomain {

    [Table("IColaborador")]
    public class InstituicaoColaborador : IBaseModel {

        public InstituicaoColaborador() {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public UsuarioInfo Usuario { get; set; }
        public Instituicao Instituicao { get; set; }
        public string Perfis { get; set; }
        public DateTime? Ativo { get; set; } = null;
    }
}