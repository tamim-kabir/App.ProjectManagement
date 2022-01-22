using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.DataAccess.Interfaces
{
    public interface IBaseRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllRecords();
        IQueryable<T> GetAllIQueryable();
        Task<T> GetRecordById(int id);
        Task CreateNewRecord(T model);
        Task<T> UpdateRecord(T model);
        Task<T> DeleteRecord(int id);
        Task<bool> SaveChange();
    }
}
