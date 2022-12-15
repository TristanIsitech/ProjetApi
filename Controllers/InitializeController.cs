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
    public class InitializeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InitializeController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // Fonction pour l'initialisation de la base de donnée, création des éléments et leurs faiblesse/résistance
        // Ne marche pas
        // [HttpPost]
        // public async Task<IActionResult> InitializeDataBase()
        // {
        //     await _context.Elements.Add();
        //     return Ok("Cette collection à bien été supprimé");
        // }
    }
}