using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaAdapter {

        public static InstituicaoCursoOcorrenciaVM ToViewModel(InstituicaoCursoOcorrencia model, List<InstituicaoCursoOcorrenciaAluno> alunos, Dictionary<InstituicaoCursoOcorrenciaProfessor, List<InstituicaoCursoOcorrenciaProfessorPeriodoAula>> professores, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaVM();

            vm.ID = model.ID.ToString();
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            if (model.Coordenador != null) {
                vm.Coordenador = ProfessorAdapter.ToViewModel(model.Coordenador, false);
            }

            if (alunos != null) {
                vm.Alunos = alunos.Select(x => InstituicaoCursoOcorrenciaAlunoAdapter.ToViewModel(x, true)).ToList();
            }

            if (professores != null) {
                vm.Professores = professores.Select(x => InstituicaoCursoOcorrenciaProfessorAdapter.ToViewModel(x.Key, x.Value, false)).ToList();
            }

            return vm;
        }

        public static InstituicaoCursoOcorrencia ToModel(InstituicaoCursoOcorrenciaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrencia();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            if (vm.Coordenador != null) {
                model.Coordenador = ProfessorAdapter.ToModel(vm.Coordenador, false);
            }
            
            return model;
        }

        public static List<InstituicaoCursoOcorrenciaAluno> InstituicaoCursoOcorrenciaAlunosFrom(InstituicaoCursoOcorrenciaVM vm) {
            return vm.Alunos.Select(x => InstituicaoCursoOcorrenciaAlunoAdapter.ToModel(x, true)).ToList();
        }

        public static Dictionary<InstituicaoCursoOcorrenciaProfessor, List<InstituicaoCursoOcorrenciaProfessorPeriodoAula>> InstituicaoCursoOcorrenciaProfessoresFrom(InstituicaoCursoOcorrenciaVM vm) {
            return vm.Professores.ToDictionary(x => InstituicaoCursoOcorrenciaProfessorAdapter.ToModel(x, true), x => InstituicaoCursoOcorrenciaProfessorAdapter.InstituicaoCursoOcorrenciaProfessorPeriodoAulasFrom(x));
        }

    }
}