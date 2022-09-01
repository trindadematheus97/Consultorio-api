using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Consultorio.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository _repository;
        private readonly IMapper _mapper;

        public PacienteController(IPacienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var pacientes = await _repository.GetPacientesAsync();

            if (pacientes != null)

                return Ok(pacientes);

            return NotFound("Pacientes não encontrados.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _repository.GetPacientesByIdAsync(id);

            var pacienteRetorno = _mapper.Map<PacienteDetailsDto>(paciente);

            var pacienteTest = _mapper.Map<Paciente>(pacienteRetorno);

            if (pacienteRetorno == null)
                return NotFound("Paciente não encontrado.");

            return Ok(pacienteRetorno);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PacienteAdicionarDto paciente)
        {
            if (paciente == null) return BadRequest("Dados inválidos");

            //var pacienteAdicionar = _mapper.Map<Paciente>(paciente);

            var pacienteAdicionar = Paciente.FactoryPaciente.Registrar(paciente);

            _repository.Add(pacienteAdicionar);

            return await _repository.SaveChangesAsync()
                ? Ok("Paciente adicionado com sucesso.")
                : BadRequest("Erro ao salvar o paciente!");
        }
    }
}
