using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoAdapter {

        public static InstituicaoCursoVM ToViewModel(InstituicaoCursoDto model, bool deep) {
            var vm = new InstituicaoCursoVM();
            vm.ID = model.ID.ToString();

            vm.Label = model.Curso.Nome;
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            if (model.Curso != null) {
                vm.Curso = CursoAdapter.ToViewModel(model.Curso, false);
            }
            if (model.CursoGrade != null) {
                vm.CursoGrade = CursoGradeAdapter.ToViewModel(model.CursoGrade, false);
            }
            if (model.Periodos != null) {
                vm.Periodos = model.Periodos.Select(x => InstituicaoCursoPeriodoAdapter.ToViewModel(x, false)).ToList();
            }
            if (model.Turmas != null) {
                vm.Turmas = model.Turmas.Select(x => InstituicaoCursoTurmaAdapter.ToViewModel(x, false)).ToList();
            }

            return vm;
        }

        public static InstituicaoCursoDto ToModel(InstituicaoCursoVM vm, bool deep) {
            var model = new InstituicaoCursoDto();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            if (vm.Curso != null) {
                model.Curso = CursoAdapter.ToModel(vm.Curso, false);
            }
            if (vm.CursoGrade != null) {
                model.CursoGrade = CursoGradeAdapter.ToModel(vm.CursoGrade, false);
            }
            if (vm.Periodos != null) {
                model.Periodos = vm.Periodos.Select(x => InstituicaoCursoPeriodoAdapter.ToModel(x, false)).ToList();
            }
            if (vm.Turmas != null) {
                model.Turmas = vm.Turmas.Select(x => InstituicaoCursoTurmaAdapter.ToModel(x, false)).ToList();
            }

            return model;
        }

    }
}