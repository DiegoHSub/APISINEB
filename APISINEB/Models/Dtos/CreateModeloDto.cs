using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models.Dtos
{
    public class CreateModeloDto
    {
        //este archivo DTO se va a exponer para Crear la categoria
        [Required(ErrorMessage = "El número de Modelo es obligatorio")]
        //[MaxLength(2, ErrorMessage = "El número máximo de caracteres es de 2")]
        public Int32 Id_Modelo { get; set; }

        [Required(ErrorMessage = "La Descripción del modelo es obligatorio")]
        [MaxLength(50, ErrorMessage = "El número máximo de caracteres es de 50")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El Usuario es obligatorio")]
        public Int32 Id_Usuario { get; set; }
    }
}
