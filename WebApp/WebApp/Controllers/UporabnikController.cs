using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

         //GET: api/Uporabnik/5
        //[Authorize]
        //[HttpGet]
        //public async Task<IResult> GetUporabniki()
        //{ 
        //    return results.ok("Avtoriziran dostop");
        //}


        // PUT: api/Uporabnik/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUporabnik(Uporabnik uporabnik, UserService userService)
        {

            var user = userService.GetUserByUsername(uporabnik.ime);
            if (user != null && userService.VerifyPassword(uporabnik.geslo, user.geslo))
            {
                var token = userService.GenerateJwtToken(user);
                return Ok(token);
            }

            return BadRequest("Ne morem ustvariti žetona");
        }

        // POST: api/Uporabnik
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUporabnik(Uporabnik uporabnik, UserService userservice)
        {
            var obstaja = await _context.Uporabniki.Where(e => e.ime == uporabnik.ime).FirstOrDefaultAsync(); //preverimo, da se uporabnik ne podvaja
            if (obstaja != null)
            {
                return BadRequest("Uporabniško ime mora biti edinstveno");
            }
            var user = new Uporabnik
            {
                ime = uporabnik.ime,
                geslo = userservice.HashPassword(uporabnik.geslo),
                jeActive = uporabnik.jeActive,
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
