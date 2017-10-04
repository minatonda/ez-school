using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Models;

namespace Api.Data.ViewModels
{
    public class InstituicaoAdapter
    {

        public static InstituicaoVM ToViewModel(Instituicao model, bool deep)
        {
            var vm = new InstituicaoVM();
            vm.ID = model.ID;
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.CNPJ = model.CNPJ;

            return vm;
        }

        public static ShortVM ToViewModelShort(Instituicao model)
        {
            var vm = new ShortVM();
            vm.ID = model.ID;
            vm.Label = model.Nome;
            return vm;
        }

        public static Instituicao ToModel(InstituicaoVM vm, bool deep)
        {
            var model = new Instituicao();
            model.ID = vm.ID;

            model.Nome = vm.Nome;
            model.CNPJ = vm.CNPJ;

            return model;
        }

        public static InstituicaoCursoVM ToViewModel(InstituicaoCursoDto model, bool deep)
        {
            var vm = new InstituicaoCursoVM();
            vm.ID = model.ID;
            vm.Curso = CursoAdapter.ToViewModel(model.Curso, false);
            vm.CursoGrade = CursoAdapter.ToViewModel(model.CursoGrade, false);
            vm.Label = model.Curso.Nome;
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;
            return vm;
        }

        public static InstituicaoCursoDto ToModel(InstituicaoCursoVM vm, bool deep)
        {
            var model = new InstituicaoCursoDto();
            model.ID = vm.ID;
            model.Curso = CursoAdapter.ToModel(vm.Curso, false);
            model.CursoGrade = CursoAdapter.ToModel(vm.CursoGrade, false);
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;
            return model;
        }

        public static InstituicaoCursoOcorrenciaVM ToViewModel(InstituicaoCursoOcorrenciaDto model, bool deep)
        {
            var vm = new InstituicaoCursoOcorrenciaVM();
            vm.ID = model.ID;
            vm.Coordenador = ProfessorAdapter.ToViewModel(model.Coordenador, false);
            vm.Alunos = model.Alunos.Select(x => AlunoAdapter.ToViewModel(x, false)).ToList();
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;
            return vm;
        }

        public static InstituicaoCursoOcorrenciaDto ToModel(InstituicaoCursoOcorrenciaVM vm, bool deep)
        {
            var model = new InstituicaoCursoOcorrenciaDto();
            model.ID = vm.ID;
            model.Coordenador = ProfessorAdapter.ToModel(vm.Coordenador, false);
            model.Alunos = vm.Alunos.Select(x => AlunoAdapter.ToModel(x, false)).ToList();
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;
            return model;
        }

    }
}