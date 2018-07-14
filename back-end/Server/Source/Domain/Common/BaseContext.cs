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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioInfo> UsuarioInfos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoGrade> CursoGrades { get; set; }
        public DbSet<CursoGradeMateria> CursoGradeMaterias { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<InstituicaoColaborador> InstituicoesColaboradores { get; set; }
        public DbSet<InstituicaoColaboradorPerfil> InstituicoesColaboradoresPerfis { get; set; }
        public DbSet<InstituicaoCategoria> InstituicoesCategorias { get; set; }
        public DbSet<InstituicaoCurso> InstituicoesCursos { get; set; }
        public DbSet<InstituicaoCursoOcorrencia> InstituicoesCursosOcorrencias { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaNota> InstituicoesCursosOcorrenciasNotas { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaAusencia> InstituicoesCursosOcorrenciasAusencias { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodo> InstituicoesCursosOcorrenciasPeriodos { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaAluno> InstituicoesCursosOcorrenciasAlunos { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoProfessor> InstituicoesCursosOcorrenciasPeriodosProfessores { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoAluno> InstituicoesCursosOcorrenciasPeriodosAlunos { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula> InstituicoesCursosOcorrenciasPeriodosProfessoresPeriodoAulas { get; set; }
        public DbSet<InstituicaoCursoPeriodo> InstituicoesCursosPeriodos { get; set; }
        public DbSet<InstituicaoCursoTurma> InstituicoesCursosTurmas { get; set; }
        public DbSet<InstituicaoInstituicaoCategoria> InstituicoesInstituicoesCategorias { get; set; }
        public DbSet<CategoriaProfissional> CategoriasProfissionais { get; set; }
        public DbSet<AreaInteresse> AreasInteresses { get; set; }
        public DbSet<MateriaRelacionamento> MateriasRelacionamentos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

    }
}