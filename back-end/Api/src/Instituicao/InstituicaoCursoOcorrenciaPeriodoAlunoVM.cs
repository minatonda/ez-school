
using Api.Common.Base;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoAlunoVM : BaseVM {

        public InstituicaoCursoOcorrenciaPeriodoAlunoVM() {

        }

        public AlunoVM Aluno { get; set; }
        public InstituicaoCursoTurmaVM InstituicaoCursoTurma { get; set; }
        public InstituicaoCursoPeriodoVM InstituicaoCursoPeriodo { get; set; }

    }
}