using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Infrastructure.Persistence.Helpers
{
    public class SecurityHelper
    {
        public static Task<string> ComputeMD5Hash(string text)
        {
            using MD5 md5 = MD5.Create();
            var createdProductHash = md5.ComputeHash(Encoding.Default.GetBytes(text));
            StringBuilder builder = new StringBuilder();
            foreach (byte i in createdProductHash)
            {
                builder.Append(i.ToString("X2"));
            }
            return Task.FromResult(builder.ToString());
        }
    }
}
