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
    public class CursoAdapter {

        public static CursoVM ToViewModel (Curso model, bool deep) {
            var vm = new CursoVM ();
            vm.ID = model.ID.ToString ();
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }
        public static SelectVM ToViewModelShort (Curso model) {
            var vm = new SelectVM ();
            vm.ID = model.ID.ToString ();
            vm.Label = model.Nome;
            return vm;
        }
        public static Curso ToModel (CursoVM vm, bool deep) {
            var model = new Curso ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

        public static CursoGradeVM ToViewModel (CursoGradeDto model, bool deep) {
            var vm = new CursoGradeVM ();
            vm.ID = model.ID.ToString ();
            vm.Label = model.Descricao;

            vm.Descricao = model.Descricao;
            vm.DataCriacao = model.DataCriacao;
            foreach (var item in model.Materias) {
                vm.Materias.Add (CursoAdapter.ToViewModel (item, false));
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
            foreach (var item in vm.Materias) {
                model.Materias.Add (CursoAdapter.ToModel (item, false));
            }
            return model;
        }

        public static CursoGradeMateriaVM ToViewModel (CursoGradeMateriaDto model, bool deep) {
            var vm = new CursoGradeMateriaVM ();

            vm.ID = model.ID.ToString ();
            vm.Label = model.Descricao;

            vm.Descricao = model.Descricao;
            vm.Materia = MateriaAdapter.ToViewModel (model.Materia, true);

            return vm;
        }
        public static CursoGradeMateriaDto ToModel (CursoGradeMateriaVM vm, bool deep) {
            var model = new CursoGradeMateriaDto ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }
            model.ID = long.Parse (vm.ID);
            model.Descricao = vm.Descricao;
            model.Materia = MateriaAdapter.ToModel (vm.Materia, true);
            return model;
        }

    }
}