using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;
using Domain.Models.Interface;


namespace Domain.Dto {
    public class CursoGradeDto {

        public CursoGradeDto (CursoGrade cursoGrade, List<CursoGradeMateria> cursoGradeMaterias) {
            this.ID = cursoGrade.ID;
            this.Descricao = cursoGrade.Descricao;
            this.DataCriacao = cursoGrade.DataCriacao;

            if (cursoGradeMaterias != null) {
                this.Materias = cursoGradeMaterias.Select (x => new CursoGradeMateriaDto (x)).ToList ();
            }
        }

        public CursoGradeDto () {
            this.Materias = new List<CursoGradeMateriaDto> ();
        }

        public long ID { get; set; }
        public string Descricao { get; set; }
        public List<CursoGradeMateriaDto> Materias { get; set; }
        public DateTime? DataCriacao { get; set; }

    }
}