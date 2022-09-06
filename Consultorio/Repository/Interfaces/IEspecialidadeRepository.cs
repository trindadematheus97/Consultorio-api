﻿using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consultorio.Repository.Interfaces
{
    public interface IEspecialidadeRepository : IBaseRepository
    {
        Task<IEnumerable<EspecialidadeDto>> GetEspecialidades();
        Task<Especialidade> GetEspecialidadeById(int id);
    }
}
