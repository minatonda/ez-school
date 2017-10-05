using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain {
    public static class BaseContextInitializer {
        public static void Initialize (BaseContext context) {
            var listUsuario = getBaseUsuarios ();
            context.Usuarios.AddRange (listUsuario);

            var listCurso = getBaseCursos ();
            context.Cursos.AddRange (listCurso);

            var listMateria = getBaseMaterias ();
            context.Materias.AddRange (listMateria);

            var listCursoGrade = getBaseCursoGrades (listCurso);
            context.CursoGrades.AddRange (listCursoGrade);

            var listCursoGradeMateria = getBaseCursoGradeMaterias (listCursoGrade, listMateria);
            context.CursoGradeMaterias.AddRange (listCursoGradeMateria);

            var listInstituicoes = getBaseInstituicoes ();
            context.Instituicoes.AddRange (listInstituicoes);

            context.SaveChanges ();
        }

        public static Instituicao[] getBaseInstituicoes () {
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
        public static Materia[] getBaseMaterias () {
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
        public static Curso[] getBaseCursos () {
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

        public static CursoGrade[] getBaseCursoGrades (Curso[] cursos) {
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

        public static CursoGradeMateria[] getBaseCursoGradeMaterias (CursoGrade[] cursoGrades, Materia[] materias) {
            return new CursoGradeMateria[] {
                new CursoGradeMateria () {
                        CursoGrade = cursoGrades[0],
                            Materia = materias[2]
                    },
                    new CursoGradeMateria () {
                        CursoGrade = cursoGrades[0],
                            Materia = materias[4]
                    },
                    new CursoGradeMateria () {
                        CursoGrade = cursoGrades[0],
                            Materia = materias[5]
                    },
                    new CursoGradeMateria () {
                        CursoGrade = cursoGrades[1],
                            Materia = materias[2]
                    },
            };
        }
        public static Usuario[] getBaseUsuarios () {
            var usuario = new Usuario () {
                Username = "dev",
                Password = "dev",
                Email = "dev@hiperfast.com"
            };
            var usuarioInfo = new UsuarioInfo () {
                ID = usuario.ID,
                Nome = "Matheus Carvalho",
                DataNascimento = DateTime.ParseExact ("1994-12-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                CPF = "42187917835",
                RG = "421920816"
            };
            usuario.UsuarioInfo = usuarioInfo;
            return new Usuario[] {
                usuario
            };
        }

    }
}