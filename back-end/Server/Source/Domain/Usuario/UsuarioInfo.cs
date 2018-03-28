using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.EnderecoDomain;

namespace Domain.UsuarioDomain {

    public class UsuarioInfo : IBaseModel {

        public UsuarioInfo() {

        }

        [Key]
        public string ID { get; set; }

        public string Nome { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Roles { get; set; } = "";

        public string Genero { get; set; }

        public string EstadoCivil { get; set; }

        public Endereco Endereco { get; set; }

        [ForeignKey("idPai")]
        public UsuarioInfo Pai { get; set; }

        [ForeignKey("idMae")]
        public UsuarioInfo Mae { get; set; }

        public DateTime? Ativo { get; set; } = null;

    }

}