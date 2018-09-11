using Hangman.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.Service.Interface
{
    public interface IUserLeaveService
    {
        int ApplyLeave(UserLeave data);
        int CancelLeave(UserLeave data);
        int ApproveLeave(UserLeave data);
        int RejectLeave(UserLeave data);
        IQueryable<object> GetLeaveById(string id);
        IQueryable<object> GetAllLeave();
     }
}
