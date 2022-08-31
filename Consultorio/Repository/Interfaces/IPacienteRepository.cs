using Consultorio.Models.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Consultorio.Repository.Interfaces
{
    public interface IPacienteRepository : IBaseRepository
    {
        IEnumerable<Paciente> Get();

        Paciente GetById(int id);
    }
}
