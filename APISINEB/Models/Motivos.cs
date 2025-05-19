using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models
{
    public class Motivos
    {
        [Key]
        public Int32 Id_Motivo { get; set; }
        [Required]
        public Int32 Cve_Motivo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string? Cves_Movimiento {  get; set; }
        [Required]
        public Int32 Sn_Status { get; set; }
        [Required]
        public Int32 Id_Usuario { get; set; }
        [Required]
        public DateTime Fec_Actualiza {  get; set; }
    }
}
