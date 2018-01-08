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
    public class MateriaService {

        private MateriaRepository _materiaRepository;

        public MateriaService(MateriaRepository materiaRepository) {
            this._materiaRepository = materiaRepository;
        }
        public void Add(MateriaVM viewModel) {
            var model = MateriaAdapter.ToModel(viewModel, true);
            this._materiaRepository.Add(model);
            viewModel.MateriasRelacionadas.ForEach(x => {
                var modelMateriaRelacionada = MateriaRelacionamentoAdapter.ToModel(x, true);
                modelMateriaRelacionada.MateriaPrincipal = model;
                this._materiaRepository.AddMateriaRelacionada(modelMateriaRelacionada);
            });
            this._materiaRepository.SaveChanges();
        }
        public void Update(MateriaVM viewModel) {
            var model = MateriaAdapter.ToModel(viewModel, true);
            this._materiaRepository.Update(model);

            var materiaRelacionadaAttached = this._materiaRepository.GetRelacionadas(model.ID, true);
            var materiaRelacionadaAttachedToRemove = materiaRelacionadaAttached.Where(x => !viewModel.MateriasRelacionadas.Select(y => MateriaRelacionamentoAdapter.ToModel(y, true).ID).Contains(x.ID)).ToList();

            materiaRelacionadaAttachedToRemove.ForEach(y => {
                this._materiaRepository.DisableMateriaRelacionada(y.ID);
            });

            viewModel.MateriasRelacionadas.ForEach(x => {
                var modelMateriasRelacionadas = MateriaRelacionamentoAdapter.ToModel(x, true);
                modelMateriasRelacionadas.MateriaPrincipal = model;

                if (!materiaRelacionadaAttached.Select(y => y.ID).Contains(modelMateriasRelacionadas.ID)) {
                    this._materiaRepository.AddMateriaRelacionada(modelMateriasRelacionadas);
                }
            });

            this._materiaRepository.SaveChanges();
        }
        public void Disable(long id) {
            this._materiaRepository.Disable(id);
            this._materiaRepository.SaveChanges();
        }
        public MateriaVM Detail(long id) {
            var materia = this._materiaRepository.Get(id);
            var materiaVM = MateriaAdapter.ToViewModel(materia, true);

            var materias = this._materiaRepository.GetRelacionadas(id, true);
            materiaVM.MateriasRelacionadas = materias.Select(x => MateriaRelacionamentoAdapter.ToViewModel(x, true)).ToList();
            return materiaVM;
        }
        public List<MateriaVM> All() {
            return this._materiaRepository.GetAll(true).Select(x => MateriaAdapter.ToViewModel(x, true)).ToList();
        }
    }
}