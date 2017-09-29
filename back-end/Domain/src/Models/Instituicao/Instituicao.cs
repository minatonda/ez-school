using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Interface;

namespace Domain.Models {
    public class Instituicao : IBaseModel {
        public Instituicao () {

        }

        [Key]
        [DatabaseGeneratedAttribute (DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public bool Ativo { get; set; } = true;
    }
}