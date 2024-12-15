namespace Nsu.Contest.Web.HRManager.Controllers;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRManager.Services;

using Microsoft.AspNetCore.Mvc;

public record HRRequest(Employee Employee, Wishlist Wishlist);

[ApiController]
[Route("api")]
public class HRManagerController : ControllerBase
{
    private readonly HRManagerService _service;

    public HRManagerController(HRManagerService service)
    {
        _service = service;
    }

    [HttpPost("submit")]
    public async Task<IActionResult> AddEmployee([FromBody] HRRequest request)
    {
        await _service.SaveEmployeeAsync(employee);
        return Ok("Employee data saved.");
    }

    [HttpPost("test")]
    public async Task<IActionResult> Test()
    {
        Console.WriteLine("Its alive, its working!");
        return Ok("Its working.");
    }
}
