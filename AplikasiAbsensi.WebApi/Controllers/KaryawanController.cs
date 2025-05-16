using AplikasiAbsensi.Core.Models;
using AplikasiAbsensi.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Aplikasi_Absensi_Perusahaan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KaryawanController : ControllerBase
    {
        private readonly KaryawanService _karyawanService;

        public KaryawanController()
        {
            _karyawanService = new KaryawanService();

            // Opsional: menambahkan jobdesk dummy (jika tidak disimpan ke DB)
            _karyawanService.GetSampleKaryawan()[0].Jobdesks.Add("Membuat laporan mingguan");
            _karyawanService.GetSampleKaryawan()[1].Jobdesks.Add("Maintenance sistem absensi");
        }

        // GET: api/karyawan
        [HttpGet]
        public ActionResult<List<Karyawan>> GetAll()
        {
            return Ok(_karyawanService.GetSampleKaryawan());
        }

        // GET: api/karyawan/jobdesk/1
        [HttpGet("jobdesk/{id}")]
        public IActionResult GetJobdesk(int id)
        {
            var karyawan = _karyawanService.GetSampleKaryawan().FirstOrDefault(k => k.Id_Karyawan == id);

            if (karyawan == null)
            {
                return NotFound(new { message = "Karyawan tidak ditemukan." });
            }

            return Ok(new
            {
                Nama = karyawan.Nama_Karyawan,
                Jobdesks = karyawan.Jobdesks.Count > 0 ? karyawan.Jobdesks : new List<string> { "Belum ada jobdesk." }
            });
        }

        [HttpGet("log")]
        public IActionResult GetLog()
        {
            var logs = LogPerubahanHelper.LoadLog();
            return Ok(logs);
        }
    }
}
