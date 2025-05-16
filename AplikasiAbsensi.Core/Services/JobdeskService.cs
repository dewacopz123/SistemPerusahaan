using AplikasiAbsensi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AplikasiAbsensi.Core.Helper;

namespace AplikasiAbsensi.Core.Services
{
    public class JobdeskService
    {
        private List<JobDesk> jobdeskList;
        private List<Karyawan> daftarKaryawan;

        public JobdeskService()
        {
            jobdeskList = JobdeskHelper.LoadJobdesk();
        }

        public void TampilkanMenuJobdesk(List<Karyawan> daftarKaryawan)
        {
            this.daftarKaryawan = daftarKaryawan;
            bool lanjut = true;

            var menuActions = new Dictionary<string, Action>
            {
                { "1", TambahJobdesk },
                { "2", TampilkanJobdesk },
                { "3", HapusJobdesk },
                { "4", BerikanJobdeskKeKaryawan },
                { "5", TampilkanJobdeskKaryawan }
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
            Console.Write("Masukkan nama jobdesk: ");
            string deskripsi = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(deskripsi))
            {
                Console.WriteLine("Nama jobdesk tidak boleh kosong. Jobdesk tidak disimpan.");
                return;
            }

            var jobdesk = new JobDesk
            {
                IdJobdesk = jobdeskList.Count + 1,
                NamaJobdesk = deskripsi,
                TugasUtama = new List<string>()
            };

            Console.Write("Masukkan jumlah tugas utama: ");
            if (int.TryParse(Console.ReadLine(), out int jumlah))
            {
                for (int i = 0; i < jumlah; i++)
                {
                    Console.Write($"Tugas #{i + 1}: ");
                    var tugas = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(tugas))
                        jobdesk.TugasUtama.Add(tugas);
                }
            }

            if (jobdesk.TugasUtama.Count == 0)
            {
                Console.WriteLine("Jobdesk harus memiliki setidaknya satu tugas utama. Jobdesk tidak disimpan.");
                return;
            }

            JobdeskHelper.TambahJobdesk(jobdesk);
            jobdeskList = JobdeskHelper.LoadJobdesk();
            Console.WriteLine("Jobdesk ditambahkan.");
        }


        public void TampilkanJobdesk()
        {
            jobdeskList = JobdeskHelper.LoadJobdesk();

            if (jobdeskList.Count == 0)
            {
                Console.WriteLine("Belum ada jobdesk.");
                return;
            }

            Console.WriteLine("Daftar Jobdesk:");
            foreach (var j in jobdeskList)
            {
                Console.WriteLine($"{j.IdJobdesk}. {j.NamaJobdesk} - Tugas: {string.Join(", ", j.TugasUtama)}");
            }
        }

        public void HapusJobdesk()
        {
            TampilkanJobdesk();
            Console.Write("Masukkan ID jobdesk yang ingin dihapus: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool berhasil = JobdeskHelper.HapusJobdesk(id);
                if (berhasil)
                {
                    Console.WriteLine("Jobdesk dihapus.");
                    jobdeskList = JobdeskHelper.LoadJobdesk();
                }
                else
                {
                    Console.WriteLine("ID jobdesk tidak ditemukan.");
                }
            }
            else
            {
                Console.WriteLine("Input bukan angka.");
            }
        }

        public void BerikanJobdeskKeKaryawan()
        {
            daftarKaryawan = KaryawanHelper.LoadKaryawan().Cast<Karyawan>().ToList(); 

            var daftarKaryawanFiltered = daftarKaryawan.Where(k => k.Role != Role.Manager).ToList();

            if (daftarKaryawanFiltered.Count == 0)
            {
                Console.WriteLine("Tidak ada karyawan tersedia.");
                return;
            }

            Console.WriteLine("Pilih Karyawan:");
            for (int i = 0; i < daftarKaryawanFiltered.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {daftarKaryawanFiltered[i].Nama_Karyawan}");
            }

            Console.Write("Nomor karyawan: ");
            if (int.TryParse(Console.ReadLine(), out int karyawanIndex))
            {
                karyawanIndex--;
                if (karyawanIndex >= 0 && karyawanIndex < daftarKaryawanFiltered.Count)
                {
                    var karyawanDipilih = daftarKaryawanFiltered[karyawanIndex];
                    TampilkanJobdesk();

                    Console.Write("ID jobdesk yang ingin diberikan: ");
                    if (int.TryParse(Console.ReadLine(), out int jobdeskId))
                    {
                        var jobdesk = JobdeskHelper.GetById(jobdeskId);
                        if (jobdesk != null)
                        {
                            karyawanDipilih.Jobdesk = jobdesk;
                            karyawanDipilih.JobdeskId = jobdesk.IdJobdesk;

                            var indexAsli = daftarKaryawan.FindIndex(k => k.Id_Karyawan == karyawanDipilih.Id_Karyawan);
                            if (indexAsli != -1)
                            {
                                daftarKaryawan[indexAsli] = karyawanDipilih;
                                KaryawanHelper.SimpanData(daftarKaryawan);
                                Console.WriteLine("Jobdesk diberikan!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Jobdesk tidak ditemukan.");
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
            daftarKaryawan = KaryawanHelper.LoadKaryawan().Cast<Karyawan>().ToList();
            var daftarKaryawanFiltered = daftarKaryawan.Where(k => k.Role != Role.Manager).ToList();

            Console.WriteLine("Jobdesk Karyawan:");
            int idx = 1;
            foreach (var k in daftarKaryawanFiltered)
            {
                string tugas = k.Jobdesk != null && k.Jobdesk.TugasUtama.Any()
                    ? string.Join(", ", k.Jobdesk.TugasUtama)
                    : "Tidak Ada";
                Console.WriteLine($"{idx}. {k.Nama_Karyawan} - {tugas}");
                idx++;
            }
        }
    }
}