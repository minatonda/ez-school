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
    public class CursoAdapter {

        public static CursoVM ToViewModel (Curso model, bool deep) {
            var vm = new CursoVM ();
            vm.ID = model.ID;
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static ShortVM ToViewModelShort (Curso model) {
            var vm = new ShortVM ();
            vm.ID = model.ID;
            vm.Label = model.Nome;
            return vm;
        }

        public static Curso ToModel (CursoVM vm, bool deep) {
            var model = new Curso ();
            model.ID = vm.ID;

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}