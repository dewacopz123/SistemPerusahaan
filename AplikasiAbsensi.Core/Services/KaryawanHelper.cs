using System.Text.Json;
using AplikasiAbsensi.Core.Models;

namespace AplikasiAbsensi.Core.Services
{
    public static class KaryawanHelper
    {
        private static readonly string filePath = "data_karyawan.json";

        public static void TambahKaryawan(Karyawan karyawan)
        {
            var data = LoadKaryawan();
            data.Add(karyawan);
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static List<Karyawan> LoadKaryawan()
        {
            if (!File.Exists(filePath)) return new List<Karyawan>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Karyawan>>(json) ?? new List<Karyawan>();
        }

        public static void SimpanData<T>(List<T> daftarKaryawan)
        {
            var json = JsonSerializer.Serialize(daftarKaryawan, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static bool HapusKaryawan(int idKaryawan)
        {
            var data = LoadKaryawan();
            var karyawan = data.FirstOrDefault(k => k.Id_Karyawan == idKaryawan);
            if (karyawan != null)
            {
                data.Remove(karyawan);
                SimpanData(data);
                return true; // sukses dihapus
            }
            return false; // tidak ditemukan
        }
    }
}
