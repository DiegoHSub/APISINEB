namespace APISINEB.Models.Dtos
{
    public class UserDataDto
    {
        //regresa los datos del usuario que se retornan al login
        public string Id_Usuario { get; set; }
        public string Cve_Usuario { get; set; }
        public string Txt_Nombre { get; set; }
        public string Txt_PrimerA { get; set; }
        public string Txt_SegundoA { get; set; }
    }
}
