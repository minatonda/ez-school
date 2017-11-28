using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaPeriodoAlunoAdapter {

        public static InstituicaoCursoOcorrenciaPeriodoAlunoVM ToViewModel(InstituicaoCursoOcorrenciaPeriodoAluno model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaPeriodoAlunoVM();

            vm.ID = model.ID.ToString();

            if (model.InstituicaoCursoOcorrenciaAluno != null) {
                vm.Aluno = InstituicaoCursoOcorrenciaAlunoAdapter.ToViewModel(model.InstituicaoCursoOcorrenciaAluno, true).Aluno;
            }

            if (model.InstituicaoCursoPeriodo != null) {
                vm.InstituicaoCursoPeriodo = InstituicaoCursoPeriodoAdapter.ToViewModel(model.InstituicaoCursoPeriodo, false);
            }

            if (model.InstituicaoCursoTurma != null) {
                vm.InstituicaoCursoTurma = InstituicaoCursoTurmaAdapter.ToViewModel(model.InstituicaoCursoTurma, false);
            }

            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodoAluno ToModel(InstituicaoCursoOcorrenciaPeriodoAlunoVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodoAluno();

            if (vm.InstituicaoCursoPeriodo != null) {
                model.InstituicaoCursoPeriodo = InstituicaoCursoPeriodoAdapter.ToModel(vm.InstituicaoCursoPeriodo, false);
            }

            if (vm.InstituicaoCursoTurma != null) {
                model.InstituicaoCursoTurma = InstituicaoCursoTurmaAdapter.ToModel(vm.InstituicaoCursoTurma, false);
            }

            return model;
        }

    }
}