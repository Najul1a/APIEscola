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

// Adicionar o serviço de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Adicionar o serviço de autenticação
// Serviço de EndPoints do Identity Framework
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false; // Exige confirmação de email
    options.SignIn.RequireConfirmedAccount = false; // Exige confirmação de conta
    options.User.RequireUniqueEmail = true; // Exige email único
    options.Password.RequireNonAlphanumeric = false; // Exige caracteres não alfanuméricos
    options.Password.RequireLowercase = false; // Exige letras minúsculas
    options.Password.RequireUppercase = false; // Exige letras maiúsculas
    options.Password.RequireDigit = false; // Exige dígitos numéricos
    options.Password.RequiredLength = 4; // Exige comprimento mínimo da senha
})
.AddRoles<IdentityRole>() // Adicionando o serviço de roles
.AddEntityFrameworkStores<APIEscolaContext>() // Adicionando o serviço de EntityFramework
.AddDefaultTokenProviders(); // Adiocionando o provedor de tokens padrão

// Codigo do Arquivo: program.cs

// Swagger com Autenticação JWT Bearer
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


// Adicionar o serviço de autenticação  e Autorização
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


var app = builder.Build(); //cria o aplicativo

app.UseSwagger(); // Habilitar o Swagger
app.UseSwaggerUI(); // Habilitar a interface do Swagger


app.UseHttpsRedirection();//Redireciona requisições HTTP para HTTPS

app.UseCors("AllowAll"); // Habilitar o CORS

app.UseAuthentication(); // Habilitar a autenticação

app.UseAuthorization(); // Habilitar a autorização

app.MapControllers(); // Mapear os controladores

app.MapGroup("/Usuario").MapIdentityApi<IdentityUser>(); // Mapear os endpoints de autenticação

app.Run(); // Executar o aplicativo
