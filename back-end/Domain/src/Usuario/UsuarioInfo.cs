using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.UsuarioDomain {

    public class UsuarioInfo : IBaseModel {

        public UsuarioInfo() {

        }

        [Key]
        public string ID { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Perfis { get; set; } = "";
        public DateTime? Ativo { get; set; } = null;

    }
}