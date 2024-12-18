namespace Nsu.Contest.Web.HRDirector.Controllers;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRDirector.Services;

using Microsoft.AspNetCore.Mvc;

public record HRDirectorRequest(
    IEnumerable<Wishlist> JuniorsWishlists, 
    IEnumerable<Wishlist> TeamleadsWishlists, 
    IEnumerable<Team>     Teams
);

[ApiController]
[Route("api")]
public class HRDirectorController(IHRDirectorService service) : ControllerBase
{
    IHRDirectorService _service = service;

    [HttpPost("submit")]
    public IActionResult Submit([FromBody] HRDirectorRequest request)
    {
        var juniorWishlists   = new List<Wishlist>(request.JuniorsWishlists);
        var teamleadWishlists = new List<Wishlist>(request.TeamleadsWishlists);
        var teams = new List<Team>(request.Teams);
        _service.HandleWishlistsTeams(juniorWishlists, teamleadWishlists, teams);
        return Ok("OK!");
    }

}
