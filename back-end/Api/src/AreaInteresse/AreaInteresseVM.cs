using System.Collections.Generic;
using Api.CategoriaProfissionalApi;
using Api.Common.Base;
using Api.UsuarioApi;

namespace Api.AreaInteresseApi {

    public class AreaInteresseVM : BaseVM<long> {

        public AreaInteresseVM() {

        }

        public CategoriaProfissionalVM CategoriaProfissional { get; set; }

    }
}