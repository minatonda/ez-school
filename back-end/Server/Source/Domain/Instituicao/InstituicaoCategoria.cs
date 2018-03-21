using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.InstituicaoDomain {

    public class InstituicaoCategoria : IBaseModel {
    
        public InstituicaoCategoria () {

        }

        [Key]
        [DatabaseGeneratedAttribute (DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime? Ativo { get; set; } = null;
    }
}