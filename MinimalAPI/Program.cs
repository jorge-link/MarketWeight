using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MarketWeightDb>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)) // Ajustá la versión según tu servidor
    )
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


// ====================== USUARIOS ======================

// GET: Listar todos
app.MapGet("/usuarios", (MarketWeightDb db) =>
    db.Usuarios.ToList());

// GET: por email (campo único)
app.MapGet("/usuarios/email/{email}", (string email, MarketWeightDb db) =>
{
    var usuario = db.Usuarios.FirstOrDefault(u => u.Email == email);
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
});

// POST: crear
app.MapPost("/usuarios", (Usuario usuario, MarketWeightDb db) =>
{
    db.Usuarios.Add(usuario);
    db.SaveChanges();
    return Results.Created($"/usuarios", usuario);
});


// ====================== MONEDAS ======================

// GET: Listar todas
app.MapGet("/monedas", (MarketWeightDb db) =>
    db.Monedas.ToList());

// GET: por nombre (campo único)
app.MapGet("/monedas/nombre/{nombre}", (string nombre, MarketWeightDb db) =>
{
    var moneda = db.Monedas.FirstOrDefault(m => m.Nombre == nombre);
    return moneda is not null ? Results.Ok(moneda) : Results.NotFound();
});

// POST: crear
app.MapPost("/monedas", (Moneda moneda, MarketWeightDb db) =>
{
    db.Monedas.Add(moneda);
    db.SaveChanges();
    return Results.Created($"/monedas", moneda);
});


// ====================== HISTORIAL ======================

// GET: Listar todo
app.MapGet("/historial", (MarketWeightDb db) =>
    db.Historiales.ToList());

// GET: por IdUsuario
app.MapGet("/historial/usuario/{idUsuario}", (uint idUsuario, MarketWeightDb db) =>
{
    var lista = db.Historiales.Where(h => h.IdUsuario == idUsuario).ToList();
    return Results.Ok(lista);
});

// GET: por idMoneda
app.MapGet("/historial/moneda/{idMoneda}", (uint idMoneda, MarketWeightDb db) =>
{
    var lista = db.Historiales.Where(h => h.idMoneda == idMoneda).ToList();
    return Results.Ok(lista);
});

// POST: crear
app.MapPost("/historial", (Historial historial, MarketWeightDb db) =>
{
    historial.FechaHora = DateTime.UtcNow;
    db.Historiales.Add(historial);
    db.SaveChanges();
    return Results.Created($"/historial", historial);
});

app.Run();
