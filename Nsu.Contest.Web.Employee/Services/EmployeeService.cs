namespace Nsu.Contest.Web.Employee.Services;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.Employee.Clients;
using Nsu.Contest.Web.HRManager.Controllers;
using Nsu.Contest.Web.Common.Util;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

public class EmployeeService
{
    private readonly IHRManagerClient _hrManagerClient;
    private readonly ILogger<EmployeeService> _logger;
    private readonly IOptions<EmployeeConfig> _config;

    public EmployeeService(
        IHRManagerClient hrManagerClient, ILogger<EmployeeService> logger, IOptions<EmployeeConfig> options)
    {
        _hrManagerClient = hrManagerClient;
        _config = options;
        _logger = logger;
    }

    public async Task SendPreferencesAsync()
    {
        try 
        {
            EmployeeReader employeeReader = new();
            Employee employee;
            IEnumerable<Employee> opponentEmployees;

            if(_config.Value.EmployeeType == "junior")
            {
                employee = employeeReader.GetEmployeeById<Junior>(_config.Value.JuniorFilePath, _config.Value.EmployeeId);
                opponentEmployees = employeeReader.ReadEmployees<Teamlead>(_config.Value.TeamleadFilePath);
            } 
            else if (_config.Value.EmployeeType == "teamlead")
            {
                employee = employeeReader.GetEmployeeById<Teamlead>(_config.Value.TeamleadFilePath, _config.Value.EmployeeId);
                opponentEmployees = employeeReader.ReadEmployees<Junior>(_config.Value.JuniorFilePath);
            }
            else
            {
                throw new ArgumentException("Invalid employee type");
            }
            
            var wishlist = WishlistGenerator.GenerateWishlist(employee, opponentEmployees);

            var request = new HRRequest(employee, wishlist);

            await _hrManagerClient.SubmitDataAsync(request);
        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }
    }
}
