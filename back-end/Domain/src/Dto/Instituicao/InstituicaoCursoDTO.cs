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
    public class InstituicaoCursoDto
    {

        public InstituicaoCursoDto()
        {

        }

        public long ID { get; set; }
        public Curso Curso { get; set; }
        public CursoGradeDto CursoGrade { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}