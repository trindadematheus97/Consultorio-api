using Consultorio.Context;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using System.Threading.Tasks;

namespace Consultorio.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ConsultorioContext _context;

        public BaseRepository(ConsultorioContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}
