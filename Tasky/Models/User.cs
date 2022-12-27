using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasky.Interfaces.Models;

namespace Tasky.Models;

[Table("User")]
public class User : IModel
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("email")]
    [Required]
    [StringLength(50, MinimumLength = 4)]
    public string Email { get; set; }

    [Column("password")]
    [Required]
    public string PassworHash { get; set; }

}
