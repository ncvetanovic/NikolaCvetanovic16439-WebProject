using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApotekaController : ControllerBase
    {

        public ApotekaContext Context { get; set; }

        public ApotekaController(ApotekaContext context)
        {
            Context = context;
        }

        [Route("KreirajApoteku")]
        [HttpPost]
        public async Task KreirajApoteku([FromBody] Apoteka apoteka)
        {
            Context.Apoteke.Add(apoteka);
            await Context.SaveChangesAsync();
        }

        [Route("KreirajLek")]
        [HttpPost]

        public async Task KreirajLek([FromBody] Lek lek)
        {
            Context.Lekovi.Add(lek);
            await Context.SaveChangesAsync();
        }
        [Route("IzbrisiLek/{id}")]
        [HttpDelete]
        public async Task ObrisiLek(int id){
         var lek = await Context.Lekovi.FindAsync(id);
         Context.Remove(lek);
         await Context.SaveChangesAsync();
        }

        [Route("IzmeniLek")]
        [HttpPut]
        public async Task IzmeniLek([FromBody] Lek lek){
         Context.Update<Lek>(lek);
         await Context.SaveChangesAsync();
        }
        [Route("PreuzmiSveLekove")]
        [HttpGet]
        public async Task<List<Lek>> PreuzmiSveLekove(){
        await Context.SaveChangesAsync();
        return  Context.Lekovi.AsEnumerable().Select(lek => lek).ToList();
        //  IEnumerable<Lek> query =
        //     from lek in Context.Lekovi.AsEnumerable()
        //     select lek;


        //     Console.WriteLine("Product Names:");
        //     foreach (Lek p in asd)
        //     {
        //         Console.WriteLine(p.Opis);
        //     }

        }


        


   
    }
}
