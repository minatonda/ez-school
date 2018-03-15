using System.Collections.Generic;
using Api.Common.Base;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {
    public class InstituicaoColaboradorPerfilVM : BaseVM<long> {

        public InstituicaoColaboradorPerfilVM() {
            this.Roles = new List<string>();
        }
        public string Nome { get; set; }
        public List<string> Roles { get; set; }

    }
}