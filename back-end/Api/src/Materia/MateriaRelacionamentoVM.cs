using Api.Common.ViewModels;

namespace Api.MateriaApi {
    
    public class MateriaRelacionamentoVM : SelectVM {

        public MateriaRelacionamentoVM() {

        }

        public MateriaVM MateriaPai { get; set; }

    }
}