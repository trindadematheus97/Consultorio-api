using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consultorio.Repository.Interfaces
{
    public interface IPacienteRepository : IBaseRepository
    {
        Task<IEnumerable<PacienteDto>> GetPacientesAsync();
        Task<Paciente> GetPacientesByIdAsync(int id);

    }
}
