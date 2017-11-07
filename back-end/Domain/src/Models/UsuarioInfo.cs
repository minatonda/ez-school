using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class UsuarioInfo : IBaseModel {
        public UsuarioInfo () {

        }

        [Key]
        public string ID { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}