using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AplikasiAbsensi.Core.Models
{
    public class Karyawan
    {
        [JsonPropertyName("Id_Karyawan")]
        public int Id_Karyawan { get; set; }

        [JsonPropertyName("Nama_Karyawan")]
        public string Nama_Karyawan { get; set; }

        [JsonPropertyName("Email_Karyawan")]
        public string Email_Karyawan { get; set; }

        [JsonPropertyName("Phone_Karyawan")]
        public string Phone_Karyawan { get; set; }

        [JsonPropertyName("Role")]
        public int Role { get; set; }

        [JsonPropertyName("Status")]
        public int Status { get; set; }

        [JsonPropertyName("Gaji")]
        public int Gaji { get; set; }

        [JsonPropertyName("Jobdesks")]
        public List<string> Jobdesks { get; set; }

        [JsonPropertyName("CheckInTime")]
        public DateTime? CheckInTime { get; set; }

        [JsonPropertyName("CheckOutTime")]
        public DateTime? CheckOutTime { get; set; }

        [JsonPropertyName("Waktu")]
        public DateTime Waktu { get; set; }

        [JsonPropertyName("Tipe")]
        public string Tipe { get; set; }

        public Karyawan() { }

        public Karyawan(int id_Karyawan, string nama_Karyawan, string email_Karyawan, string phone_Karyawan, int role, int status, int gaji)
        {
            Id_Karyawan = id_Karyawan;
            Nama_Karyawan = nama_Karyawan;
            Email_Karyawan = email_Karyawan;
            Phone_Karyawan = phone_Karyawan;
            Role = role;
            Status = status;
            Gaji = gaji;
            Jobdesks = new List<string>();
            CheckInTime = null;
            CheckOutTime = null;
            Waktu = DateTime.Now;
            Tipe = null;
        }

        public override string ToString()
        {
            return $"{Nama_Karyawan} ({Id_Karyawan}) - {Role}";
        }

        public class RecordPresensi
        {
            // "Check-in" atau "Check-out"
        }
    }
}