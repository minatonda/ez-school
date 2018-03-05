using Api.Common.Base;

namespace Api.InstituicaoApi {
    public class InstituicaoVM : BaseVM<long> {
        public string Nome { get; set; }
        public string CNPJ { get; set; }

    }
}