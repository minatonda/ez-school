using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;

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

        public static InstituicaoCursoVM ToViewModel(InstituicaoCurso model, List<Materia> materias, bool deep)
        {
            var vm = new InstituicaoCursoVM();
            vm.ID = model.ID;
            vm.Curso = CursoAdapter.ToViewModel(model.Curso, false);
            vm.Instituicao = InstituicaoAdapter.ToViewModel(model.Instituicao, false);
            vm.CursoGrade = CursoAdapter.ToViewModel(model.CursoGrade, materias, false);
            vm.Label = model.Curso.Nome;
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;
            return vm;
        }

        public static InstituicaoCurso ToModel(InstituicaoCursoVM vm, bool deep)
        {
            var model = new InstituicaoCurso();
            model.ID = vm.ID;
            model.Curso = CursoAdapter.ToModel(vm.Curso, false);
            model.Instituicao = InstituicaoAdapter.ToModel(vm.Instituicao, false);
            model.CursoGrade = CursoAdapter.ToModel(vm.CursoGrade, false);
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;
            return model;
        }

    }
}