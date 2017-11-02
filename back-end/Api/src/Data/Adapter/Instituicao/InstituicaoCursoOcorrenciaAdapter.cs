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

        public static InstituicaoCursoOcorrenciaVM ToViewModel(InstituicaoCursoOcorrencia model, List<InstituicaoCursoOcorrenciaAluno> alunos, List<InstituicaoCursoOcorrenciaProfessor> professores, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaVM();

            vm.ID = model.ID.ToString();
            vm.Coordenador = ProfessorAdapter.ToViewModel(model.Coordenador, false);
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            vm.Alunos = alunos.Select(x => InstituicaoCursoOcorrenciaAlunoAdapter.ToViewModel(x, true)).ToList();
            vm.Professores = professores.Select(x => InstituicaoCursoOcorrenciaProfessorAdapter.ToViewModel(x, false)).ToList();

            return vm;
        }

        public static InstituicaoCursoOcorrencia ToModel(InstituicaoCursoOcorrenciaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrencia();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.Coordenador = ProfessorAdapter.ToModel(vm.Coordenador, false);
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;
            return model;
        }

        public static List<InstituicaoCursoOcorrenciaAluno> InstituicaoCursoOcorrenciaAlunosFrom(InstituicaoCursoOcorrenciaVM vm) {
            return vm.Alunos.Select(x => InstituicaoCursoOcorrenciaAlunoAdapter.ToModel(x, true)).ToList();
        }

        public static List<InstituicaoCursoOcorrenciaProfessor> InstituicaoCursoOcorrenciaProfessoresFrom(InstituicaoCursoOcorrenciaVM vm) {
            return vm.Professores.Select(x => InstituicaoCursoOcorrenciaProfessorAdapter.ToModel(x, true)).ToList();
        }

    }
}