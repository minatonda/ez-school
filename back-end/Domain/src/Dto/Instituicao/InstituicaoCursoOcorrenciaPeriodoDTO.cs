using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;
using Domain.Models.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Dto {
    public class InstituicaoCursoOcorrenciaPeriodoDto {

        public InstituicaoCursoOcorrenciaPeriodoDto() {
            this.Alunos = new List<Aluno>();
            this.Professores = new List<InstituicaoCursoOcorrenciaProfessorDto>();
        }

        public InstituicaoCursoOcorrenciaPeriodoDto(InstituicaoCursoPeriodo instituicaoCursoPeriodo, List<Aluno> alunos, List<InstituicaoCursoOcorrenciaMateriaProfessor> professores) {
            this.Periodo = new InstituicaoCursoPeriodoDto(instituicaoCursoPeriodo);
            this.Alunos = alunos;
            this.Professores = professores.Select(x => new InstituicaoCursoOcorrenciaProfessorDto(x.InstituicaoCursoOcorrenciaMateria, x)).ToList();
        }

        public InstituicaoCursoPeriodoDto Periodo { get; set; }
        public List<Aluno> Alunos { get; set; }
        public List<InstituicaoCursoOcorrenciaProfessorDto> Professores { get; set; }


    }
}