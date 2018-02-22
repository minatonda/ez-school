
using System;
using System.Collections.Generic;
using Api.Common.ViewModels;

namespace Api.CursoApi {

    public class CursoGradeVM : SelectVM {
        
        public CursoGradeVM() {
            this.Materias = new List<CursoGradeMateriaVM>();
        }

        public List<CursoGradeMateriaVM> Materias { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }

    }
}