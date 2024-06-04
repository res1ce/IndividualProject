using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndidProject.models
{
    [Table("Availabilitys")]
    public class Avaibality
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int number_apteka { get; set; }
        [Required]
        public int number_drug { get; set; }
        [Required]
        public DateTime release_date { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public float cost { get; set; }
    }
}
