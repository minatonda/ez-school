using System;
using Api.Common.Base;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM : BaseVM {

        public InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM() {

        }

        public TimeSpan Inicio { get; set; }
        public TimeSpan Fim { get; set; }
        public DayOfWeek Dia { get; set; }

    }
}