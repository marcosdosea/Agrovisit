using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Service;


namespace AgroVisitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly IProjetoService _projetoService;
        private readonly IIntervencaoService _intervencaoService;
        private readonly IPropriedadeService _propriedadeService;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ProjetosController(IProjetoService projetoService, IIntervencaoService intervencaoService, IPropriedadeService propriedadeService, IClienteService clienteService, IMapper mapper)
        {
            _projetoService = projetoService;
            _intervencaoService = intervencaoService;
            _propriedadeService = propriedadeService;
            _clienteService = clienteService;
            _mapper = mapper;
        }
        // GET: api/<ProjetosController>
        [HttpGet]
        public ActionResult Get()
        {
            var listaProjetos = _projetoService.GetAll();
            if (listaProjetos == null)
                return NotFound();
            return Ok(listaProjetos);
        }

        // GET api/<ProjetosController>/5
        [HttpGet("{id}")]
        public ActionResult Get(uint id)
        {
            Projeto projeto = _projetoService.Get(id);
            if (projeto == null)
                return NotFound();
            return Ok(projeto);
        }

        // POST api/<ProjetosController>
        [HttpPost]
        public ActionResult Post([FromBody] ProjetoViewModel projetoModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var projeto = _mapper.Map<Projeto>(projetoModel);
            _projetoService.Create(projeto);

            return Ok();
        }

        // PUT api/<ProjetosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(uint id, [FromBody] ProjetoViewModel projetoModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var projeto = _mapper.Map<Projeto>(projetoModel);
            projeto.Id = id;
            if (projeto == null)
                return NotFound();

            _projetoService.Edit(projeto);

            return Ok();
        }

        // DELETE api/<ProjetosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(uint id)
        {
            Projeto projeto = _projetoService.Get(id);
            if (projeto == null)
                return NotFound();

            _projetoService.Delete(id);
            return Ok();
        }
    }
}
