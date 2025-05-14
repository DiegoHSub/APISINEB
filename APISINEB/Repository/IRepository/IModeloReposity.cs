using APISINEB.Models;

namespace APISINEB.Repository.IRepository
{
    public interface IModeloReposity
    {
        //En la interfaz solo de definen los metodos mas no la logica. 
        ICollection<Modelos> GetModelos();

        Modelos GetModelo(int Id_Modelo);
        bool ExistsModelo(int Id_Modelo);
        bool ExistsModelo(string Descripcion);
        bool CreateModelo(Modelos modelo);
        bool Guardar();

    }
}
