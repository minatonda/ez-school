
using System;
using Api.Common.Base;

namespace Api.UsuarioApi {

    public class UsuarioInfoVM : BaseVM {

        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; } = true;

    }
}