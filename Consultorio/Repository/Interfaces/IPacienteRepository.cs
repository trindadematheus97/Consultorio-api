using Consultorio.Models.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consultorio.Repository.Interfaces
{
    public interface IPacienteRepository : IBaseRepository
    {
       Task<IEnumerable<Paciente>> GetPacientesAsync();

        Task<Paciente> GetPacientesById(int id);
    }
}
