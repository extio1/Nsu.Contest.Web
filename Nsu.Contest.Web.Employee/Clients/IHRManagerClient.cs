namespace Nsu.Contest.Web.Employee.Clients;

using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.HRManager.Controllers;

using Refit;

public interface IHRManagerClient
{
    [Post("/api/hrmanager/submit")]
    Task SubmitDataAsync([Body] HRRequest request);
}
