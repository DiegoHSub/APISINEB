using APISINEB.Data;
using APISINEB.Repository.IRepository;
using APISINEB.Repository;
using APISINEB.SINEBMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//crear cadena de conexion a sql
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

//Agregamos los repositorios (servicios)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IModeloReposity, ModeloRepository>();


//agregamos el automapper
builder.Services.AddAutoMapper(typeof(SINEBMapper));

//configuramos la autenticación
var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");
builder.Services.AddAuthentication(
    x => {
        //agregar y configurar los servios de autenticacion de conexion a la api
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(x =>
    {   //configura el JWT bearer 
        x.RequireHttpsMetadata = false; //no se requiere https para pruebas
        x.SaveToken = true; //se almacena el JWT cuando esta autentificado
        x.TokenValidationParameters = new TokenValidationParameters
        {
            //establece los parametros de autentificacion
            ValidateIssuerSigningKey = true, //valida la firma del emisor
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), //establece la clave de firma del emisor de token
            ValidateIssuer = false, //validar emisor de token
            ValidateAudience = false, //se valida la audiencia.
        };

    }
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Soporte para CORS
//Se pueden habilitar: 1- un dominio o 2. multimples dominios
//3 caulqueir dominio Tener en cuenta seguridad
//
//Se usa (*) para todos los dominios
builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", build =>
{
    //se pasan los dominios que podran acceder al API 
    //mas d eun dominio se separa por comas ejemplo: http://milocalfront1.com,http://milocalfront2.com
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("swagger/v1/swagger.json", "ApiV1");
            options.RoutePrefix = "";
        });
}
    

app.UseHttpsRedirection();

//soporte y agregacion de CORS a nivel global.
app.UseCors("CorsPolicy");

//soporte para autenticación
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
