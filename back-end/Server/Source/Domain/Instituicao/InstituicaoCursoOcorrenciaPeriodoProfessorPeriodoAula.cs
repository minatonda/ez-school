using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.InstituicaoDomain {
    public class InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula : IBaseModel {

        public InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula() {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoProfessor InstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        public DayOfWeek Dia { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fim { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}