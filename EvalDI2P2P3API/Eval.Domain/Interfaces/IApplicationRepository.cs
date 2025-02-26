using Eval.Domain.Models;

namespace Eval.Domain.Interfaces;

public interface IApplicationRepository
{
    public Task<List<Application>> GetApplications();
    public Task<Application> GetApplication(int idApplication);
    public Task AddApplication(Application application);
}