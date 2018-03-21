using System.Collections.Generic;
using System.Linq;
using Domain.CursoDomain;

namespace Api.CursoApi {

    public class CursoService {

        private CursoRepository _cursoRepository;

        public CursoService(CursoRepository cursoRepository) {
            this._cursoRepository = cursoRepository;
        }

        public void Add(CursoVM viewModel) {
            var model = CursoAdapter.ToModel(viewModel, true);
            this._cursoRepository.Add(model);

            viewModel.Grades.ForEach(x => {
                var modelCursoGrade = CursoGradeAdapter.ToModel(x, true);
                modelCursoGrade.Curso = model;
                this._cursoRepository.AddCursoGrade(modelCursoGrade);

                x.Materias.ForEach(y => {
                    var modelCursoGradeMateria = CursoGradeMateriaAdapter.ToModel(y, true);
                    modelCursoGradeMateria.CursoGrade = modelCursoGrade;
                    this._cursoRepository.AddCursoGradeMateria(modelCursoGradeMateria);
                });
            });
            this._cursoRepository.SaveChanges();
        }

        public void Update(CursoVM viewModel) {
            var model = CursoAdapter.ToModel(viewModel, true);
            this._cursoRepository.Update(model);

            var cursoGradesAttached = this._cursoRepository.GetAllCursoGradesByCurso(model.ID, true);
            var cursoGradesAttachedToRemove = cursoGradesAttached.Where(x => !viewModel.Grades.Select(y => CursoGradeAdapter.ToModel(y, true).ID).Contains(x.ID)).ToList();

            cursoGradesAttachedToRemove.ForEach(x => {
                this._cursoRepository.DisableCursoGrade(x.ID);
            });

            viewModel.Grades.ForEach(x => {
                var modelCursoGrade = CursoGradeAdapter.ToModel(x, true);
                modelCursoGrade.Curso = model;

                if (!cursoGradesAttached.Select(y => y.ID).Contains(modelCursoGrade.ID)) {
                    this._cursoRepository.AddCursoGrade(modelCursoGrade);
                } else {
                    this._cursoRepository.UpdateCursoGrade(modelCursoGrade);
                }

                var cursoGradeMateriasAttached = this._cursoRepository.GetAllCursoGradeMateriasByCursoGrade(modelCursoGrade.ID, true);
                var cursoGradeMateriasAttachedToRemove = cursoGradeMateriasAttached.Where(y => !x.Materias.Select(z => CursoGradeMateriaAdapter.ToModel(z, true).ID).Contains(y.ID)).ToList();

                cursoGradeMateriasAttachedToRemove.ForEach(y => {
                    this._cursoRepository.DisableCursoGradeMateria(y.ID);
                });

                x.Materias.ForEach(y => {
                    var modelCursoGradeMateria = CursoGradeMateriaAdapter.ToModel(y, true);
                    modelCursoGradeMateria.CursoGrade = modelCursoGrade;

                    if (!cursoGradeMateriasAttached.Select(z => z.ID).Contains(modelCursoGradeMateria.ID)) {
                        this._cursoRepository.AddCursoGradeMateria(modelCursoGradeMateria);
                    } else {
                        this._cursoRepository.UpdateCursoGradeMateria(modelCursoGradeMateria);
                    }

                });
            });
            this._cursoRepository.SaveChanges();
        }

        public void Disable(long id) {
            this._cursoRepository.Disable(id);
            this._cursoRepository.SaveChanges();
        }

        public CursoVM Detail(long id) {
            var viewModel = CursoAdapter.ToViewModel(this._cursoRepository.Get(id), true);

            viewModel.Grades = this._cursoRepository.GetAllCursoGradesByCurso(id, true).Select(x => {
                var viewModelCursoGrade = CursoGradeAdapter.ToViewModel(x, true);

                viewModelCursoGrade.Materias = this._cursoRepository.GetAllCursoGradeMateriasByCursoGrade(x.ID, true).Select(y => {
                    return CursoGradeMateriaAdapter.ToViewModel(y, true);
                }).ToList();

                return viewModelCursoGrade;

            }).ToList();

            return viewModel;
        }

        public List<CursoVM> All() {
            return this._cursoRepository.GetAll(true).Select(x => CursoAdapter.ToViewModel(x, true)).ToList();
        }



    }
}