namespace Nsu.Contest.Web.HRManager.Controllers;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRManager.Services;

using Microsoft.AspNetCore.Mvc;

public record HRRequestTeamlead(Teamlead Employee, Wishlist Wishlist);
public record HRRequestJunior(Junior Employee, Wishlist Wishlist);

[ApiController]
[Route("api")]
public class HRManagerController(IHRManagerService service) : ControllerBase
{
    private readonly IHRManagerService _service = service;

    [HttpPost("teamlead/submit")]
    public IActionResult SubmitTeamlead([FromBody] HRRequestTeamlead request)
    {
        var teamlead = new Teamlead(request.Employee.Id, request.Employee.Name);
        var wishlist = new Wishlist(teamlead, request.Wishlist.DesiredEmployees);
        _service.HandleTeamleadWishlist(teamlead, wishlist);
        return Ok("Teamlead data saved.");
    }

    [HttpPost("junior/submit")]
    public IActionResult SubmitJunior([FromBody] HRRequestJunior request)
    {
        var junior = new Junior(request.Employee.Id, request.Employee.Name);
        var wishlist = new Wishlist(junior, request.Wishlist.DesiredEmployees);
        _service.HandleJuniorWishlist(junior, wishlist);
        return Ok("Junior data saved.");
    }

}
