using ApiRestful.Models;
using System.Collections.Generic;

namespace ApiRestful.Business.Interfaces
{
    interface IUserContactService
    {
        IList<UserContact> Handle(GetUserPhones input);
    }
}
