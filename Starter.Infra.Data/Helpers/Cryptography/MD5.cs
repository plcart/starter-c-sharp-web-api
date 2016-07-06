using System.Text;

namespace Starter.Infra.Data.Helpers.Cryptography
{
    public static class MD5
    {
        public static string Encrypt(string value)
        {
            byte[] data = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
