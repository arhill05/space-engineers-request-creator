using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace EngineersAdminApiKeyGen
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = "/vrageremote/v1/server";
            var rand = new Random();
            int random = rand.Next(0, int.MaxValue);
            string nonce = random.ToString();
            string date = DateTime.UtcNow.ToString("r", CultureInfo.InvariantCulture);
            StringBuilder message = new StringBuilder();

            message.AppendLine(url);
            message.AppendLine(nonce);
            message.AppendLine(date);

            byte[] messageBuffer = Encoding.UTF8.GetBytes(message.ToString());
            byte[] key = Convert.FromBase64String("ojyJQ631XVtQWtpdg5TO8w==");
            byte[] computedHash;
            using (HMACSHA1 hmac = new HMACSHA1(key))
            {
                computedHash = hmac.ComputeHash(messageBuffer);
            };

            string hash = Convert.ToBase64String(computedHash);

            Console.WriteLine("nonce: " + nonce);
            Console.WriteLine("hash: " + hash);
            Console.WriteLine("date: " + date);
            Console.WriteLine("authorization: " + $"{nonce}:{hash}");
            Console.ReadKey();
        }
    }
}
