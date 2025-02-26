using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eval.Domain.Models;

[Table("ApplicationType")]
public class ApplicationType
{
    [Key]
    public int IdApplicationType { get; set; }
    
    [Required]
    public string Name { get; set; }
}