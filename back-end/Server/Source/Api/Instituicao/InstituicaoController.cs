using System;
using System.Collections.Generic;
using Api.Common.Base;
using Api.CursoApi;
using Domain.Common;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.InstituicaoApi {

    [Route("api/instituicao")]
    public class InstituicaoController : BaseController {

        private InstituicaoService _instituicaoService;

        public InstituicaoController(InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository, UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) : base(usuarioRepository, areaInteresseRepository, instituicaoRepository, cursoRepository) {
            this._instituicaoService = new InstituicaoService(instituicaoRepository, cursoRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] InstituicaoVM viewModel) {
            if (!this.IsAuthorized(BaseRole.ADD_INSTITUICAO)) {
                throw new BaseUnauthorizedException();
            }
            this._instituicaoService.Add(viewModel);
        }

        [HttpPut("{id}/instituicao-colaborador/add")]
        public void AddInstituicaoColaborador(long id, [FromBody] InstituicaoColaboradorVM viewModel) {
            if (!this.IsAuthorizedInstituicao(viewModel.ID, BaseRole.EDIT_INSTITUICAO_CURSO)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.EDIT_INSTITUICAO_CURSO, id);
            }
            this._instituicaoService.AddInstituicaoColaborador(id, viewModel);
        }

        [HttpPut("{id}/instituicao-colaborador-perfil/add")]
        public void AddInstituicaoColaboradorPerfil(long id, [FromBody] InstituicaoColaboradorPerfilVM viewModel) {
            if (!this.IsAuthorizedInstituicao(viewModel.ID, BaseRole.EDIT_INSTITUICAO_CURSO)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.EDIT_INSTITUICAO_CURSO, id);
            }
            this._instituicaoService.AddInstituicaoColaboradorPerfil(id, viewModel);
        }

        [HttpPut("{id}/instituicao-curso/add")]
        public void AddInstituicaoCurso(long id, [FromBody] InstituicaoCursoVM viewModel) {
            if (!this.IsAuthorizedInstituicao(id, BaseRole.ADD_INSTITUICAO_CURSO)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.ADD_INSTITUICAO_CURSO, id);
            }
            this._instituicaoService.AddInstituicaoCurso(id, viewModel);
        }

        [HttpPut("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/add")]
        public void AddInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel) {
            if (!this.IsAuthorizedInstituicao(id, BaseRole.ADD_INSTITUICAO_CURSO_OCORRENCIA)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.ADD_INSTITUICAO_CURSO_OCORRENCIA, id);
            }
            this._instituicaoService.AddInstituicaoCursoOcorrencia(idInstituicaoCurso, viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] InstituicaoVM viewModel) {
            if (!this.IsAuthorized(BaseRole.EDIT_INSTITUICAO)) {
                if (!this.IsAuthorizedInstituicao(viewModel.ID, BaseRole.OWNER_INSTITUICAO)) {
                    throw new BaseUnauthorizedInstituicaoException(BaseRole.OWNER_INSTITUICAO, viewModel.ID);
                } else {
                    throw new BaseUnauthorizedException(BaseRole.EDIT_INSTITUICAO);
                }
            }
            this._instituicaoService.Update(viewModel);
        }

        [HttpPost("{id}/instituicao-colaborador/update")]
        public void UpdateInstituicaoColaborador(long id, [FromBody] InstituicaoColaboradorVM viewModel) {
            if (!this.IsAuthorizedInstituicao(viewModel.ID, BaseRole.EDIT_INSTITUICAO_CURSO)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.EDIT_INSTITUICAO_CURSO, id);
            }
            this._instituicaoService.UpdateInstituicaoColaborador(id, viewModel);
        }

        [HttpPost("{id}/instituicao-colaborador-perfil/update")]
        public void UpdateInstituicaoColaboradorPerfil(long id, [FromBody] InstituicaoColaboradorPerfilVM viewModel) {
            if (!this.IsAuthorizedInstituicao(viewModel.ID, BaseRole.EDIT_INSTITUICAO_CURSO)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.EDIT_INSTITUICAO_CURSO, id);
            }
            this._instituicaoService.UpdateInstituicaoColaboradorPerfil(id, viewModel);
        }

        [HttpPost("{id}/instituicao-curso/update")]
        public void UpdateInstituicaoCurso(long id, [FromBody] InstituicaoCursoVM viewModel) {
            if (!this.IsAuthorizedInstituicao(viewModel.ID, BaseRole.EDIT_INSTITUICAO_CURSO)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.EDIT_INSTITUICAO_CURSO, id);
            }
            this._instituicaoService.UpdateInstituicaoCurso(id, viewModel);
        }

        [HttpPost("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/update")]
        public void UpdateInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel) {
            if (!this.IsAuthorizedInstituicao(viewModel.ID, BaseRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA, id);
            }
            this._instituicaoService.UpdateInstituicaoCursoOcorrencia(idInstituicaoCurso, viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            if (!this.IsAuthorizedInstituicao(id, BaseRole.DISABLE_INSTITUICAO)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.DISABLE_INSTITUICAO, id);
            }
            this._instituicaoService.Disable(id);
        }

        [HttpDelete("{id}/instituicao-curso/disable")]
        public void DisableInstituicaoCurso(long id, [FromQuery]long idInstituicaoCurso) {
            if (!this.IsAuthorizedInstituicao(id, BaseRole.DISABLE_INSTITUICAO_CURSO)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.DISABLE_INSTITUICAO_CURSO, id);
            }
            this._instituicaoService.DisableInstituicaoCurso(idInstituicaoCurso);
        }

        [HttpDelete("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/disable")]
        public void DisableInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromQuery]long idInstituicaoCursoOcorrencia) {
            if (!this.IsAuthorizedInstituicao(id, BaseRole.DISABLE_INSTITUICAO_CURSO_OCORRENCIA)) {
                throw new BaseUnauthorizedInstituicaoException(BaseRole.DISABLE_INSTITUICAO_CURSO_OCORRENCIA, id);
            }
            this._instituicaoService.DisableInstituicaoCursoOcorrencia(idInstituicaoCursoOcorrencia);
        }

        [HttpGet("detail/{id}")]
        public InstituicaoVM Detail(long id) {
            return this._instituicaoService.Detail(id);
        }

        [HttpGet("detail/{id}/instituicao-colaborador/detail/{idInstituicaoColaborador}")]
        public InstituicaoColaboradorVM DetailInstituicaoColaborador(long id, long idInstituicaoColaborador) {
            return this._instituicaoService.DetailInstituicaoColaborador(idInstituicaoColaborador);
        }

        [HttpGet("detail/{id}/instituicao-colaborador-perfil/detail/{idInstituicaoColaboradorPerfil}")]
        public InstituicaoColaboradorPerfilVM DetailInstituicaoColaboradorPerfil(long id, long idInstituicaoColaboradorPerfil) {
            return this._instituicaoService.DetailInstituicaoColaboradorPerfil(idInstituicaoColaboradorPerfil);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}")]
        public InstituicaoCursoVM DetailInstituicaoCurso(long id, long idInstituicaoCurso) {
            return this._instituicaoService.DetailInstituicaoCurso(idInstituicaoCurso);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/instituicao-curso-ocorrencia/detail/{idInstituicaoCursoOcorrencia}")]
        public InstituicaoCursoOcorrenciaVM DetailInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, long idInstituicaoCursoOcorrencia) {
            return this._instituicaoService.DetailInstituicaoCursoOcorrencia(idInstituicaoCursoOcorrencia);
        }

        [HttpGet]
        public List<InstituicaoVM> All() {
            return this._instituicaoService.All();
        }

        [HttpGet("detail/{id}/instituicao-colaborador")]
        public List<InstituicaoColaboradorVM> AllInstituicaoColaborador(long id) {
            return this._instituicaoService.AllInstituicaoColaborador(id);
        }

        [HttpGet("detail/{id}/instituicao-colaborador-perfil")]
        public List<InstituicaoColaboradorPerfilVM> AllInstituicaoColaboradorPerfil(long id) {
            return this._instituicaoService.AllInstituicaoColaboradorPerfil(id);
        }

        [HttpGet("detail/{id}/instituicao-curso")]
        public List<InstituicaoCursoVM> AllInstituicaoCurso(long id) {
            return this._instituicaoService.AllInstituicaoCurso(id);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/instituicao-curso-ocorrencia")]
        public List<InstituicaoCursoOcorrenciaVM> AllInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoOcorrencia(idInstituicaoCurso);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/instituicao-curso-periodo")]
        public List<InstituicaoCursoPeriodoVM> AllInstituicaoCursoPeriodo(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoPeriodo(idInstituicaoCurso);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/instituicao-curso-turma")]
        public List<InstituicaoCursoTurmaVM> AllInstituicaoCursoTurma(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoTurma(idInstituicaoCurso);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/curso-grade-materia")]
        public List<CursoGradeMateriaVM> AllCursoGradeMateria(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllCursoGradeMateria(idInstituicaoCurso);
        }

        [HttpGet("roles")]
        public List<BaseRole> AllRoles() {
            return new List<BaseRole>(){
                BaseRole.OWNER_INSTITUICAO,
                BaseRole.ADD_INSTITUICAO_CURSO,
                BaseRole.EDIT_INSTITUICAO_CURSO,
                BaseRole.DISABLE_INSTITUICAO_CURSO,
                BaseRole.DETAIL_INSTITUICAO_CURSO,
                BaseRole.LIST_INSTITUICAO_CURSO,
                BaseRole.ADD_INSTITUICAO_CURSO_OCORRENCIA,
                BaseRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA,
                BaseRole.DISABLE_INSTITUICAO_CURSO_OCORRENCIA,
                BaseRole.DETAIL_INSTITUICAO_CURSO_OCORRENCIA,
                BaseRole.LIST_INSTITUICAO_CURSO_OCORRENCIA,
            };
        }


    }
}