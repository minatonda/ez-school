using Api.Common.ViewModels;

namespace Api.InstituicaoApi {
    public class InstituicaoVM : SelectVM {
        public string Nome { get; set; }
        public string CNPJ { get; set; }

    }
}