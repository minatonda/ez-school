using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Models;
using Domain.Models.Interface;

namespace Api.Data.ViewModels
{
    public class CursoAdapter
    {

        public static CursoVM ToViewModel(Curso model, bool deep)
        {
            var vm = new CursoVM();
            vm.ID = model.ID;
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }
        public static ShortVM ToViewModelShort(Curso model)
        {
            var vm = new ShortVM();
            vm.ID = model.ID;
            vm.Label = model.Nome;
            return vm;
        }
        public static Curso ToModel(CursoVM vm, bool deep)
        {
            var model = new Curso();
            model.ID = vm.ID;

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

        public static CursoGradeVM ToViewModel(CursoGradeDto model, bool deep)
        {
            var viewModel = new CursoGradeVM();
            viewModel.ID = model.ID;
            viewModel.Label = model.Descricao;

            viewModel.Descricao = model.Descricao;
            viewModel.DataCriacao = model.DataCriacao;
            foreach (var item in model.Materias)
            {
                viewModel.Materias.Add(CursoAdapter.ToViewModel(item, false));
            }
            return viewModel;
        }
        public static CursoGradeDto ToModel(CursoGradeVM viewModel, bool deep)
        {
            var model = new CursoGradeDto();
            model.ID = viewModel.ID;
            model.Descricao = viewModel.Descricao;
            model.DataCriacao = viewModel.DataCriacao;
            foreach (var item in viewModel.Materias)
            {
                model.Materias.Add(CursoAdapter.ToModel(item, false));
            }
            return model;
        }

        public static CursoGradeMateriaVM ToViewModel(CursoGradeMateriaDto model, bool deep)
        {
            var viewModel = new CursoGradeMateriaVM();

            viewModel.ID = model.ID;
            viewModel.Label = model.Descricao;
            
            viewModel.Descricao = model.Descricao;
            viewModel.Materia = MateriaAdapter.ToViewModel(model.Materia, true);

            return viewModel;
        }
        public static CursoGradeMateriaDto ToModel(CursoGradeMateriaVM viewModel, bool deep)
        {
            var model = new CursoGradeMateriaDto();
            model.ID = viewModel.ID;
            model.Descricao = viewModel.Descricao;
            model.Materia = MateriaAdapter.ToModel(viewModel.Materia,true);
            return model;
        }

    }
}