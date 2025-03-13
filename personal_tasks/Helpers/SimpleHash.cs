using System.Security.Cryptography;
using System.Text;

namespace personal_tasks.Helpers
{
    public static class SimpleHash
    {
        public static string ComputeHash(string input, string hashAlgorithmName = "SHA256", byte[] saltBytes = null)
        {
            // 預設若未提供 salt 則略過
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // 如果提供了 salt 就先合併
            byte[] bytesToHash;
            if (saltBytes != null && saltBytes.Length > 0)
            {
                bytesToHash = new byte[saltBytes.Length + inputBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, bytesToHash, 0, saltBytes.Length);
                Buffer.BlockCopy(inputBytes, 0, bytesToHash, saltBytes.Length, inputBytes.Length);
            }
            else
            {
                bytesToHash = inputBytes;
            }

            // 根據指定的演算法計算雜湊值
            using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName))
            {
                if (hashAlgorithm == null)
                    throw new InvalidOperationException("Specified hash algorithm is not valid");

                byte[] hashBytes = hashAlgorithm.ComputeHash(bytesToHash);

                // Convert hash bytes to hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
