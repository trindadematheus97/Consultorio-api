using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository;
using Consultorio.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IConsultaRepository _repository;
        private readonly IMapper _mapper;

        public AgendamentoController(IConsultaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ConsultaParams parametro)
        {
            var consulta = await _repository.GetCosultas(parametro);

            var consultaRetorno =  _mapper.Map<IEnumerable<ConsultaDetalhesDto>>(consulta);

            return consultaRetorno.Any()
                ? Ok(consultaRetorno)
                : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Consulta inválida.");
            var consulta = await  _repository.GetConsultaById(id);

            var consultaRetorno = _mapper.Map<IEnumerable<ConsultaDetalhesDto>>(consulta);

            return consultaRetorno.Any()
                ? Ok(consultaRetorno)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConsultaAdicionarDto consulta)
        {
            if (consulta == null) return BadRequest("Dados inválidos.");

            var consultaAdicionar = _mapper.Map<Consulta>(consulta);

            _repository.Add(consultaAdicionar);


            return await _repository.SaveChangesAsync()
                 ? Ok("Consulta agendada.")
                : NotFound("Erro ao agendar consulta.");

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,ConsultaAdicionarDto consulta)
        {
            if (consulta == null) return BadRequest("Dados inválidos.");

            var consultaBanco = await _repository.GetConsultaById(id);

            if (consultaBanco == null) return BadRequest("Dado não existente");

            if (consulta.DataHorario == new DateTime()) consulta.DataHorario = consultaBanco.DataHorario;

            if(consulta.ProfissionalId <= 0) consulta.ProfissionalId = consultaBanco.ProfissionalId;

            var consultaAtualizar = _mapper.Map(consulta, consultaBanco);

            _repository.Update(consultaAtualizar);

            return await _repository.SaveChangesAsync()
                ? Ok("Agendamento de consulta atualizado.")
                : BadRequest("Erro ao atualizar agendamento de consulta.");
        }

    }
}
