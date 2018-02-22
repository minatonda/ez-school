using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.InstituicaoDomain {

    public class InstituicaoInstituicaoCategoria {

        public InstituicaoInstituicaoCategoria() {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Key]
        public Instituicao Instituicao { get; set; }
        [Key]
        public InstituicaoCategoria InstituicaoCategoria { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}