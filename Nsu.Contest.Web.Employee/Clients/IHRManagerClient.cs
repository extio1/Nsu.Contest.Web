namespace Nsu.Contest.Web.Employee.Clients;

using Nsu.Contest.Web.HRManager.Controllers;

using Refit;

public interface IHRManagerClient
{
    [Post("/api/teamlead/submit")]
    Task SubmitDataAsync([Body] HRRequestTeamlead request);

    [Post("/api/junior/submit")]
    Task SubmitDataAsync([Body] HRRequestJunior request);
}
