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
    private readonly HRManagerService _service;

    public HRManagerController(HRManagerService service)
    {
        _service = service;
    }

    [HttpPost("teamlead/submit")]
    public async Task<IActionResult> SubmitTeamlead([FromBody] HRRequestTeamlead request)
    {
        await _service.SaveEmployeeWishlistAsync(request.Employee, request.Wishlist);
        await _service.MakeTeamsAndSendAsync();
        return Ok("Employee data saved.");
    }

    [HttpPost("junior/submit")]
    public async Task<IActionResult> SubmitJunior([FromBody] HRRequestJunior request)
    {
        await _service.SaveEmployeeWishlistAsync(request.Employee, request.Wishlist);
        await _service.MakeTeamsAndSendAsync();
        return Ok("Employee data saved.");
    }

}
