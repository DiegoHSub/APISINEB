using APISINEB.Data;
using APISINEB.Models;
using APISINEB.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace APISINEB.Repository
{
    public class ModeloRepository : IModeloReposity
    {
        private readonly ApplicationDbContext _db;
        public ModeloRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateModelo(Modelos modelo)
        {
            modelo.Fec_Actualiza = DateTime.Now;
            _db.Cat_Modelo.Add(modelo);
            return Guardar();
        }

        public bool ExistsModelo(int Id_modelo)
        {
            return _db.Cat_Modelo.Any(c => c.Id_Modelo == Id_modelo);
        }

        public bool ExistsModelo(string descripcion)
        {
            bool valor = _db.Cat_Modelo.Any(c => c.Descripcion.ToLower().Trim() == descripcion.ToLower().Trim());
            return valor;
        }

        public Modelos GetModelo(int Id_modelo)
        {
            return _db.Cat_Modelo.FirstOrDefault(c => c.Id_Modelo == Id_modelo);
        }

        public ICollection<Modelos> GetModelos()
        {
            return _db.Cat_Modelo.OrderBy(c=> c.Id_Modelo).ToList();
            //return _db.Database.SqlQuery<Cat_Modelo>($"exec modelos 1,0,0").ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
