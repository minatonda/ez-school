
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.Common.Base;
using Domain.MateriaDomain;

namespace Api.MateriaApi {

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

        public void ValidateMateria(MateriaVM materia) {
            var exception = new BaseException();
            exception.Code = BaseExceptionCode.RESOURCE_REFUSED;
            exception.HttpStatusCode = HttpStatusCode.NotAcceptable;
            exception.Infos = new List<BaseExceptionFieldInfo>();

            if (materia.Nome == null || materia.Nome.Trim() == "") {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.MATERIA_NOME,
                });
            }

            if (materia.Nome != null) {
                var materiaByNome = this._materiaRepository.GetByNome(materia.Nome);
                if (materia.Nome != null && materiaByNome != null && materiaByNome.ID != materia.ID) {

                    if (materiaByNome.Nome == materia.Nome) {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.REGISTER_WITH_SAME_VALUE_EXISTS,
                            Field = BaseExceptionField.MATERIA_NOME,
                            Value = materia.Nome
                        });
                    }

                }
            }

            if (exception.Infos.Count > 0) {
                throw exception;
            }
        }
    }
}