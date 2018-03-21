using System.Collections.Generic;
using Api.Common.Base;

namespace Api.MateriaApi {
    
    public class MateriaVM : BaseVM<long> {

        public MateriaVM() {
            this.MateriasRelacionadas = new List<MateriaRelacionamentoVM>();
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<MateriaRelacionamentoVM> MateriasRelacionadas { get; set; }

    }
}