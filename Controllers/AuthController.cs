using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Films.Api.Data;
using Films.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Films.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
       private readonly FilmsContext _context;

       public AuthController(FilmsContext context)
       {
            _context = context;
       }

       [HttpPost]
       [Route("/account")]
       public async Task<IActionResult> CreateAcconunt(User user)
       {
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
           return Ok(new 
           {

                user = new 
                {
                    id = user.Id,
                    name = user.Name,
                    email = user.Email,
                },

                token = GenereteToken()

           });
       }

       [HttpPost]
       [Route("/auth/login")]
       public async Task<IActionResult> Login(Login login)
       {
            var user = await _context.Users.Where(p => p.Email == login.Email && p.Password == login.Password).FirstOrDefaultAsync();

            if(user == null)
                return Unauthorized();
            return Ok(new 
           {

                user = new 
                {
                    id = user.Id,
                    name = user.Name,
                    email = user.Email,
                },

                token = GenereteToken()

           });    
       }

       private static string GenereteToken()
       {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("gfFGTfsdfGFdfRFsfsdhhuy465SDasds55Ssdas");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


       }

    }
}