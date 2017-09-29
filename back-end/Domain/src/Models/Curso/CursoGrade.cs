using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Interface;

namespace Domain.Models
{
    public class CursoGrade : IBaseModel
    {

        public CursoGrade()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public Curso Curso { get; set; }
        public bool Ativo { get; set; } = true;

    }
}