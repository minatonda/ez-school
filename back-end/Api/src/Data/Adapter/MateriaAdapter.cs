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
    public class MateriaAdapter {

        public static MateriaVM ToViewModel(Materia model, List<Materia> materiasRelacionadas, bool deep) {
            var vm = new MateriaVM();
            vm.ID = model.ID.ToString();
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            if(materiasRelacionadas != null) {
                vm.MateriasRelacionadas = materiasRelacionadas.Select(x => MateriaAdapter.ToViewModel(x, null, true)).ToList();
            }

            return vm;
        }

        public static SelectVM ToViewModelShort(Materia model) {
            var vm = new SelectVM();
            vm.ID = model.ID.ToString();
            vm.Label = model.Nome;
            return vm;
        }

        public static Materia ToModel(MateriaVM vm, bool deep) {
            var model = new Materia();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }
        public static List<Materia> MateriasRelacionadasFromVM(MateriaVM vm) {
            return vm.MateriasRelacionadas.Select(x => MateriaAdapter.ToModel(x, true)).ToList();
        }

    }
}