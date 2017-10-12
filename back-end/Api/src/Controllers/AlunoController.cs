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
        public List<AlunoVM> Get()
        {
            return this._alunoService.GetAll();
        }
        [HttpGet("{id}")]
        public AlunoVM GetDetail(string id)
        {
            return this._alunoService.GetDetail(id);
        }
        [HttpPut("add")]
        public AlunoVM Put([FromBody] AlunoVM viewModel)
        {
            return this._alunoService.Add(viewModel);
        }
        [HttpPost("upd")]
        public AlunoVM Post([FromBody] AlunoVM viewModel)
        {
            return this._alunoService.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete([FromQuery] long id)
        {
            this._alunoService.Disable(id);
        }
    }
}