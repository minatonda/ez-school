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
    public class CursoGradeMateriaAdapter {
    
        public static CursoGradeMateriaVM ToViewModel (CursoGradeMateria model, bool deep) {
            var vm = new CursoGradeMateriaVM ();

            vm.ID = model.ID.ToString ();
            vm.Label = model.Descricao;

            vm.Descricao = model.Descricao;
            vm.Materia = MateriaAdapter.ToViewModel (model.Materia, null,  true);

            return vm;
        }
        public static CursoGradeMateria ToModel (CursoGradeMateriaVM vm, bool deep) {
            var model = new CursoGradeMateria ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }
            model.Descricao = vm.Descricao;
            model.Materia = MateriaAdapter.ToModel (vm.Materia, true);
            return model;
        }

    }
}