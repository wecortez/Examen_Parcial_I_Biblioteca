using System.ComponentModel.DataAnnotations;

namespace Examen_Biblioteca.Models;

public partial class Miembros
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "El campo es requerido")]
    public string Apellido { get; set; } = null!;
    [EmailAddress(ErrorMessage = "El correo electrónico no cumple con el formato.")]
    [Required(ErrorMessage = "El campo es requerido")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "El campo es requerido")]
    [Display(Name = "Fecha de Suscripción")]
    public DateOnly FechaSuscripcion { get; set; }

    public virtual ICollection<Prestamos> Prestamos { get; set; } = new List<Prestamos>();

    public string NombreCompleto
    {
        get { return Nombre + " " + Apellido; }
    }
}
