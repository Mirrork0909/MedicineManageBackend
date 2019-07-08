using System;
using System.Security.Cryptography;
using System.Text;

namespace MedicineManageProject.Utils
{
    public class Md5
    {
        public static string getMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            byte[] inputBytes = Encoding.Default.GetBytes(input);

            byte[] data = md5Hasher.ComputeHash(inputBytes);

            StringBuilder sBuilder = new StringBuilder();

            //将data中的每个字符都转换为16进制的
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// 验证Md5 hash
        /// </summary>
        /// <param name="input">原字符串</param>
        /// <param name="hash">原字符串的md5码</param>
        /// <returns></returns>
        public static bool verifyMd5Hash(string input, string hash)
        {
            string hashOfInput = getMd5Hash(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
