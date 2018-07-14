using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.InstituicaoDomain;

namespace Domain.CursoDomain {

    [Table("CGrade")]
    public class CursoGrade {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Descricao { get; set; }
        public Curso Curso { get; set; }
        public Instituicao Instituicao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}