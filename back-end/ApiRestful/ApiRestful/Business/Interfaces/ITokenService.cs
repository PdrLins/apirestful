
using DesafioPitang.Models;

namespace DesafioPitang.Business.Interfaces
{
    public interface ITokenService
    {
        object CreateToken(User userName);
    }
}
