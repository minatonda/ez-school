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

    }
}