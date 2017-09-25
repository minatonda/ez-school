using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Interface;

namespace Domain.Models {
    public class Materia : IBaseModel {
        public Materia () {

        }

        [Key]
        [DatabaseGeneratedAttribute (DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;
    }
}