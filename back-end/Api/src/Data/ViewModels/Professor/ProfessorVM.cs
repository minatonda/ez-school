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
    public class ProfessorVM : SelectVM
    {
        public ProfessorVM() {
            this.CategoriaProfissionais = new List<CategoriaProfissionalVM>();
        }
        
        public UsuarioInfoVM UsuarioInfo { get; set; }
        public List<CategoriaProfissionalVM> CategoriaProfissionais { get; set; }

    }
}