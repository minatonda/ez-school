using System.Collections.Generic;
using Api.Common.Base;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoColaboradorVM : BaseVM<long> {

        public InstituicaoColaboradorVM() {
            this.Perfis = new List<string>();
        }

        public UsuarioInfoVM Usuario { get; set; }
        public List<string> Perfis { get; set; }

    }
}