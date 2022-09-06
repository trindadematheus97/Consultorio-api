using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using System.Collections;
using System.Threading.Tasks;

namespace Consultorio.Repository.Interfaces
{
    public interface IConsultaRepository : IBaseRepository
    {
        Task<IEnumerable> GetCosultas(ConsultaParams parametro);

        Task<Consulta> GetConsultaById(int id);
    }
}
