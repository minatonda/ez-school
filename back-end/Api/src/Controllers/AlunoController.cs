using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/aluno")]
    public class AlunoController : Controller
    {

        private AlunoService _alunoService;
        public AlunoController(AlunoRepository alunoRepository)
        {
            this._alunoService = new AlunoService(alunoRepository);
        }
        [HttpGet]
        public List<AlunoVM> All()
        {
            return this._alunoService.All();
        }
        [HttpGet("{id}")]
        public AlunoVM Detail(string id)
        {
            return this._alunoService.Detail(id);
        }
        [HttpPut("add")]
        public AlunoVM Add([FromBody] AlunoVM viewModel)
        {
            return this._alunoService.Add(viewModel);
        }
        [HttpPost("update")]
        public AlunoVM Update([FromBody] AlunoVM viewModel)
        {
            return this._alunoService.Update(viewModel);
        }
        [HttpDelete("disable")]
        public void Disable([FromQuery] long id)
        {
            this._alunoService.Disable(id);
        }
    }
}