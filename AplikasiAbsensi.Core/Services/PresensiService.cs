using System;
using System.Collections.Generic;
using System.Linq;
using AplikasiAbsensi.Core.Models;
using AplikasiAbsensi.Core.Helpers;
using AplikasiAbsensi.Core.Helper;

namespace AplikasiAbsensi.Core.Services
{
    public class PresensiService
    {
        private List<Karyawan> daftarKaryawan;
        private List<Karyawan> daftarPresensi = new();

        public PresensiService(List<Karyawan> karyawanList)
        {
            daftarKaryawan = karyawanList;
            daftarPresensi = PresensiHelper.LoadPresensi(); // Load riwayat dari file JSON
        }

        public void PilihMenuPresensi()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Menu Presensi ===");
                Console.WriteLine("1. Lakukan Presensi");
                Console.WriteLine("2. Lihat Riwayat Presensi");
                Console.WriteLine("0. Kembali");
                Console.Write("Pilih menu: ");

                string inputMenu = Console.ReadLine();
                switch (inputMenu)
                {
                    case "1":
                        MenuPilihKaryawan();
                        break;
                    case "2":
                        TampilkanRiwayatPresensi();
                        break;
                    case "0":
                        Console.WriteLine("Kembali ke menu utama.");
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Console.WriteLine("Tekan ENTER untuk lanjut...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void MenuPilihKaryawan()
        {
            daftarKaryawan = KaryawanHelper.LoadKaryawan().Cast<Karyawan>().ToList();
            var daftarKaryawanFiltered = daftarKaryawan.Where(k => k.Role != Role.Manager).ToList();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Pilih Karyawan ===");
                for (int i = 0; i < daftarKaryawanFiltered.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {daftarKaryawanFiltered[i].Nama_Karyawan}");
                }
                Console.WriteLine("0. Kembali");
                Console.Write("Nomor karyawan: ");

                if (int.TryParse(Console.ReadLine(), out int karyawanIndex))
                {
                    if (karyawanIndex == 0) break;

                    karyawanIndex--;
                    if (karyawanIndex >= 0 && karyawanIndex < daftarKaryawanFiltered.Count)
                    {
                        var karyawan = daftarKaryawanFiltered[karyawanIndex];

                        Console.Clear();
                        Console.WriteLine($"Presensi untuk {karyawan.Nama_Karyawan}");
                        Console.WriteLine("1. Check-in");
                        Console.WriteLine("2. Check-out");
                        Console.Write("Nomor aksi: ");

                        if (int.TryParse(Console.ReadLine(), out int aksiPresensi))
                        {
                            if (aksiPresensi == 1)
                            {
                                CheckIn(karyawan);
                            }
                            else if (aksiPresensi == 2)
                            {
                                CheckOut(karyawan);
                            }
                            else
                            {
                                Console.WriteLine("Pilihan tidak valid.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Input tidak valid.");
                        }

                        Console.WriteLine("\nTekan ENTER untuk kembali ke menu...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Nomor karyawan tidak valid.");
                        Console.WriteLine("Tekan ENTER untuk lanjut...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Input tidak valid.");
                    Console.WriteLine("Tekan ENTER untuk lanjut...");
                    Console.ReadLine();
                }
            }
        }

        private void CheckIn(Karyawan karyawan)
        {
            var sudahPresensi = daftarPresensi.Any(p => p.Id_Karyawan == karyawan.Id_Karyawan && p.Tipe == "Check-in" && p.Waktu.Date == DateTime.Now.Date);

            if (!sudahPresensi)
            {
                karyawan.CheckInTime = DateTime.Now;
                karyawan.Tipe = "Check-in";
                karyawan.Waktu = DateTime.Now;

                Console.WriteLine($"{karyawan.Nama_Karyawan} berhasil check-in pada {karyawan.CheckInTime.Value}");

                SimpanPresensi(karyawan);
            }
            else
            {
                Console.WriteLine($"{karyawan.Nama_Karyawan} sudah melakukan check-in hari ini.");
            }
        }

        private void CheckOut(Karyawan karyawan)
        {
            var sudahCheckIn = daftarPresensi.FirstOrDefault(p => p.Id_Karyawan == karyawan.Id_Karyawan && p.Tipe == "Check-in" && p.Waktu.Date == DateTime.Now.Date);

            if (sudahCheckIn != null)
            {
                var sudahCheckout = daftarPresensi.Any(p => p.Id_Karyawan == karyawan.Id_Karyawan && p.Tipe == "Check-out" && p.Waktu.Date == DateTime.Now.Date);
                if (!sudahCheckout)
                {
                    karyawan.CheckOutTime = DateTime.Now;
                    karyawan.Tipe = "Check-out";
                    karyawan.Waktu = DateTime.Now;

                    Console.WriteLine($"{karyawan.Nama_Karyawan} berhasil check-out pada {karyawan.CheckOutTime.Value}");

                    SimpanPresensi(karyawan);
                }
                else
                {
                    Console.WriteLine($"{karyawan.Nama_Karyawan} sudah check-out hari ini.");
                }
            }
            else
            {
                Console.WriteLine($"{karyawan.Nama_Karyawan} belum melakukan check-in.");
            }
        }

        private void SimpanPresensi(Karyawan karyawan)
        {
            var log = new Karyawan(
                karyawan.Id_Karyawan,
                karyawan.Nama_Karyawan,
                karyawan.Email_Karyawan,
                karyawan.Phone_Karyawan,
                karyawan.Role,
                karyawan.Status,
                karyawan.Gaji,
                karyawan.JobdeskId
            )
            {
                Tipe = karyawan.Tipe,
                Waktu = karyawan.Waktu,
                CheckInTime = karyawan.CheckInTime,
                CheckOutTime = karyawan.CheckOutTime
            };

            daftarPresensi.Add(log);
            PresensiHelper.SimpanPresensi(daftarPresensi); // Simpan ke JSON
        }

        private void TampilkanRiwayatPresensi()
        {
            Console.Clear();
            Console.WriteLine("=== Riwayat Presensi ===");

            if (daftarPresensi.Count == 0)
            {
                Console.WriteLine("Belum ada data presensi.");
            }
            else
            {
                foreach (var presensi in daftarPresensi.OrderBy(p => p.Waktu))
                {
                    Console.WriteLine($"[{presensi.Waktu:yyyy-MM-dd HH:mm}] {presensi.Nama_Karyawan} - {presensi.Tipe}");
                }
            }

            Console.WriteLine("\nTekan ENTER untuk kembali...");
            Console.ReadLine();
        }
    }
}
