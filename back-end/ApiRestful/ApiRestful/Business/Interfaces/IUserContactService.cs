using DesafioPitang.Models;
using System.Collections.Generic;

namespace DesafioPitang.Business.Interfaces
{
    interface IUserContactService
    {
        IList<UserContact> Handle(GetUserPhones input);
    }
}
