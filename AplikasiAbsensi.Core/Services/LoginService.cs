using AplikasiAbsensi.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AplikasiAbsensi.Core.Services
{
    public class LoginService
    {
        private readonly string _filePath = "data_karyawan.json";

        public List<Karyawan> GetAllKaryawan()
        {
            return LoadKaryawan();
        }

        public Karyawan Login(string nama, string email)
        {
            var karyawanList = LoadKaryawan();

            return karyawanList.FirstOrDefault(k =>
                k.Nama_Karyawan?.Trim().ToLower() == nama.Trim().ToLower() &&
                k.Email_Karyawan?.Trim().ToLower() == email.Trim().ToLower());
        }

        private List<Karyawan> LoadKaryawan()
        {
            if (!File.Exists(_filePath))
                return new List<Karyawan>();

            var json = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Karyawan>>(json, options) ?? new List<Karyawan>();
        }

        public static void TampilTidakPunyaAkses()
        {
            Console.WriteLine("❌ Anda tidak punya akses ke menu ini.");
        }
    }
}
