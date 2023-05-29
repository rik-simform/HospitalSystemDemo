using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public class patient
    {
        [Key]
        public int patientId { get; set; }
        [Required]
        public string patientName { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public string  gender { get; set; }
        [Required]
        public string bloodGroup { get; set; }
        [Required]
        public string contactNo { get; set; }
        [Required]
        public string emergencyContact { get; set; }
    }
}
