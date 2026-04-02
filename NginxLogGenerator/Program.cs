namespace NginxLogGenerator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var random = new Random();

            var paths = new[] { "/api/products", "/api/suppliers", "/api/stock", "/login", "/logout", "/health" };
            var statusCodes = new[] { 200, 301, 400, 401, 403, 404, 500 };
            string RandomIp() => $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(1, 255)}";
            var users = new[] { "-", "admin", "user1", "user2" };
            var startDate = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var entries = new List<NginxLogEntry>();

            for (int i = 0; i < 1000; i++)
            {
                var timestamp = startDate.AddSeconds(random.Next(0, 31536000));

                var entry = new NginxLogEntry
                {
                    Id = i + 1,
                    Epoch = timestamp.ToUnixTimeSeconds(),
                    RemoteIpAddress = RandomIp(),
                    RemoteUser = users[random.Next(users.Length)],
                    Timestamp = timestamp,
                    RequestPath = paths[random.Next(paths.Length)],
                    StatusCode = statusCodes[random.Next(statusCodes.Length)],
                    BytesSent = random.Next(200, 50000)
                };

                entries.Add(entry);
            }

            var filename = "nginx_logs.csv";
            using var writer = new StreamWriter(filename);
            await writer.WriteLineAsync("Id,Epoch,RemoteIpAddress,RemoteUser,Timestamp,RequestPath,StatusCode,BytesSent");

            foreach (var entry in entries)
            {
                await writer.WriteLineAsync($"{entry.Id},{entry.Epoch},{entry.RemoteIpAddress},{entry.RemoteUser},{entry.Timestamp:yyyy-MM-dd HH:mm:ss.fffffff zzz},{entry.RequestPath},{entry.StatusCode},{entry.BytesSent}");
            }

            Console.WriteLine($"Δημιουργήθηκαν {entries.Count} εγγραφές στο {filename}");
        }
    }
}
