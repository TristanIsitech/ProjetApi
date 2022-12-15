using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProjetApi.Context;
using ProjetApi.Entities;

namespace ProjetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PokemonController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // Get All Pokemon
        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetAllPokemon()
        {
            return Ok(await _context.Pokemons.ToListAsync());
        }

        // Add Pokemon
        [HttpPost]
        public async Task<ActionResult<string>> AddPokemon([FromBody] Pokemon pokemon)
        {
            if(pokemon == null){
                return BadRequest();
            }
            await _context.Pokemons.AddAsync(pokemon);
            await _context.SaveChangesAsync();
            return Ok("Pokemon créer !");
        }

        // Get a pokemon with is name
        [HttpGet("{PokemonName}")]
        public async Task<ActionResult<Pokemon>> GetPokemonByName(string PokemonName)
        {
            try
            {
                return Ok(await _context.Pokemons.Where(p => p.name == PokemonName).ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // For delete a pokemon
        [HttpDelete("DeleteByName")]
        public async Task<ActionResult<Pokemon>> DeleteByName([FromBody]string PokemonName)
        {
            try
            {
                Pokemon? pokemon = _context.Pokemons.FirstOrDefault(p => p.name == PokemonName);
                if (pokemon == null) return Ok("Pokemon introuvable !!");
                _context.Pokemons.Remove(pokemon);
                await _context.SaveChangesAsync();
                return Ok("Le Pokemon a bien été supprimé");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // [HttpDelete("DeleteAll")]
        // public async Task<ActionResult<Pokemon>> DeleteAll()
        // {
        //     try
        //     {
        //         if (pokemon == null) return Ok("Pokemon introuvable !!");
        //         _context.Pokemons.Remove(pokemon);
        //         await _context.SaveChangesAsync();
        //         return Ok("Le Pokemon a bien été supprimé");
        //     }
        //     catch (Exception)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError,
        //             "Error retrieving data from the database");
        //     }
        // }
    }
}