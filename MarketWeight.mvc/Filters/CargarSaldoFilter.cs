using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MarketWeight.Core.Persistencia;

public class CargarSaldoFilter : IActionFilter
{
    private readonly IRepoUsuario _repoUsuario;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CargarSaldoFilter(IRepoUsuario repoUsuario, IHttpContextAccessor httpContextAccessor)
    {
        _repoUsuario = repoUsuario;
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        int? idUsuario = session.GetInt32("IdUsuario");

        if (idUsuario != null)
        {
            var usuario = _repoUsuario.DetalleAsync((uint)idUsuario).Result;
            if (usuario != null)
            {
                var controller = context.Controller as Controller;
                controller.ViewBag.Saldo = usuario.Saldo;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
