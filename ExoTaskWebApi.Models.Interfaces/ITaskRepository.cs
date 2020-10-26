using System;
using System.Collections;
using System.Collections.Generic;

namespace ExoTaskWebApi.Models.Interfaces
{
    public interface ITaskRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Insert(TEntity entity);
        bool Update(int id, TEntity entity);
        bool Delete(int id);
    }
}
