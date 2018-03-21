using System;
using Api.Common.Base;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoOcorrenciaNotaVM : BaseVM<long> {
        public InstituicaoCursoOcorrenciaNotaVM() {

        }

        public string IDTag { get; set; }
        public double Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoAlunoVM InstituicaoCursoOcorrenciaPeriodoAluno { get; set; }

    }
}