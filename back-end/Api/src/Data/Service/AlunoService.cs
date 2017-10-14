using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;
using Domain.Repositories;
using Api.Data.ViewModels;

namespace Api.Data.Service {
    public class AlunoService {

        private AlunoRepository _alunoRepository;

        public AlunoService(AlunoRepository alunoRepository) {
            this._alunoRepository = alunoRepository;
        }
        public List < AlunoVM > All() {
            return this._alunoRepository.GetAll(true).Select(x => AlunoAdapter.ToViewModel(x, true)).ToList();
        }
        public AlunoVM Detail(string id) {
            return AlunoAdapter.ToViewModel(this._alunoRepository.Get(id), true);
        }
        public AlunoVM Add(AlunoVM viewModel) {
            var model = AlunoAdapter.ToModel(viewModel, true);
            return AlunoAdapter.ToViewModel(this._alunoRepository.Add(model), true);
        }
        public AlunoVM Update(AlunoVM viewModel) {
            var model = AlunoAdapter.ToModel(viewModel, true);
            return AlunoAdapter.ToViewModel(this._alunoRepository.Update(model), true);
        }
        public void Disable(long id) {
            this._alunoRepository.Disable(id);
        }

    }
}