using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Backend.Models
{
    [Table("Lek")]
    public class Lek
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Opis")]
        [MaxLength(255)]
        public string Opis { get; set; }

        [Column("Cena")]
        public double Cena { get; set; }

        [JsonIgnore]
        public Recept Recept { get; set; }

        [JsonIgnore]
        public Apoteka Apoteka { get; set; }
    }
}
