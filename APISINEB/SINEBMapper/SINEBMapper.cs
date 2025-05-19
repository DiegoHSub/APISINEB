using APISINEB.Models.Dtos;
using APISINEB.Models;
using AutoMapper;
namespace APISINEB.SINEBMapper
{
    public class SINEBMapper: Profile
    {
        public SINEBMapper()
        {
            //aqui se comunican los DTO con el modelo categoria de ida y vuelta
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<Users, UserCreateDto>().ReverseMap();

            CreateMap<Modelos, ModeloDto>().ReverseMap();
            CreateMap<Modelos, CreateModeloDto>().ReverseMap();

            CreateMap<Motivos, MotivoDto>().ReverseMap();
            CreateMap<Motivos, CreateMotivoDto>().ReverseMap();
        }
    }
}
