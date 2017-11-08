using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoPeriodoVM : SelectVM {

        public InstituicaoCursoPeriodoVM() {
            this.DiaSemana = new List<DayOfWeek>();
        }

        public string Inicio { get; set; }
        public string Fim { get; set; }
        public List<DayOfWeek> DiaSemana { get; set; }

    }
}