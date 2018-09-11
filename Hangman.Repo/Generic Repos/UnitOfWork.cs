using Hangman.Domain;
using System;

namespace Hangman.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("Context was not supplied");
        }

        UserRepository _userRepository;

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_dbContext));
            }
        }

        UserLeaveRepository _userLeaveRepository;
        public UserLeaveRepository UserLeaveRepository
        {
            get
            {
                return _userLeaveRepository ?? (_userLeaveRepository = new UserLeaveRepository(_dbContext));
            }
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
