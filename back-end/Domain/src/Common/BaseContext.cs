using Domain.AreaInteresseDomain;
using Domain.CategoriaProfissionalDomain;
using Domain.CursoDomain;
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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioInfo> UsuariosInfo { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoGrade> CursoGrades { get; set; }
        public DbSet<CursoGradeMateria> CursoGradeMaterias { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<InstituicaoCurso> InstituicaoCursos { get; set; }
        public DbSet<InstituicaoCategoria> InstituicaoCategorias { get; set; }
        public DbSet<InstituicaoCursoOcorrencia> InstituicaoCursoOcorrencias { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaNota> InstituicaoCursoOcorrenciaNotas { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodo> InstituicaoCursoOcorrenciaPeriodos { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaAluno> InstituicaoCursoOcorrenciaAlunos { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoProfessor> InstituicaoCursoOcorrenciaPeriodoProfessores { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoAluno> InstituicaoCursoOcorrenciaPeriodoAlunos { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula> InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas { get; set; }
        public DbSet<InstituicaoInstituicaoCategoria> InstituicaoInstituicaoCategorias { get; set; }
        public DbSet<InstituicaoCursoPeriodo> InstituicaoCursoPeriodos { get; set; }
        public DbSet<CategoriaProfissional> CategoriaProfissionais { get; set; }
        public DbSet<InstituicaoCursoTurma> InstituicaoCursoTurmas { get; set; }
        public DbSet<AreaInteresse> AreaInteresse { get; set; }
        public DbSet<MateriaRelacionamento> MateriaRelacionamento { get; set; }

    }
}