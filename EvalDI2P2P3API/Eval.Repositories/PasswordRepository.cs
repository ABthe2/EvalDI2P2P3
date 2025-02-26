using Eval.Domain.Interfaces;
using Eval.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Eval.Repositories;

public class PasswordRepository : IPasswordRepository
{
    private readonly AppDbContext _context;
    
    public PasswordRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Account>> GetPasswords()
    {
        return _context.Passwords
            .Include(p => p.Application)
            .ThenInclude(a => a.ApplicationType)
            .ToList();
    }

    public async Task AddPassword(Account account)
    {
        await _context.Passwords.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePassword(int idPassword)
    {
        var password = _context.Passwords.Find(idPassword);
        if (password != null)
        {
            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Password not found");
        }
    }
}