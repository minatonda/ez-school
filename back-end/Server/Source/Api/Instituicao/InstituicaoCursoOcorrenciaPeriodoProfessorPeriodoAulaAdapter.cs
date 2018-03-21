using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter {

        public static InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM ToViewModel(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM();
            vm.ID = model.ID;
            vm.Inicio = model.Inicio;
            vm.Fim = model.Fim;
            vm.Dia = model.Dia;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula ToModel(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula();
            model.ID = vm.ID;
            model.Inicio = vm.Inicio;
            model.Fim = vm.Fim;
            model.Dia = vm.Dia;

            return model;
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula ToModel(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM vm, InstituicaoCursoOcorrenciaPeriodoProfessorVM instituicaoCursoOcorrenciaPeriodoProfessor, bool deep) {
            var model = InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToModel(vm, true);
            model.InstituicaoCursoOcorrenciaPeriodoProfessor = InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToModel(instituicaoCursoOcorrenciaPeriodoProfessor, true);

            return model;
        }

    }
}