using ContractManagementValue.Common;
using ContractManagementValue.Data;
using ContractManagementValue.Interfaces;
using ContractManagementValue.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ContractManagementValue.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> _table;
       
        public GenericRepo(ApplicationDbContext context)
        {
            _context= context;
            _table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetId(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T t)
        {
            _table.Add(t);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            _table.Update(t);
            Save();
        }
        public void Delete(int id)
        {
            T Existing = _table.Find(id);
            
            _table.Remove(Existing);
        }

        public  IList<T> GetList(int id)
        {
            return _table.ToList(); 
        }


        public async Task<PaginatedResult<T>> GetPaginated(int page, int pageSize)
        {
            var count = await _table.CountAsync();
           // var totalCount = await _table.CountAsync(); // Get the total count of records
            var records = await _table// Get the total count of records
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<T>
            {
                result = records,
                Page = page,
                TotalCount = (int)Math.Ceiling(count / (double)pageSize)
            };
        }

    }
}
