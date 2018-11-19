using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DesafioPitang.Utils
{
    public sealed class Utils
    {
        public static Utils Instance { get; } = new Utils();

        public string GenerateMd5(string password)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Count(); i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public object ResultMessage(object message, bool isSucess, int messageCode)
        {

            return new { Message = message, MessageCode = messageCode, IsSucess = isSucess };
        }
    }
}
