using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.Migrations
{
    public partial class MigInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crs",
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
                    table.PrimaryKey("PK_Crs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CtgPrfsn",
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
                    table.PrimaryKey("PK_CtgPrfsn", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Endrc",
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
                    table.PrimaryKey("PK_Endrc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ittc",
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
                    table.PrimaryKey("PK_Ittc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IttcCtgr",
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
                    table.PrimaryKey("PK_IttcCtgr", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Mtr",
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
                    table.PrimaryKey("PK_Mtr", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CrsGrd",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CursoID = table.Column<long>(type: "bigint", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrsGrd", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CrsGrd_Crs_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Crs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsrInf",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(127)", nullable: false),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CPF = table.Column<string>(type: "longtext", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EnderecoID = table.Column<long>(type: "bigint", nullable: true),
                    EstadoCivil = table.Column<string>(type: "longtext", nullable: true),
                    Genero = table.Column<string>(type: "longtext", nullable: true),
                    Nome = table.Column<string>(type: "longtext", nullable: true),
                    RG = table.Column<string>(type: "longtext", nullable: true),
                    Roles = table.Column<string>(type: "longtext", nullable: true),
                    idMae = table.Column<string>(type: "varchar(127)", nullable: true),
                    idPai = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsrInf", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UsrInf_Endrc_EnderecoID",
                        column: x => x.EnderecoID,
                        principalTable: "Endrc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsrInf_UsrInf_idMae",
                        column: x => x.idMae,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsrInf_UsrInf_idPai",
                        column: x => x.idPai,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcClbdPrf",
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
                    table.PrimaryKey("PK_IttcClbdPrf", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcClbdPrf_Ittc_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Ittc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcIttcCtgr",
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
                    table.PrimaryKey("PK_IttcIttcCtgr", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcIttcCtgr_IttcCtgr_InstituicaoCategoriaID",
                        column: x => x.InstituicaoCategoriaID,
                        principalTable: "IttcCtgr",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcIttcCtgr_Ittc_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Ittc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MtrRlc",
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
                    table.PrimaryKey("PK_MtrRlc", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MtrRlc_Mtr_MateriaPaiID",
                        column: x => x.MateriaPaiID,
                        principalTable: "Mtr",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MtrRlc_Mtr_MateriaPrincipalID",
                        column: x => x.MateriaPrincipalID,
                        principalTable: "Mtr",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrsGrdMtr",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CursoGradeID = table.Column<long>(type: "bigint", nullable: true),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    MateriaID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrsGrdMtr", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CrsGrdMtr_CrsGrd_CursoGradeID",
                        column: x => x.CursoGradeID,
                        principalTable: "CrsGrd",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrsGrdMtr_Mtr_MateriaID",
                        column: x => x.MateriaID,
                        principalTable: "Mtr",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CursoGradeID = table.Column<long>(type: "bigint", nullable: true),
                    CursoID = table.Column<long>(type: "bigint", nullable: true),
                    DataExpiracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    InstituicaoID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IttcCrs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrs_CrsGrd_CursoGradeID",
                        column: x => x.CursoGradeID,
                        principalTable: "CrsGrd",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrs_Crs_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Crs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrs_Ittc_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Ittc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aln",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(127)", nullable: false),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioInfoID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aln", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Aln_UsrInf_UsuarioInfoID",
                        column: x => x.UsuarioInfoID,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArItrs",
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
                    table.PrimaryKey("PK_ArItrs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArItrs_CtgPrfsn_CategoriaProfissionalID",
                        column: x => x.CategoriaProfissionalID,
                        principalTable: "CtgPrfsn",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArItrs_UsrInf_UsuarioInfoID",
                        column: x => x.UsuarioInfoID,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcClbd",
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
                    table.PrimaryKey("PK_IttcClbd", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcClbd_Ittc_InstituicaoID",
                        column: x => x.InstituicaoID,
                        principalTable: "Ittc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcClbd_UsrInf_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prf",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(127)", nullable: false),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioInfoID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prf", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prf_UsrInf_UsuarioInfoID",
                        column: x => x.UsuarioInfoID,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usr",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(127)", nullable: false),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Password = table.Column<string>(type: "longtext", nullable: true),
                    Username = table.Column<string>(type: "longtext", nullable: true),
                    UsuarioInfoID = table.Column<string>(type: "varchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usr", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Usr_UsrInf_UsuarioInfoID",
                        column: x => x.UsuarioInfoID,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsOcrnc",
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
                    table.PrimaryKey("PK_IttcCrsOcrnc", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrnc_UsrInf_CoordenadorID",
                        column: x => x.CoordenadorID,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrnc_IttcCrs_InstituicaoCursoID",
                        column: x => x.InstituicaoCursoID,
                        principalTable: "IttcCrs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsPrd",
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
                    table.PrimaryKey("PK_IttcCrsPrd", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsPrd_IttcCrs_InstituicaoCursoID",
                        column: x => x.InstituicaoCursoID,
                        principalTable: "IttcCrs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsTrm",
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
                    table.PrimaryKey("PK_IttcCrsTrm", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsTrm_IttcCrs_InstituicaoCursoID",
                        column: x => x.InstituicaoCursoID,
                        principalTable: "IttcCrs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsOcrncAln",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AlunoID = table.Column<string>(type: "varchar(127)", nullable: true),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpiracaoMotivo = table.Column<int>(type: "int", nullable: false),
                    InstituicaoCursoOcorrenciaID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IttcCrsOcrncAln", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncAln_UsrInf_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncAln_IttcCrsOcrnc_InstituicaoCursoOcorrenciaID",
                        column: x => x.InstituicaoCursoOcorrenciaID,
                        principalTable: "IttcCrsOcrnc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsOcrncPrd",
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
                    table.PrimaryKey("PK_IttcCrsOcrncPrd", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrd_IttcCrsOcrnc_InstituicaoCursoOcorrenciaID",
                        column: x => x.InstituicaoCursoOcorrenciaID,
                        principalTable: "IttcCrsOcrnc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsOcrncPrdAln",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    InstituicaoCursoOcorrenciaAlunoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoOcorrenciaPeriodoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoPeriodoID = table.Column<long>(type: "bigint", nullable: true),
                    InstituicaoCursoTurmaID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IttcCrsOcrncPrdAln", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdAln_IttcCrsOcrncAln_InstituicaoCursoOcorrenciaAlunoID",
                        column: x => x.InstituicaoCursoOcorrenciaAlunoID,
                        principalTable: "IttcCrsOcrncAln",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdAln_IttcCrsOcrncPrd_InstituicaoCursoOcorrenciaPeriodoID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoID,
                        principalTable: "IttcCrsOcrncPrd",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdAln_IttcCrsPrd_InstituicaoCursoPeriodoID",
                        column: x => x.InstituicaoCursoPeriodoID,
                        principalTable: "IttcCrsPrd",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdAln_IttcCrsTrm_InstituicaoCursoTurmaID",
                        column: x => x.InstituicaoCursoTurmaID,
                        principalTable: "IttcCrsTrm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsOcrncPrdPrf",
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
                    table.PrimaryKey("PK_IttcCrsOcrncPrdPrf", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdPrf_CrsGrdMtr_CursoGradeMateriaID",
                        column: x => x.CursoGradeMateriaID,
                        principalTable: "CrsGrdMtr",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdPrf_IttcCrsOcrncPrd_InstituicaoCursoOcorrenciaPeriodoID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoID,
                        principalTable: "IttcCrsOcrncPrd",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdPrf_IttcCrsPrd_InstituicaoCursoPeriodoID",
                        column: x => x.InstituicaoCursoPeriodoID,
                        principalTable: "IttcCrsPrd",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdPrf_IttcCrsTrm_InstituicaoCursoTurmaID",
                        column: x => x.InstituicaoCursoTurmaID,
                        principalTable: "IttcCrsTrm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdPrf_UsrInf_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "UsrInf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsOcrncNt",
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
                    table.PrimaryKey("PK_IttcCrsOcrncNt", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncNt_IttcCrsOcrncPrdAln_InstituicaoCursoOcorrenciaPeriodoAlunoID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoAlunoID,
                        principalTable: "IttcCrsOcrncPrdAln",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncNt_IttcCrsOcrncPrdPrf_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoProfessorID,
                        principalTable: "IttcCrsOcrncPrdPrf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IttcCrsOcrncPrdPrfPrdAul",
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
                    table.PrimaryKey("PK_IttcCrsOcrncPrdPrfPrdAul", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IttcCrsOcrncPrdPrfPrdAul_IttcCrsOcrncPrdPrf_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                        column: x => x.InstituicaoCursoOcorrenciaPeriodoProfessorID,
                        principalTable: "IttcCrsOcrncPrdPrf",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aln_UsuarioInfoID",
                table: "Aln",
                column: "UsuarioInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_ArItrs_CategoriaProfissionalID",
                table: "ArItrs",
                column: "CategoriaProfissionalID");

            migrationBuilder.CreateIndex(
                name: "IX_ArItrs_UsuarioInfoID",
                table: "ArItrs",
                column: "UsuarioInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_CrsGrd_CursoID",
                table: "CrsGrd",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_CrsGrdMtr_CursoGradeID",
                table: "CrsGrdMtr",
                column: "CursoGradeID");

            migrationBuilder.CreateIndex(
                name: "IX_CrsGrdMtr_MateriaID",
                table: "CrsGrdMtr",
                column: "MateriaID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcClbd_InstituicaoID",
                table: "IttcClbd",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcClbd_UsuarioID",
                table: "IttcClbd",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcClbdPrf_InstituicaoID",
                table: "IttcClbdPrf",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrs_CursoGradeID",
                table: "IttcCrs",
                column: "CursoGradeID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrs_CursoID",
                table: "IttcCrs",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrs_InstituicaoID",
                table: "IttcCrs",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrnc_CoordenadorID",
                table: "IttcCrsOcrnc",
                column: "CoordenadorID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrnc_InstituicaoCursoID",
                table: "IttcCrsOcrnc",
                column: "InstituicaoCursoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncAln_AlunoID",
                table: "IttcCrsOcrncAln",
                column: "AlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncAln_InstituicaoCursoOcorrenciaID",
                table: "IttcCrsOcrncAln",
                column: "InstituicaoCursoOcorrenciaID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncNt_InstituicaoCursoOcorrenciaPeriodoAlunoID",
                table: "IttcCrsOcrncNt",
                column: "InstituicaoCursoOcorrenciaPeriodoAlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncNt_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                table: "IttcCrsOcrncNt",
                column: "InstituicaoCursoOcorrenciaPeriodoProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrd_InstituicaoCursoOcorrenciaID",
                table: "IttcCrsOcrncPrd",
                column: "InstituicaoCursoOcorrenciaID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdAln_InstituicaoCursoOcorrenciaAlunoID",
                table: "IttcCrsOcrncPrdAln",
                column: "InstituicaoCursoOcorrenciaAlunoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdAln_InstituicaoCursoOcorrenciaPeriodoID",
                table: "IttcCrsOcrncPrdAln",
                column: "InstituicaoCursoOcorrenciaPeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdAln_InstituicaoCursoPeriodoID",
                table: "IttcCrsOcrncPrdAln",
                column: "InstituicaoCursoPeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdAln_InstituicaoCursoTurmaID",
                table: "IttcCrsOcrncPrdAln",
                column: "InstituicaoCursoTurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdPrf_CursoGradeMateriaID",
                table: "IttcCrsOcrncPrdPrf",
                column: "CursoGradeMateriaID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdPrf_InstituicaoCursoOcorrenciaPeriodoID",
                table: "IttcCrsOcrncPrdPrf",
                column: "InstituicaoCursoOcorrenciaPeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdPrf_InstituicaoCursoPeriodoID",
                table: "IttcCrsOcrncPrdPrf",
                column: "InstituicaoCursoPeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdPrf_InstituicaoCursoTurmaID",
                table: "IttcCrsOcrncPrdPrf",
                column: "InstituicaoCursoTurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdPrf_ProfessorID",
                table: "IttcCrsOcrncPrdPrf",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsOcrncPrdPrfPrdAul_InstituicaoCursoOcorrenciaPeriodoProfessorID",
                table: "IttcCrsOcrncPrdPrfPrdAul",
                column: "InstituicaoCursoOcorrenciaPeriodoProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsPrd_InstituicaoCursoID",
                table: "IttcCrsPrd",
                column: "InstituicaoCursoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcCrsTrm_InstituicaoCursoID",
                table: "IttcCrsTrm",
                column: "InstituicaoCursoID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcIttcCtgr_InstituicaoCategoriaID",
                table: "IttcIttcCtgr",
                column: "InstituicaoCategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_IttcIttcCtgr_InstituicaoID",
                table: "IttcIttcCtgr",
                column: "InstituicaoID");

            migrationBuilder.CreateIndex(
                name: "IX_MtrRlc_MateriaPaiID",
                table: "MtrRlc",
                column: "MateriaPaiID");

            migrationBuilder.CreateIndex(
                name: "IX_MtrRlc_MateriaPrincipalID",
                table: "MtrRlc",
                column: "MateriaPrincipalID");

            migrationBuilder.CreateIndex(
                name: "IX_Prf_UsuarioInfoID",
                table: "Prf",
                column: "UsuarioInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Usr_UsuarioInfoID",
                table: "Usr",
                column: "UsuarioInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_UsrInf_EnderecoID",
                table: "UsrInf",
                column: "EnderecoID");

            migrationBuilder.CreateIndex(
                name: "IX_UsrInf_idMae",
                table: "UsrInf",
                column: "idMae");

            migrationBuilder.CreateIndex(
                name: "IX_UsrInf_idPai",
                table: "UsrInf",
                column: "idPai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aln");

            migrationBuilder.DropTable(
                name: "ArItrs");

            migrationBuilder.DropTable(
                name: "IttcClbd");

            migrationBuilder.DropTable(
                name: "IttcClbdPrf");

            migrationBuilder.DropTable(
                name: "IttcCrsOcrncNt");

            migrationBuilder.DropTable(
                name: "IttcCrsOcrncPrdPrfPrdAul");

            migrationBuilder.DropTable(
                name: "IttcIttcCtgr");

            migrationBuilder.DropTable(
                name: "MtrRlc");

            migrationBuilder.DropTable(
                name: "Prf");

            migrationBuilder.DropTable(
                name: "Usr");

            migrationBuilder.DropTable(
                name: "CtgPrfsn");

            migrationBuilder.DropTable(
                name: "IttcCrsOcrncPrdAln");

            migrationBuilder.DropTable(
                name: "IttcCrsOcrncPrdPrf");

            migrationBuilder.DropTable(
                name: "IttcCtgr");

            migrationBuilder.DropTable(
                name: "IttcCrsOcrncAln");

            migrationBuilder.DropTable(
                name: "CrsGrdMtr");

            migrationBuilder.DropTable(
                name: "IttcCrsOcrncPrd");

            migrationBuilder.DropTable(
                name: "IttcCrsPrd");

            migrationBuilder.DropTable(
                name: "IttcCrsTrm");

            migrationBuilder.DropTable(
                name: "Mtr");

            migrationBuilder.DropTable(
                name: "IttcCrsOcrnc");

            migrationBuilder.DropTable(
                name: "UsrInf");

            migrationBuilder.DropTable(
                name: "IttcCrs");

            migrationBuilder.DropTable(
                name: "Endrc");

            migrationBuilder.DropTable(
                name: "CrsGrd");

            migrationBuilder.DropTable(
                name: "Ittc");

            migrationBuilder.DropTable(
                name: "Crs");
        }
    }
}
