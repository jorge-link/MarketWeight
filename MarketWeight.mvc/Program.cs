using MySql.Data.MySqlClient;
using System.Data;
using MarketWeight.Core.Persistencia;
using MarketWeight.Ado.Dapper;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuración de cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 🔹 Registrar IDbConnection
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

// 🔹 Repositorios
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<IRepoMoneda, RepoMoneda>();

// 🔹 MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Compra}/{action=ComprarMoneda}/{id?}");

app.Run();
