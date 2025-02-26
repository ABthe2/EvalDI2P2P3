using Eval.Business.DTO;

namespace Eval.Business.Services.Interfaces;

public interface IPasswordService
{
    Task<List<GetAccountDTO>> GetPasswords();
    Task AddPassword(CreateAccountDTO createAccount);
    Task DeletePassword(int idPassword);
}