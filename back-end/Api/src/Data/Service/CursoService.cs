using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;
using Domain.Repositories;
using Api.Data.ViewModels;

namespace Api.Data.Service
{
    public class CursoService
    {
        private CursoRepository _cursoRepository;
        public CursoService(CursoRepository cursoRepository)
        {
            this._cursoRepository = cursoRepository;
        }
        public List<CursoVM> All()
        {
            return this._cursoRepository.GetAll(true).Select(x => CursoAdapter.ToViewModel(x, true)).ToList();
        }
        public CursoVM Detail(long id)
        {
            return CursoAdapter.ToViewModel(this._cursoRepository.Get(id), true);
        }
        public CursoVM Add(CursoVM viewModel)
        {
            var model = CursoAdapter.ToModel(viewModel, true);
            return CursoAdapter.ToViewModel(this._cursoRepository.Add(model), true);
        }
        public CursoVM Update(CursoVM viewModel)
        {
            var model = CursoAdapter.ToModel(viewModel, true);
            return CursoAdapter.ToViewModel(this._cursoRepository.Update(model), true);
        }
        public void Disable(long id)
        {
            this._cursoRepository.Disable(id);
        }
        
        public List<CursoGradeVM> AllGrade(long id)
        {
            return this._cursoRepository.GetGrades(id).Select(x => CursoAdapter.ToViewModel(x, true)).ToList();
        }
        public CursoGradeVM DetailGrade(long id, long idGrade)
        {
            return CursoAdapter.ToViewModel(this._cursoRepository.GetGrade(id, idGrade), true);
        }
        public CursoGradeVM AddGrade(long id, CursoGradeVM model)
        {
            return CursoAdapter.ToViewModel(this._cursoRepository.AddGrade(id, CursoAdapter.ToModel(model, true)), true);
        }
        public CursoGradeVM UpdateGrade(long id, CursoGradeVM model)
        {
            return CursoAdapter.ToViewModel(this._cursoRepository.UpdateGrade(id, CursoAdapter.ToModel(model, true)), true);
        }
        public void DeleteGrade(long id, long idGrade)
        {
            this._cursoRepository.DeleteGrade(id, idGrade);
        }

    }
}