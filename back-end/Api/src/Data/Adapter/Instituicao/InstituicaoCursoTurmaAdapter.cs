using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoTurmaAdapter {

        public static InstituicaoCursoTurmaVM ToViewModel (InstituicaoCursoTurmaDto model, bool deep) {
            var vm = new InstituicaoCursoTurmaVM ();

            vm.ID = model.ID.ToString ();
            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static InstituicaoCursoTurmaDto ToModel (InstituicaoCursoTurmaVM vm, bool deep) {
            var model = new InstituicaoCursoTurmaDto ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}