using Hangman.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hangman.Repo
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> _dbSet;
        ApplicationDbContext _dbContext;

        UnitOfWork _uow;

        public GenericRepository(UnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public GenericRepository(ApplicationDbContext gpsReconContext)
        {
            _dbContext = gpsReconContext ?? throw new ArgumentNullException("Context was not supplied");
            _dbSet = _dbContext.Set<TEntity>();
        }

        public TEntity AddEntity(TEntity entityToAdd)
        {
            _dbSet.Add(entityToAdd);
            return entityToAdd;
        }

        public void Delete(TEntity entityToDelete)
        {
            _dbSet.Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
        }

        public IQueryable<TEntity> FindInRecords(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public TEntity FindSingleInRecords(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAllRecords()
        {
            return _dbSet;
        }

        public TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return entityToUpdate;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
