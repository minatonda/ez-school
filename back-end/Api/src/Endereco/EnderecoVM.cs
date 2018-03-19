using Api.Common.Base;
using Api.MateriaApi;

namespace Api.EnderecoApi {

    public class EnderecoVM : BaseVM<long> {

        public EnderecoVM() {

        }

        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }

    }
}