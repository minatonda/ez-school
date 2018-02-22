
using System;
using Api.Common.ViewModels;

namespace Api.UsuarioApi {

    public class UsuarioInfoVM : SelectVM {

        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; } = true;

    }
}