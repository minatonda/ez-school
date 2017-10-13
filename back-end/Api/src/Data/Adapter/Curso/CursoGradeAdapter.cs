using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Models;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class CursoGradeAdapter {

        public static CursoGradeVM ToViewModel (CursoGradeDto model, bool deep) {
            var vm = new CursoGradeVM ();
            vm.ID = model.ID.ToString ();

            vm.Label = model.Descricao;
            vm.Descricao = model.Descricao;
            vm.DataCriacao = model.DataCriacao;

            if (model.Materias != null) {
                vm.Materias = model.Materias.Select (x => CursoGradeMateriaAdapter.ToViewModel (x, false)).ToList ();
            }
            return vm;
        }
        public static CursoGradeDto ToModel (CursoGradeVM vm, bool deep) {
            var model = new CursoGradeDto ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }

            model.Descricao = vm.Descricao;
            model.DataCriacao = vm.DataCriacao;

            if (vm.Materias != null) {
                model.Materias = vm.Materias.Select (x => CursoGradeMateriaAdapter.ToModel (x, false)).ToList ();
            }
            return model;
        }

    }
}