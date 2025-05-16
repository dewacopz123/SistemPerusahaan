using AplikasiAbsensi.Core.Models;
using AplikasiAbsensi.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AplikasiAbsensi.Core
{
    public class MengelolaKaryawan<T> where T : Karyawan
    {
        private List<T> daftarKaryawan;
        private readonly KaryawanService karyawanService = new();

        private static readonly Dictionary<int, string> StatusMap = new()
        {
            { 1, "Pending" },
            { 2, "Hadir" },
            { 3, "Cuti" },
            { 4, "Alpa" },
            { 5, "Sakit" }
        };

        public MengelolaKaryawan()
        {
            daftarKaryawan = JsonHelper.LoadKaryawan().Cast<T>().ToList();
            ResetIdKaryawan();
        }


        public void TampilkanMenukaryawan()
        {
            bool kembali = false;

            var menu = new Dictionary<string, Action>
            {
                { "1", TampilkanDaftarKaryawan },
                { "2", TambahKaryawan },
                { "3", EditKaryawan },
                { "4", HapusKaryawan },
                { "5", () => kembali = true }
            };

            while (!kembali)
            {
                Console.Clear();
                Console.WriteLine("=== MENU MENGELOLA KARYAWAN ===");
                Console.WriteLine("1. Lihat Daftar Karyawan");
                Console.WriteLine("2. Tambah Karyawan");
                Console.WriteLine("3. Edit Karyawan");
                Console.WriteLine("4. Hapus Karyawan");
                Console.WriteLine("5. Kembali");
                Console.Write("Pilih menu: ");
                var input = Console.ReadLine();

                if (menu.TryGetValue(input, out var aksi))
                {
                    aksi.Invoke();
                    if (input != "5")
                    {
                        Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid.");
                    Console.ReadLine();
                }
            }
        }

        private void TampilkanDaftarKaryawan()
        {
            Console.WriteLine("\n--- Daftar Karyawan ---");
            int index = 1;
            foreach (var k in daftarKaryawan.Where(k => k.Role != 2))
            {
                string status = StatusMap.ContainsKey(k.Status) ? StatusMap[k.Status] : "Tidak Diketahui";
                Console.WriteLine($"{index}. {k.Nama_Karyawan} - {k.Email_Karyawan} - {k.Phone_Karyawan} - Status: {status}");
                index++;
            }
        }

        private void TambahKaryawan()
        {
            Console.WriteLine("\n--- Tambah Karyawan ---");
            int id = daftarKaryawan.Count + 1;

            Console.Write("Nama: ");
            string nama = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Telepon: ");
            string telepon = Console.ReadLine();

            Console.Write("Role (angka): ");
            int.TryParse(Console.ReadLine(), out int role);

            if (role == 2 || role == 3)
            {
                Console.WriteLine("Role 2 dan 3 tidak diperbolehkan.");
                return;
            }

            Console.WriteLine("Status Karyawan:");
            foreach (var s in StatusMap)
            {
                Console.WriteLine($"{s.Key}. {s.Value}");
            }
            Console.Write("Pilih status (angka): ");
            int.TryParse(Console.ReadLine(), out int status);

            var instance = Activator.CreateInstance(typeof(T), id, nama, email, telepon, role, status, 0);
            if (instance is T karyawan)
            {
                daftarKaryawan.Add(karyawan);
                ResetIdKaryawan();

                // ⬇️ Tambahkan pencatatan log di sini
                LogPerubahanHelper.TambahLog(new LogPerubahan
                {
                    Waktu = DateTime.Now,
                    Aksi = "Tambah",
                    NamaKaryawan = nama,
                    IdKaryawan = karyawan.Id_Karyawan,
                    Detail = $"Email: {email}, Telepon: {telepon}, Role: {role}, Status: {status}"
                });

                Console.WriteLine("Karyawan berhasil ditambahkan.");
            }
            else
            {
                Console.WriteLine("Gagal menambahkan karyawan.");
            }
        }

        private void EditKaryawan()
        {
            TampilkanDaftarKaryawan();
            Console.Write("\nMasukkan nomor urutan karyawan yang ingin diedit: ");
            int.TryParse(Console.ReadLine(), out int nomor);
            int index = nomor - 1;

            var karyawanList = daftarKaryawan.Where(k => k.Role != 2).ToList();

            if (index >= 0 && index < karyawanList.Count)
            {
                var karyawan = karyawanList[index];

                Console.WriteLine("[Kosongkan Jika Tidak Mau Diubah]");

                Console.Write("Nama Baru: ");
                string nama = Console.ReadLine();
                if (!string.IsNullOrEmpty(nama)) karyawan.Nama_Karyawan = nama;

                Console.Write("Email Baru: ");
                string email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email)) karyawan.Email_Karyawan = email;

                Console.Write("Telepon Baru: ");
                string telepon = Console.ReadLine();
                if (!string.IsNullOrEmpty(telepon)) karyawan.Phone_Karyawan = telepon;

                Console.Write("Role Baru (angka): ");
                int.TryParse(Console.ReadLine(), out int role);
                if (role != 2) karyawan.Role = role;

                Console.WriteLine("Status Baru:");
                foreach (var s in StatusMap)
                {
                    Console.WriteLine($"{s.Key}. {s.Value}");
                }
                Console.Write("Pilih status (angka): ");
                int.TryParse(Console.ReadLine(), out int statusBaru);
                karyawan.Status = statusBaru;

                Console.WriteLine("Karyawan berhasil diupdate.");
            }
            else
            {
                Console.WriteLine("Nomor urutan tidak ditemukan.");
            }
        }

        private void HapusKaryawan()
        {
            TampilkanDaftarKaryawan();
            Console.Write("\nMasukkan nomor urutan karyawan yang ingin dihapus: ");
            int.TryParse(Console.ReadLine(), out int nomor);
            int index = nomor - 1;

            var karyawanList = daftarKaryawan.Where(k => k.Role != 2).ToList();

            if (index >= 0 && index < karyawanList.Count)
            {
                var karyawan = karyawanList[index];
                daftarKaryawan.Remove(karyawan);
                ResetIdKaryawan();
                Console.WriteLine("Karyawan berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Nomor urutan tidak ditemukan.");
            }
        }

        private void ResetIdKaryawan()
        {
            int newId = 1;
            foreach (var k in daftarKaryawan.Where(k => k.Role != 2))
            {
                k.Id_Karyawan = newId++;
            }
        }
    }
}
