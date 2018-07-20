using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProfissional",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProfissional", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Bairro = table.Column<string>(type: "longtext", nullable: true),
                    Cidade = table.Column<string>(type: "longtext", nullable: true),
                    Complemento = table.Column<string>(type: "longtext", nullable: true),
                    Estado = table.Column<string>(type: "longtext", nullable: true),
                    Lat = table.Column<string>(type: "longtext", nullable: true),
                    Lon = table.Column<string>(type: "longtext", nullable: true),
                    Numero = table.Column<string>(type: "longtext", nullable: true),
                    Rua = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ICategoria",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICategoria", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Instituicao",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CNPJ = table.Column<string>(type: "longtext", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicao", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UInfo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(127)", nullable: false),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CPF = table.Column<string>(type: "longtext", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    EnderecoID = table.Column<long>(type: "bigint", nullable: true),
                    EstadoCivil = table.Column<string>(type: "longtext", nullable: true),
                    Genero = table.Column<string>(type: "longtext", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true),
                    RG = table.Column<string>(type: "longtext", nullable: true),
                    Roles = table.Column<string>(type: "longtext", nullable: true),
                    Telefone = table.Column<string>(type: "longtext", nullable: true),
                    idMae = table.Column<string>(type: "varchar(127)", nullable: true),
                    idPai = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UInfo_Endereco_EnderecoID",
                        column: x => x.EnderecoID,
                        principalTable: "Endereco",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UInfo_UInfo_idMae",
                        column: x => x.idMae,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UInfo_UInfo_idPai",
                        column: x => x.idPai,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CGrade",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CursoID = table.Column<long>(type: "bigint", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    InstituicaoID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CGrade", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CGrade_Curso_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Curso",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CGrade_Instituicao_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Instituicao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICPerfil",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InstituicaoID = table.Column<long>(type: "bigint", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true),
                    Roles = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICPerfil", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICPerfil_Instituicao_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Instituicao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IInstituicaoCategoria",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InstituicaoCategoriaID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IInstituicaoCategoria", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IInstituicaoCategoria_ICategoria_InstituicaoCategoriaID",
                        column: x => x.InstituicaoCategoriaID,
                        principalTable: "ICategoria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IInstituicaoCategoria_Instituicao_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Instituicao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MateriaRelacionamento",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MateriaPaiID = table.Column<long>(type: "bigint", nullable: true),
                    MateriaPrincipalID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaRelacionamento", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MateriaRelacionamento_Materia_MateriaPaiID",
                        column: x => x.MateriaPaiID,
                        principalTable: "Materia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MateriaRelacionamento_Materia_MateriaPrincipalID",
                        column: x => x.MateriaPrincipalID,
                        principalTable: "Materia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(127)", nullable: false),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioInfoID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Aluno_UInfo_UsuarioInfoID",
                        column: x => x.UsuarioInfoID,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AreaInteresse",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CategoriaProfissionalID = table.Column<long>(type: "bigint", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    UsuarioInfoID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaInteresse", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AreaInteresse_CategoriaProfissional_CategoriaProfissionalID",
                        column: x => x.CategoriaProfissionalID,
                        principalTable: "CategoriaProfissional",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AreaInteresse_UInfo_UsuarioInfoID",
                        column: x => x.UsuarioInfoID,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IColaborador",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InstituicaoID = table.Column<long>(type: "bigint", nullable: true),
                    Perfis = table.Column<string>(type: "longtext", nullable: true),
                    UsuarioID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IColaborador", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IColaborador_Instituicao_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Instituicao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IColaborador_UInfo_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(127)", nullable: false),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioInfoID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Professor_UInfo_UsuarioInfoID",
                        column: x => x.UsuarioInfoID,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(127)", nullable: false),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Password = table.Column<string>(type: "longtext", nullable: true),
                    Username = table.Column<string>(type: "longtext", nullable: true),
                    UsuarioInfoID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Usuario_UInfo_UsuarioInfoID",
                        column: x => x.UsuarioInfoID,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CGMateria",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CursoGradeID = table.Column<long>(type: "bigint", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    Grupo = table.Column<string>(type: "longtext", nullable: true),
                    MateriaID = table.Column<long>(type: "bigint", nullable: true),
                    NomeExibicao = table.Column<string>(type: "longtext", nullable: true),
                    NumeroAulas = table.Column<long>(type: "bigint", nullable: false),
                    Tags = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CGMateria", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CGMateria_CGrade_CursoGradeID",
                        column: x => x.CursoGradeID,
                        principalTable: "CGrade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CGMateria_Materia_MateriaID",
                        column: x => x.MateriaID,
                        principalTable: "Materia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICurso",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CursoGradeID = table.Column<long>(type: "bigint", nullable: true),
                    CursoID = table.Column<long>(type: "bigint", nullable: true),
                    DataExpiracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InstituicaoID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICurso", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICurso_CGrade_CursoGradeID",
                        column: x => x.CursoGradeID,
                        principalTable: "CGrade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICurso_Curso_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Curso",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICurso_Instituicao_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Instituicao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICOcorrencia",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CoordenadorID = table.Column<string>(type: "varchar(127)", nullable: true),
                    DataExpiracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InstituicaoCursoID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICOcorrencia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICOcorrencia_UInfo_CoordenadorID",
                        column: x => x.CoordenadorID,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOcorrencia_ICurso_InstituicaoCursoID",
                        column: x => x.InstituicaoCursoID,
                        principalTable: "ICurso",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICPeriodo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Dom = table.Column<bool>(type: "bit", nullable: false),
                    Fim = table.Column<string>(type: "longtext", nullable: true),
                    Inicio = table.Column<string>(type: "longtext", nullable: true),
                    InstituicaoCursoID = table.Column<long>(type: "bigint", nullable: true),
                    PausaFim = table.Column<string>(type: "longtext", nullable: true),
                    PausaInicio = table.Column<string>(type: "longtext", nullable: true),
                    Qua = table.Column<bool>(type: "bit", nullable: false),
                    Quebras = table.Column<long>(type: "bigint", nullable: false),
                    Qui = table.Column<bool>(type: "bit", nullable: false),
                    Sab = table.Column<bool>(type: "bit", nullable: false),
                    Seg = table.Column<bool>(type: "bit", nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    Ter = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICPeriodo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICPeriodo_ICurso_InstituicaoCursoID",
                        column: x => x.InstituicaoCursoID,
                        principalTable: "ICurso",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICTurma",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataExpiracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    InstituicaoCursoID = table.Column<long>(type: "bigint", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICTurma", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICTurma_ICurso_InstituicaoCursoID",
                        column: x => x.InstituicaoCursoID,
                        principalTable: "ICurso",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICOAluno",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AlunoID = table.Column<string>(type: "varchar(127)", nullable: true),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataExpiracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpiracaoMotivo = table.Column<int>(type: "int", nullable: false),
                    InstituicaoCursoOcorrenciaID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICOAluno", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICOAluno_UInfo_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOAluno_ICOcorrencia_InstituicaoCursoOcorrenciaID",
                        column: x => x.InstituicaoCursoOcorrenciaID,
                        principalTable: "ICOcorrencia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICOPeriodo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataExpiracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InstituicaoCursoOcorrenciaID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICOPeriodo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICOPeriodo_ICOcorrencia_InstituicaoCursoOcorrenciaID",
                        column: x => x.InstituicaoCursoOcorrenciaID,
                        principalTable: "ICOcorrencia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICOPAluno",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataConfirmacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InstituicaoCursoOcorrenciaAlunoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoOcorrenciaPeriodoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoPeriodoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoTurmaID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICOPAluno", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICOPAluno_ICOAluno_InstituicaoCursoOcorrenciaAlunoID",
                        column: x => x.InstituicaoCursoOcorrenciaAlunoID,
                        principalTable: "ICOAluno",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOPAluno_ICOPeriodo_InstituicaoCursoOcorrenciaPeriodoID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoID,
                        principalTable: "ICOPeriodo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOPAluno_ICPeriodo_InstituicaoCursoPeriodoID",
                        column: x => x.InstituicaoCursoPeriodoID,
                        principalTable: "ICPeriodo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOPAluno_ICTurma_InstituicaoCursoTurmaID",
                        column: x => x.InstituicaoCursoTurmaID,
                        principalTable: "ICTurma",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICOPProfessor",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    CursoGradeMateriaID = table.Column<long>(type: "bigint", nullable: true),
                    DataExpiracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FormulaNotaFinal = table.Column<string>(type: "longtext", nullable: true),
                    InstituicaoCursoOcorrenciaPeriodoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoPeriodoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoTurmaID = table.Column<long>(type: "bigint", nullable: true),
                    ProfessorID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICOPProfessor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICOPProfessor_CGMateria_CursoGradeMateriaID",
                        column: x => x.CursoGradeMateriaID,
                        principalTable: "CGMateria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOPProfessor_ICOPeriodo_InstituicaoCursoOcorrenciaPeriodoID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoID,
                        principalTable: "ICOPeriodo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOPProfessor_ICPeriodo_InstituicaoCursoPeriodoID",
                        column: x => x.InstituicaoCursoPeriodoID,
                        principalTable: "ICPeriodo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOPProfessor_ICTurma_InstituicaoCursoTurmaID",
                        column: x => x.InstituicaoCursoTurmaID,
                        principalTable: "ICTurma",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOPProfessor_UInfo_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "UInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICOAusencia",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataAusencia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    InstituicaoCursoOcorrenciaPeriodoAlunoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoOcorrenciaPeriodoProfessorID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICOAusencia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICOAusencia_ICOPAluno_InstituicaoCursoOcorrenciaPeriodoAlunoID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoAlunoID,
                        principalTable: "ICOPAluno",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICOAusencia_ICOPProfessor_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoProfessorID,
                        principalTable: "ICOPProfessor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICONota",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataLancamento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IDTag = table.Column<string>(type: "longtext", nullable: true),
                    InstituicaoCursoOcorrenciaPeriodoAlunoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoOcorrenciaPeriodoProfessorID = table.Column<long>(type: "bigint", nullable: true),
                    Valor = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICONota", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICONota_ICOPAluno_InstituicaoCursoOcorrenciaPeriodoAlunoID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoAlunoID,
                        principalTable: "ICOPAluno",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ICONota_ICOPProfessor_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoProfessorID,
                        principalTable: "ICOPProfessor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ICOPPPeriodoAula",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Dia = table.Column<int>(type: "int", nullable: false),
                    Fim = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Inicio = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    InstituicaoCursoOcorrenciaPeriodoProfessorID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICOPPPeriodoAula", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ICOPPPeriodoAula_ICOPProfessor_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoProfessorID,
                        principalTable: "ICOPProfessor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_UsuarioInfoID",
                table: "Aluno",
                column: "UsuarioInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_AreaInteresse_CategoriaProfissionalID",
                table: "AreaInteresse",
                column: "CategoriaProfissionalID");

            migrationBuilder.CreateIndex(
                name: "IX_AreaInteresse_UsuarioInfoID",
                table: "AreaInteresse",
                column: "UsuarioInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_CGMateria_CursoGradeID",
                table: "CGMateria",
                column: "CursoGradeID");

            migrationBuilder.CreateIndex(
                name: "IX_CGMateria_MateriaID",
                table: "CGMateria",
                column: "MateriaID");

            migrationBuilder.CreateIndex(
                name: "IX_CGrade_CursoID",
                table: "CGrade",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_CGrade_InstituicaoID",
                table: "CGrade",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOAluno_AlunoID",
                table: "ICOAluno",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOAluno_InstituicaoCursoOcorrenciaID",
                table: "ICOAluno",
                column: "InstituicaoCursoOcorrenciaID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOAusencia_InstituicaoCursoOcorrenciaPeriodoAlunoID",
                table: "ICOAusencia",
                column: "InstituicaoCursoOcorrenciaPeriodoAlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOAusencia_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                table: "ICOAusencia",
                column: "InstituicaoCursoOcorrenciaPeriodoProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOcorrencia_CoordenadorID",
                table: "ICOcorrencia",
                column: "CoordenadorID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOcorrencia_InstituicaoCursoID",
                table: "ICOcorrencia",
                column: "InstituicaoCursoID");

            migrationBuilder.CreateIndex(
                name: "IX_IColaborador_InstituicaoID",
                table: "IColaborador",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_IColaborador_UsuarioID",
                table: "IColaborador",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_ICONota_InstituicaoCursoOcorrenciaPeriodoAlunoID",
                table: "ICONota",
                column: "InstituicaoCursoOcorrenciaPeriodoAlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICONota_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                table: "ICONota",
                column: "InstituicaoCursoOcorrenciaPeriodoProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPAluno_InstituicaoCursoOcorrenciaAlunoID",
                table: "ICOPAluno",
                column: "InstituicaoCursoOcorrenciaAlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPAluno_InstituicaoCursoOcorrenciaPeriodoID",
                table: "ICOPAluno",
                column: "InstituicaoCursoOcorrenciaPeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPAluno_InstituicaoCursoPeriodoID",
                table: "ICOPAluno",
                column: "InstituicaoCursoPeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPAluno_InstituicaoCursoTurmaID",
                table: "ICOPAluno",
                column: "InstituicaoCursoTurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPeriodo_InstituicaoCursoOcorrenciaID",
                table: "ICOPeriodo",
                column: "InstituicaoCursoOcorrenciaID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPPPeriodoAula_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                table: "ICOPPPeriodoAula",
                column: "InstituicaoCursoOcorrenciaPeriodoProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPProfessor_CursoGradeMateriaID",
                table: "ICOPProfessor",
                column: "CursoGradeMateriaID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPProfessor_InstituicaoCursoOcorrenciaPeriodoID",
                table: "ICOPProfessor",
                column: "InstituicaoCursoOcorrenciaPeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPProfessor_InstituicaoCursoPeriodoID",
                table: "ICOPProfessor",
                column: "InstituicaoCursoPeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPProfessor_InstituicaoCursoTurmaID",
                table: "ICOPProfessor",
                column: "InstituicaoCursoTurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_ICOPProfessor_ProfessorID",
                table: "ICOPProfessor",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_ICPerfil_InstituicaoID",
                table: "ICPerfil",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICPeriodo_InstituicaoCursoID",
                table: "ICPeriodo",
                column: "InstituicaoCursoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICTurma_InstituicaoCursoID",
                table: "ICTurma",
                column: "InstituicaoCursoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICurso_CursoGradeID",
                table: "ICurso",
                column: "CursoGradeID");

            migrationBuilder.CreateIndex(
                name: "IX_ICurso_CursoID",
                table: "ICurso",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_ICurso_InstituicaoID",
                table: "ICurso",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_IInstituicaoCategoria_InstituicaoCategoriaID",
                table: "IInstituicaoCategoria",
                column: "InstituicaoCategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_IInstituicaoCategoria_InstituicaoID",
                table: "IInstituicaoCategoria",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaRelacionamento_MateriaPaiID",
                table: "MateriaRelacionamento",
                column: "MateriaPaiID");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaRelacionamento_MateriaPrincipalID",
                table: "MateriaRelacionamento",
                column: "MateriaPrincipalID");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_UsuarioInfoID",
                table: "Professor",
                column: "UsuarioInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_UInfo_EnderecoID",
                table: "UInfo",
                column: "EnderecoID");

            migrationBuilder.CreateIndex(
                name: "IX_UInfo_idMae",
                table: "UInfo",
                column: "idMae");

            migrationBuilder.CreateIndex(
                name: "IX_UInfo_idPai",
                table: "UInfo",
                column: "idPai");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UsuarioInfoID",
                table: "Usuario",
                column: "UsuarioInfoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "AreaInteresse");

            migrationBuilder.DropTable(
                name: "ICOAusencia");

            migrationBuilder.DropTable(
                name: "IColaborador");

            migrationBuilder.DropTable(
                name: "ICONota");

            migrationBuilder.DropTable(
                name: "ICOPPPeriodoAula");

            migrationBuilder.DropTable(
                name: "ICPerfil");

            migrationBuilder.DropTable(
                name: "IInstituicaoCategoria");

            migrationBuilder.DropTable(
                name: "MateriaRelacionamento");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "CategoriaProfissional");

            migrationBuilder.DropTable(
                name: "ICOPAluno");

            migrationBuilder.DropTable(
                name: "ICOPProfessor");

            migrationBuilder.DropTable(
                name: "ICategoria");

            migrationBuilder.DropTable(
                name: "ICOAluno");

            migrationBuilder.DropTable(
                name: "CGMateria");

            migrationBuilder.DropTable(
                name: "ICOPeriodo");

            migrationBuilder.DropTable(
                name: "ICPeriodo");

            migrationBuilder.DropTable(
                name: "ICTurma");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "ICOcorrencia");

            migrationBuilder.DropTable(
                name: "UInfo");

            migrationBuilder.DropTable(
                name: "ICurso");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "CGrade");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Instituicao");
        }
    }
}
