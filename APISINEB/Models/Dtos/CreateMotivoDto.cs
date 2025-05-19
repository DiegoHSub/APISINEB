using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models.Dtos
{
    public class CreateMotivoDto
    {
        [Required(ErrorMessage = "El número de motivo es obligatorio")]
        public int Cve_Motivo { get; set; }

        [Required(ErrorMessage = "La descripción del motivo es obligatorio")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100")]
        public string? Descripcion { get; set; }

        public string? Cves_Movimiento { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public Int32 Id_Usuario { get; set; }

        [Required(ErrorMessage = "El status es obligatorio")]
        public Int32 Sn_Status { get; set; }
    }
}
