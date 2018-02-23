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

        public InstituicaoBusinessController(InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository, UsuarioRepository usuarioRepository) : base(usuarioRepository) {
            this._instituicaoService = new InstituicaoService(instituicaoRepository, cursoRepository);
        }

        [HttpGet("/all-instituicao-curso-ocorrencia-periodo-by-professor/{id}")]
        public List<InstituicaoCursoOcorrenciaPeriodoVM> AllInstituicaoCursoOcorrenciaPeriodoByProfessor(string id) {
            return this._instituicaoService.AllInstituicaoCursoOcorrenciaPeriodoByProfessor(id);
        }

        public void AddAusenciasByInstituicaoCursoOcorrenciaPeriodoAndCursoGradeMateria(long idInstituicaoCursoOcorrenciaPeriodo, long idCursoGradeMateria, List<AlunoVM> alunos) {
            this._instituicaoService.AddInstituicaoCursoOcorrenciaPeriodoAlunoAusenciaByInstituicaoCursoOcorrenciaPeriodoAndCursoGradeMateria(idInstituicaoCursoOcorrenciaPeriodo, idCursoGradeMateria, alunos);
        }

    }
}