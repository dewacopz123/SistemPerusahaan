using AplikasiAbsensi.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AplikasiAbsensi.Core.Services
{
    public class LoginService
    {
        private string _filePath = "data_karyawan.json";

        public Karyawan Login()
        {
            List<Karyawan> karyawanList = LoadKaryawan();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== LOGIN ===");
                Console.Write("Nama: ");
                string nama = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("❌ Nama dan email tidak boleh kosong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("DEBUG: Mencocokkan nama dan email...");
                foreach (var k in karyawanList)
                {
                    Console.WriteLine($"DEBUG: Bandingkan input '{nama}' dengan '{k.Nama_Karyawan}' dan input '{email}' dengan '{k.Email_Karyawan}'");

                    Console.WriteLine($"  Nama cocok? {(k.Nama_Karyawan?.Trim().ToLower() == nama.Trim().ToLower())}");
                    Console.WriteLine($"  Email cocok? {(k.Email_Karyawan?.Trim().ToLower() == email.Trim().ToLower())}");
                }


                var karyawan = karyawanList.FirstOrDefault(k =>
                    k.Nama_Karyawan?.Trim().ToLower() == nama.Trim().ToLower() &&
                    k.Email_Karyawan?.Trim().ToLower() == email.Trim().ToLower());

                if (karyawan != null)
                {
                    Console.WriteLine("✅ Login berhasil!");
                    return karyawan;
                }

                Console.WriteLine("❌ Nama atau email salah. Tekan ENTER untuk coba lagi.");
                Console.ReadLine();
            }
        }

        private List<Karyawan> LoadKaryawan()
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("❌ File data_karyawan.json tidak ditemukan.");
                return new List<Karyawan>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Karyawan>>(json) ?? new List<Karyawan>();
        }

        public static void TampilTidakPunyaAkses()
        {
            Console.WriteLine("❌ Anda tidak punya akses ke menu ini.");
        }
    }
}
