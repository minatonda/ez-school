using System;
using Api.Common.ViewModels;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM : SelectVM {

        public InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM() {

        }

        public string Inicio { get; set; }
        public string Fim { get; set; }
        public DayOfWeek Dia { get; set; }

    }
}