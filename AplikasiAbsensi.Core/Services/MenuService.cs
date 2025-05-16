using AplikasiAbsensi.Core.Models;
using System;
using System.Collections.Generic;



namespace AplikasiAbsensi.Core.Services
{
    public class Menu
    {
        private List<Karyawan> daftarKaryawan = new();
        private LogManager<Karyawan> logManager = new();
        JobdeskService jobdeskService = new JobdeskService();



        public void TampilkanMenu()
        {
            

                bool lanjut = true;

            var kelola = new MengelolaKaryawan<Karyawan>();
            var karyawanService = new KaryawanService(); // ✅ ditambahkan
            var penggajihan = new Penggajihan(karyawanService); // ✅ gunakan konstruktor baru

            while (lanjut)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEM JOBDESK KARYAWAN ===");
                Console.WriteLine("1. Melihat Jobdesk (via API)");
                Console.WriteLine("2. Melakukan Presensi");
                Console.WriteLine("3. Mengelola Jobdesk Karyawan");
                Console.WriteLine("4. Mengelola Data Karyawan");
                Console.WriteLine("5. Mengelola Penggajihan");
                Console.WriteLine("6. Keluar");
                Console.Write("Pilihan Anda: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        //LihatJobdeskViaApi(); 
                        break;
                    case "2":
                        KaryawanService service = new KaryawanService();
                        daftarKaryawan = service.GetSampleKaryawan();
                        PresensiService presensi = new PresensiService(daftarKaryawan);
                        presensi.PilihMenuPresensi();
                        break;
                    case "3":
                        jobdeskService.TampilkanMenuJobdesk(daftarKaryawan);
                        break;
                    case "4":
                        kelola.TampilkanMenukaryawan(); // ← hanya dipanggil jika user pilih opsi 4
                        break;
                    case "5":
                        penggajihan.TampilkanMenuUtama();
                        break;
                    case "6":
                        lanjut = false;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }

                if (lanjut)
                {
                    Console.WriteLine("\nTekan ENTER untuk kembali ke menu...");
                    Console.ReadLine();
                }
            }
        }
    }
}