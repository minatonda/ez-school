using Api.Common.Base;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaAlunoVM : BaseVM<long> {

        public InstituicaoCursoOcorrenciaAlunoVM() {

        }

        public UsuarioInfoVM Aluno { get; set; }

    }
}