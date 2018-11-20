using ApiRestful.Models;

namespace ApiRestful.Business.Interfaces
{
    public interface IUserService
    {
        User Handle(SaveUser input);
        bool Handle(CheckHasEmail input);
        User Handle(FindUser input);
        void Handle(UpdateLastLoginUser input);
    }
}
