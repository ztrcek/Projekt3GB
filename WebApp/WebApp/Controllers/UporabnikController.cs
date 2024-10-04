using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UporabnikController : ControllerBase
    {
        private readonly PodatkiDb _context;

        public UporabnikController(PodatkiDb context)
        {
            _context = context;
        }

        // GET: api/Uporabnik
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Uporabnik>>> GetUporabniki()
        {
            return await _context.Uporabniki.ToListAsync();
        }

        // GET: api/Uporabnik/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Uporabnik>> GetUporabnik(int id)
        {
            var uporabnik = await _context.Uporabniki.FindAsync(id);

            if (uporabnik == null)
            {
                return NotFound();
            }

            return uporabnik;
        }

        // PUT: api/Uporabnik/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{ime}")]
        public async Task<IActionResult> PutUporabnik(string ime, Uporabnik uporabnik)
        {
            var u = _context.Uporabniki.Where(a => a.ime == ime).FirstOrDefault();
            if (u==null)
            {
                return BadRequest();
            }
            if (u.geslo == uporabnik.geslo)
                return NoContent();
            else
                return BadRequest();

           
        }

        // POST: api/Uporabnik
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUporabnik(Uporabnik uporabnik, UserService userservice)
        {
            var obstaja = await _context.Uporabniki.Where(e => e.Ime == uporabnik.Ime).FirstOrDefaultAsync(); //preverimo, da se uporabnik ne podvaja
            if (obstaja != null)
            {
                return BadRequest("Uporabniško ime mora biti edinstveno");
            }
            var user = new Uporabnik
            {
                Ime = uporabnik.Ime,
                KodiranoGeslo = userservice.HashPassword(uporabnik.KodiranoGeslo),
                JeAktiven = uporabnik.JeAktiven
            };
            userservice.CreateUser(user);
            return Ok();
        }


        // DELETE: api/Uporabnik/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUporabnik(int id)
        {
            var uporabnik = await _context.Uporabniki.FindAsync(id);
            if (uporabnik == null)
            {
                return NotFound();
            }

            _context.Uporabniki.Remove(uporabnik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UporabnikExists(int id)
        {
            return _context.Uporabniki.Any(e => e.id == id);
        }
    }
}
