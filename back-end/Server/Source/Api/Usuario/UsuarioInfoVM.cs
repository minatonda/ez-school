
using System;
using System.Collections.Generic;
using Api.AreaInteresseApi;
using Api.CategoriaProfissionalApi;
using Api.Common.Base;
using Api.EnderecoApi;

namespace Api.UsuarioApi {

    public class UsuarioInfoVM : BaseVM<string> {

        public UsuarioInfoVM() {
            this.AreaInteresses = new List<AreaInteresseVM>();
        }

        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public List<string> Roles { get; set; }
        public AlunoVM Aluno { get; set; }
        public ProfessorVM Professor { get; set; }
        public EnderecoVM Endereco { get; set; }
        public UsuarioInfoVM Pai { get; set; }
        public UsuarioInfoVM Mae { get; set; }
        public List<AreaInteresseVM> AreaInteresses { get; set; }

    }
}