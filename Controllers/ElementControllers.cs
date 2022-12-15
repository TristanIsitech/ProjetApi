using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProjetApi.Context;
using ProjetApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ElementController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // Get All Element
        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetAllElement()
        {
            return Ok(await _context.Pokemons.ToListAsync());
        }
    }
}