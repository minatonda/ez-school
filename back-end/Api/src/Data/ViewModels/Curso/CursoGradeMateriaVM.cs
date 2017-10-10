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
    public class CursoGradeMateriaVM : SelectVM
    {
        public CursoGradeMateriaVM()
        {

        }

        public string Descricao { get; set; }
        public MateriaVM Materia { get; set; }

    }
}