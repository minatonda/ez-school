using System;
using Api.Common.Base;
using Domain.Common;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaAusenciaVM : BaseVM<long> {

        public InstituicaoCursoOcorrenciaPeriodoAlunoVM InstituicaoCursoOcorrenciaPeriodoAluno { get; set; }
        public DateTime DataAusencia { get; set; }

    }

}