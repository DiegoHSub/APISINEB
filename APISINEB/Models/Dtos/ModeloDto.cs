using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models.Dtos
{
    public class ModeloDto
    {
        [Required(ErrorMessage = "El número de Modelo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El número máximo de caracteres es de 2")]
        public int Id_Modelo { get; set; }

        [Required(ErrorMessage = "La Descripción del modelo es obligatorio")]
        [MaxLength(50, ErrorMessage = "El número máximo de caracteres es de 50")]
        public string Descripcion { get; set; }

        public int Id_Usuario { get; set; }
    }
}
