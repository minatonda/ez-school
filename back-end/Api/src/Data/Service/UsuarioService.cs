using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;
using Domain.Repositories;
using Api.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Data.Service {
    public class UsuarioService {
        private UsuarioRepository _usuarioRepository;
        private AreaInteresseRepository _areaInteresseRepository;

        public UsuarioService(UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) {
            this._usuarioRepository = usuarioRepository;
            this._areaInteresseRepository = areaInteresseRepository;
        }


        public void Add(UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            this._usuarioRepository.Add(model);
            this._usuarioRepository.AddAluno(model.UsuarioInfo);
            this._usuarioRepository.AddProfessor(model.UsuarioInfo);
            this._usuarioRepository.SaveChanges();
        }

        public void Update(UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            this._usuarioRepository.Update(model);
            this._usuarioRepository.SaveChanges();
        }

        public void UpdateAluno(AlunoVM viewModel) {
            var _model = AlunoAdapter.ToModel(viewModel, true);
            var _attached = this._usuarioRepository.GetAluno(_model.ID);
            var _attachedAreaInteresses = this._areaInteresseRepository.GetAllByAluno(_model.ID, true);
            var _attachedAreaInteressesToRemove = _attachedAreaInteresses.Where(x => !viewModel.AreaInteresses.Select(y => y.ID).Contains(x.ID.ToString())).ToList();

            _attachedAreaInteressesToRemove.ForEach(y => {
                this._areaInteresseRepository.DisableAreaInteresse(y.ID);
            });
            
            viewModel.AreaInteresses.ForEach(x => {
                var modelAreaInteresse = AreaInteresseAdapter.ToModel(x, true);
                modelAreaInteresse.Aluno = _model;

                if (!_attachedAreaInteresses.Select(y => y.ID.ToString()).Contains(x.ID)) {
                    this._areaInteresseRepository.Add(modelAreaInteresse);
                }
            });

            this._usuarioRepository.UpdateAluno(_model);
            this._usuarioRepository.SaveChanges();
        }

        public void UpdateProfessor(ProfessorVM viewModel) {
            var _model = ProfessorAdapter.ToModel(viewModel, true);
            var _attached = this._usuarioRepository.GetProfessor(_model.ID);
            var _attachedAreaInteresses = this._areaInteresseRepository.GetAllByProfessor(_model.ID, true);
            var _attachedAreaInteressesToRemove = _attachedAreaInteresses.Where(x => !viewModel.AreaInteresses.Select(y => y.ID).Contains(x.ID.ToString())).ToList();

            _attachedAreaInteressesToRemove.ForEach(y => {
                this._areaInteresseRepository.DisableAreaInteresse(y.ID);
            });

            viewModel.AreaInteresses.ForEach(x => {
                var modelAreaInteresse = AreaInteresseAdapter.ToModel(x, true);
                modelAreaInteresse.Professor = _model;

                if (!_attachedAreaInteresses.Select(y => y.ID.ToString()).Contains(x.ID)) {
                    this._areaInteresseRepository.Add(modelAreaInteresse);
                }
            });
            
            this._usuarioRepository.UpdateProfessor(_model);
            this._usuarioRepository.SaveChanges();
        }

        public void Disable(string id) {
            this._usuarioRepository.Disable(id);
            this._usuarioRepository.SaveChanges();
        }

        public UsuarioVM Detail(string id) {
            return UsuarioAdapter.ToViewModel(this._usuarioRepository.Get(id), true);
        }

        public AlunoVM DetailAluno(string id) {
            var areainteresses = this._areaInteresseRepository.GetAllByAluno(id, true);
            return AlunoAdapter.ToViewModel(this._usuarioRepository.GetAluno(id), areainteresses, true);
        }

        public ProfessorVM DetailProfessor(string id) {
            var areainteresses = this._areaInteresseRepository.GetAllByProfessor(id, true);
            return ProfessorAdapter.ToViewModel(this._usuarioRepository.GetProfessor(id), areainteresses, true);
        }

        public List<UsuarioVM> All() {
            return this._usuarioRepository.GetAll(true).Select(x => UsuarioAdapter.ToViewModel(x, true)).ToList();
        }

        public List<AlunoVM> GetAllAlunosByTermo(string termo) {
            return this._usuarioRepository.GetAllAlunosByTermo(termo, true).Select(x => AlunoAdapter.ToViewModel(x, null, true)).ToList();
        }

        public List<ProfessorVM> GetAllProfessoresByTermo(string termo) {
            return this._usuarioRepository.GetAllProfessoresByTermo(termo, true).Select(x => ProfessorAdapter.ToViewModel(x, null, true)).ToList();
        }

    }
}