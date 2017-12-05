using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaPeriodoAluno : IBaseModel {

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaPeriodo InstituicaoCursoOcorrenciaPeriodo { get; set; }
        public InstituicaoCursoOcorrenciaAluno InstituicaoCursoOcorrenciaAluno { get; set; }
        public InstituicaoCursoPeriodo InstituicaoCursoPeriodo { get; set; }
        public InstituicaoCursoTurma InstituicaoCursoTurma { get; set; }
        public bool Confirmado { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }

}