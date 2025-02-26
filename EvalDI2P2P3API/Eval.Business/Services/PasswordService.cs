using Eval.Business.DTO;
using Eval.Business.DTO.ApplicationType;
using Eval.Business.Encryption;
using Eval.Business.Services.Interfaces;
using Eval.Business.Utils;
using Eval.Domain.Interfaces;
using Eval.Domain.Models;

namespace Eval.Business.Services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordRepository _passwordRepository;
    private readonly IApplicationRepository _applicationRepository;
    
    public PasswordService(IPasswordRepository passwordRepository, IApplicationRepository applicationRepository)
    {
        _passwordRepository = passwordRepository;
        _applicationRepository = applicationRepository;
    }
    
    public async Task<List<GetPasswordDTO>> GetPasswords()
    {
        EncryptionContext encryptionContext;
        var passwords = await _passwordRepository.GetPasswords();
        passwords.ForEach(pa =>
        {
            if (pa.IdApplication == 1)
            {
                encryptionContext = new EncryptionContext(new AESUtils());
            }
            else if (pa.IdApplication == 2)
            {
                encryptionContext = new EncryptionContext(new RSAUtils());
            }
            else
            {
                throw new Exception("Invalid application type");
            }
            pa.Password = encryptionContext.Decrypt(pa.Password); 
        });
        return passwords.Select(p => new GetPasswordDTO
        {
            IdPassword = p.IdAccount,
            Value = p.Password,
            IdApplication = p.IdApplication,
            Application = new GetApplicationDTO
            {
                IdApplication = p.IdApplication,
                Name = p.Application.Name,
                IdApplicationType = p.Application.IdApplicationType,
                ApplicationType = new ApplicationTypeDTO
                {
                    IdApplicationType = p.Application.ApplicationType.IdApplicationType,
                    Name = p.Application.ApplicationType.Name
                }
            }
        }).ToList();
    }

    public async Task AddPassword(CreatePasswordDTO createPassword)
    {
        var application = await _applicationRepository.GetApplication(createPassword.IdApplication);
        EncryptionContext encryptionContext;
        if (application.IdApplicationType == 1)
        {
            encryptionContext = new EncryptionContext(new AESUtils());
        } 
        else
        {
            encryptionContext = new EncryptionContext(new RSAUtils());
        }

        if (application.IdApplicationType != 1 && application.IdApplicationType != 2)
        {
            throw new Exception("Invalid application type");
        }
        
        var newPassword = new Account
        {
            Password = encryptionContext.Encrypt(createPassword.Value),
            IdApplication = createPassword.IdApplication,
        };
        
        await _passwordRepository.AddPassword(newPassword);
    }

    public Task DeletePassword(int idPassword)
    {
        return _passwordRepository.DeletePassword(idPassword);
    }
}