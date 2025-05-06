using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models.Dtos
{
    public class UserCreateDto
    {
        [Required(ErrorMessage ="El nombre del usuario es obligatorio")]
        public string Cve_Usuario { get; set; }
        [Required(ErrorMessage = "la contraseña es obligatoria")]
        public string Txt_Password { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Txt_Nombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        public string Txt_PrimerA { get; set; }
        [Required(ErrorMessage = "El segundo apellido es obligatorio")]
        public string Txt_SegundoA { get; set; }
        [Required(ErrorMessage = "El rol del usuario es obligatorio")]
        public Int16 Id_Rol { get; set; }
    }
}
