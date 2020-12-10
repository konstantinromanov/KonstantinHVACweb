using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace KonstantinHVACweb.BusinessLogic.Extensions
{
    public static class StringExtensions
    {
        public const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`{}|~\w])*)(?<=[-!#\$%&'\*\+\/=\?\^`{}|~\w])@))(([0-9a-z][-0-9a-z]*[0-9a-z]*.)+[a-z0-9][-a-z0-9]{0,22}[a-z0-9])$";

        private const string DefaultEncryptionKey = "YFM6xp9amsQqTDHrGcKgPDex9FkVvhY1EQ1vKaynFatx4RgF4fbewuv7IPs9tiHU";
        private const string RandomizeChars = "abcdefghijklmnopqrstuvwxyzABCDEFKHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new Random();

        public static string Encrypt(this string s) => s.Encrypt(DefaultEncryptionKey);

        public static string Encrypt(this string s, string encryptionKey)
        {
            var sArray = Encoding.UTF8.GetBytes(s);

            var hashMd5 = new MD5CryptoServiceProvider();
            var encryptionKeyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            hashMd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = encryptionKeyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(sArray, 0, sArray.Length);
            tdes.Clear();

            return HttpUtility.UrlEncode(Convert.ToBase64String(resultArray, 0, resultArray.Length));
        }

        public static string Decrypt(this string s) => s.Decrypt(DefaultEncryptionKey);

        public static string Decrypt(this string s, string encryptionKey)
        {
            var sArray = Convert.FromBase64String(HttpUtility.UrlDecode(s));

            var hashMd5 = new MD5CryptoServiceProvider();
            var encryptionKeyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            hashMd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = encryptionKeyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            try
            {
                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(sArray, 0, sArray.Length);
                tdes.Clear();
                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsEmail(this string s) => new Regex(EmailRegex, RegexOptions.IgnoreCase).Match(s).Success;

        public static string Randomize(this string s) => new string(Enumerable.Repeat(RandomizeChars, s.Length).Select(z => z[Random.Next(z.Length)]).ToArray());

        public static string Randomize(this string s, string chars) => new string(Enumerable.Repeat(chars, s.Length).Select(z => z[Random.Next(z.Length)]).ToArray());

        public static string Base64Encode(this string s) => Convert.ToBase64String(Encoding.UTF8.GetBytes(s));

        public static string Base64Decode(this string s) => Encoding.UTF8.GetString(Convert.FromBase64String(s));

        public static int GetDeterministicHashCode(this string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        public static T MyToEnum<T>(this string s, T defaultValue = default)
        {
            if (s != null && Enum.IsDefined(typeof(T), s))
                return (T)Enum.Parse(typeof(T), s);

            return defaultValue;
        }
    }
}
