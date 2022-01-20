using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Klijent")]
    public class Klijent
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
    
        [Column("Username")]
        // [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Column("Password")]
        // [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        [Column("Email")]
        // [Required]
        [MaxLength(64)]
        public string Email { get; set; }

        public virtual List<Recept> Recepti { get; set; }

        
    }
}
