using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalloFlix.Models;

[Table("Movie")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Display(Name = "Título Original")]
    [Required(ErrorMessage = "Por favor, informe o Título Original")]
    [StringLength(100, ErrorMessage = "O Título Original deve possuir no máximo 100 caracteres")]
    public string OriginalTitle { get; set; }

    [Display(Name = "Título")]
    [Required(ErrorMessage = "Por favor, informe o Título")]
    [StringLength(100, ErrorMessage = "O Título deve possuir no máximo 100 caracteres")]
    public string Title { get; set; }

    [Display(Name = "Resumo")]
    [StringLength(8000, ErrorMessage = "O Resumo deve possuir no máximo 8000 caracteres")]
    public string Synopsis { get; set; }

    [Column(TypeName = "Year")]
    [Display(Name = "Ano de Estreia")]
    public Int16 MovieYear { get; set; }

    [Display(Name = "Duração (em minutos)")]
    [Required(ErrorMessage = "Por favor, informe a Duração")]
    public Int16 Duration { get; set; }

    [Display(Name = "Classificação Etária")]
    [Required(ErrorMessage = "Por favor, informe a Classificação Etária")]
    public sbyte AgeRating { get; set; } = 0;

    [StringLength(200)]
    [Display(Name = "Foto")]
    public string Image { get; set; }

    [NotMapped]
    [Display(Name = "Duração")]
    public string HourDuration { get { 
        return TimeSpan.FromMinutes(Duration).ToString(@"%h'h 'mm'min'");
    } }

    public ICollection<MovieGenre> Genres { get; set; }

}
