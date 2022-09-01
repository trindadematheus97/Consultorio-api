using Consultorio.Context;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Repository
{
    public class PacienteRepository : BaseRepository, IPacienteRepository
    {
        private readonly ConsultorioContext _context;

        public PacienteRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PacienteDto>> GetPacientesAsync()
        {
            return await _context.Pacientes
                .Select(x => new PacienteDto { Id = x.Id, Nome = x.Nome})
                .ToListAsync();
           
        }

        public Task<Paciente> GetPacientesById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Paciente> GetPacientesByIdAsync(int id)
        {
            return await _context.Pacientes.Include(x => x.Consultas)
                .ThenInclude(c => c.Especialidade)
                .ThenInclude(c => c.Profissionais)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        
    }
}
