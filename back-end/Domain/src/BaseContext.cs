using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain {
    public class BaseContext : DbContext {
        public BaseContext (DbContextOptions<BaseContext> options) : base (options) {

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
        public DbSet<InstituicaoCursoOcorrencia> InstituicaoCursoOcorrencias { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaCoordenador> InstituicaoCursoOcorrenciaCoordenadores { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaAluno> InstituicaoCursoOcorrenciaAlunos { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaMateria> InstituicaoCursoOcorrenciaMaterias { get; set; }
        public DbSet<InstituicaoCursoOcorrenciaMateriaProfessor> InstituicaoCursoOcorrenciaMateriaProfessores { get; set; }
        public DbSet<InstituicaoCategoria> InstituicaoCategorias { get; set; }
        public DbSet<InstituicaoInstituicaoCategoria> InstituicaoInstituicaoCategorias { get; set; }
        public DbSet<InstituicaoCursoPeriodo> InstituicaoCursoPeriodos { get; set; }
        public DbSet<CategoriaProfissional> CategoriaProfissionais { get; set; }
    }
}