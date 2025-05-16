using System.Text.Json;
using AplikasiAbsensi.Core.Models;

namespace AplikasiAbsensi.Core.Services
{
    public static class LogPerubahanHelper
    {
        private static readonly string filePath = "log_perubahan.json";

        public static void TambahLog(LogPerubahan log)
        {
            var logs = LoadLog();
            logs.Add(log);
            var json = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static List<LogPerubahan> LoadLog()
        {
            if (!File.Exists(filePath)) return new List<LogPerubahan>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<LogPerubahan>>(json) ?? new List<LogPerubahan>();
        }
    }
}
