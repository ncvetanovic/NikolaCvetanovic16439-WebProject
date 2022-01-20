using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
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

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Backend.DTOs.KlijentDTO klijent)
        {
            if(klijent.Username == "")
            {
                return BadRequest(new { message = "Unesite username" });
            }

            if(klijent.Password == "")
            {
                return BadRequest(new { message = "Unesite lozinku" });
            }

            var k = await Context.Klijenti.
                    Where(k => k.Username == klijent.Username && k.Password == klijent.Password).FirstOrDefaultAsync();

            if(k == null)
            {
                return BadRequest(new { message = "Pogresni kredencijalni" });
            }
            else
            {
                return Ok(k);
            }

            
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
        public async Task KreirajLek([FromBody] Lek lek){
            Context.Lekovi.Add(lek);
            await Context.SaveChangesAsync();
        }
        
        [Route("DodajLekURecept/{idRecepta}/{idLeka}")]
        [HttpPost]
        public async Task DodajLekURecept(int idRecepta, int idLeka)
        {
            var recept = await Context.Recepti.FindAsync(idRecepta);
            var lek = await Context.Lekovi.FindAsync(idLeka);
            await Context.SaveChangesAsync();
        }

        [Route("IzbrisiLek/{id}")]
        [HttpDelete]
        public async Task<IActionResult> ObrisiLek(int id){
          try
         {
             var lek = await Context.Lekovi.FindAsync(id);
            if(lek != null)
            {
             Context.Remove(lek);
             await Context.SaveChangesAsync();
             return Ok(lek);
            }
            else
            {
                  return BadRequest(new {message = "Ne postoji lek"});
            }
         }
         catch(Exception e)
         {
               return BadRequest(e.Message);
         }
        }


        [Route("IzmeniLek")]
        [HttpPut]
        public async Task<IActionResult> IzmeniLek([FromBody] Lek lek){

         try
            {
                 var l = await Context.Lekovi.FindAsync(lek.ID);
                if(l != null)
                {
                    l.Cena = lek.Cena;
                    l.Opis = lek.Opis;
                    await  Context.SaveChangesAsync();
                    return Ok(l);

                }
                else
                {
                    return BadRequest(new {message = "Ne postoji recept"});
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }



        }


        [Route("DodajLekUApoteku/{idApoteke}/{idLeka}")]
        [HttpPost]
        public async Task DodajLekUApoteku(int idApoteke, int idLeka)
        {
            var apoteka = await Context.Apoteke.FindAsync(idApoteke);
            var lek = await Context.Lekovi.FindAsync(idLeka);
            
            lek.Apoteka = apoteka;

            await Context.SaveChangesAsync();
        }
        [Route("PreuzmiSveLekove")]
        [HttpGet]
        public async Task<List<Lek>> PreuzmiSveLekove(){
            return await Context.Lekovi.Include(l => l.Apoteka).ToListAsync();
        }

        [Route("IzbrisiApoteku/{id}")]
        [HttpDelete]
        public async Task<IActionResult> IzbrisiApoteku(int id){
        try
         {
             var apoteka = await Context.Apoteke.FindAsync(id);
            if(apoteka != null)
            {
            var lekovi = await Context.Lekovi.Where(lek => lek.Apoteka.ID == id).ToListAsync();

               if (lekovi != null)
                lekovi.ForEach(lek =>{
                    lek.Apoteka = null;
                    Context.Update(lek);
                    });

                 Context.Remove(apoteka);
                 await Context.SaveChangesAsync();
                return Ok(apoteka);
                }
                else
                {
                  return BadRequest(new {message = "Ne postoji apoteka"});
                }
         }  
         catch(Exception e)
         {
               return BadRequest(e.Message);
         }
        }
  
        [Route("PreuzmiSveApoteke")]
        [HttpGet]
        public async Task<List<Apoteka>> PreuzmiSveApoteke(){
            return await Context.Apoteke.Include(p => p.Lekovi).ToListAsync();
        }


        [Route("KreirajRecept/{idKlijenta}")]
        [HttpPost]
        public async Task<IActionResult> KreirajRecept([FromBody] Backend.DTOs.ReceptDTO recept,int idKlijenta)
        {
            var k = await Context.Klijenti.FindAsync(idKlijenta);

               
          

            if(recept.DatumOd >= recept.DatumDo)
            {
                return BadRequest(new { message = "Lose unet vremenski period. Datum isteka mora biti veci od datuma pocetka!" });
            }

            if(recept.Lekovi.Count == 0)
            {
                return BadRequest(new { message = "Izaberite lekove" });

            }
         
            var r = new Recept()
            {
                DatumOd = recept.DatumOd,
                DatumDo = recept.DatumDo, 
                Klijent = k,
            };

            Context.Recepti.Add(r);

            foreach(var lek in recept.Lekovi)
            {
                Context.LekoviUReceptu.Add(new LekUReceptu()
                {
                    LekID = lek.ID,
                    Recept = r
                });
            }

            try
            {
                await Context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
            recept.ID = r.ID;

            return  Ok(recept);
        }

        [Route("KreirajKlijenta")]
        [HttpPost]
        public async Task<IActionResult> KreirajKlijenta([FromBody]  Backend.DTOs.KlijentDTO klijent)
        {
             if(klijent.Username == "")
            {
                return BadRequest(new { message = "Unesite username!" });
            }

            if(klijent.Password == "")
            {
                return BadRequest(new { message = "Unesite lozinku!" });
            }

            if(klijent.Email == "")
            {
                return BadRequest(new { message = "Unesite email adresu!" });
            }

            var klijentSaPostojecimMailom = await Context.Klijenti.
                    Where(k => k.Email == klijent.Email).FirstOrDefaultAsync();

            if(klijentSaPostojecimMailom != null)
            {
                return BadRequest(new { message = "Email adresa " + klijent.Email + " vec postoji, molimo Vas unesti drugu email adresu"   });
            }
            else
            {
                var k = new Klijent()
                {
                    Username = klijent.Username,
                    Password = klijent.Password,
                    Email = klijent.Email
                };

                Context.Klijenti.Add(k);
                await Context.SaveChangesAsync();

                return Ok(k);
            }

        
        }

        [Route("PreuzmiKlijenta/{id}")]
        [HttpGet]
        public async Task<IActionResult> PreuzmiKLijenta(int id)
        {
            var result = await Context.Klijenti
            .Where(k => k.ID == id)
            .Select(k => new Backend.DTOs.KlijentDTO()
            {
                ID = k.ID,
                Email = k.Email,
                Username = k.Username,
                Password = k.Password,
                Recepti =  k.Recepti.Select(r => new Backend.DTOs.ReceptDTO()
                {
                    ID = r.ID,
                    DatumOd = r.DatumOd,
                    DatumDo = r.DatumDo,
                    Lekovi = r.Lekovi.Select(l => new Backend.DTOs.LekDTO()
                    {
                        ID = l.Lek.ID,
                        Cena = l.Lek.Cena,
                        Opis = l.Lek.Opis

                    }).ToList()
                }).ToList()
            }).FirstOrDefaultAsync();

            return Ok(result);
            
        }

        [Route("PreuzmiSveRecepte")]
        [HttpGet]
        public async Task<List<Recept>> PreuzmiSveRecepte(){
            return await Context.Recepti.Include(p => p.Lekovi).ToListAsync();
        }

        [Route("IzbrisiRecept/{id}")]
        [HttpDelete]
        public async  Task<IActionResult>  IzbrisiRecept(int id){
         
         try
         {
             var recept = await Context.Recepti.FindAsync(id);
            if(recept != null)
            {
             Context.Remove(recept);
             await Context.SaveChangesAsync();
             return Ok(recept);
            }
            else
            {
                  return BadRequest(new {message = "Ne postoji recept"});
            }
         }
         catch(Exception e)
         {
               return BadRequest(e.Message);
         }
        }

        [Route("IzmeniRecept")]
        [HttpPut]
        public async  Task<IActionResult> IzmeniRecept([FromBody] Recept recept){
            try
            {
                 var r = await Context.Recepti.FindAsync(recept.ID);
                if(r != null)
                {
                    r.DatumDo = r.DatumDo;
                    await  Context.SaveChangesAsync();
                    return Ok(r);

                }
                else
                {
                    return BadRequest(new {message = "Ne postoji recept"});
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }


          
        }

        [Route("PronadjiApoteku/{idRecepta}")]
        [HttpGet]
        public async Task<IActionResult> PronadjiApoteku(  int idRecepta)
        {
           var apoteke = await Context.Apoteke.Include(p => p.Lekovi).ToListAsync();

            var recept = await Context.Recepti
            .Where(r => r.ID == idRecepta)
            .Select(r => new Backend.DTOs.ReceptDTO()
            {
                ID = r.ID,
                DatumDo = r.DatumDo,
                DatumOd = r.DatumOd,
                Lekovi = r.Lekovi.Select(l => new Backend.DTOs.LekDTO()
                {
                    ID = l.Lek.ID,
                    Cena = l.Lek.Cena,
                    Opis = l.Lek.Opis
                }).ToList()
            }).FirstOrDefaultAsync();


            Apoteka apotekaKojaSadrziSveLekove = new Apoteka();
           
            foreach(var apoteka in apoteke)
            {
                bool sadrziSveLekove = true;
                foreach(var lek in recept.Lekovi)
                {
                    bool lekPostojiUApoteci = false;
                    foreach(var lekUApoteci in apoteka.Lekovi)
                    {
                        if( lek.ID == lekUApoteci.ID)
                        {
                            lekPostojiUApoteci = true;
                            break;
                        }
                    }
                    if(!lekPostojiUApoteci)
                    {
                        sadrziSveLekove = false;
                        break;
                    }
                }
                if(sadrziSveLekove)
                {
                   apotekaKojaSadrziSveLekove = apoteka;
                    break;
                }
            }

         if(apotekaKojaSadrziSveLekove.ID !=0)
         {   
            return Ok(apotekaKojaSadrziSveLekove);
         }
         else
         {
             return BadRequest(new {message = "Nazalost apoteka sa datim skupom lekova ne postoji."});
         }        
        }


    }
}
