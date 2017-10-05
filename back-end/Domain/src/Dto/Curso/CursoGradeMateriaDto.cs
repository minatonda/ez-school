using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;
using Domain.Models.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Dto
{
    public class CursoGradeMateriaDto
    {

        public CursoGradeMateriaDto(CursoGradeMateria cursoGradeMateria)
        {
            this.ID = cursoGradeMateria.ID;
            this.Materia = cursoGradeMateria.Materia;
            this.Descricao = cursoGradeMateria.Descricao;
        }
        public CursoGradeMateriaDto()
        {
        }

        public long ID { get; set; }
        public Materia Materia { get; set; }
        public string Descricao { get; set; }

    }
}