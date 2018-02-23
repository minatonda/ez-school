using Api.Common.Base;

namespace Api.MateriaApi {
    
    public class MateriaRelacionamentoVM : BaseVM {

        public MateriaRelacionamentoVM() {

        }

        public MateriaVM MateriaPai { get; set; }

    }
}