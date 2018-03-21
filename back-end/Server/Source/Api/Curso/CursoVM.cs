
using System.Collections.Generic;
using Api.Common.Base;

namespace Api.CursoApi {

    public class CursoVM : BaseVM<long> {

        public CursoVM() {
            this.Grades = new List<CursoGradeVM>();
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<CursoGradeVM> Grades { get; set; }

    }
}