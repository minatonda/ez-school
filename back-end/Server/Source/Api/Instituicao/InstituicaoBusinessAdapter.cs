using Api.CursoApi;
using Api.UsuarioApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoBusinessAdapter {

        public static InstituicaoBusinessAulaVM ToInstituicaoBusinessAulaViewModel(InstituicaoCursoOcorrenciaPeriodoProfessor instituicaoCursoOcorrenciaPeriodoProfessor) {
            var vm = InstituicaoBusinessAdapter.ToIInstituicaoBusinessAulaViewModel(instituicaoCursoOcorrenciaPeriodoProfessor, new InstituicaoBusinessAulaVM()) as InstituicaoBusinessAulaVM;
            vm.ID = vm.IdInstituicaoCursoOcorrenciaPeriodoProfessor;
            return vm;
        }

        public static InstituicaoBusinessAulaVM ToInstituicaoBusinessAulaViewModel(InstituicaoCursoOcorrenciaPeriodoProfessor instituicaoCursoOcorrenciaPeriodoProfessor, long sequencia) {
            var vm = InstituicaoBusinessAdapter.ToIInstituicaoBusinessAulaViewModel(instituicaoCursoOcorrenciaPeriodoProfessor, new InstituicaoBusinessAulaVM(), sequencia) as InstituicaoBusinessAulaVM;
            return vm;
        }

        public static InstituicaoBusinessAulaDetalheAlunoVM ToInstituicaoBusinessAulaDetalheAlunoViewModel(InstituicaoCursoOcorrenciaPeriodoProfessor instituicaoCursoOcorrenciaPeriodoProfessor, long sequencia, double? nota) {
            var vm = InstituicaoBusinessAdapter.ToIInstituicaoBusinessAulaViewModel(instituicaoCursoOcorrenciaPeriodoProfessor, new InstituicaoBusinessAulaDetalheAlunoVM(), sequencia) as InstituicaoBusinessAulaDetalheAlunoVM;
            vm.Nota = nota;
            vm.Ausencias = 0;
            return vm;
        }

        private static IInstituicaoBusinessAulaVM ToIInstituicaoBusinessAulaViewModel(InstituicaoCursoOcorrenciaPeriodoProfessor instituicaoCursoOcorrenciaPeriodoProfessor, IInstituicaoBusinessAulaVM vm) {
            vm.IdInstituicaoCursoOcorrenciaPeriodoProfessor = instituicaoCursoOcorrenciaPeriodoProfessor.ID;
            vm.IdInstituicaoCursoOcorrencia = instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia.ID;
            vm.Curso = CursoAdapter.ToViewModel(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia.InstituicaoCurso.Curso, false);
            vm.Professor = UsuarioAdapter.ToViewModel(instituicaoCursoOcorrenciaPeriodoProfessor.Professor, false);
            vm.Instituicao = InstituicaoAdapter.ToViewModel(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia.InstituicaoCurso.Instituicao, false);
            vm.Materia = CursoGradeMateriaAdapter.ToViewModel(instituicaoCursoOcorrenciaPeriodoProfessor.CursoGradeMateria, false);
            vm.DataInicio = instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.DataInicio;
            vm.DataExpiracao = instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.DataExpiracao;
            return vm;
        }

        private static IInstituicaoBusinessAulaVM ToIInstituicaoBusinessAulaViewModel(InstituicaoCursoOcorrenciaPeriodoProfessor instituicaoCursoOcorrenciaPeriodoProfessor, IInstituicaoBusinessAulaVM vm, long sequencia) {
            vm = InstituicaoBusinessAdapter.ToIInstituicaoBusinessAulaViewModel(instituicaoCursoOcorrenciaPeriodoProfessor, vm);
            vm.PeriodoSequencia = sequencia;
            return vm;
        }




    }
}