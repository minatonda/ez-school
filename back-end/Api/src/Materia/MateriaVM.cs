using System.Collections.Generic;
using Api.Common.ViewModels;

namespace Api.MateriaApi {
    
    public class MateriaVM : SelectVM {

        public MateriaVM() {
            this.MateriasRelacionadas = new List<MateriaRelacionamentoVM>();
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<MateriaRelacionamentoVM> MateriasRelacionadas { get; set; }

    }
}