namespace Nsu.Contest.Web.Employee.Services;

using MassTransit;
using Microsoft.Extensions.Options;
using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.Common.Util;

public class WishlistProducer(
    EmployeeReader _employeeReader, WishlistGenerator _wishlistGenerator, 
    IOptions<EmployeeConfig> _config)
{
    public Wishlist ProduceWishlist(Guid hackathonId)
    {

        EmployeeReader employeeReader = new();
        if(_config.Value.EmployeeType == "junior")
        {
            Junior employee = employeeReader.GetEmployeeById<Junior>(_config.Value.JuniorFilePath, _config.Value.EmployeeId);
            IEnumerable<Employee> opponentEmployees = employeeReader.ReadEmployees<Teamlead>(_config.Value.TeamleadFilePath);
            return WishlistGenerator.GenerateWishlist(hackathonId, employee, opponentEmployees); 
        } 
        else if (_config.Value.EmployeeType == "teamlead")
        {
            Teamlead employee = employeeReader.GetEmployeeById<Teamlead>(_config.Value.TeamleadFilePath, _config.Value.EmployeeId);
            IEnumerable<Employee> opponentEmployees = employeeReader.ReadEmployees<Junior>(_config.Value.JuniorFilePath);
            return WishlistGenerator.GenerateWishlist(hackathonId, employee, opponentEmployees);
        }
        else
        {
            throw new ArgumentException("Invalid employee type");
        } 
    }
}
