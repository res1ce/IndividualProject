using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IndidProject.models
{
    [Table("Drugs")]
    public class Drug
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int number { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int groupId { get; set; }
        [Required]
        public int dosage { get; set; }
        [Required]
        public int expiration_days { get; set; }
        [NotMapped]
        public string groupName { get; set; }
    }
}
