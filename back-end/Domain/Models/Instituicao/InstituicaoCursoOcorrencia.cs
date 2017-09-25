using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrencia : IBaseModel {

        public InstituicaoCursoOcorrencia (InstituicaoCurso instituicaoCurso, DateTime dataInicio) {
            this.InstituicaoCurso = instituicaoCurso;
        }

        [Key]
        public string ID { get; set; }
        public InstituicaoCurso InstituicaoCurso { get; set; }
        public List<InstituicaoAluno> Alunos { get; set; }
        public bool Ativo { get; set; } = true;

        public DateTime DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}