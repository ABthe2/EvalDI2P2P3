using Eval.Business.DTO;

namespace Eval.Business.Services.Interfaces;

public interface IApplicationService
{
    Task<List<GetApplicationDTO>> GetApplications();
    Task AddApplication(CreateApplicationDTO createApplication);
}