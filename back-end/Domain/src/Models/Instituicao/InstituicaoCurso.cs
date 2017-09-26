using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCurso : IBaseModel {

        public InstituicaoCurso (Instituicao instituicao, Curso curso, CursoGrade cursoGrade) {
            this.Instituicao = instituicao;
            this.Curso = curso;
            this.CursoGrade = cursoGrade;
        }

        [Key]
        public string ID { get; set; }
        public Instituicao Instituicao { get; set; }
        public Curso Curso { get; set; }
        public CursoGrade CursoGrade { get; set; }
        public bool Ativo { get; set; } = true;

        public DateTime DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}