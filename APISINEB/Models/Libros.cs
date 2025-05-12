using System.ComponentModel.DataAnnotations;

namespace APISINEB.Models
{
    public class Libros
    {
        [Key]
        public Int32 Id_Libros { get; set; }
        public string Txt_Clave { get; set; }
        public string Txt_Titulo { get; set; }
    }
}
