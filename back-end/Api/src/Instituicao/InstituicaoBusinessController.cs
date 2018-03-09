using System;
using System.Collections.Generic;
using Api.Common.Base;
using Api.CursoApi;
using Api.UsuarioApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.InstituicaoApi {

    [Route("api/business/instituicao")]
    public class InstituicaoBusinessController : BaseController {

        private InstituicaoService _instituicaoService;

        public InstituicaoBusinessController(InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository, UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) : base(usuarioRepository, areaInteresseRepository) {
            this._instituicaoService = new InstituicaoService(instituicaoRepository, cursoRepository);
        }

        [HttpPost("instituicao-curso-ocorrencia-notas/{idInstituicaoCursoOcorrenciaPeriodoProfessor}/save")]
        public void SaveInstituicaoCursoOcorrenciaNotas(long idInstituicaoCursoOcorrenciaPeriodoProfessor, [FromBody] List<InstituicaoCursoOcorrenciaNotaVM> instituicaoCursoOcorrenciaNotas) {
            this._instituicaoService.SaveInstituicaoCursoOcorrenciaNotas(instituicaoCursoOcorrenciaNotas, idInstituicaoCursoOcorrenciaPeriodoProfessor);
        }

        [HttpPost("formula-nota-final/{idInstituicaoCursoOcorrenciaPeriodoProfessor}/save")]
        public void SaveFormulaNotaFinal(long idInstituicaoCursoOcorrenciaPeriodoProfessor, [FromBody] string[] formulaNotaFinal) {
            this._instituicaoService.SaveFormulaNotaFinal(String.Join(',', formulaNotaFinal), idInstituicaoCursoOcorrenciaPeriodoProfessor);
        }

        [HttpGet("formula-nota-final/{idInstituicaoCursoOcorrenciaPeriodoProfessor}")]
        public string[] GetFormulaNotaFinal(long idInstituicaoCursoOcorrenciaPeriodoProfessor) {
            var notaFinal = this._instituicaoService.GetFormulaNotaFinal(idInstituicaoCursoOcorrenciaPeriodoProfessor);
            return notaFinal == null ? new string[0] : notaFinal.Split(',');
        }

        [HttpGet("instituicao-curso-ocorrencia-notas/by-instituicao-curso-ocorrencia-periodo-professor/{idInstituicaoCursoOcorrenciaPeriodoProfessor}")]
        public List<InstituicaoCursoOcorrenciaNotaVM> AllInstituicaoCursoOcorrenciaNotaByInstituicaoCursoOcorrenciaPeriodoProfessor(long idInstituicaoCursoOcorrenciaPeriodoProfessor) {
            return this._instituicaoService.AllInstituicaoCursoOcorrenciaNotaByInstituicaoCursoOcorrenciaPeriodoProfessor(idInstituicaoCursoOcorrenciaPeriodoProfessor);
        }

        [HttpGet("instituicao-business-aula/by-professor/{id}")]
        public List<InstituicaoBusinessAulaVM> AllInstituicaoBusinessAulaByProfessor(string id) {
            return this._instituicaoService.AllInstituicaoBusinessAulaByProfessor(id);
        }

        [HttpGet("instituicao-curso-ocorrencia-periodo-aluno/by-instituicao-curso-ocorrencia-periodo-professor/{id}")]
        public List<InstituicaoCursoOcorrenciaPeriodoAlunoVM> AllInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOCorrenciaPeriodoProfessor(long id) {
            return this._instituicaoService.AllInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOCorrenciaPeriodoProfessor(id);
        }

        [HttpGet("instituicao-business-aula/by-aluno/{id}")]
        public List<InstituicaoBusinessAulaDetalheAlunoVM> AllInstituicaoBusinessAulaByAluno(string id) {
            return this._instituicaoService.AllInstituicaoBusinessAulaByAluno(id);
        }

    }
}