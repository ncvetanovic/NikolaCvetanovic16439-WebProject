using System;
using System.Collections.Generic;

namespace Backend.DTOs
{
    public class ReceptDTO
    {
        public int ID { get ; set ; }
        public DateTime DatumOd { get; set; }

        public DateTime DatumDo { get; set; }
       
        public List<LekDTO> Lekovi { get; set; } = new List<LekDTO>();

    }
}
