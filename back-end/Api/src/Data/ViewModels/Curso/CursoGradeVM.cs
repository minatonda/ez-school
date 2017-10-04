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
    public class CursoGradeVM : ShortVM
    {
        public CursoGradeVM()
        {
            this.Materias = new List<MateriaVM>();
        }

        public List<MateriaVM> Materias { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }

    }
}