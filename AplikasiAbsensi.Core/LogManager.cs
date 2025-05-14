using System;
using System.Collections.Generic;

namespace AplikasiAbsensi.Core
{
    public class LogManager<T> where T : notnull  // Menambahkan constraint 'notnull'
    {
        private Dictionary<T, DateTime> checkInLog = new Dictionary<T, DateTime>();
        private Dictionary<T, DateTime> checkOutLog = new Dictionary<T, DateTime>();

        public void CheckIn(T karyawan)
        {
            if (!checkInLog.ContainsKey(karyawan))
            {
                checkInLog[karyawan] = DateTime.Now;
                Console.WriteLine("Check-in berhasil pada " + checkInLog[karyawan]);
            }
            else
            {
                Console.WriteLine("Karyawan sudah check-in!");
            }
        }

        public void CheckOut(T karyawan)
        {
            if (checkInLog.ContainsKey(karyawan) && !checkOutLog.ContainsKey(karyawan))
            {
                checkOutLog[karyawan] = DateTime.Now;
                Console.WriteLine("Check-out berhasil pada " + checkOutLog[karyawan]);
            }
            else if (!checkInLog.ContainsKey(karyawan))
            {
                Console.WriteLine("Karyawan belum check-in!");
            }
            else
            {
                Console.WriteLine("Karyawan sudah check-out!");
            }
        }

        public void ShowLogs()
        {
            Console.WriteLine("\n Log Kehadiran:");
            foreach (var entry in checkInLog)
            {
                string checkoutTime = checkOutLog.ContainsKey(entry.Key) ? checkOutLog[entry.Key].ToString() : "Belum Check-Out";
                Console.WriteLine($"{entry.Key}: IN: {entry.Value}, OUT: {checkoutTime}");
            }
        }
    }
}
