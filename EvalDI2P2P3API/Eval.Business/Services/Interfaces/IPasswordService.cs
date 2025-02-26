using Eval.Business.DTO;

namespace Eval.Business.Services.Interfaces;

public interface IPasswordService
{
    Task<List<GetPasswordDTO>> GetPasswords();
    Task AddPassword(CreatePasswordDTO createPassword);
    Task DeletePassword(int idPassword);
}