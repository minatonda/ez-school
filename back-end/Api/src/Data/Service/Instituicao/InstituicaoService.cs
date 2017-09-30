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
    public class InstituicaoService
    {
        private InstituicaoRepository _instituicaoRepository;
        private CursoRepository _cursoRespository;
        public InstituicaoService(InstituicaoRepository instituicaoRepository, CursoRepository cursoRespository)
        {
            this._instituicaoRepository = instituicaoRepository;
            this._cursoRespository = cursoRespository;

        }
        public List<InstituicaoVM> GetAll()
        {
            return this._instituicaoRepository.GetAll(true).Select(x => InstituicaoAdapter.ToViewModel(x, true)).ToList();
        }
        public List<ShortVM> GetAllShort()
        {
            return this._instituicaoRepository.GetAll(true).Select(x => InstituicaoAdapter.ToViewModelShort(x)).ToList();
        }
        public InstituicaoVM GetDetail(long id)
        {
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Get(id), true);
        }
        public InstituicaoVM Add(InstituicaoVM viewModel)
        {
            var model = InstituicaoAdapter.ToModel(viewModel, true);
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Add(model), true);
        }
        public InstituicaoVM Update(InstituicaoVM viewModel)
        {
            var model = InstituicaoAdapter.ToModel(viewModel, true);
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Update(model), true);
        }
        public void Delete(long id)
        {
            this._instituicaoRepository.Delete(id);
        }
        public List<InstituicaoCategoriaVM> GetCategorias(long id)
        {
            return this._instituicaoRepository.GetCategorias(id).Select(x => InstituicaoCategoriaAdapter.ToViewModel(x, false)).ToList();
        }
        public void AddCategoria(long id, InstituicaoCategoriaVM viewModel)
        {
            this._instituicaoRepository.AddCategoria(id, InstituicaoCategoriaAdapter.ToModel(viewModel, false));
        }
        public void DeleteCategoria(long id, long idCategoria)
        {
            this._instituicaoRepository.DeleteCategoria(id, idCategoria);
        }

        public List<InstituicaoCursoVM> GetCursos(long id)
        {
            var listInstituicaoCurso = this._instituicaoRepository.GetCursos(id);
            var listInstituicaoCursoVM = new List<InstituicaoCursoVM>();
            foreach (var cursoCategoria in listInstituicaoCurso)
            {
                listInstituicaoCursoVM.Add(InstituicaoAdapter.ToViewModel(cursoCategoria, this._cursoRespository.GetGradeMaterias(cursoCategoria.Curso.ID, cursoCategoria.CursoGrade.ID), false));
            }
            return listInstituicaoCursoVM;
        }

        public InstituicaoCursoVM GetCurso(long id, long idCurso)
        {
            var instituicaoCurso = this._instituicaoRepository.GetCurso(id, idCurso);
            return InstituicaoAdapter.ToViewModel(instituicaoCurso, this._cursoRespository.GetGradeMaterias(instituicaoCurso.Curso.ID, instituicaoCurso.CursoGrade.ID), false);
        }
        public void AddCurso(long id, InstituicaoCursoVM instituicaoCurso)
        {
            this._instituicaoRepository.AddCurso(id, instituicaoCurso.Curso.ID, instituicaoCurso.CursoGrade.ID);
        }
        public void DeleteCurso(long id, long idCurso)
        {
            this._instituicaoRepository.DeleteCurso(id, idCurso);
        }


    }
}