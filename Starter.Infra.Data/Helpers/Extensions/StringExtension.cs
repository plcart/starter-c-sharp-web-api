using Starter.Infra.Data.Helpers.Cryptography;
using System;

namespace Starter.Infra.Data.Helpers.Extensions
{
    public static class StringExtension
    {
        public static string ToMD5(this string value)
        {
            return MD5.Encrypt(value);
        }

        public static byte[] ToByteArray(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
