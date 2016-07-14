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

        public static byte[] ToByteArray(this string value)
        {
            byte[] bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
