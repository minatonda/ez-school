using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Dto;
using Domain.Models.Interface;

namespace Domain.Models {
    public class CursoGrade {

        public CursoGrade (CursoGradeDto cursoGradeDto, Curso curso) {
            this.ID = cursoGradeDto.ID;
            this.Descricao = cursoGradeDto.Descricao;
            this.DataCriacao = cursoGradeDto.DataCriacao;
            this.Curso = curso;
        }

        public CursoGrade () {

        }

        [Key]
        [DatabaseGeneratedAttribute (DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string Descricao { get; set; }
        public Curso Curso { get; set; }
        public DateTime? DataCriacao { get; set; }

    }
}