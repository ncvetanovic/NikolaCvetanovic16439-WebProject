using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("LekUReceptu")]
    public class LekUReceptu
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey(nameof(Recept))]
        public int ReceptID { get; set; }

        public virtual Recept Recept {get; set;}


        [ForeignKey(nameof(Lek))]
        public int LekID { get; set; }
        public virtual Lek Lek {get; set;}


        
    }
}
