using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaMateria : IBaseModel {

        public InstituicaoCursoOcorrenciaMateria (InstituicaoCursoOcorrencia instituicaoCursoOcorrencia, Materia materia) {
            this.InstituicaoCursoOcorrencia = instituicaoCursoOcorrencia;
        }

        [Key]
        public string ID { get; set; }
        public InstituicaoCursoOcorrencia InstituicaoCursoOcorrencia { get; set; }
        public Materia Materia { get; set; }
        public bool Ativo { get; set; } = true;

        public DateTime DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}