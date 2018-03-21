using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.MateriaDomain;

namespace Domain.CursoDomain {
    
    public class CursoGradeMateria {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public CursoGrade CursoGrade { get; set; }
        public Materia Materia { get; set; }
        public string Descricao { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}