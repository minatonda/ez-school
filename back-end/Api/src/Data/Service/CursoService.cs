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
        public List<CursoVM> GetAll()
        {
            return this._cursoRepository.GetAll(true).Select(x => CursoAdapter.ToViewModel(x, true)).ToList();
        }
        public List<ShortVM> GetAllShort()
        {
            return this._cursoRepository.GetAll(true).Select(x => CursoAdapter.ToViewModelShort(x)).ToList();
        }
        public CursoVM GetDetail(long id)
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
        public void Delete(long id)
        {
            this._cursoRepository.Delete(id);
        }
        public List<CursoGradeVM> GetGrades(long id)
        {
            var retorno = new List<CursoGradeVM>();
            var grades = this._cursoRepository.GetGrades(id);
            foreach (var grade in grades)
            {
                retorno.Add(
                    CursoAdapter.ToViewModel(grade, this._cursoRepository.GetGradeMaterias(id, grade.ID), false)
                );
            }
            return retorno;
        }
        public CursoGradeVM GetGradeDetail(long id, long idGrade)
        {
            var grade = this._cursoRepository.GetGrade(id, idGrade);
            return CursoAdapter.ToViewModel(grade, this._cursoRepository.GetGradeMaterias(id, grade.ID), false);
        }
        public CursoGradeVM AddGrades(long id, CursoGradeVM model)
        {
            var grade = CursoAdapter.ToModel(model, true);
            var materias = model.Materias.Select(x => MateriaAdapter.ToModel(x, false)).ToList();
            this._cursoRepository.AddGrade(id, grade, materias);
            return CursoAdapter.ToViewModel(grade, this._cursoRepository.GetGradeMaterias(id, grade.ID), true);
        }
        public void DeleteGrades(long id, long idGrade)
        {
            this._cursoRepository.DeleteGrade(id, idGrade);
        }
        public List<MateriaVM> GetGradeMaterias(long id, long idGrade)
        {
            return this._cursoRepository.GetGradeMaterias(id, idGrade).Select(x => MateriaAdapter.ToViewModel(x, true)).ToList();
        }
        public void AddGradeMaterias(long id, long idGrade, MateriaVM model)
        {
            this._cursoRepository.AddGradeMateria(id, idGrade, MateriaAdapter.ToModel(model, true));
        }
        public void DeleteGradeMaterias(long id, long idGrade, long idMateria)
        {
            this._cursoRepository.DeleteGradeMateria(id, idGrade, idMateria);
        }

    }
}