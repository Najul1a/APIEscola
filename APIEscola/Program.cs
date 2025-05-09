using APIEscola.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Adicionar Servico de banco de dados
builder.Services.AddDbContext<APIEscolaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar o servi�o de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Adicionar o servi�o de autentica��o
// Servi�o de EndPoints do Identity Framework
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false; // Exige confirma��o de email
    options.SignIn.RequireConfirmedAccount = false; // Exige confirma��o de conta
    options.User.RequireUniqueEmail = true; // Exige email �nico
    options.Password.RequireNonAlphanumeric = false; // Exige caracteres n�o alfanum�ricos
    options.Password.RequireLowercase = false; // Exige letras min�sculas
    options.Password.RequireUppercase = false; // Exige letras mai�sculas
    options.Password.RequireDigit = false; // Exige d�gitos num�ricos
    options.Password.RequiredLength = 4; // Exige comprimento m�nimo da senha
})
.AddRoles<IdentityRole>() // Adicionando o servi�o de roles
.AddEntityFrameworkStores<APIEscolaContext>() // Adicionando o servi�o de EntityFramework
.AddDefaultTokenProviders(); // Adiocionando o provedor de tokens padr�o

// Codigo do Arquivo: program.cs

// Swagger com Autentica��o JWT Bearer
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "APIEscola", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Adicionar o servi�o de autentica��o  e Autoriza��o
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


var app = builder.Build(); //cria o aplicativo

app.UseSwagger(); // Habilitar o Swagger
app.UseSwaggerUI(); // Habilitar a interface do Swagger


app.UseHttpsRedirection();//Redireciona requisi��es HTTP para HTTPS

app.UseCors("AllowAll"); // Habilitar o CORS

app.UseAuthentication(); // Habilitar a autentica��o

app.UseAuthorization(); // Habilitar a autoriza��o

app.MapControllers(); // Mapear os controladores

app.MapGroup("/Usuario").MapIdentityApi<IdentityUser>(); // Mapear os endpoints de autentica��o

app.Run(); // Executar o aplicativo
