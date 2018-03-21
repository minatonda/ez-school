using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.MateriaDomain {

    public class MateriaRelacionamento : IBaseModel {
        
        public MateriaRelacionamento() {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public Materia MateriaPrincipal { get; set; }
        public Materia MateriaPai { get; set; }
        public DateTime? Ativo { get; set; } = null;
    }
}