using APISINEB.Models;

namespace APISINEB.Repository.IRepository
{
    public interface IMotivoRepository
    {
        ICollection<Motivos> GetMotivos();

        Motivos GetMotivo(int Id_Motivo);
        Motivos GetMotivoCve(int Cve_Motivo);
        bool ExistsMotivo(int Id_Motivo);
        bool ExistsMotivo(string Descripcion);
        bool ExistsMotivoCve(int Cve_Motivo);
        bool CreateMotivo(Motivos motivo);
        bool Guardar();
    }
}
