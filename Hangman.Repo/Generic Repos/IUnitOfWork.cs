using System;

namespace Hangman.Repo
{
    public interface IUnitOfWork : IDisposable
    {
        GameDataRepository GameDataRepository { get; }
        int Save();
    }

}
