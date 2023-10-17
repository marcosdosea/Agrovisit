using AgroVisitAPI.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Service;


namespace AgroVisitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntervencaoController : ControllerBase
    {
        private readonly IIntervencaoService _intervencaoService;
        private readonly IMapper _mapper;

        public IntervencaoController(IIntervencaoService intervencaoService, IMapper mapper)
        {
            _intervencaoService = intervencaoService;
            _mapper = mapper;
        }

        // GET: api/<IntervencaoController>
        [HttpGet]
        public ActionResult Get()
        {
            var listaIntervencoes = _intervencaoService.GetAll();
            if (listaIntervencoes == null)
            {
                return NotFound();
            }
            return Ok(listaIntervencoes);
        }

        // GET api/<IntervencaoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(uint id)
        {
            Intervencao intervencao = _intervencaoService.Get(id);
            if (intervencao == null)
            {
                return NotFound();
            }
            return Ok(intervencao);
        }

        // POST api/<IntervencaoController>
        [HttpPost]
        public ActionResult Post([FromBody] IntervencaoViewModel intervencaoModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var intervencao = _mapper.Map<Intervencao>(intervencaoModel);
            _intervencaoService.Create(intervencao);

            return Ok();
        }

        // PUT api/<IntervencaoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] IntervencaoViewModel intervencaoModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var intervencao = _mapper.Map<Intervencao>(intervencaoModel);
            if (intervencao == null)
                return NotFound();

            _intervencaoService.Edit(intervencao);

            return Ok();
        }

        // DELETE api/<IntervencaoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(uint id)
        {
            Intervencao intervencao = _intervencaoService.Get(id);
            if (intervencao == null)
                return NotFound();

            _intervencaoService.Delete(id);
            return Ok();
        }
    }
}
