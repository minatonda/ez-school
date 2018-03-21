using Api.Common.Base;

namespace Api.CategoriaProfissionalApi {
    public class CategoriaProfissionalVM : BaseVM<long> {
        public string Nome { get; set; }
        public string Descricao { get; set; }

    }
}