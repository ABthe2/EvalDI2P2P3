using Eval.Domain.Interfaces;
using Eval.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Eval.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _context;
    
    public ApplicationRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Application>> GetApplications()
    {
        return _context.Applications
            .Include(a => a.ApplicationType)
            .ToList();
    }

    public Task<Application> GetApplication(int idApplication)
    {
        var application = _context.Applications.FirstOrDefault(a => a.IdApplication == idApplication);
        if (application == null)
        {
            throw new Exception("No application was found");
        }
        return Task.FromResult(application);
    }

    public async Task AddApplication(Application application)
    {
        await _context.Applications.AddAsync(application);
        await _context.SaveChangesAsync();
    }
}