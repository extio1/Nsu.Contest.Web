namespace Nsu.Contest.Web.Employee.Clients;

using Nsu.Contest.Web.HRManager.Controllers;

using Refit;

public interface IHRManagerClient
{
    [Post("/api/submit")]
    Task SubmitDataAsync([Body] HRRequest request);
}
