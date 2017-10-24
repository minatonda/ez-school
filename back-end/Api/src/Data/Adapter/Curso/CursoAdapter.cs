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
            vm.ID = model.ID.ToString ();
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

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
        
    }
}