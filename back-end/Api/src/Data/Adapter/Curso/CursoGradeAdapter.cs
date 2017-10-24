using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class CursoGradeAdapter {

        public static CursoGradeVM ToViewModel(CursoGrade model, List<CursoGradeMateria> materias, bool deep) {
            var vm = new CursoGradeVM();
            vm.ID = model.ID.ToString();

            vm.Label = model.Descricao;
            vm.Descricao = model.Descricao;
            vm.DataCriacao = model.DataCriacao;

            if (materias != null) {
                vm.Materias = materias.Select(x => CursoGradeMateriaAdapter.ToViewModel(x, false)).ToList();
            }
            return vm;
        }
        public static CursoGrade ToModel(CursoGradeVM vm, bool deep) {
            var model = new CursoGrade();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            model.Descricao = vm.Descricao;
            model.DataCriacao = vm.DataCriacao;
            return model;
        }

    }
}