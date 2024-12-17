namespace Nsu.Contest.Web.HRManager.Controllers;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRManager.Services;

using Microsoft.AspNetCore.Mvc;

public record HRRequestTeamlead(Teamlead Employee, Wishlist Wishlist);
public record HRRequestJunior(Junior Employee, Wishlist Wishlist);

[ApiController]
[Route("api")]
public class HRManagerController : ControllerBase
{
    private readonly IHRManagerService _service;

    public HRManagerController(IHRManagerService service)
    {
        _service = service;
    }

    [HttpPost("teamlead/submit")]
    public async Task<IActionResult> SubmitTeamlead([FromBody] HRRequestTeamlead request)
    {
        await _service.HandleTeamleadWishlistAsync(request.Employee, request.Wishlist);
        return Ok("Employee data saved.");
    }

    [HttpPost("junior/submit")]
    public async Task<IActionResult> SubmitJunior([FromBody] HRRequestJunior request)
    {
        await _service.HandleJuniorWishlistAsync(request.Employee, request.Wishlist);
        return Ok("Employee data saved.");
    }

}
