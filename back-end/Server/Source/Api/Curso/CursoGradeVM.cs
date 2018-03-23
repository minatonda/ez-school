
using System;
using System.Collections.Generic;
using Api.Common.Base;
using Api.InstituicaoApi;

namespace Api.CursoApi {

    public class CursoGradeVM : BaseVM<long> {

        public CursoGradeVM() {
            this.Materias = new List<CursoGradeMateriaVM>();
        }

        public List<CursoGradeMateriaVM> Materias { get; set; }
        public InstituicaoVM Instituicao { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }

    }
}