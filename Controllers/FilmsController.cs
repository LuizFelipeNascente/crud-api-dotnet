using Films.Api.Data;
using Films.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Films.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
       private readonly FilmsContext _context;

       public FilmsController(FilmsContext context)
       {
            _context = context;
       }

       [HttpGet]
       [Route("/films")]
       [Authorize]
       public async Task<ActionResult> GetFilms() 
       {
            return Ok(await _context.Films.ToArrayAsync());
       }

       [HttpPost]
       [Route("/films")]
       [Authorize]
       public async Task<ActionResult> CreateFilm(Film film)
       {
            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();

            return Ok(film);
       }

       [HttpPut]
       [Route("/films")]
       [Authorize]
       public async Task<ActionResult> UpdateFilm(Film film)
       {
            var dbFilm = await _context.Films.FindAsync(film.Id);

            if (dbFilm == null)
                return NotFound();

            dbFilm.Name = film.Name;
            dbFilm.Director = film.Director;
            dbFilm.Producer = film.Producer;
            dbFilm.ReleaseYear = film.ReleaseYear;
            dbFilm.Category = film.Category;

            await _context.SaveChangesAsync();

            return Ok(film);
             
       }

       [HttpDelete]
       [Route("/films")]
       [Authorize]
       public async Task<ActionResult> DeleteFilm(Guid Id)
       {
            var dbFilm = await _context.Films.FindAsync(Id);

            if (dbFilm == null) 
            {
                return NotFound();
            }    

            _context.Films.Remove(dbFilm);

            await _context.SaveChangesAsync();

            return NoContent();
             
       }
    }
}