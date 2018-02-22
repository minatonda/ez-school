using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoAdapter {

        public static InstituicaoCursoOcorrenciaPeriodoVM ToViewModel(InstituicaoCursoOcorrenciaPeriodo model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaPeriodoVM();
            vm.ID = model.ID.ToString();
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;
            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodo ToModel(InstituicaoCursoOcorrenciaPeriodoVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodo();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            return model;
        }

        public static List<InstituicaoCursoOcorrenciaPeriodoAluno> InstituicaoCursoOcorrenciaPeriodoAlunoFrom(InstituicaoCursoOcorrenciaPeriodoVM vm) {
            return vm.InstituicaoCursoOcorrenciaPeriodoAlunos.Select(x => InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToModel(x, true)).ToList();
        }

        public static Dictionary<InstituicaoCursoOcorrenciaPeriodoProfessor, List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula>> InstituicaoCursoOcorrenciaPeriodoProfessorFrom(InstituicaoCursoOcorrenciaPeriodoVM vm) {
            return vm.InstituicaoCursoOcorrenciaPeriodoProfessores.ToDictionary(x => InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToModel(x, true), x => x.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.Select(y => InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToModel(y, x, true)).ToList());
        }

    }
}