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
    public class CursoGradeDto
    {

        public CursoGradeDto(List<CursoGradeMateria> CursoGradeMaterias)
        {
            this.ID = CursoGradeMaterias.Select(x => x.CursoGrade).FirstOrDefault().ID;
            this.Descricao = CursoGradeMaterias.Select(x => x.CursoGrade).FirstOrDefault().Descricao;
            this.DataCriacao = CursoGradeMaterias.Select(x => x.CursoGrade).FirstOrDefault().DataCriacao;
            this.Materias = CursoGradeMaterias.Select(x => x.Materia).ToList();
        }

        public CursoGradeDto()
        {

        }

        public long ID { get; set; }
        public string Descricao { get; set; }
        public List<Materia> Materias { get; set; }
        public DateTime? DataCriacao { get; set; }

    }
}