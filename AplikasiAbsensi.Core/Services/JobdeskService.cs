using AplikasiAbsensi.Core.Models;
using System;
using System.Collections.Generic;

namespace AplikasiAbsensi.Core.Services
{
    public class JobdeskService
    {
        private List<string> jobdeskList = new List<string>();
        private List<Karyawan> daftarKaryawan;

        public JobdeskService()
        {
            daftarKaryawan = new KaryawanService().GetSampleKaryawan();
        }

        public void TampilkanMenuJobdesk(List<Karyawan> daftarKaryawan)
        {
            bool lanjut = true;

            var menuActions = new Dictionary<string, Action>
    {
        { "1", () => TambahJobdesk() },
        { "2", () => TampilkanJobdesk() },
        { "3", () => HapusJobdesk() },
        { "4", () => BerikanJobdeskKeKaryawan() },
        { "5", () => TampilkanJobdeskKaryawan() }
    };

            while (lanjut)
            {
                Console.Clear();
                Console.WriteLine("=== MENU JOBDESK KARYAWAN ===");
                Console.WriteLine("1. Tambah Jobdesk");
                Console.WriteLine("2. Tampilkan Semua Jobdesk");
                Console.WriteLine("3. Hapus Jobdesk");
                Console.WriteLine("4. Berikan Jobdesk ke Karyawan");
                Console.WriteLine("5. Lihat Jobdesk Karyawan");
                Console.WriteLine("6. Kembali");
                Console.Write("Pilihan Anda: ");
                string input = Console.ReadLine();

                if (input == "6")
                {
                    lanjut = false;
                }
                else if (menuActions.ContainsKey(input))
                {
                    menuActions[input].Invoke();
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid.");
                }

                if (lanjut)
                {
                    Console.WriteLine("\nTekan ENTER untuk kembali...");
                    Console.ReadLine();
                }
            }
        }


        public void TambahJobdesk()
        {
            Console.Write("Masukkan jobdesk baru: ");
            string jobdesk = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(jobdesk))
            {
                jobdeskList.Add(jobdesk);
                Console.WriteLine("Jobdesk ditambahkan.");
            }
            else
            {
                Console.WriteLine("Jobdesk tidak boleh kosong.");
            }
        }

        public void TampilkanJobdesk()
        {
            if (jobdeskList.Count == 0)
            {
                Console.WriteLine("Belum ada jobdesk.");
                return;
            }

            Console.WriteLine("Daftar Jobdesk:");
            for (int i = 0; i < jobdeskList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {jobdeskList[i]}");
            }
        }

        public void HapusJobdesk()
        {
            TampilkanJobdesk();
            Console.Write("Masukkan nomor jobdesk yang ingin dihapus: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index--;
                if (index >= 0 && index < jobdeskList.Count)
                {
                    Console.WriteLine($"Jobdesk \"{jobdeskList[index]}\" dihapus.");
                    jobdeskList.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("Nomor tidak valid.");
                }
            }
            else
            {
                Console.WriteLine("Input bukan angka.");
            }
        }

        public void BerikanJobdeskKeKaryawan()
        {
            Console.WriteLine("Pilih Karyawan:");
            for (int i = 0; i < daftarKaryawan.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {daftarKaryawan[i].Nama_Karyawan}");
            }

            Console.Write("Nomor karyawan: ");
            if (int.TryParse(Console.ReadLine(), out int karyawanIndex))
            {
                karyawanIndex--;
                if (karyawanIndex >= 0 && karyawanIndex < daftarKaryawan.Count)
                {
                    TampilkanJobdesk();
                    Console.Write("Nomor jobdesk yang ingin diberikan: ");
                    if (int.TryParse(Console.ReadLine(), out int jobdeskIndex))
                    {
                        jobdeskIndex--;
                        if (jobdeskIndex >= 0 && jobdeskIndex < jobdeskList.Count)
                        {
                            daftarKaryawan[karyawanIndex].Jobdesks.Add(jobdeskList[jobdeskIndex]);
                            Console.WriteLine("Jobdesk diberikan!");
                        }
                        else
                        {
                            Console.WriteLine("Nomor jobdesk tidak valid.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Nomor karyawan tidak valid.");
                }
            }
        }

        public void TampilkanJobdeskKaryawan()
        {
            Console.WriteLine("Pilih Karyawan:");
            for (int i = 0; i < daftarKaryawan.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {daftarKaryawan[i].Nama_Karyawan}");
            }

            Console.Write("Nomor karyawan: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                index--;
                if (index >= 0 && index < daftarKaryawan.Count)
                {
                    var karyawan = daftarKaryawan[index];
                    Console.WriteLine($"Jobdesk {karyawan.Nama_Karyawan}:");
                    if (karyawan.Jobdesks.Count == 0)
                    {
                        Console.WriteLine("Belum ada jobdesk.");
                    }
                    else
                    {
                        for (int i = 0; i < karyawan.Jobdesks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {karyawan.Jobdesks[i]}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Nomor karyawan tidak valid.");
                }
            }
            else
            {
                Console.WriteLine("Input bukan angka.");
            }
        }
    }
}
