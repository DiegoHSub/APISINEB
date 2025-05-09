using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APISINEB.Data;
using APISINEB.Models;
using APISINEB.Models.Dtos;
using APISINEB.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using XAct.Users;
using XSystem.Security.Cryptography;

namespace APISINEB.Repository
{
    public class UserRepository: IUserRepository
    {
        public readonly ApplicationDbContext _bd;
        private string claveSecret;

        public UserRepository(ApplicationDbContext bd, IConfiguration config)
        {
            _bd = bd;
            claveSecret = config.GetValue<string>("ApiSettings:Secreta");
        }

        public ICollection<Users> GetUsers()
        {
            //return _bd.Users.OrderBy(c=> c.Txt_Nombre).ToList();
            var option = 1;
            return _bd.Database.SqlQuery<Users>($"exec usuarios {option},'',0").ToList();
        }

       

        public Users GetUser(int userid)
        {
            return _bd.Users.FirstOrDefault(c => c.Id_Usuario == userid);
        }

        public bool IsUniqueUser(string user)
        {
            var userBd = _bd.Users.FirstOrDefault(c => c.Txt_Nombre == user);
            if (userBd == null) {
                return true;
            }
            return false;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var tokenReturn = "";
            var passEncrypt = getmd5(userLoginDto.Txt_Password);
            var user = _bd.Users.FirstOrDefault(
                u => u.Cve_Usuario.ToLower() == userLoginDto.Cve_Usuario.ToLower()
                && u.Txt_Password == passEncrypt
                );
            //validar si el usuario no existe con la combinación de usuario y contraseña
            if (user == null)
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }
            //verificamos si tiene Token y es activo
            //obtenemos la fecha actual.
            var fecActual = DateTime.Now;
            if (fecActual >= user.Fec_Token_Ini && fecActual <= user.Fec_Token_Fin && user.Fec_Token_Ini != null && user.Token != null)
            { 
                tokenReturn = user.Token; 
            }
            else
            {
                //Existe el usuario encontes procedemos a acceder
                var manejoToken = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(claveSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name,user.Cve_Usuario.ToString()),
                    new Claim(ClaimTypes.Role,user.Id_Rol.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1), //numero de dias que durara el token
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //firmar token con la llave guardada
                };

                var token = manejoToken.CreateToken(tokenDescriptor); //esta linea crea como tal el JWT
                tokenReturn = manejoToken.WriteToken(token);

                //guardamos el nuevo token en el registro del usuario existente
                var saveToken = _bd.Database.ExecuteSql($"exec usuarios 2,{tokenReturn},{user.Id_Usuario}");

            }

            UserLoginResponseDto userLoginResponseDto = new UserLoginResponseDto()
            {
                Token = tokenReturn,
                User = user
            };

            return userLoginResponseDto;

        }

        public async Task<Users> Register(UserCreateDto userCreateDto)
        {
            var passEncrypt = getmd5(userCreateDto.Txt_Password);

            Users user = new Users()
            {
                Cve_Usuario = userCreateDto.Cve_Usuario,
                Txt_Password = passEncrypt,
                Txt_Nombre = userCreateDto.Txt_Nombre,
                Txt_PrimerA = userCreateDto.Txt_PrimerA,
                Txt_SegundoA = userCreateDto.Txt_SegundoA,
                Id_Rol = userCreateDto.Id_Rol,
                Sn_Activo = true,
            };

            _bd.Users.Add(user);
            await _bd.SaveChangesAsync(); //guardar cambios
            user.Txt_Password = passEncrypt;
            return user;
        }

        public ICollection<Libros> GetLibros()
        {
            //return _bd.Users.OrderBy(c=> c.Txt_Nombre).ToList();
            var option = 1;
            return _bd.Database.SqlQuery<Libros>($"exec librosPrueba {option}").ToList();
        }

        //Metodo para encriptar contraseña con MD5 se usa tanto en el acceso como en el registro
        public static string getmd5(string password)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();
            }
            return resp;
        }
        
    }
}
