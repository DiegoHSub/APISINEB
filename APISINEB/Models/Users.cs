using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models
{
    public class Users
    {
        [Key]
        public Int16 Id_Usuario { get; set; }
        public string Cve_Usuario { get; set; }
        public string Txt_Password { get; set; }
        public string Txt_Nombre { get; set; }
        public string Txt_PrimerA { get; set; }
        public string Txt_SegundoA { get; set; }
        public Boolean Sn_Activo { get; set; }
        public Int16 Id_Rol { get; set; }
        public DateTime? Fec_Token_Ini { get; set; }
        public DateTime? Fec_Token_Fin { get; set; }
        public string? Token { get; set; }
        public DateTime? Fec_Alta { get; set; }
        public Int16? Id_Usr_Alta { get; set; }
        public DateTime? Fec_Actualiza { get; set; }
        public Int16? Id_Usr_Actualiza { get; set; }
        public string? Txt_Motivo_Baja { get; set; }
    }
}
