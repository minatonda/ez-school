using Api.Common.Base;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaAlunoVM : BaseVM {
    
        public InstituicaoCursoOcorrenciaAlunoVM() {

        }

        public AlunoVM Aluno { get; set; }

    }
}