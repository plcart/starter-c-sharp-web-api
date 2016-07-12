using Starter.Infra.Data.Helpers.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Infra.Data.Helpers.Extensions
{
    public static class StringExtension
    {
        public static string ToMD5(this string value)
        {
            return MD5.Encrypt(value);
        }
    }
}
