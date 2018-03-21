using Domain.AreaInteresseDomain;
using Domain.CategoriaProfissionalDomain;
using Domain.CursoDomain;
using Domain.EnderecoDomain;
using Domain.InstituicaoDomain;
using Domain.MateriaDomain;
using Domain.UsuarioDomain;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common {

    public class BaseContext : DbContext {

        public BaseContext(DbContextOptions<BaseContext> options) : base(options) {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Usuario> Usr { get; set; }
        public DbSet<UsuarioInfo> UsrInf { get; set; }
        public DbSet<Materia> Mtr { get; set; }
        public DbSet<Professor> Prf { get; set; }
        public DbSet<Aluno> Aln { get; set; }
        public DbSet<Curso> Crs { get; set; }
        public DbSet<CursoGrade> CrsGrd { get; set; }
        public DbSet<CursoGradeMateria> CrsGrdMtr { get; set; }
        public DbSet<Instituicao> Ittc { get; set; }
        public DbSet<InstituicaoColaborador> IttcClbd { get; set; }
        public DbSet<InstituicaoColaboradorPerfil> IttcClbdPrf { get; set; }
        public DbSet<InstituicaoCategoria> IttcCtgr { get; set; }
        public DbSet<InstituicaoCurso> IttcCrs { get; set; }
        public DbSet<InstituicaoCursoOcorrencia> IttcCrsOcrnc { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaNota> IttcCrsOcrncNt { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaAusencia> IttcCrsOcrncAsnc { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodo> IttcCrsOcrncPrd { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaAluno> IttcCrsOcrncAln { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoProfessor> IttcCrsOcrncPrdPrf { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoAluno> IttcCrsOcrncPrdAln { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula> IttcCrsOcrncPrdPrfPrdAul { get; set; }
        public DbSet<InstituicaoCursoPeriodo> IttcCrsPrd { get; set; }
        public DbSet<InstituicaoCursoTurma> IttcCrsTrm { get; set; }
        public DbSet<InstituicaoInstituicaoCategoria> IttcIttcCtgr { get; set; }
        public DbSet<CategoriaProfissional> CtgPrfsn { get; set; }
        public DbSet<AreaInteresse> ArItrs { get; set; }
        public DbSet<MateriaRelacionamento> MtrRlc { get; set; }
        public DbSet<Endereco> Endrc { get; set; }

    }
}