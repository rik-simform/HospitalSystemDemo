using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public class medicalHistory
    {
        [Key]
        public int medicalId { get; set; }
        [Required]
        public string disease { get; set; }
        [Required]
        public string diseaseDescription { get; set; }
        [Required]
        public int testDetails { get; set; }
        [Display(Name ="patient")]
        public int patientId { get; set; }
        [ForeignKey("patientId")]
        public virtual patient patients { get; set; }
    }
}
