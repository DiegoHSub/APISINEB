namespace APISINEB.Models.Dtos
{
    public class UserDto
    {
        public int Id_Usuario { get; set; }
        public string Cve_Usuario { get; set; }
        public string Txt_Password { get; set; }
        public string Txt_Nombre { get; set; }
        public string Txt_PrimerA { get; set; }
        public string Txt_SegundoA { get; set; }
        public int Sn_Activo { get; set; }
        public int Id_Rol { get; set; }
        public string Txt_Motivo_Baja { get; set; }
    }
}
