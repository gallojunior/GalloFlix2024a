using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalloFlix.Models;

[Table("Movie")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public sbyte Id { get; set; }

    [Display(Name = "Título Original")]
    [Required(ErrorMessage = "Por favor, informe o Título Original")]
    [StringLength(100, ErrorMessage = "O Título Original deve possuir no máximo 100 caracteres")]
    public string OriginalTitle { get; set; }
}
