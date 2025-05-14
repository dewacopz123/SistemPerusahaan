using AplikasiAbsensi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiAbsensi.Core.Services
{
    class StaffService
    {
        public List<Staff> GetSampleStaff()
        {
            var daftarStaff = new List<Staff>
            {
                new Staff(1, "Krisna Aditya", "krisna@email.com", "081234567900", 2, 1),
                new Staff(2, "Lina Rahmawati", "lina@email.com", "081234567901", 2, 1),
                new Staff(3, "Maya Suryani", "maya@email.com", "081234567902", 2, 1),
                new Staff(4, "Nino Pratama", "nino@email.com", "081234567903", 2, 1),
                new Staff(5, "Oscar Kurniawan", "oscar@email.com", "081234567904", 2, 1),
                new Staff(6, "Prita Sari", "prita@email.com", "081234567905", 2, 1),
                new Staff(7, "Qori Azmi", "qori@email.com", "081234567906", 2, 1),
                new Staff(8, "Rizki Ramadhan", "rizki@email.com", "081234567907", 2, 1),
                new Staff(9, "Siti Aisyah", "siti@email.com", "081234567908", 2, 1),
                new Staff(10, "Teguh Wijaya", "teguh@email.com", "081234567909", 2, 1)
            };

            return daftarStaff;
        }
    }
}
