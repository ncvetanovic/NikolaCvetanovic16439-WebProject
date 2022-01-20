using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Apoteka")]
    public class Apoteka
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
    
        [Column("Naziv")]
        [MaxLength(255)]
        public string Naziv { get; set; }

        [Column("Adresa")]
        [MaxLength(255)]
        public string Adresa { get; set; }

        [Column("Opis")]
        public string Opis { get; set;}

        public virtual List<Lek> Lekovi { get; set; }

        
    }
}
