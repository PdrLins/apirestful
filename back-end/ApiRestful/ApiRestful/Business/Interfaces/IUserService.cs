using DesafioPitang.Models;

namespace DesafioPitang.Business.Interfaces
{
    public interface IUserService
    {
        void Handle(SaveUser input);
        bool Handle(CheckHasEmail input);
        User Handle(FindUser input);
        void Handle(UpdateLastLoginUser input);
    }
}
