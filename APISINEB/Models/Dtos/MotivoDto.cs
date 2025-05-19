using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models.Dtos
{
    public class MotivoDto
    {
        [Required(ErrorMessage = "El número de motivo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El número máximo de caracteres es de 2")]
        public int Id_Motivo { get; set; }

        [Required(ErrorMessage = "La clave del motivo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El número máximo de caracteres es de 2")]
        public int Cve_Motivo { get; set; }

        [Required(ErrorMessage = "La descripción del motivo es obligatorio")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public string Descripcion { get; set; }

        public string? Cves_Movimiento { get; set; }

        public int Sn_Status { get; set; }

        public int Id_Usuario { get; set; }
    }
}
