using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AplikasiAbsensi.Core.Models; // Sesuaikan namespace model

namespace AplikasiAbsensi.Core.Services
{
    public static class JsonHelper
    {
        private static string filePath = "data_karyawan.json";

        public static List<Karyawan> LoadKaryawan()
        {
            if (!File.Exists(filePath))
                return new List<Karyawan>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Karyawan>>(json) ?? new List<Karyawan>();
        }

        public static void SaveKaryawan(List<Karyawan> daftarKaryawan)
        {
            string json = JsonSerializer.Serialize(daftarKaryawan, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
