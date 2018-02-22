using System.Collections.Generic;
using Api.CategoriaProfissionalApi;
using Api.Common.ViewModels;

namespace Api.UsuarioApi {

    public class ProfessorVM : SelectVM {
        public ProfessorVM() {
            this.CategoriaProfissionais = new List<CategoriaProfissionalVM>();
        }

        public UsuarioInfoVM UsuarioInfo { get; set; }
        public List<CategoriaProfissionalVM> CategoriaProfissionais { get; set; }

    }
}