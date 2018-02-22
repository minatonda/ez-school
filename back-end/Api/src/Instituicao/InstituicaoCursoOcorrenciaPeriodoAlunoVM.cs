
using Api.Common.ViewModels;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoAlunoVM : SelectVM {

        public InstituicaoCursoOcorrenciaPeriodoAlunoVM() {

        }

        public AlunoVM Aluno { get; set; }
        public InstituicaoCursoTurmaVM InstituicaoCursoTurma { get; set; }
        public InstituicaoCursoPeriodoVM InstituicaoCursoPeriodo { get; set; }

    }
}