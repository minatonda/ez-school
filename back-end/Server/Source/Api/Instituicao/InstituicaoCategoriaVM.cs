using Api.Common.Base;

namespace Api.InstituicaoApi {
    public class InstituicaoCategoriaVM : BaseVM<long> {

        public string Nome { get; set; }
        public string Descricao { get; set; }

    }
}