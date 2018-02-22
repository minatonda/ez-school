using System.Collections.Generic;
using Api.CategoriaProfissionalApi;
using Api.Common.ViewModels;

namespace Api.UsuarioApi {

    public class AlunoVM : SelectVM {

        public AlunoVM() {
            this.CategoriaProfissionais = new List<CategoriaProfissionalVM>();
        }

        public UsuarioInfoVM UsuarioInfo { get; set; }
        public List<CategoriaProfissionalVM> CategoriaProfissionais { get; set; }

    }
}