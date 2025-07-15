using System.Data;
using MySqlConnector;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// =================== CONFIG ====================
builder.Services.AddSingleton<IDbConnection>(sp =>
    new MySqlConnection(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =================== REPOS =====================
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<IRepoMoneda, RepoMoneda>();
builder.Services.AddScoped<IRepoHistorial, RepoHistorial>();

// =================== SWAGGER ===================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// =================== SWAGGER UI =================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "MarketWeight API V1");
        c.RoutePrefix = "docs";
    });

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


// =================== ENDPOINTS ==================

// ------------- USUARIOS -------------
app.MapGet("/usuarios", async (IRepoUsuario repo) =>
{
    var usuarios = await repo.ObtenerAsync();
    return Results.Ok(usuarios);
});

app.MapGet("/usuarios/{id}", async (uint id, IRepoUsuario repo) =>
{
    var usuario = await repo.DetalleAsync(id);
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
});

app.MapPost("/usuarios", async (Usuario usuario, IRepoUsuario repo) =>
{
    await repo.AltaAsync(usuario);
    return Results.Created($"/usuarios", usuario);
});


// ------------- MONEDAS -------------
app.MapGet("/monedas", async (IRepoMoneda repo) =>
{
    var monedas = await repo.ObtenerAsync();
    return Results.Ok(monedas);
});

app.MapGet("/monedas/{id}", async (uint id, IRepoMoneda repo) =>
{
    var moneda = await repo.DetalleAsync(id);
    return moneda is not null ? Results.Ok(moneda) : Results.NotFound();
});

app.MapPost("/monedas", async (Moneda moneda, IRepoMoneda repo) =>
{
    await repo.AltaAsync(moneda);
    return Results.Created($"/monedas", moneda);
});


// ------------- HISTORIAL -------------
app.MapGet("/historial", async (IRepoHistorial repo) =>
{
    var historial = await repo.ObtenerAsync();
    return Results.Ok(historial);
});

app.MapGet("/historial/{id}", async (uint id, IRepoHistorial repo) =>
{
    var detalle = await repo.DetalleAsync(id);
    return detalle is not null ? Results.Ok(detalle) : Results.NotFound();
});

app.MapPost("/historial", async (Historial historial, IRepoHistorial repo) =>
{
    await repo.AltaAsync(historial);
    return Results.Created($"/historial", historial);
});

app.Run();
