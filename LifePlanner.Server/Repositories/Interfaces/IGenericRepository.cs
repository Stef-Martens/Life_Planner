﻿using LifePlanner.Server.Data;
using LifePlanner.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifePlanner.Server.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);

    }
}
