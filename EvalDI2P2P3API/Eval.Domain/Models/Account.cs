using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eval.Domain.Models;

[Table("Account")]
public class Account
{
    [Key]
    public int IdAccount { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public int IdApplication { get; set; }
    
    [ForeignKey(nameof(IdApplication))]
    public Application? Application { get; set; }
}