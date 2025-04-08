using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MANAGEMENT.TEMPLATE.Models;

[Table("table_name", Schema = "dbo")]
public class Model
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string value1 { get; set; } = string.Empty;

    [Required]
    public string value2 { get; set; } = string.Empty;

    public DateTime? Created { get; set; }

    [MaxLength(100)]
    [Column("created_by")]
    public string? CreatedBy { get; set; }

    public DateTime? Modified { get; set; }

    [MaxLength(100)]
    [Column("modified_by")]
    public string? ModifiedBy { get; set; }
}
