using Eval.Business.DTO.ApplicationType;

namespace Eval.Business.DTO;

public class GetApplicationDTO
{
    public int IdApplication { get; set; }
    public string Name { get; set; }
    public int IdApplicationType { get; set; }
    public ApplicationTypeDTO ApplicationType { get; set; }
}