using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;
using Domain.Models.Interface;

namespace Domain.Dto {
    public class InstituicaoCursoDto {

        public InstituicaoCursoDto() {
            this.Periodos = new List<InstituicaoCursoPeriodoDto>();
            this.Turmas = new List<InstituicaoCursoTurmaDto>();
        }

        public InstituicaoCursoDto(InstituicaoCurso instituicaoCurso, List<InstituicaoCursoPeriodo> periodos, List<InstituicaoCursoTurma> turmas, List<CursoGradeMateria> cursoGradeMaterias) {
            this.ID = instituicaoCurso.ID;
            this.DataInicio = instituicaoCurso.DataInicio;
            this.DataExpiracao = instituicaoCurso.DataExpiracao;
            this.Curso = instituicaoCurso.Curso;

            if (instituicaoCurso.CursoGrade != null && cursoGradeMaterias != null) {
                this.CursoGrade = new CursoGradeDto(instituicaoCurso.CursoGrade, cursoGradeMaterias);
            }
            if (periodos != null) {
                this.Periodos = periodos.Select(x => new InstituicaoCursoPeriodoDto(x)).ToList();
            }
            if (turmas != null) {
                this.Turmas = turmas.Select(x => new InstituicaoCursoTurmaDto(x)).ToList();
            }
        }

        public long ID { get; set; }
        public Curso Curso { get; set; }
        public CursoGradeDto CursoGrade { get; set; }
        public List<InstituicaoCursoPeriodoDto> Periodos { get; set; }
        public List<InstituicaoCursoTurmaDto> Turmas { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}