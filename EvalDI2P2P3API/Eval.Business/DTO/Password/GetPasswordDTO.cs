namespace Eval.Business.DTO;

public class GetPasswordDTO
{
    public int IdPassword { get; set; }
    public string Value { get; set; }
    public int IdApplication { get; set; }
    public GetApplicationDTO Application { get; set; }
}