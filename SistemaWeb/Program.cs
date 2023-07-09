using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SistemaContatos.Helper;
using SistemaContatos.Repositorio;
using SistemaContatos.Repository;
using SistemaWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços ao contêiner.
builder.Services.AddControllersWithViews();

// Configuração do banco de dados
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<BancoContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DataBase")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISessao, Sessao>();

builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
