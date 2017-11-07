using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models
{
    public class InstituicaoCurso : IBaseModel
    {

        public InstituicaoCurso()
        {

        }

        [Key]
        public long ID { get; set; }
        public Instituicao Instituicao { get; set; }
        public Curso Curso { get; set; }
        public CursoGrade CursoGrade { get; set; }
        public DateTime? Ativo { get; set; } = null;

        public DateTime DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}