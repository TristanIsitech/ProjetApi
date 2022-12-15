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
            // On ajout le pokemon
            await _context.Pokemons.AddAsync(pokemon);

            // NE MARCHE PAS !! 
            // ======
            // On prend la nouvelle list de pokemon de ce type 
            List<Pokemon> pokemons = await _context.Pokemons.Where(p => p.element_name == pokemon.element_name).ToListAsync();
            // On ajout la list de pokemon au type en question
            _context.Elements.Update(new Element(pokemon.element_name,pokemons));
            // ======

            // On effectue les changements 
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

        // For update a Pokemon
        // NE MARCHE PAS !! 
        [HttpPatch]
        public async Task<ActionResult<Pokemon>> ModifierPokemon([FromBody]Pokemon newPokemon)
        {
            // _context.Pokemons.AsNoTracking();
            Pokemon? oldPokemon = _context.Pokemons.FirstOrDefault(p => p.numero == newPokemon.numero);

            if (oldPokemon == null) return BadRequest("Erreur dans l'ajout");
            _context.Pokemons.Update(newPokemon);
            await _context.SaveChangesAsync();
            return Ok("Le pokemon a bien été modifié");
        }

        // For delete a pokemon
        [HttpDelete("DeleteByNumero")]
        public async Task<ActionResult<Pokemon>> DeleteByNumero([FromBody]int PokemonNumber)
        {
            try
            {
                Pokemon? pokemon = _context.Pokemons.FirstOrDefault(p => p.numero == PokemonNumber);
                if (pokemon == null) return BadRequest("Pokemon introuvable !!");
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

        [HttpDelete("DeleteAll")]
        public async Task<ActionResult<Pokemon>> DeleteAll()
        {
            try
            {
                List<Pokemon> pokemons = await _context.Pokemons.ToListAsync();

                foreach(Pokemon pokemon in pokemons){
                    _context.Pokemons.Remove(pokemon);
                }
                await _context.SaveChangesAsync();
                return Ok("Les Pokemons ont bien été supprimés");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}