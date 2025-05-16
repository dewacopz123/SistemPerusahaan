using System;
using System.Collections.Generic;
using AplikasiAbsensi.Core.Models;

namespace AplikasiAbsensi.Core.Services
{
    public class PresensiService
    {
        private List<Karyawan> daftarKaryawan;
        private List<Karyawan> daftarPresensi = new();

        public PresensiService(List<Karyawan> karyawanList)
        {
            daftarKaryawan = karyawanList;
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Pilih Karyawan ===");
                for (int i = 0; i < daftarKaryawan.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {daftarKaryawan[i].Nama_Karyawan}");
                }
                Console.WriteLine("0. Kembali");
                Console.Write("Nomor karyawan: ");

                if (int.TryParse(Console.ReadLine(), out int karyawanIndex))
                {
                    if (karyawanIndex == 0) break;

                    karyawanIndex--;
                    if (karyawanIndex >= 0 && karyawanIndex < daftarKaryawan.Count)
                    {
                        Console.Clear();
                        Console.WriteLine($"Presensi untuk {daftarKaryawan[karyawanIndex].Nama_Karyawan}");
                        Console.WriteLine("1. Check-in");
                        Console.WriteLine("2. Check-out");
                        Console.Write("Nomor aksi: ");

                        if (int.TryParse(Console.ReadLine(), out int aksiPresensi))
                        {
                            if (aksiPresensi == 1)
                            {
                                CheckIn(karyawanIndex);
                            }
                            else if (aksiPresensi == 2)
                            {
                                CheckOut(karyawanIndex);
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



        private void CheckIn(int karyawanIndex)
        {
            var karyawan = daftarKaryawan[karyawanIndex];
            if (karyawan.CheckInTime == null)
            {
                karyawan.CheckInTime = DateTime.Now;
                karyawan.Tipe = "Check-in";
                karyawan.Waktu = DateTime.Now;

                Console.WriteLine($"{karyawan.Nama_Karyawan} berhasil check-in pada {karyawan.CheckInTime.Value}");

                SimpanPresensi(karyawan);
            }
            else
            {
                Console.WriteLine($"{karyawan.Nama_Karyawan} sudah melakukan check-in sebelumnya.");
            }
        }

        private void CheckOut(int karyawanIndex)
        {
            var karyawan = daftarKaryawan[karyawanIndex];
            if (karyawan.CheckInTime == null)
            {
                Console.WriteLine($"{karyawan.Nama_Karyawan} belum melakukan check-in.");
            }
            else
            {
                karyawan.CheckOutTime = DateTime.Now;
                karyawan.Tipe = "Check-out";
                karyawan.Waktu = DateTime.Now;

                Console.WriteLine($"{karyawan.Nama_Karyawan} berhasil check-out pada {karyawan.CheckOutTime.Value}");

                SimpanPresensi(karyawan);
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
                karyawan.Gaji
            )
            {
                Tipe = karyawan.Tipe,
                Waktu = karyawan.Waktu
            };

            daftarPresensi.Add(log);
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
                foreach (var presensi in daftarPresensi)
                {
                    Console.WriteLine($"[{presensi.Waktu}] {presensi.Nama_Karyawan} - {presensi.Tipe}");
                }
            }

            Console.WriteLine("\nTekan ENTER untuk kembali...");
            Console.ReadLine();
        }
    }
}