using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public class CrypHelper
    {
        public static string GetMD5HashString(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(input);
                byte[] hash = md5.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }

        }
    }
}
