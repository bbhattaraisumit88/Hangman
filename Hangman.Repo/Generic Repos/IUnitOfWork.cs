using System;

namespace Hangman.Repo
{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository UserRepository { get; }
        int Save();
    }

}
