using Microsoft.EntityFrameworkCore;
using Project_Management.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.DataAccess
{
    public class BaseRepo<T, TC> : IBaseRepo<T> where T : class where TC : DbContext
    {
        private readonly DbSet<T> _ContextSet;
        private readonly TC _Context;
        public BaseRepo(TC conbtext)
        {
            _Context = conbtext;
            _ContextSet = _Context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllRecords()
            => await _Context.Set<T>().ToListAsync();


        public async Task<T> GetRecordById(int id)
            => await _ContextSet.FindAsync(id);

        public async Task<T> CreateNewRecord(T model)
        {
            await _ContextSet.AddAsync(model);
            await _Context.SaveChangesAsync();
            return model;
        }

        public async Task<T> UpdateRecord(T model)
        {
            _Context.Entry(model).State = EntityState.Modified;
            await _Context.SaveChangesAsync();

            return model;
        }

        public async Task<T> DeleteRecord(int id)
        {
            var entity = await GetRecordById(id);
            if (entity == null)
                return entity;
            _ContextSet.Remove(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> SaveChange()
            => await _Context.SaveChangesAsync() >= 0;

    }
}
