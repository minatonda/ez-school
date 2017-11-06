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
            vm.Coordenador = ProfessorAdapter.ToViewModel(model.Coordenador, null, false);
            //vm.Alunos = model.Alunos.Select (x => AlunoAdapter.ToViewModel (x, false)).ToList ();
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            return vm;
        }

        public static InstituicaoCursoOcorrencia ToModel(InstituicaoCursoOcorrenciaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrencia();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.Coordenador = ProfessorAdapter.ToModel(vm.Coordenador, false);
            //model.Alunos = vm.Alunos.Select (x => AlunoAdapter.ToModel (x, false)).ToList ();
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            return model;
        }

    }
}