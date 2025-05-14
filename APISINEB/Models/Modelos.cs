using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models
{
    public class Modelos
    {
        [Key]
        public Int32 Id_Modelo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public Int32 Id_Usuario { get; set; }
        [Required]
        public DateTime Fec_Actualiza { get; set; }

    }
}
