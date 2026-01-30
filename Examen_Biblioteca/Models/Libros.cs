using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen_Biblioteca.Models;

public partial class Libros
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    public string Titulo { get; set; } = null!;
    [Required(ErrorMessage = "El campo es requerido")]
    public string Autor { get; set; } = null!;
    [Required(ErrorMessage = "El campo es requerido")]
    public string Genero { get; set; } = null!;
    [Required(ErrorMessage = "El campo es requerido")]
    [Display(Name = "Año de publicación")]
    public int AnioPublicacion { get; set; }

    public virtual ICollection<Prestamos> Prestamos { get; set; } = new List<Prestamos>();
}
