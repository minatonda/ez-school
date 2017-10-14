using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;
using Domain.Models.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Dto {
    public class InstituicaoCursoOcorrenciaDto {

        public InstituicaoCursoOcorrenciaDto() {

        }

        public InstituicaoCursoOcorrenciaDto(InstituicaoCursoOcorrencia ocorrencia, Professor coordenador, List<InstituicaoCursoOcorrenciaAluno> alunos, List<InstituicaoCursoOcorrenciaMateriaProfessor> professores) : this(ocorrencia, coordenador) {
            var periodos = alunos.Select(x => x.Periodo).ToList().Concat(professores.Select(x => x.Periodo).ToList()).GroupBy(x => x.ID).Select(x => x.First()).ToList();
            this.Periodos = periodos.GroupBy(x => x.ID)
            .Select(x => x.First())
            .ToList()
            .Select(x => new InstituicaoCursoOcorrenciaPeriodoDto(x, alunos.Where(y => y.Periodo.ID == x.ID).Select(y => y.Aluno).ToList(), professores.Where(y => y.Periodo.ID == x.ID).ToList())).ToList();
        }

        public InstituicaoCursoOcorrenciaDto(InstituicaoCursoOcorrencia ocorrencia, Professor coordenador) {
            this.ID = ocorrencia.ID;
            this.DataInicio = ocorrencia.DataInicio;
            this.DataExpiracao = ocorrencia.DataExpiracao;
            this.Coordenador = coordenador;
        }

        public long ID { get; set; }
        public Professor Coordenador { get; set; }
        public List<InstituicaoCursoOcorrenciaPeriodoDto> Periodos { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}