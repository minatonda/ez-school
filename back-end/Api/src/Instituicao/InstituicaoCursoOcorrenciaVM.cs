using System;
using System.Collections.Generic;
using Api.Common.Base;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaVM : BaseVM {

        public InstituicaoCursoOcorrenciaVM() {
            this.InstituicaoCursoOcorrenciaPeriodos = new List<InstituicaoCursoOcorrenciaPeriodoVM>();
        }

        public ProfessorVM Coordenador { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public List<InstituicaoCursoOcorrenciaPeriodoVM> InstituicaoCursoOcorrenciaPeriodos { get; set; }

    }
}