using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consultorio.Repository.Interfaces
{
    public interface IProfissionalRepository : IBaseRepository
    {
        Task<IEnumerable<ProfissionalDto>> GetProfisionais();
        Task<Profissional> GetProfisionalbyId(int id);
        Task<ProfissionalEspecialidade> GetProfissionalEspecialidade(int profissionalId, int especialidadeId);
    }
}
