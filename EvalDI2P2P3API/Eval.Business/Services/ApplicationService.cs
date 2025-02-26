using Eval.Business.DTO;
using Eval.Business.DTO.ApplicationType;
using Eval.Business.Services.Interfaces;
using Eval.Domain.Interfaces;
using Eval.Domain.Models;

namespace Eval.Business.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    
    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
    
    public async Task<List<GetApplicationDTO>> GetApplications()
    {
        var applications = await _applicationRepository.GetApplications();
        return applications.Select(a => new GetApplicationDTO()
        {
            IdApplication = a.IdApplication,
            Name = a.Name,
            IdApplicationType = a.IdApplicationType,
            ApplicationType = new ApplicationTypeDTO
            {
                IdApplicationType = a.ApplicationType.IdApplicationType,
                Name = a.ApplicationType.Name
            }
        }).ToList();
    }

    public Task AddApplication(CreateApplicationDTO createApplication)
    {
        var newApplication = new Application
        {
            Name = createApplication.Name,
            IdApplicationType = createApplication.IdApplicationType,
        };
        return _applicationRepository.AddApplication(newApplication);
    }
}