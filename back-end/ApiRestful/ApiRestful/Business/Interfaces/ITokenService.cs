
using ApiRestful.Models;

namespace ApiRestful.Business.Interfaces
{
    public interface ITokenService
    {
        object CreateToken(User userName);
    }
}
