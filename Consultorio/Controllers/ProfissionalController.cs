using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalRepository _repository;
        private readonly IMapper _mapper;

        public ProfissionalController(IProfissionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profissionais = await _repository.GetProfisionais();
            return profissionais.Any()
                ? Ok(profissionais)
                : NotFound("Profissionais não encontrados.");
        
       }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0) return BadRequest("Profissional inválido!");
            var profissional = await _repository.GetProfisionalbyId(id);
            return profissional!= null
               ? Ok(profissional)
               : NotFound("Profissional não encontrado.");
       }
        [HttpPost]
        public async Task<IActionResult> Post(ProfissionalAdicionarDto profissional)
        {
            if (string.IsNullOrEmpty(profissional.Nome)) return BadRequest("Dados inválidos!");
            var profissionalAdicionar = _mapper.Map<Profissional>(profissional);

            _repository.Add(profissionalAdicionar);

            return await _repository.SaveChangesAsync()
                ? Ok("Profissional adicionado.")
                : BadRequest("Erro ao adicionar profissional.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProfissionaAtualizarDto profissional)
        {
            if (id <= 0) return BadRequest("Profissional inválido!");
            var profissionalBanco = await _repository.GetProfisionalbyId(id);

            if (profissionalBanco == null) return NotFound("Profissional não encontrado.");

            var profissionalAtualizar = _mapper.Map(profissional, profissionalBanco);

            _repository.Update(profissionalAtualizar);

            return await _repository.SaveChangesAsync()
                ? Ok("Profissional atualizado.")
                : BadRequest("Erro ao atualizar profissional.");

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Profissional inválido!");
            var profissionalBanco = await _repository.GetProfisionalbyId(id);

            if (profissionalBanco == null) return NotFound("Profissional não encontrado.");

            _repository.Delete(profissionalBanco);

            return await _repository.SaveChangesAsync()
               ? Ok("Profissional deletado.")
               : BadRequest("Erro ao deletar profissional.");

        }

        [HttpPost("adicionar-profissional")]
        public async Task<IActionResult> PostProfissionalEspecialidade(ProfissionalAdicionarEspecialidadeDto profissional)
        {
            int profissionalId = profissional.ProfissionalId;
            int especialidadeId = profissional.EspecialidadeId;

            if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Dados inválidos.");

            var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);

            if (profissionalEspecialidade != null) return Ok("Especialidade já cadastrada");


            var especialidadeAdicionar = new ProfissionalEspecialidade
            {
                ProfissionalId = profissionalId,
                EspecialidadeId = especialidadeId
            };

            _repository.Add(especialidadeAdicionar);

            return await _repository.SaveChangesAsync()
            ? Ok("Especialidade adicionada.")
            : BadRequest("Erro ao adicionar especialidade");
        }
        [HttpDelete("{profissionalId}/deletar-especialidade/{especialidadeId}")]
        public async Task<IActionResult> DeleteProfissionalEspecialidade(int profissionalId, int especialidadeId)
        {
            if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Dados inválidos");

            var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);

            if (profissionalEspecialidade == null)
                return BadRequest("Especialidade não cadastrada");

            _repository.Delete(profissionalEspecialidade);

            return await _repository.SaveChangesAsync()
                ? Ok("Especialiadade deletada do profissional")
                : BadRequest("Erro ao deletar especialidade do profissional");
        }
    }
}
