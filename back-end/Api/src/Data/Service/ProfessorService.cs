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
    public class ProfessorService
    {

        private ProfessorRepository _professorRepository;

        public ProfessorService(ProfessorRepository professorRepository)
        {
            this._professorRepository = professorRepository;
        }
        public List<ProfessorVM> GetAll()
        {
            return this._professorRepository.GetAll(true).Select(x => ProfessorAdapter.ToViewModel(x, true)).ToList();
        }
        public ProfessorVM GetDetail(long id)
        {
            return ProfessorAdapter.ToViewModel(this._professorRepository.Get(id), true);
        }
        public ProfessorVM Add(ProfessorVM viewModel)
        {
            var model = ProfessorAdapter.ToModel(viewModel, true);
            return ProfessorAdapter.ToViewModel(this._professorRepository.Add(model), true);
        }
        public ProfessorVM Update(ProfessorVM viewModel)
        {
            var model = ProfessorAdapter.ToModel(viewModel, true);
            return ProfessorAdapter.ToViewModel(this._professorRepository.Update(model), true);
        }
        public void Delete(long id)
        {
            this._professorRepository.Disable(id);
        }

    }
}