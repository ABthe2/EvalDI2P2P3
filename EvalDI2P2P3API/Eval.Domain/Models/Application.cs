using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eval.Domain.Models;

[Table("Application")]
public class Application
{
    [Key]
    public int IdApplication { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required int IdApplicationType { get; set; }
    
    [ForeignKey(nameof(IdApplicationType))]
    public ApplicationType? ApplicationType { get; set; }
}