using System;

namespace Hangman.Repo
{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository UserRepository { get; }
        UserLeaveRepository UserLeaveRepository { get; }
        int Save();
    }

}
