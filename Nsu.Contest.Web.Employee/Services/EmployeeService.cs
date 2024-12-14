namespace Nsu.Contest.Web.Employee.Services;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.Employee.Clients;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeeService
{
    private readonly IHRManagerClient _hrManagerClient;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(
        IHRManagerClient hrManagerClient, ILogger<EmployeeService> logger)
    {
        _hrManagerClient = hrManagerClient;
        _logger = logger;
    }

    public async Task SendPreferencesAsync()
    {
        // var employeeId = int.Parse(Environment.GetEnvironmentVariable("ID") ?? throw new InvalidOperationException("ID not specified"));
        // var employeeType = Environment.GetEnvironmentVariable("TYPE") ?? throw new InvalidOperationException("TYPE not specified");
        
        // var wishlist = _wishlistGenerator.GenerateWishlist(employeeId, allEmployeeIds);

        var request = new HRRequest(new Teamlead(1, "Jorge"), new Wishlist(new Teamlead(1, "Jorge"), [1, 2, 3]));

        await _hrManagerClient.SubmitDataAsync(request);
    }
}
