using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.CursoDomain;

namespace Domain.InstituicaoDomain {

    [Table("ICurso")]
    public class InstituicaoCurso : IBaseModel {

        public InstituicaoCurso() {

        }

        [Key]
        public long ID { get; set; }
        public Instituicao Instituicao { get; set; }
        public Curso Curso { get; set; }
        public CursoGrade CursoGrade { get; set; }
        public DateTime? Ativo { get; set; } = null;

        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}