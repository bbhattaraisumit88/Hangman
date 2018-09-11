using Hangman.Domain;
using Hangman.Repo;
using Hangman.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.Service.Service
{
   public class UserLeaveService: IUserLeaveService
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext appContext;
        public UserLeaveService(IUnitOfWork _uow, ApplicationDbContext appContext)
        {
            this._uow = _uow;
            this.appContext = appContext;
        }

        public int ApplyLeave(UserLeave data)
        {
            _uow.UserLeaveRepository.AddEntity(data);
            return _uow.Save();
        }

        public int CancelLeave(UserLeave data)
        {
            _uow.UserLeaveRepository.Update(data);
            return _uow.Save();
        }

        public int ApproveLeave(UserLeave data)
        {
            _uow.UserLeaveRepository.Update(data);
            return _uow.Save();
        }

        public int RejectLeave(UserLeave data)
        {
            _uow.UserLeaveRepository.Update(data);
            return _uow.Save();
        }

        public IQueryable<object> GetAllLeave()
        {
            return from leave in appContext.UserLeaves
                   join users in appContext.Users on leave.IdentityId equals users.Id
                   where leave.Status == "unapproved"
                   select new { leave.IdentityId, leave.Id, users.UserName, leave.From, leave.To, leave.Reason, leave.Status };
        }

        public IQueryable<object> GetLeaveById(string id)
        {
            return from leave in appContext.UserLeaves
                   join users in appContext.Users on leave.IdentityId equals users.Id
                   where leave.IdentityId == id && leave.Status != "deleted"
                   select new { leave.Id, users.UserName, leave.From, leave.To, leave.Reason, leave.Status };
        }
    }
}
