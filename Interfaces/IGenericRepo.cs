using ContractManagementValue.Common;

namespace ContractManagementValue.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<PaginatedResult<T>> GetPaginated(int page, int pageSize);
        IList<T>GetList(int id);
        T GetId(object id);
        void Insert(T t);
        void Update(T t);
        void Delete(int id);
        void Save();
    }
}
