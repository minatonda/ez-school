using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaPeriodoAdapter {

        public static InstituicaoCursoOcorrenciaPeriodoVM ToViewModel(InstituicaoCursoOcorrenciaPeriodo model, Dictionary<InstituicaoCursoOcorrenciaPeriodoProfessor, List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula>> professores, List<InstituicaoCursoOcorrenciaPeriodoAluno> alunos, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaPeriodoVM();
            vm.ID = model.ID.ToString();
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            if (alunos != null) {
                vm.instituicaoCursoOcorrenciaPeriodoAlunos = alunos.Select(x => InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToViewModel(x, true)).ToList();
            }

            if (professores != null) {
                vm.instituicaoCursoOcorrenciaPeriodoProfessores = professores.Select(x => InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToViewModel(x.Key, x.Value, true)).ToList();
            }


            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodo ToModel(InstituicaoCursoOcorrenciaPeriodoVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodo();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            return model;
        }

        public static List<InstituicaoCursoOcorrenciaAluno> InstituicaoCursoOcorrenciaAlunoFrom(InstituicaoCursoOcorrenciaPeriodoVM vm) {
            return vm.instituicaoCursoOcorrenciaPeriodoAlunos.Select(x => InstituicaoCursoOcorrenciaAlunoAdapter.ToModel(x.InstituicaoCursoOcorrenciaAluno, true)).ToList();
        }

        public static List<InstituicaoCursoOcorrenciaPeriodoAluno> InstituicaoCursoOcorrenciaPeriodoAlunoFrom(InstituicaoCursoOcorrenciaPeriodoVM vm) {
            return vm.instituicaoCursoOcorrenciaPeriodoAlunos.Select(x => InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToModel(x, true)).ToList();
        }

        public static Dictionary<InstituicaoCursoOcorrenciaPeriodoProfessor, List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula>> InstituicaoCursoOcorrenciaPeriodoProfessorFrom(InstituicaoCursoOcorrenciaPeriodoVM vm) {
            return vm.instituicaoCursoOcorrenciaPeriodoProfessores.ToDictionary(x => InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToModel(x, true), x => x.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.Select(y => InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToModel(y, x, true)).ToList());
        }

    }
}