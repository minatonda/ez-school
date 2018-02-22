using System;
using System.Collections.Generic;
using Api.Common.ViewModels;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoOcorrenciaPeriodoVM : SelectVM {

        public InstituicaoCursoOcorrenciaPeriodoVM() {
            this.InstituicaoCursoOcorrenciaPeriodoProfessores = new List<InstituicaoCursoOcorrenciaPeriodoProfessorVM>();
            this.InstituicaoCursoOcorrenciaPeriodoAlunos = new List<InstituicaoCursoOcorrenciaPeriodoAlunoVM>();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessorVM> InstituicaoCursoOcorrenciaPeriodoProfessores { get; set; }
        public List<InstituicaoCursoOcorrenciaPeriodoAlunoVM> InstituicaoCursoOcorrenciaPeriodoAlunos { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}