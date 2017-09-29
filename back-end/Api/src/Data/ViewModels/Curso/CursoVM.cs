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
    public class CursoVM : ShortVM
    {
        public CursoVM()
        {
            this.Grades = new List<CursoGradeVM>();
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<CursoGradeVM> Grades { get; set; }

    }
}