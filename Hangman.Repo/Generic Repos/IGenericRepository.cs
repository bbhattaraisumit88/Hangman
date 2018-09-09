using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hangman.Repo
{
    internal interface IGenericRepository<T>
    {
        IQueryable<T> FindInRecords(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllRecords();
        T AddEntity(T entityToAdd);
        T Update(T entityToUpdate);
        T GetById(Guid id);
        void Delete(T entityToDelete);
        void Save();
    }
}
