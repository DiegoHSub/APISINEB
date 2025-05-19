using APISINEB.Data;
using APISINEB.Models;
using APISINEB.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace APISINEB.Repository
{
    public class MotivoRepository : IMotivoRepository
    {
        private readonly ApplicationDbContext _db;
        public MotivoRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateMotivo(Motivos motivo)
        {
            motivo.Fec_Actualiza = DateTime.Now;
            _db.Cat_Motivos.Add(motivo);
            return Guardar();
        }

        public bool ExistsMotivo(int Id_Motivo)
        {
            return _db.Cat_Motivos.Any(c => c.Id_Motivo == Id_Motivo);
        }

        public bool ExistsMotivo(string Descripcion)
        {
            bool valor = _db.Cat_Motivos.Any(c => c.Descripcion.ToLower().Trim() == Descripcion.ToLower().Trim());
            return valor;
        }

        public bool ExistsMotivoCve(int Cve_Motivo)
        {
            return _db.Cat_Motivos.Any(c => c.Cve_Motivo == Cve_Motivo);
        }

        public Motivos GetMotivo(int Id_Motivo)
        {
            return _db.Cat_Motivos.FirstOrDefault(c => c.Id_Motivo == Id_Motivo);
        }

        public Motivos GetMotivoCve(int Cve_Motivo)
        {
            return _db.Cat_Motivos.FirstOrDefault(c => c.Cve_Motivo == Cve_Motivo);
        }

        public ICollection<Motivos> GetMotivos()
        {
            return _db.Cat_Motivos.OrderBy(c =>  c.Id_Motivo).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
