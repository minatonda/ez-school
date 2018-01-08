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
    public class MateriaRelacionamentoAdapter {

        public static MateriaRelacionamentoVM ToViewModel(MateriaRelacionamento model, bool deep) {
            var vm = new MateriaRelacionamentoVM();
            vm.ID = model.ID.ToString();

            if(model.MateriaPai!=null){
                vm.MateriaPai = MateriaAdapter.ToViewModel(model.MateriaPai,true);
            }

            return vm;
        }

        public static MateriaRelacionamento ToModel(MateriaRelacionamentoVM vm, bool deep) {
            var model = new MateriaRelacionamento();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            if (vm.MateriaPai != null) {
                model.MateriaPai = MateriaAdapter.ToModel(vm.MateriaPai, true);
            }

            return model;
        }
      

    }
}