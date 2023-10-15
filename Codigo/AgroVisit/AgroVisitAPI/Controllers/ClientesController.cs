using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using AgroVisitAPI.Models;

namespace AgroVisitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET: api/<ClientesController>
        [HttpGet]
        public ActionResult Get()
        {
            var listaClientes = _clienteService.GetAll();
            if (listaClientes == null)
                return NotFound();
            return Ok(listaClientes);
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(uint id)
        {
            Cliente cliente = _clienteService.Get(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        // POST api/<ClientesController>
        [HttpPost]
        public ActionResult Post([FromBody] ClienteViewModel clienteModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var cliente = _mapper.Map<Cliente>(clienteModel);
            _clienteService.Create(cliente);

            return Ok();
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(uint id, [FromBody] ClienteViewModel clienteModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var cliente = _mapper.Map<Cliente>(clienteModel);
            cliente.Id = id;
            if (cliente == null)
                return NotFound();

            _clienteService.Edit(cliente);

            return Ok();
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(uint id)
        {
            Cliente cliente = _clienteService.Get(id);
            if (cliente == null)
                return NotFound();

            _clienteService.Delete(id);
            return Ok();
        }
    }
}
