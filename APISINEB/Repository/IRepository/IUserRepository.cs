using APISINEB.Models;
using APISINEB.Models.Dtos;

namespace APISINEB.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<Users> GetUsers(); //obtener usuarios
        Users  GetUser(int userid); //obtener usuario de manera individual

        bool IsUniqueUser(string user);//Saber si el usuario existe

        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto); //logueo
        Task<Users> Register(UserCreateDto userCreateDto); //Registro

        ICollection<Libros> GetLibros(); //obtener libros

    }
}
