using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    [Table("Recept")]
    public class Recept
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("DatumOd")]
        public DateTime DatumOd { get; set; }

        [Column("DatumDo")]
        public DateTime DatumDo { get; set; }
       
        [JsonIgnore]
        public Apoteka Apoteka { get; set; }
        public virtual  List<Lek> Lekovi { get; set; }
    }
}
