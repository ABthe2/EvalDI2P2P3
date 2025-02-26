using Eval.Domain.Models;

namespace Eval.Domain.Interfaces;

public interface IPasswordRepository
{
    public Task<List<Account>> GetPasswords();
    public Task AddPassword(Account account);
    public Task DeletePassword(int idPassword);
}