using MarketWeight.Core.Persistencia;
using MarketWeight.Ado.Dapper;
using System.Data;
using MySql.Data.MySqlClient; // si usas MySQL

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

// Registrar la conexión a la base de datos
builder.Services.AddTransient<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar el repositorio
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();

var app = builder.Build();

// Configuración del middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
