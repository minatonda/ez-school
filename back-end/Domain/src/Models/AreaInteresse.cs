using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Interface;

namespace Domain.Models {
    public class AreaInteresse {
        public AreaInteresse() {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public Aluno Aluno { get; set; }
        public Professor Professor { get; set; }
        public CategoriaProfissional CategoriaProfissional { get; set; }
        public DateTime? Ativo { get; set; } = null;
    }
}