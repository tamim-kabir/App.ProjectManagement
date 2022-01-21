﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Management.DataAccess.Interfaces
{
    public interface IBaseRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllRecords();
        Task<T> GetRecordById(int id);
        Task<T> CreateNewRecord(T model);
        Task<T> UpdateRecord(T model);
        Task<T> DeleteRecord(int id);
        Task<bool> SaveChange();
    }
}