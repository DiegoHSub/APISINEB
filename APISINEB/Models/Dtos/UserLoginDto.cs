using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage ="El nombre del usuario es obligatorio")]
        public string Cve_Usuario { get; set; }
        [Required(ErrorMessage = "la contraseña es obligatoria")]
        public string Txt_Password { get; set; }

    }
}
