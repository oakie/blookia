using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Blookia
{
    public static class Util
    {
        public static string ComputeHash(object data)
        {
            if (data == null)
                return null;

            var serializer = new DataContractSerializer(data.GetType());
            using (var sha1 = new SHA1CryptoServiceProvider())
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, data);
                return HexStringFromBytes(sha1.ComputeHash(stream.ToArray()));
            }
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
