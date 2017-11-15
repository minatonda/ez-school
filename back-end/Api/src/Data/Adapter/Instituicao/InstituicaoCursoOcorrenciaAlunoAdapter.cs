using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaAlunoAdapter {

        public static InstituicaoCursoOcorrenciaAlunoVM ToViewModel(InstituicaoCursoOcorrenciaAluno model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaAlunoVM();

            vm.ID = model.ID.ToString();

            if (model.Aluno != null) {
                vm.Aluno = AlunoAdapter.ToViewModel(model.Aluno, null, true);
            }

            vm.Label = vm.Aluno.UsuarioInfo.Nome;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaAluno ToModel(InstituicaoCursoOcorrenciaAlunoVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaAluno();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            
            if (vm.Aluno != null) {
                model.Aluno = AlunoAdapter.ToModel(vm.Aluno, true);
            }

            return model;
        }

    }
}