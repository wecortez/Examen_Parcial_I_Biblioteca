using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen_Biblioteca.Models;

public partial class Prestamos
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    public int LibroId { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    public int MiembroId { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    [Display(Name = "Fecha de Préstamo")]
    public DateOnly FechaPrestamo { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    [Display(Name = "Fecha de Devolución")]
    public DateOnly? FechaDevolucion { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    public bool Devuelto { get; set; }

    public virtual Libros Libro { get; set; } = null!;

    public virtual Miembros Miembro { get; set; } = null!;
}
