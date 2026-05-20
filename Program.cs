using ExoApi.Contexts;
using ExoApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Cria o construtor da nossa aplicação web.
var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURAÇÃO DE SEGURANÇA E BANCO DE DADOS --- //

// 1. O builder busca a nossa string de conexão lá no cofre (appsettings.json).
string connectionString = builder.Configuration.GetConnectionString("ExoApiDatabase");

// 2. Avisamos a aplicação para adicionar o ExoContext e explicamos como ele deve se conectar.
// Aqui nós dizemos: "Use o MySQL, com a string de conexão que pegamos do appsettings".

builder.Services.AddDbContext<ExoContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// -------------------------------------------------- //

// Adiciona os Controladores (os garçons) para que a API saiba responder rotas.
builder.Services.AddControllers();

// Cadastra o Repositório. 
// O "AddTransient" significa que toda vez que alguém pedir o Repositório, a API entrega um novinho.
builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();

builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer" , options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao-senai-123")),
        ClockSkew = TimeSpan.FromMinutes(30),
        ValidIssuer = "exoapi.webapi",
        ValidAudience = "exoapi.webapi"
    };
});

// Constrói a aplicação com as peças que configuramos acima.
var app = builder.Build();

// Habilita o sistema de rotas (caminhos da URL).
app.UseRouting();

app.UseCors ("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization ();


// Mapeia os Controladores para que eles fiquem ouvindo as chamadas na internet.
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Dá o "play" na aplicação!
app.Run();