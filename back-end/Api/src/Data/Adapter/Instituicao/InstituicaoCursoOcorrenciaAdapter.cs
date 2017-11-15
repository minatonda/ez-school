using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaAdapter {

        public static InstituicaoCursoOcorrenciaVM ToViewModel(InstituicaoCursoOcorrencia model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaVM();

            vm.ID = model.ID.ToString();
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            if (model.Coordenador != null) {
                vm.Coordenador = ProfessorAdapter.ToViewModel(model.Coordenador, null, false);
            }

            return vm;
        }

        public static InstituicaoCursoOcorrencia ToModel(InstituicaoCursoOcorrenciaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrencia();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            if (vm.Coordenador != null) {
                model.Coordenador = ProfessorAdapter.ToModel(vm.Coordenador, false);
            }

            return model;
        }

    }
}