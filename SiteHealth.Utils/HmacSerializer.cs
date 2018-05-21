using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BotMagic.Utils
{
    public class HmacSerializer<T> : IHmacSerializer<T>
    {
        private readonly HashAlgorithm _encoder;
        public HmacSerializer(string key)
        {
            var bytes = Encoding.Default.GetBytes(key);

            if (bytes.Length > 64)
                bytes = bytes.Take(64).ToArray();
            if (bytes.Length < 64)
                bytes = bytes.Concat(Enumerable.Repeat((byte)0, 64 - bytes.Length)).ToArray();

            _encoder = new HMACSHA256(bytes);
        }

        public string Serialize(T obj)
        {
            var arr = Encoding.Default.GetBytes(JsonConvert.SerializeObject(obj));
            var signature = Convert.ToBase64String(_encoder.ComputeHash(arr));
            var payload = Convert.ToBase64String(arr);
            return $"{payload}.{signature}";
        }

        public T Deserialize(string data)
        {
            if (!Regex.IsMatch(data, @"^([\w+\/=]+)\.([\w+\/=]+)$"))
                throw new HmacDeserializationException("Invalid data format");

            var payload = Convert.FromBase64String(data.Split('.')[0]);
            var trueSignature = Convert.ToBase64String(_encoder.ComputeHash(payload));

            if (data.Split('.')[1] != trueSignature)
                throw new HmacDeserializationException("Invalid signature");
            return JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(payload));
        }
    }



    [Serializable]
    public class HmacDeserializationException : Exception
    {
        public HmacDeserializationException() { }
        public HmacDeserializationException(string message) : base(message) { }
        public HmacDeserializationException(string message, Exception inner) : base(message, inner) { }
        protected HmacDeserializationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}