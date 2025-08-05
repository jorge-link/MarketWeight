using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnection>(sp =>
    new MySqlConnection(
        builder.Configuration["ConnectionStrings:DefaultConnection"]
    )
);

builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<IRepoMoneda, RepoMoneda>();
builder.Services.AddScoped<IRepoHistorial, RepoHistorial>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

app.MapGet("/usuarios", async (IRepoUsuario repo) =>
{
    var usuarios = await repo.ObtenerAsync();

    var usuariosDto = usuarios.Select(u => new UsuarioDTO
    {
        IdUsuario = u.IdUsuario,
        Nombre = u.Nombre,
        Apellido = u.Apellido,
        Email = u.Email,
        Saldo = u.Saldo
    });

    return Results.Ok(usuariosDto);
});


app.MapGet("/usuarios/{id}", async (uint id, IRepoUsuario repo) =>
{
    var usuario = await repo.DetalleAsync(id);
    
    if (usuario is null)
        return Results.NotFound();

    var dto = new UsuarioDTO
    {
        IdUsuario = usuario.IdUsuario,
        Nombre = usuario.Nombre,
        Apellido = usuario.Apellido,
        Email = usuario.Email,
        Saldo = usuario.Saldo
    };

    return Results.Ok(dto);
});


app.MapPost("/usuarios", async (Usuario usuario, IRepoUsuario repo) =>
{
    await repo.AltaAsync(usuario);
    return Results.Created($"/usuarios", usuario);
});

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
