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
            bool keluarAplikasi = false;

            while (!keluarAplikasi)
            {
                Console.Clear();
                var loginService = new LoginService();
                Console.WriteLine("=== Login ===");
                Console.Write("Masukkan Nama: ");
                string nama = Console.ReadLine();
                Console.Write("Masukkan Email: ");
                string email = Console.ReadLine();
                Karyawan userLogin = loginService.Login(nama, email);

                if (userLogin == null)
                {
                    Console.WriteLine("Login gagal. Tekan ENTER untuk coba lagi...");
                    Console.ReadLine();
                    continue;
                }


                bool lanjut = true;
                var kelola = new MengelolaKaryawan<Karyawan>();
                var karyawanService = new KaryawanService();
                var penggajihan = new Penggajihan(karyawanService);
                JobdeskService jobdeskService = new JobdeskService();

                while (lanjut)
                {
                    Console.Clear();
                    Console.WriteLine($"=== MENU UTAMA - {userLogin.Nama_Karyawan} (Role: {userLogin.Role}) ===");
                    Console.WriteLine("1. Melihat Jobdesk (via API)");
                    Console.WriteLine("2. Melakukan Presensi");
                    if (userLogin.Role == 2 || userLogin.Role == 3)
                    {
                        Console.WriteLine("3. Mengelola Jobdesk Karyawan");
                        Console.WriteLine("4. Mengelola Data Karyawan");
                        Console.WriteLine("5. Mengelola Penggajihan");
                    }
                    Console.WriteLine("0. Logout / Kembali ke Login");

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
                            if (userLogin.Role == 2 || userLogin.Role == 3)
                                jobdeskService.TampilkanMenuJobdesk(new List<Karyawan> { userLogin });
                            else
                                LoginService.TampilTidakPunyaAkses();
                            break;
                        case "4":
                            if (userLogin.Role == 2 || userLogin.Role == 3)
                                kelola.TampilkanMenukaryawan();
                            else
                                LoginService.TampilTidakPunyaAkses();
                            break;
                        case "5":
                            if (userLogin.Role == 2 || userLogin.Role == 3)
                                penggajihan.TampilkanMenuUtama();
                            else
                                LoginService.TampilTidakPunyaAkses();
                            break;
                        case "0":
                            Console.WriteLine("Logout berhasil. Kembali ke login...");
                            lanjut = false;
                            break;
                        default:
                            Console.WriteLine("❌ Pilihan tidak valid.");
                            break;
                    }

                    Console.WriteLine("\nTekan ENTER untuk kembali ke menu...");
                    Console.ReadLine();
                }
            }
        }
    }
}