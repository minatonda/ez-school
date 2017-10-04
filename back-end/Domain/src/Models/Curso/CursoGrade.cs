using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Interface;

namespace Domain.Models
{
    public class CursoGrade
    {

        public CursoGrade()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Descricao { get; set; }
        public Curso Curso { get; set; }
        public DateTime? DataCriacao { get; set; }

    }
}