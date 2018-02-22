using Api.Common.ViewModels;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaAlunoVM : SelectVM {
    
        public InstituicaoCursoOcorrenciaAlunoVM() {

        }

        public AlunoVM Aluno { get; set; }

    }
}