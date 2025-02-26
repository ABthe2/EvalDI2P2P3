namespace Eval.Business.DTO;

public class GetAccountDTO
{
    public int IdAccount { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public int IdApplication { get; set; }
    public GetApplicationDTO Application { get; set; }
}