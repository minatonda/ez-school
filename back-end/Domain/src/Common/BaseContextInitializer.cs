using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.MateriaDomain;
using Domain.UsuarioDomain;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common {
    public static class BaseContextInitializer {
        public static void Initialize(BaseContext context) {
            var listUsuario = getBaseUsuarios();
            context.Usuarios.AddRange(listUsuario);

            var listCurso = getBaseCursos();
            context.Cursos.AddRange(listCurso);

            var listMateria = getBaseMaterias();
            context.Materias.AddRange(listMateria);

            var listCursoGrade = getBaseCursoGrades(listCurso);
            context.CursoGrades.AddRange(listCursoGrade);

            var listCursoGradeMateria = getBaseCursoGradeMaterias(listCursoGrade, listMateria);
            context.CursoGradeMaterias.AddRange(listCursoGradeMateria);

            var listInstituicoes = getBaseInstituicoes();
            context.Instituicoes.AddRange(listInstituicoes);

            var listAlunos = getBaseAlunos(listUsuario);
            context.Alunos.AddRange(listAlunos);

            var listProfessores = getBaseProfessores(listUsuario);
            context.Professores.AddRange(listProfessores);

            var listInstituicaoCurso = getBaseInstituicaoCursos(listCursoGrade, listInstituicoes);
            context.InstituicaoCursos.AddRange(listInstituicaoCurso);

            var listInstituicaoCursoPeriodo = getBaseInstituicaoCursoPeriodos(listInstituicaoCurso);
            context.InstituicaoCursoPeriodos.AddRange(listInstituicaoCursoPeriodo);

            var listInstituicaoCursoTurma = getBaseInstituicaoCursoTurma(listInstituicaoCurso);
            context.InstituicaoCursoTurmas.AddRange(listInstituicaoCursoTurma);

            var listInstituicaoCursoOcorrencia = getBaseInstituicaoCursoOcorrencias(listInstituicaoCurso, listProfessores);
            context.InstituicaoCursoOcorrencias.AddRange(listInstituicaoCursoOcorrencia);

            var listInstituicaoCursoOcorrenciaAluno = getBaseInstituicaoCursoOcorrenciaAlunos(listAlunos, listInstituicaoCursoOcorrencia);
            context.InstituicaoCursoOcorrenciaAlunos.AddRange(listInstituicaoCursoOcorrenciaAluno);

            var listInstituicaoCursoOcorrenciaPeriodo = getBaseInstituicaoCursoOcorrenciaPeriodos(listInstituicaoCursoOcorrencia);
            context.InstituicaoCursoOcorrenciaPeriodos.AddRange(listInstituicaoCursoOcorrenciaPeriodo);

            var listInstituicaoCursoOcorrenciaPeriodoAluno = getBaseInstituicaoCursoOcorrenciaPeriodoAlunos(listInstituicaoCursoOcorrenciaAluno, listInstituicaoCursoOcorrenciaPeriodo, listInstituicaoCursoPeriodo, listInstituicaoCursoTurma);
            context.InstituicaoCursoOcorrenciaPeriodoAlunos.AddRange(listInstituicaoCursoOcorrenciaPeriodoAluno);

            var listInstituicaoCursoOcorrenciaPeriodoProfessor = getBaseInstituicaoCursoOcorrenciaPeriodoProfessores(listProfessores, listCursoGradeMateria, listInstituicaoCursoOcorrenciaPeriodo, listInstituicaoCursoPeriodo, listInstituicaoCursoTurma);
            context.InstituicaoCursoOcorrenciaPeriodoProfessores.AddRange(listInstituicaoCursoOcorrenciaPeriodoProfessor);

            var listInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = getBaseInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas(listInstituicaoCursoOcorrenciaPeriodoProfessor);
            context.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.AddRange(listInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula);

            context.SaveChanges();
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
                                Descricao = materias[2].Descricao
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
                                Descricao = materias[2].Descricao
                        },
                        new CursoGradeMateria () {
                            CursoGrade = cursoGrades[1],
                                Materia = materias[2],
                                Descricao = materias[2].Descricao
                        },
                };
        }

        public static Usuario[] getBaseUsuarios() {
            var carvalho = new Usuario() {
                Username = "dev",
                Password = "dev",
                Email = "dev@ezschool.com"
            };
            var carvalhoInfo = new UsuarioInfo() {
                ID = carvalho.ID,
                Nome = "Matheus Carvalho",
                DataNascimento = DateTime.ParseExact("1994-12-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                CPF = "42187917835",
                RG = "421920816"
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
                CPF = "42187917835",
                RG = "421920816"
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
                CPF = "52176819820",
                RG = "510984128"
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
                CPF = "768309116406",
                RG = "760942814"
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

        public static InstituicaoCursoOcorrencia[] getBaseInstituicaoCursoOcorrencias(InstituicaoCurso[] instituicaoCursos, Professor[] professores) {
            return new InstituicaoCursoOcorrencia[]{
                new InstituicaoCursoOcorrencia(){
                    Coordenador = professores[0],
                    InstituicaoCurso = instituicaoCursos[0],
                    DataInicio = DateTime.Now,
                    DataExpiracao = DateTime.Now.AddDays(1800)
                }
            };
        }

        public static InstituicaoCursoOcorrenciaAluno[] getBaseInstituicaoCursoOcorrenciaAlunos(Aluno[] alunos, InstituicaoCursoOcorrencia[] instituicaoCursoOcorrencias) {
            return new InstituicaoCursoOcorrenciaAluno[]{
                new InstituicaoCursoOcorrenciaAluno(){
                    Aluno = alunos[0],
                    DataInicio = DateTime.Now,
                    InstituicaoCursoOcorrencia = instituicaoCursoOcorrencias[0]
                }
            };
        }

        public static InstituicaoCursoOcorrenciaPeriodo[] getBaseInstituicaoCursoOcorrenciaPeriodos(InstituicaoCursoOcorrencia[] instituicaoCursoOcorrencias) {
            return new InstituicaoCursoOcorrenciaPeriodo[]{
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
                    InstituicaoCursoTurma = instituicaoCursoTurmas[0],
                    Confirmado = true
                }
            };
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessor[] getBaseInstituicaoCursoOcorrenciaPeriodoProfessores(Professor[] professores, CursoGradeMateria[] cursoGradeMaterias, InstituicaoCursoOcorrenciaPeriodo[] instituicaoCursoOcorrenciaPeriodos, InstituicaoCursoPeriodo[] instituicaoCursoPeriodos, InstituicaoCursoTurma[] instituicaoCursoTurmas) {
            return new InstituicaoCursoOcorrenciaPeriodoProfessor[]{
                new InstituicaoCursoOcorrenciaPeriodoProfessor(){
                    InstituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodos[0],
                    InstituicaoCursoPeriodo = instituicaoCursoPeriodos[0],
                    InstituicaoCursoTurma = instituicaoCursoTurmas[0],
                    Professor = professores[0],
                    CursoGradeMateria = cursoGradeMaterias[0],
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

    }
}