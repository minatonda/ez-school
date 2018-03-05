using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.CursoDomain;
using Domain.UsuarioDomain;

namespace Domain.InstituicaoDomain {
    
    public class InstituicaoCursoOcorrenciaPeriodoProfessor : IBaseModel {

        public InstituicaoCursoOcorrenciaPeriodoProfessor() {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaPeriodo InstituicaoCursoOcorrenciaPeriodo { get; set; }
        public InstituicaoCursoPeriodo InstituicaoCursoPeriodo { get; set; }
        public CursoGradeMateria CursoGradeMateria { get; set; }
        public InstituicaoCursoTurma InstituicaoCursoTurma { get; set; }
        public UsuarioInfo Professor { get; set; }
        public bool Confirmado { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}