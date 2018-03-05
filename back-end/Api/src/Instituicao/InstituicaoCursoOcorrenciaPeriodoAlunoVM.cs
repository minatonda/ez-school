
using Api.Common.Base;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoAlunoVM : BaseVM<long> {

        public InstituicaoCursoOcorrenciaPeriodoAlunoVM() {

        }

        public UsuarioInfoVM Aluno { get; set; }
        public InstituicaoCursoTurmaVM InstituicaoCursoTurma { get; set; }
        public InstituicaoCursoPeriodoVM InstituicaoCursoPeriodo { get; set; }

    }
}