using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;

namespace Service.Data.ViewModels {
    public class MateriaAdapter {

        public static MateriaVM ToViewModel (Materia model, bool deep) {
            var vm = new MateriaVM ();
            vm.ID = model.ID;
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static ShortVM ToViewModelShort (Materia model) {
            var vm = new ShortVM ();
            vm.ID = model.ID;
            vm.Label = model.Nome;
            return vm;
        }

        public static Materia ToModel (MateriaVM vm, bool deep) {
            var model = new Materia ();
            model.ID = vm.ID;

            model.Nome = vm.Nome;
            vm.Descricao = model.Descricao;

            return model;
        }

    }
}