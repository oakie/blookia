
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Blookia
{
    public class Block
    {
        public Block Parent { get; protected set; }
        public string ParentHash { get; protected set; }
        public string Hash { get; protected set; }
        public object Data { get; protected set; }

        public Block(Block parent, object data)
        {
            Parent = parent;
            Data = data;

            ParentHash = parent?.Hash;
            Hash = ComputeHash(new object[] { ComputeHash(Data), ParentHash });
        }

        protected static string ComputeHash(object data)
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

        protected static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            var p = ParentHash ?? "        ";
            var h = Hash ?? "        ";
            return p.Substring(0, 8) + "\t" + h.Substring(0, 8) + "\t[" + Data + "]";
        }
    }
}
