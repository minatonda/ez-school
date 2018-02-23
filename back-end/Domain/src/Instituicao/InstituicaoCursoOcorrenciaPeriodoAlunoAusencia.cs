using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.InstituicaoDomain {

    public class InstituicaoCursoOcorrenciaPeriodoAlunoAusencia : IBaseModel {

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoAluno InstituicaoCursoOcorrenciaPeriodoAluno { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoProfessor InstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        public DateTime DataAusencia { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }

}