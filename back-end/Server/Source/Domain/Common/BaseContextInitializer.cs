using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.CategoriaProfissionalDomain;
using Domain.CursoDomain;
using Domain.EnderecoDomain;
using Domain.InstituicaoDomain;
using Domain.MateriaDomain;
using Domain.UsuarioDomain;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common {
    public static class BaseContextInitializer {
        public static void Initialize(BaseContext context) {

            context.Database.EnsureCreated();

            if (context.Usr.ToList().Count == 0) {

                var listUsuario = getBaseUsuarios();
                context.Usr.AddRange(listUsuario);
                context.SaveChanges();

                var listCurso = BaseContextInitializer.getBaseCursos();
                context.Crs.AddRange(listCurso);
                context.SaveChanges();

                var listMateria = BaseContextInitializer.getBaseMaterias();
                context.Mtr.AddRange(listMateria);
                context.SaveChanges();

                var listCursoGrade = BaseContextInitializer.getBaseCursoGrades(listCurso);
                context.CrsGrd.AddRange(listCursoGrade);
                context.SaveChanges();

                var listCursoGradeMateria = BaseContextInitializer.getBaseCursoGradeMaterias(listCursoGrade, listMateria);
                context.CrsGrdMtr.AddRange(listCursoGradeMateria);
                context.SaveChanges();

                var listInstituicoes = BaseContextInitializer.getBaseInstituicoes();
                context.Ittc.AddRange(listInstituicoes);
                context.SaveChanges();

                var listAlunos = BaseContextInitializer.getBaseAlunos(listUsuario);
                context.Aln.AddRange(listAlunos);
                context.SaveChanges();

                var listProfessores = BaseContextInitializer.getBaseProfessores(listUsuario);
                context.Prf.AddRange(listProfessores);
                context.SaveChanges();                

                var listInstituicaoCurso = BaseContextInitializer.getBaseInstituicaoCursos(listCursoGrade, listInstituicoes);
                context.IttcCrs.AddRange(listInstituicaoCurso);
                context.SaveChanges();

                var listInstituicaoCursoPeriodo = BaseContextInitializer.getBaseInstituicaoCursoPeriodos(listInstituicaoCurso);
                context.IttcCrsPrd.AddRange(listInstituicaoCursoPeriodo);
                context.SaveChanges();

                var listInstituicaoCursoTurma = BaseContextInitializer.getBaseInstituicaoCursoTurma(listInstituicaoCurso);
                context.IttcCrsTrm.AddRange(listInstituicaoCursoTurma);
                context.SaveChanges();

                var listInstituicaoCursoOcorrencia = BaseContextInitializer.getBaseInstituicaoCursoOcorrencias(listInstituicaoCurso, listUsuario);
                context.IttcCrsOcrnc.AddRange(listInstituicaoCursoOcorrencia);
                context.SaveChanges();

                var listInstituicaoCursoOcorrenciaAluno = BaseContextInitializer.getBaseInstituicaoCursoOcorrenciaAlunos(listUsuario, listInstituicaoCursoOcorrencia);
                context.IttcCrsOcrncAln.AddRange(listInstituicaoCursoOcorrenciaAluno);
                context.SaveChanges();

                var listInstituicaoCursoOcorrenciaPeriodo = BaseContextInitializer.getBaseInstituicaoCursoOcorrenciaPeriodos(listInstituicaoCursoOcorrencia);
                context.IttcCrsOcrncPrd.AddRange(listInstituicaoCursoOcorrenciaPeriodo);
                context.SaveChanges();

                var listInstituicaoCursoOcorrenciaPeriodoAluno = BaseContextInitializer.getBaseInstituicaoCursoOcorrenciaPeriodoAlunos(listInstituicaoCursoOcorrenciaAluno, listInstituicaoCursoOcorrenciaPeriodo, listInstituicaoCursoPeriodo, listInstituicaoCursoTurma);
                context.IttcCrsOcrncPrdAln.AddRange(listInstituicaoCursoOcorrenciaPeriodoAluno);
                context.SaveChanges();

                var listInstituicaoCursoOcorrenciaPeriodoProfessor = BaseContextInitializer.getBaseInstituicaoCursoOcorrenciaPeriodoProfessores(listUsuario, listCursoGradeMateria, listInstituicaoCursoOcorrenciaPeriodo, listInstituicaoCursoPeriodo, listInstituicaoCursoTurma);
                context.IttcCrsOcrncPrdPrf.AddRange(listInstituicaoCursoOcorrenciaPeriodoProfessor);
                context.SaveChanges();

                var listInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = BaseContextInitializer.getBaseInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas(listInstituicaoCursoOcorrenciaPeriodoProfessor);
                context.IttcCrsOcrncPrdPrfPrdAul.AddRange(listInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula);
                context.SaveChanges();

                var listCategoriaProfissional = BaseContextInitializer.getBaseCategoriaProfissionais();
                context.CtgPrfsn.AddRange(listCategoriaProfissional);

                context.SaveChanges();
                
            }
        }

        public static Instituicao[] getBaseInstituicoes() {
            return new Instituicao[] {
                    new Instituicao () {
                            Nome = "ESCOLA TÉCNICA PROFESSOR EVERALDO PASSOS - ETEP",
                                CNPJ = "4211213321321"
                        },
                        new Instituicao () {
                            Nome = "FACULDADE DE TÉCNOLOGIA - FATEC",
                                CNPJ = "4211213321321"
                        },
                };
        }

        public static Materia[] getBaseMaterias() {
            return new Materia[] {
                    new Materia () {
                            Nome = "Português Fundamental",
                                Descricao = "Língua Portuguêsa"
                        },
                        new Materia () {
                            Nome = "Matemática Fundamental",
                                Descricao = "Matemática em geral."
                        },
                        new Materia () {
                            Nome = "Matemática Aplicada",
                                Descricao = "Língua Portuguêsa"
                        },
                        new Materia () {
                            Nome = "Matemática Financeira",
                                Descricao = "Matemática para Calculos e Finanças"
                        },
                        new Materia () {
                            Nome = "Desenvolvimento de Software",
                                Descricao = "Desenvolvimento de Softwares"
                        },
                        new Materia () {
                            Nome = "Sistemas Operacionais",
                                Descricao = "Sistemas Operacionais"
                        },
                };
        }

        public static Curso[] getBaseCursos() {
            return new Curso[] {
                    new Curso () {
                            Nome = "Técnologia em Análise e Desenvolvimento de Sistemas",
                                Descricao = "Técnologia em Análise e Desenvolvimento de Sistemas"
                        },
                        new Curso () {
                            Nome = "Técnologia em Administração de Empresas",
                                Descricao = "Técnologia em Administração de Empresas"
                        },
                        new Curso () {
                            Nome = "Ensino Fundamental",
                                Descricao = "Ensino Fundamental"
                        },
                        new Curso () {
                            Nome = "Ensino Médio",
                                Descricao = "Ensino Médio"
                        }
                };
        }

        public static CursoGrade[] getBaseCursoGrades(Curso[] cursos) {
            return new CursoGrade[] {
                    new CursoGrade () {
                            Curso = cursos[0],
                                DataCriacao = DateTime.Now,
                                Descricao = "ETEP Faculdades"
                        },
                        new CursoGrade () {
                            Curso = cursos[1],
                                DataCriacao = DateTime.Now,
                                Descricao = "ETEP Faculdades"
                        }
                };
        }

        public static CursoGradeMateria[] getBaseCursoGradeMaterias(CursoGrade[] cursoGrades, Materia[] materias) {
            return new CursoGradeMateria[] {
                    new CursoGradeMateria () {
                            CursoGrade = cursoGrades[0],
                                Materia = materias[2],
                                Descricao = materias[2].Nome
                        },
                        new CursoGradeMateria () {
                            CursoGrade = cursoGrades[0],
                                Materia = materias[4],
                                Descricao = "Orientação a Objetos"
                        },
                        new CursoGradeMateria () {
                            CursoGrade = cursoGrades[0],
                                Materia = materias[4],
                                Descricao = "Java"
                        },
                        new CursoGradeMateria () {
                            CursoGrade = cursoGrades[0],
                                Materia = materias[4],
                                Descricao = "C#"
                        },
                        new CursoGradeMateria () {
                            CursoGrade = cursoGrades[0],
                                Materia = materias[4],
                                Descricao = "Javascript"
                        },
                        new CursoGradeMateria () {
                            CursoGrade = cursoGrades[0],
                                Materia = materias[5],
                                Descricao = materias[2].Nome
                        },
                        new CursoGradeMateria () {
                            CursoGrade = cursoGrades[1],
                                Materia = materias[2],
                                Descricao = materias[2].Nome
                        },
                };
        }

        public static Usuario[] getBaseUsuarios() {
            var carvalho = new Usuario() {
                Username = "dev",
                Password = "dev",
                Email = "dev@ezschool.com",
            };
            var carvalhoInfo = new UsuarioInfo() {
                ID = carvalho.ID,
                Nome = "Matheus Carvalho",
                DataNascimento = DateTime.ParseExact("1994-12-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                Roles = "ADMIN",
                CPF = "42187917835",
                RG = "421920816",
                Endereco = new Endereco()
            };
            carvalho.UsuarioInfo = carvalhoInfo;

            var marcal = new Usuario() {
                Username = "qa",
                Password = "qa",
                Email = "qa@ezschool.com"
            };
            var marcalInfo = new UsuarioInfo() {
                ID = marcal.ID,
                Nome = "Matheus Marçal",
                DataNascimento = DateTime.ParseExact("1994-12-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                Roles = "",
                CPF = "42187917835",
                RG = "421920816",
                Endereco = new Endereco()
            };
            marcal.UsuarioInfo = marcalInfo;

            var thais = new Usuario() {
                Username = "thsmimi",
                Password = "12345678",
                Email = "tha_araujo@hotmail.com"
            };
            var thaisInfo = new UsuarioInfo() {
                ID = thais.ID,
                Nome = "Thais Araújo Santos",
                DataNascimento = DateTime.ParseExact("1994-12-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                Roles = "",
                CPF = "52176819820",
                RG = "510984128",
                Endereco = new Endereco()
            };
            thais.UsuarioInfo = thaisInfo;

            var barbara = new Usuario() {
                Username = "anabarbara",
                Password = "12345678",
                Email = "ana_barbara@hotmail.com"
            };
            var barbaraInfo = new UsuarioInfo() {
                ID = barbara.ID,
                Nome = "Ana Bárbara",
                DataNascimento = DateTime.ParseExact("1994-12-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                Roles = "",
                CPF = "768309116406",
                RG = "760942814",
                Endereco = new Endereco()
            };
            barbara.UsuarioInfo = barbaraInfo;

            return new Usuario[] {
                    carvalho,
                    marcal,
                    thais,
                    barbara
                };
        }

        public static Aluno[] getBaseAlunos(Usuario[] usuarios) {
            var alunos = new List<Aluno>();

            foreach (var usuario in usuarios) {
                var aluno = new Aluno();
                aluno.ID = usuario.ID;
                aluno.UsuarioInfo = usuario.UsuarioInfo;
                alunos.Add(aluno);
            }

            return alunos.ToArray();

        }

        public static Professor[] getBaseProfessores(Usuario[] usuarios) {
            var professores = new List<Professor>();

            foreach (var usuario in usuarios) {
                var professor = new Professor();
                professor.ID = usuario.ID;
                professor.UsuarioInfo = usuario.UsuarioInfo;
                professores.Add(professor);
            }

            return professores.ToArray();

        }

        public static InstituicaoCurso[] getBaseInstituicaoCursos(CursoGrade[] cursoGrades, Instituicao[] instituicoes) {
            return new InstituicaoCurso[]{
                new InstituicaoCurso(){
                    Curso = cursoGrades[0].Curso,
                    CursoGrade = cursoGrades[0],
                    DataInicio=DateTime.Now,
                    Instituicao = instituicoes[0],
                }
            };
        }

        public static InstituicaoCursoOcorrencia[] getBaseInstituicaoCursoOcorrencias(InstituicaoCurso[] instituicaoCursos, Usuario[] professores) {
            return new InstituicaoCursoOcorrencia[]{
                new InstituicaoCursoOcorrencia(){
                    Coordenador = professores[0].UsuarioInfo,
                    InstituicaoCurso = instituicaoCursos[0],
                    DataInicio = DateTime.Now,
                    DataExpiracao = DateTime.Now.AddDays(1800)
                }
            };
        }

        public static InstituicaoCursoOcorrenciaAluno[] getBaseInstituicaoCursoOcorrenciaAlunos(Usuario[] alunos, InstituicaoCursoOcorrencia[] instituicaoCursoOcorrencias) {
            return new InstituicaoCursoOcorrenciaAluno[]{
                new InstituicaoCursoOcorrenciaAluno(){
                    Aluno = alunos[0].UsuarioInfo,
                    DataInicio = DateTime.Now,
                    InstituicaoCursoOcorrencia = instituicaoCursoOcorrencias[0]
                }
            };
        }

        public static InstituicaoCursoOcorrenciaPeriodo[] getBaseInstituicaoCursoOcorrenciaPeriodos(InstituicaoCursoOcorrencia[] instituicaoCursoOcorrencias) {
            return new InstituicaoCursoOcorrenciaPeriodo[]{
                new InstituicaoCursoOcorrenciaPeriodo(){
                    InstituicaoCursoOcorrencia = instituicaoCursoOcorrencias[0],
                    DataInicio = DateTime.Now.AddDays(-720),
                    DataExpiracao = DateTime.Now.AddDays(-360)
                },
                new InstituicaoCursoOcorrenciaPeriodo(){
                    InstituicaoCursoOcorrencia = instituicaoCursoOcorrencias[0],
                    DataInicio = DateTime.Now,
                    DataExpiracao = DateTime.Now.AddDays(360)
                }
            };
        }

        public static InstituicaoCursoOcorrenciaPeriodoAluno[] getBaseInstituicaoCursoOcorrenciaPeriodoAlunos(InstituicaoCursoOcorrenciaAluno[] instituicaoCursoOcorrenciaAlunos, InstituicaoCursoOcorrenciaPeriodo[] instituicaoCursoOcorrenciaPeriodos, InstituicaoCursoPeriodo[] instituicaoCursoPeriodos, InstituicaoCursoTurma[] instituicaoCursoTurmas) {
            return new InstituicaoCursoOcorrenciaPeriodoAluno[]{
                new InstituicaoCursoOcorrenciaPeriodoAluno(){
                    InstituicaoCursoOcorrenciaAluno = instituicaoCursoOcorrenciaAlunos[0],
                    InstituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodos[0],
                    InstituicaoCursoPeriodo = instituicaoCursoPeriodos[0],
                    InstituicaoCursoTurma = instituicaoCursoTurmas[0]
                },
                new InstituicaoCursoOcorrenciaPeriodoAluno(){
                    InstituicaoCursoOcorrenciaAluno = instituicaoCursoOcorrenciaAlunos[0],
                    InstituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodos[1],
                    InstituicaoCursoPeriodo = instituicaoCursoPeriodos[0],
                    InstituicaoCursoTurma = instituicaoCursoTurmas[0]
                }
            };
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessor[] getBaseInstituicaoCursoOcorrenciaPeriodoProfessores(Usuario[] professores, CursoGradeMateria[] cursoGradeMaterias, InstituicaoCursoOcorrenciaPeriodo[] instituicaoCursoOcorrenciaPeriodos, InstituicaoCursoPeriodo[] instituicaoCursoPeriodos, InstituicaoCursoTurma[] instituicaoCursoTurmas) {
            return new InstituicaoCursoOcorrenciaPeriodoProfessor[]{
                new InstituicaoCursoOcorrenciaPeriodoProfessor(){
                    InstituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodos[1],
                    InstituicaoCursoPeriodo = instituicaoCursoPeriodos[0],
                    InstituicaoCursoTurma = instituicaoCursoTurmas[0],
                    Professor = professores[0].UsuarioInfo,
                    CursoGradeMateria = cursoGradeMaterias[0],
                    DataInicio = DateTime.Now
                },
                new InstituicaoCursoOcorrenciaPeriodoProfessor(){
                    InstituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodos[1],
                    InstituicaoCursoPeriodo = instituicaoCursoPeriodos[0],
                    InstituicaoCursoTurma = instituicaoCursoTurmas[0],
                    Professor = professores[0].UsuarioInfo,
                    CursoGradeMateria = cursoGradeMaterias[1],
                    DataInicio = DateTime.Now
                },
                new InstituicaoCursoOcorrenciaPeriodoProfessor(){
                    InstituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodos[1],
                    InstituicaoCursoPeriodo = instituicaoCursoPeriodos[0],
                    InstituicaoCursoTurma = instituicaoCursoTurmas[0],
                    Professor = professores[1].UsuarioInfo,
                    CursoGradeMateria = cursoGradeMaterias[2],
                    DataInicio = DateTime.Now
                }
            };
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula[] getBaseInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas(InstituicaoCursoOcorrenciaPeriodoProfessor[] instituicaoCursoOcorrenciaPeriodoProfessores) {
            return new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula[]{
                new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(){
                    Dia = DayOfWeek.Monday,
                    Inicio = TimeSpan.Parse("07:00"),
                    Fim = TimeSpan.Parse("07:50"),
                    InstituicaoCursoOcorrenciaPeriodoProfessor = instituicaoCursoOcorrenciaPeriodoProfessores[0]
                },
                new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(){
                    Dia = DayOfWeek.Monday,
                    Inicio = TimeSpan.Parse("07:50"),
                    Fim = TimeSpan.Parse("08:40"),
                    InstituicaoCursoOcorrenciaPeriodoProfessor = instituicaoCursoOcorrenciaPeriodoProfessores[0]
                },
                new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(){
                    Dia = DayOfWeek.Monday,
                    Inicio = TimeSpan.Parse("08:40"),
                    Fim = TimeSpan.Parse("09:30"),
                    InstituicaoCursoOcorrenciaPeriodoProfessor = instituicaoCursoOcorrenciaPeriodoProfessores[0]
                },
                new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(){
                    Dia = DayOfWeek.Monday,
                    Inicio = TimeSpan.Parse("09:50"),
                    Fim = TimeSpan.Parse("10:40"),
                    InstituicaoCursoOcorrenciaPeriodoProfessor = instituicaoCursoOcorrenciaPeriodoProfessores[0]
                },
                new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(){
                    Dia = DayOfWeek.Monday,
                    Inicio = TimeSpan.Parse("10:40"),
                    Fim = TimeSpan.Parse("11:30"),
                    InstituicaoCursoOcorrenciaPeriodoProfessor = instituicaoCursoOcorrenciaPeriodoProfessores[0]
                },
                new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(){
                    Dia = DayOfWeek.Monday,
                    Inicio = TimeSpan.Parse("11:30"),
                    Fim = TimeSpan.Parse("12:20"),
                    InstituicaoCursoOcorrenciaPeriodoProfessor = instituicaoCursoOcorrenciaPeriodoProfessores[0]
                }
            };
        }

        public static InstituicaoCursoTurma[] getBaseInstituicaoCursoTurma(InstituicaoCurso[] instituicaoCurso) {
            return new InstituicaoCursoTurma[]{
                new InstituicaoCursoTurma(){
                    Nome="Turma A",
                    Descricao="Turma A",
                    InstituicaoCurso = instituicaoCurso[0],
                    DataInicio=DateTime.Now,
                },
                new InstituicaoCursoTurma(){
                    Nome="Turma B",
                    Descricao="Turma B",
                    InstituicaoCurso = instituicaoCurso[0],
                    DataInicio=DateTime.Now,
                },
                new InstituicaoCursoTurma(){
                    Nome="Turma C",
                    Descricao="Turma C",
                    InstituicaoCurso = instituicaoCurso[0],
                    DataInicio=DateTime.Now,
                }
            };
        }

        public static InstituicaoCursoPeriodo[] getBaseInstituicaoCursoPeriodos(InstituicaoCurso[] instituicaoCursos) {
            return new InstituicaoCursoPeriodo[]{
                new InstituicaoCursoPeriodo(){
                    InstituicaoCurso=instituicaoCursos[0],
                    Inicio="08:00",
                    Fim="11:40",
                    PausaInicio="09:30",
                    PausaFim="10:00",
                    Dom=true,
                    Sab=true,
                    Sex=true,
                    Qui=true,
                    Qua=true,
                    Ter=true,
                    Seg=true
                },
                new InstituicaoCursoPeriodo(){
                    InstituicaoCurso=instituicaoCursos[0],
                    Inicio="13:00",
                    Fim="18:40",
                    PausaInicio="15:30",
                    PausaFim="14:00",
                    Dom=true,
                    Sab=true,
                    Sex=true,
                    Qui=true,
                    Qua=true,
                    Ter=true,
                    Seg=true
                }
            };
        }

        public static CategoriaProfissional[] getBaseCategoriaProfissionais() {
            return new CategoriaProfissional[]{
                new CategoriaProfissional(){
                    Nome="Tecnologia da Informação",
                    Descricao="Tecnologia da Informação"
                },
                new CategoriaProfissional(){
                    Nome="Contabilidade",
                    Descricao="Contabilidade"
                },
                new CategoriaProfissional(){
                    Nome="Medicina",
                    Descricao="Medicina"
                },
                new CategoriaProfissional(){
                    Nome="Farmácia",
                    Descricao="Farmácia"
                }
            };
        }

    }
}