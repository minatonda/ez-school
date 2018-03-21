using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.CursoDomain {

    public class Curso : IBaseModel {

        public Curso() {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}