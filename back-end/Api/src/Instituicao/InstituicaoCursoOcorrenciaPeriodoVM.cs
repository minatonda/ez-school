using System;
using System.Collections.Generic;
using Api.Common.Base;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoOcorrenciaPeriodoVM : BaseVM {

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