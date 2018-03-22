using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.InstituicaoDomain {
    
    public class InstituicaoCursoOcorrenciaPeriodoAluno : IBaseModel {

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaPeriodo InstituicaoCursoOcorrenciaPeriodo { get; set; }
        public InstituicaoCursoOcorrenciaAluno InstituicaoCursoOcorrenciaAluno { get; set; }
        public InstituicaoCursoPeriodo InstituicaoCursoPeriodo { get; set; }
        public InstituicaoCursoTurma InstituicaoCursoTurma { get; set; }
        public DateTime? DataConfirmacao { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }

}