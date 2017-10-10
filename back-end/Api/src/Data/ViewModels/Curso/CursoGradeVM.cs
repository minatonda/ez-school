using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels
{
    public class CursoGradeVM : SelectVM
    {
        public CursoGradeVM()
        {
            this.Materias = new List<CursoGradeMateriaVM>();
        }

        public List<CursoGradeMateriaVM> Materias { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }

    }
}