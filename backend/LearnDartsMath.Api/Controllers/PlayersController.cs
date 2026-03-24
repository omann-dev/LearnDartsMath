using LearnDartsMath.Api.Data;
using LearnDartsMath.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnDartsMath.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly AppDbContext _context;

    public PlayersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> GetAll()
    {
        var players = await _context.Players.ToListAsync();
        return Ok(players);
    }

    [HttpPost]
    public async Task<ActionResult<Player>> Create(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        return Ok(player);
    }
}