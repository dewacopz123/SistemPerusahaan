using AplikasiAbsensi.Core.Models;
using System;
using System.Collections.Generic;



namespace AplikasiAbsensi.Core.Services
{
    public class MenuService
    {
        private List<Karyawan> daftarKaryawan = new();
        private LogManager<Karyawan> logManager = new();
        JobdeskService jobdeskService = new JobdeskService();

        public void TampilkanMenu()
        {
            var loginService = new LoginService();
            Karyawan userLogin = loginService.Login(); // ← hanya ambil dari file JSON

            bool lanjut = true;
            var kelola = new MengelolaKaryawan<Karyawan>();
            var penggajihan = new Penggajihan(null); // tidak pakai KaryawanService
            JobdeskService jobdeskService = new JobdeskService();

            while (lanjut)
            {
                Console.Clear();
                Console.WriteLine($"=== MENU UTAMA - {userLogin.Nama_Karyawan} (Role: {userLogin.Role}) ===");
                Console.WriteLine("1. Melihat Jobdesk (via API)");
                Console.WriteLine("2. Melakukan Presensi");
                if (userLogin.Role == 2)
                {
                    Console.WriteLine("3. Mengelola Jobdesk Karyawan");
                    Console.WriteLine("4. Mengelola Data Karyawan");
                    Console.WriteLine("5. Mengelola Penggajihan");
                }

                Console.Write("Pilihan Anda: ");
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        // LihatJobdeskViaApi();
                        break;
                    case "2":
                        PresensiService presensi = new PresensiService(new List<Karyawan> { userLogin });
                        presensi.PilihMenuPresensi();
                        break;
                    case "3":
                        if (userLogin.Role == 2)
                            jobdeskService.TampilkanMenuJobdesk(new List<Karyawan> { userLogin });
                        else
                            LoginService.TampilTidakPunyaAkses();
                        break;
                    case "4":
                        if (userLogin.Role == 2)
                            kelola.TampilkanMenukaryawan();
                        else
                            LoginService.TampilTidakPunyaAkses();
                        break;
                    case "5":
                        if (userLogin.Role == 2)
                            penggajihan.TampilkanMenuUtama();
                        else
                            LoginService.TampilTidakPunyaAkses();
                        break;
                    default:
                        Console.WriteLine("❌ Pilihan tidak valid.");
                        break;
                }

                Console.WriteLine("\nTekan ENTER untuk kembali ke menu...");
                Console.ReadLine();
            }
        }



        //public void TampilkanMenu()
        //{
        //    var karyawanService = new KaryawanService();
        //    daftarKaryawan = karyawanService.GetSampleKaryawan();

        //    var loginService = new LoginService(daftarKaryawan);
        //    Karyawan karyawanLogin = loginService.Login();

        //    if (karyawanLogin == null)
        //    {
        //        Console.WriteLine("\nTekan ENTER untuk keluar...");
        //        Console.ReadLine();
        //        return;
        //    }

        //    bool lanjut = true;

        //    var kelola = new MengelolaKaryawan<Karyawan>();
        //    var KaryawanService = new KaryawanService(); // ✅ ditambahkan
        //    var penggajihan = new Penggajihan(karyawanService); // ✅ gunakan konstruktor baru

        //    while (lanjut)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("=== SISTEM JOBDESK KARYAWAN ===");
        //        Console.WriteLine("1. Melihat Jobdesk (via API)");
        //        Console.WriteLine("2. Melakukan Presensi");
        //        Console.WriteLine("3. Mengelola Jobdesk Karyawan");
        //        Console.WriteLine("4. Mengelola Data Karyawan");
        //        Console.WriteLine("5. Mengelola Penggajihan");
        //        Console.WriteLine("6. Keluar");
        //        Console.Write("Pilihan Anda: ");
        //        string input = Console.ReadLine();

        //        switch (input)
        //        {
        //            case "1":
        //                //LihatJobdeskViaApi(); 
        //                break;
        //            case "2":
        //                KaryawanService service = new KaryawanService();
        //                daftarKaryawan = service.GetSampleKaryawan();
        //                PresensiService presensi = new PresensiService(daftarKaryawan);
        //                presensi.PilihMenuPresensi();
        //                break;
        //            case "3":
        //                jobdeskService.TampilkanMenuJobdesk(daftarKaryawan);
        //                break;
        //            case "4":
        //                kelola.TampilkanMenukaryawan(); // ← hanya dipanggil jika user pilih opsi 4
        //                break;
        //            case "5":
        //                penggajihan.TampilkanMenuUtama();
        //                break;
        //            case "6":
        //                lanjut = false;
        //                break;
        //            default:
        //                Console.WriteLine("Pilihan tidak valid.");
        //                break;
        //        }

        //        if (lanjut)
        //        {
        //            Console.WriteLine("\nTekan ENTER untuk kembali ke menu...");
        //            Console.ReadLine();
        //        }
        //    }
        //}
    }
}