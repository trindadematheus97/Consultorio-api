﻿using Consultorio.Context;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Repository
{
    public class ProfissionalRepository : BaseRepository, IProfissionalRepository
    {
        private readonly ConsultorioContext _context;

        public ProfissionalRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProfissionalDto>> GetProfisionais()
        {
            return await _context.Profissionais
                .Select(x => new ProfissionalDto { Id = x.Id, Nome = x.Nome, Ativo = x.Ativo}).ToListAsync();
        }

        public async Task<Profissional> GetProfisionalbyId(int id)
        {
            return await _context.Profissionais
                .Include(x => x.Consultas)
                .Include(x => x.Especialidades)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}